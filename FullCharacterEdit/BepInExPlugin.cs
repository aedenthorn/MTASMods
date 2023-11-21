using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea;
using Pathea.ActorNs;
using Pathea.CustomPlayer;
using Pathea.CutsceneNs;
using Pathea.FrameworkNs;
using Pathea.GameFlagNs;
using Pathea.HomeNs;
using Pathea.ScenarioNs;
using Pathea.UISystemV2.UI;
using Pathea.UISystemV2.UIControl;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FullCharacterEdit
{
    [BepInPlugin("aedenthorn.FullCharacterEdit", "Full Character Edit", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<bool> canCostGold;
        public static ConfigEntry<KeyCode> editKey;

        public static bool playerCustomization;

        //public static ConfigEntry<int> nexusID;

        public static void Dbgl(string str = "", LogLevel logLevel = LogLevel.Debug)
        {
            if (isDebug.Value)
                context.Logger.Log(logLevel, str);
        }
        private void Awake()
        {

            context = this;
            modEnabled = Config.Bind<bool>("General", "Enabled", true, "Enable this mod");
            isDebug = Config.Bind<bool>("General", "IsDebug", true, "Enable debug logs");
            canCostGold = Config.Bind<bool>("Options", "CostGold", false, "Edits can cost gold");
            editKey = Config.Bind<KeyCode>("Options", "EditKey", KeyCode.Y, "Key to open UI");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(Player), "Update")]
        static class Player_Update_Patch
        {
            public static void Postfix()
            {
                if (Input.GetKeyDown(editKey.Value))
                {
                    Dbgl("Pressed customize key");
                    if (!GetSafe())
                    {
                        if (Module<UIModule>.Self.GetCurrentLogicUIStateType() == StateType.StorageBox)
                        {
                            ((UISystemMgr)AccessTools.Property(typeof(UISystemMgr), "Instance").GetValue(null)).PopTo(StateType.Gaming);
                        }
                    }
                    else
                    {
                        playerCustomization = true;
                        Module<CustomPlayerMagicMirrorModule>.Self.StartEdit();
                    }
                }
            }
        }
        [HarmonyPatch(typeof(CustomPlayerData), nameof(CustomPlayerData.GetChangeCost))]
        static class CustomPlayerData_GetChangeCost_Patch
        {
            public static bool Prefix(ref int __result)
            {
                if(playerCustomization && !canCostGold.Value)
                {
                    __result = 0;
                    return false;
                }
                return false;
            }
        }
        [HarmonyPatch(typeof(UISystemMgr), nameof(UISystemMgr.Push))]
        static class UISystemMgr_Push_Patch
        {
            public static void Prefix(ref UIContext context)
            {
                if (context is CustomPlayerUIContext && playerCustomization)
                {
                    Dbgl($"Starting player customization");
                    (context as CustomPlayerUIContext).scene = CustomPlayerUI.EditScene.NewGame;
                }
            }
        }
        [HarmonyPatch(typeof(CustomPlayerModule), "PlayHelloAnim")]
        static class CustomPlayerModule_PlayHelloAnim_Patch
        {
            public static bool Prefix(CustomPlayerModule __instance)
            {
                if (playerCustomization)
                {
                    CustomPlayerModelViewer viewer = (CustomPlayerModelViewer)AccessTools.Field(typeof(CustomPlayerModelViewerMgr), "viewer").GetValue(Singleton<CustomPlayerModelViewerMgr>.Instance);
                    viewer.curViewModel.anim.SetTrigger("Select_Start");
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(CustomPlayerMagicMirrorModule), "Pathea.CustomPlayer.ICustomPlayerProvider.EditBasicInfo")]
        static class CustomPlayerMagicMirrorModule_EditBasicInfo_Patch
        {
            public static bool Prefix(ICustomPlayerProvider __instance, ref bool __result)
            {
                if (playerCustomization)
                {
                    //Dbgl($"allow edit basic info");
                    __result = true;
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(CustomPlayerUI), "SaveName")]
        static class CustomPlayerUI_SaveName_Patch
        {

            public static void Prefix(CustomPlayerName ___nameEdit)
            {
                if (playerCustomization)
                {
                    //Dbgl($"playerName: {___nameEdit.nameInput.Text}");
                    Module<CustomPlayerModule>.Self.PlayerData.playerName = ___nameEdit.nameInput.Text;
                    Module<CustomPlayerMagicMirrorModule>.Self.CurEditData.playerName = ___nameEdit.nameInput.Text;
                }
            }
        }
        [HarmonyPatch(typeof(CustomPlayerUI), "EnterBasicMode")]
        static class CustomPlayerUI_EnterBasicMode_Patch
        {

            public static void Prefix(CustomPlayerUI __instance, CustomPlayerName ___nameEdit)
            {
                if (playerCustomization)
                {
                    ___nameEdit.nameInput.SetString(Module<Player>.Self.actor.ActorName);
                    Module<CustomPlayerModule>.Self.PlayerData.playerName = Module<Player>.Self.actor.ActorName;
                    Module<CustomPlayerMagicMirrorModule>.Self.CurEditData.playerName = Module<Player>.Self.actor.ActorName;
                }
            }
        }
        [HarmonyPatch(typeof(CustomPlayerMagicMirrorModule), "EndCustomPlayerProcess")]
        static class CustomPlayerMagicMirrorModule_EndCustomPlayerProcess_Patch
        {
            public static void Prefix(CustomPlayerMagicMirrorModule __instance, ref bool confirm, ref bool costGold)
            {
                if (playerCustomization)
                {
                    playerCustomization = false;
                    if (confirm)
                    { 
                        var playerName = Module<CustomPlayerModule>.Self.PlayerData.playerName;
                        Dbgl($"setting playerName: {playerName}");
                        AccessTools.Field(typeof(Player), "playerName").SetValue(Module<Player>.Self, playerName);
                        Module<Player>.Self.actor.ActorName = playerName;
                    }
                }

            }
        }
        private static bool GetSafe()
        {
            return modEnabled.Value && !Module<CutsceneModule>.Self.InCutscene() && Singleton<GameFlag>.Instance.Gaming && Module<UIModule>.Self.GetCurrentLogicUIStateType() == StateType.Gaming;
        }
    }
}
