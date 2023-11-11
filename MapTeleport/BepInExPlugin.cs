using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea;
using Pathea.ActorNs;
using Pathea.FrameworkNs;
using Pathea.ScenarioNs;
using Pathea.UISystemV2.UI;
using System.Reflection;
using UnityEngine;

namespace MapTeleport
{
    [BepInPlugin("aedenthorn.MapTeleport", "Map Teleport", "0.3.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<string> modKey;
        public static ConfigEntry<string> teleportKey;


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
            modKey = Config.Bind<string>("Options", "ModKey", "left shift", "Modifier key to hold for teleporting");
            teleportKey = Config.Bind<string>("Options", "TeleportKey", "mouse 0", "Key to press for teleporting");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(MapPartUI), "Update")]
        static class MapPartUI_Update_Patch
        {
            public static void Postfix(MapPartUI __instance, MapViewer ___mapViewer)
            {
                if (!modEnabled.Value || !AedenthornUtils.CheckKeyDown(teleportKey.Value) || !AedenthornUtils.CheckKeyHeld(modKey.Value, false))
                    return;
                AdditiveScene scene = ___mapViewer.GetShownScene();
                Dbgl($"Clicked in scene {scene}");
                if(scene != AdditiveScene.Main)
                {
                    return;
                }
                SceneConfig sceneConfig = Module<ScenarioModule>.Self.GetSceneConfig(scene);
                Vector3 mouseWorldPosition = __instance.GetMouseWorldPosition(sceneConfig.mapCenter, 1000f);
                if (Module<ScenarioModule>.Self.CurScene != AdditiveScene.Main)
                {
                    Module<ScenarioModule>.Self.LoadScenario(AdditiveScene.Main, new PosRot(mouseWorldPosition, GetGameRot()));
                }
                else
                {
                    Module<Player>.Self.GamePos = mouseWorldPosition;
                }
                Module<UIModule>.Self.PopAllUI();
            }

            private static Vector3 GetGameRot()
            {
                var player = Module<Player>.Self;
                if (player == null)
                {
                    return Vector3.zero;
                }
                var ride = AccessTools.FieldRefAccess<Player, Actor>(player, "currentRidable");
                if (ride != null)
                {
                    return ride.GameRot.eulerAngles;
                }
                if (player.actor == null)
                {
                    return Vector3.zero;
                }
                return player.actor.GameRot.eulerAngles;
            }
        }
    }
}
