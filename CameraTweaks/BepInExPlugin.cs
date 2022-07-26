using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Cinemachine;
using HarmonyLib;
using Pathea;
using Pathea.FrameworkNs;
using Pathea.SleepNs;
using Pathea.TimeNs;
using Pathea.UISystemV2.UI;
using Pathea.UISystemV2.UIControl;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CameraTweaks
{
    [BepInPlugin("aedenthorn.CameraTweaks", "CameraTweaks", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<float> zoomSpeed;
        public static ConfigEntry<string> increaseDistanceKey;
        public static ConfigEntry<string> decreaseDistanceKey;

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
            zoomSpeed = Config.Bind<float>("Options", "ZoomSpeed", 0.01f, "Speed to zoom in and out");
            increaseDistanceKey = Config.Bind<string>("Options", "IncreaseDistanceKey", "[2]", "Key to hold to increase distance");
            decreaseDistanceKey = Config.Bind<string>("Options", "DecreaseDistanceKey", "[8]", "Key to hold to decrease distance");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(CinemachineFreeLook), nameof(CinemachineFreeLook.InternalUpdateCameraState))]
        static class CinemachineFreeLook_InternalUpdateCameraState_Patch
        {
            public static void Postfix(CinemachineFreeLook __instance)
            {
                float zoomPercent = 1;
                if (AedenthornUtils.CheckKeyHeld(increaseDistanceKey.Value))
                    zoomPercent += zoomSpeed.Value;
                else if (AedenthornUtils.CheckKeyHeld(decreaseDistanceKey.Value))
                    zoomPercent -= zoomSpeed.Value;
                else return;


                var originalOrbits = new CinemachineFreeLook.Orbit[__instance.m_Orbits.Length];
                for (int i = 0; i < __instance.m_Orbits.Length; i++)
                {
                    originalOrbits[i].m_Height = __instance.m_Orbits[i].m_Height;
                    originalOrbits[i].m_Radius = __instance.m_Orbits[i].m_Radius;
                }
                for (int i = 0; i < __instance.m_Orbits.Length; i++)
                {
                    __instance.m_Orbits[i].m_Height = Mathf.Max(0.02f, originalOrbits[i].m_Height * zoomPercent);
                    __instance.m_Orbits[i].m_Radius = Mathf.Max(0.02f, originalOrbits[i].m_Radius * zoomPercent);
                }
            }
        }
    }
}
