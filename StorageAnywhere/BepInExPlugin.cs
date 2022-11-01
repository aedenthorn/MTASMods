using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea;
using Pathea.ActorNs;
using Pathea.CutsceneNs;
using Pathea.FrameworkNs;
using Pathea.GameFlagNs;
using Pathea.HomeNs;
using Pathea.ScenarioNs;
using Pathea.UISystemV2.UI;
using Pathea.UISystemV2.UIControl;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace StorageAnywhere
{
    [BepInPlugin("aedenthorn.StorageAnywhere", "Storage Anywhere", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<string> storageKey;
        public static ConfigEntry<int> lastStorage;

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
            storageKey = Config.Bind<string>("Options", "StorageKey", "[0]", "Key to open storage");
            lastStorage = Config.Bind<int>("Options", "LastStorage", 0, "Last open storage index");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(Player), "Update")]
        static class Player_Update_Patch
        {
            public static void Postfix()
            {
                if (AedenthornUtils.CheckKeyDown(storageKey.Value))
                {
                    Dbgl("Pressed storage key");
                    if (!GetSafe())
                    {
                        if (Module<UIModule>.Self.GetCurrentLogicUIStateType() == StateType.StorageBox)
                        {
                            ((UISystemMgr)AccessTools.Property(typeof(UISystemMgr), "Instance").GetValue(null)).PopTo(StateType.Gaming);
                        }
                    }
                    else
                    {
                        StorageUnit unit = Unit<StorageUnit>.GetUnit(lastStorage.Value);
                        if (unit is null)
                        {
                            unit = Unit<StorageUnit>.GetUnit(0);
                            lastStorage.Value = 0;
                        }
                        StorageBoxContext storageBoxContext = new StorageBoxContext
                        {
                            context = new StorageBoxPartContext
                            {
                                unit = unit,
                                storageBoxContext = new StoragePartContext(),
                                playerStorageContext = new StoragePartContext
                                {
                                    container = Module<Player>.Self.bag.storage
                                }
                            },
                            lastOpen = unit
                        };
                        storageBoxContext.context.storageBoxContext.container = storageBoxContext.context.GetContainer();
                        StorageBoxControl control = (StorageBoxControl)AccessTools.FieldRefAccess<UIModule, List<object>>(Module<UIModule>.Self, "controls").First(o => o is StorageBoxControl);
                        ((UISystemMgr)AccessTools.Property(typeof(UISystemMgr), "Instance").GetValue(null)).Push(StateType.StorageBox, control, storageBoxContext, StateLayer.Default);
                    }
                }
            }
        }
        [HarmonyPatch(typeof(StorageBoxPartControl), "ChangeBox")]
        static class StorageBoxControl_ChangeBox_Patch
        {
            public static void Postfix(StorageBoxPartControl __instance, int index)
            {
                lastStorage.Value = index;
                Dbgl($"Set last storage to {index}");
            }
        }
        private static bool GetSafe()
        {
            return modEnabled.Value && !Module<CutsceneModule>.Self.InCutscene() && Singleton<GameFlag>.Instance.Gaming && Module<UIModule>.Self.GetCurrentLogicUIStateType() == StateType.Gaming;
        }
    }
}
