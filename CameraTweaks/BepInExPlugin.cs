using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Cinemachine;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

namespace CameraTweaks
{
    [BepInPlugin("aedenthorn.CameraTweaks", "CameraTweaks", "0.2.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<float> zoomSpeed;
        public static ConfigEntry<string> increaseDistanceKey;
        public static ConfigEntry<string> decreaseDistanceKey;
        public static ConfigEntry<string> increaseHeightKey;
        public static ConfigEntry<string> decreaseHeightKey;
        public static ConfigEntry<string> increaseRadiusKey;
        public static ConfigEntry<string> decreaseRadiusKey;
        public static ConfigEntry<string> resetCameraKey;

        public static Dictionary<int, List<Orbit>> cameraDict = new Dictionary<int, List<Orbit>>();

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
            resetCameraKey = Config.Bind<string>("Options", "ResetCameraKey", "[5]", "Key to reset camera to default");
            increaseDistanceKey = Config.Bind<string>("Options", "IncreaseDistanceKey", "[2]", "Key to hold to increase distance");
            decreaseDistanceKey = Config.Bind<string>("Options", "DecreaseDistanceKey", "[8]", "Key to hold to decrease distance");
            increaseHeightKey = Config.Bind<string>("Options", "IncreaseHeightKey", "[9]", "Key to hold to increase height");
            decreaseHeightKey = Config.Bind<string>("Options", "DecreaseHeightKey", "[3]", "Key to hold to decrease height");
            increaseRadiusKey = Config.Bind<string>("Options", "IncreaseRadiusKey", "[1]", "Key to hold to increase radius");
            decreaseRadiusKey = Config.Bind<string>("Options", "DecreaseRadiusKey", "[7]", "Key to hold to decrease radius");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(CinemachineFreeLook), nameof(CinemachineFreeLook.InternalUpdateCameraState))]
        static class CinemachineFreeLook_InternalUpdateCameraState_Patch
        {
            public static void Postfix(CinemachineFreeLook __instance)
            {
                if (!cameraDict.TryGetValue(__instance.GetInstanceID(), out List<Orbit> origOrbits))
                {
                    Dbgl("Recording default orbits for camera");
                    origOrbits = new List<Orbit>();
                    foreach(Orbit orbit in __instance.m_Orbits)
                    {
                        origOrbits.Add(orbit);
                    }
                    cameraDict[__instance.GetInstanceID()] = origOrbits;
                }

                float heightPercent = 1;
                float radiusPercent = 1;
                if (AedenthornUtils.CheckKeyHeld(increaseDistanceKey.Value))
                {
                    radiusPercent += zoomSpeed.Value;
                    heightPercent += zoomSpeed.Value;
                }
                else if (AedenthornUtils.CheckKeyHeld(decreaseDistanceKey.Value))
                {
                    radiusPercent -= zoomSpeed.Value;
                    heightPercent -= zoomSpeed.Value;
                }
                else if (AedenthornUtils.CheckKeyHeld(increaseHeightKey.Value))
                {
                    heightPercent += zoomSpeed.Value;
                }
                else if (AedenthornUtils.CheckKeyHeld(decreaseHeightKey.Value))
                {
                    heightPercent -= zoomSpeed.Value;
                }
                else if (AedenthornUtils.CheckKeyHeld(increaseRadiusKey.Value))
                {
                    radiusPercent += zoomSpeed.Value;
                }
                else if (AedenthornUtils.CheckKeyHeld(decreaseRadiusKey.Value))
                {
                    radiusPercent -= zoomSpeed.Value;
                }
                else if (AedenthornUtils.CheckKeyDown(resetCameraKey.Value))
                {
                    Dbgl("Resetting camera");
                    for (int i = 0; i < __instance.m_Orbits.Length; i++)
                    {
                        __instance.m_Orbits[i].m_Height = origOrbits[i].m_Height;
                        __instance.m_Orbits[i].m_Radius = origOrbits[i].m_Radius;
                    }
                    return;
                }
                else return;

                var originalOrbits = new CinemachineFreeLook.Orbit[__instance.m_Orbits.Length];
                for (int i = 0; i < __instance.m_Orbits.Length; i++)
                {
                    originalOrbits[i].m_Height = __instance.m_Orbits[i].m_Height;
                    originalOrbits[i].m_Radius = __instance.m_Orbits[i].m_Radius;
                }
                for (int i = 0; i < __instance.m_Orbits.Length; i++)
                {
                    __instance.m_Orbits[i].m_Height = Mathf.Max(0.02f, originalOrbits[i].m_Height * heightPercent);
                    __instance.m_Orbits[i].m_Radius = Mathf.Max(0.02f, originalOrbits[i].m_Radius * radiusPercent);
                }
            }
        }
    }
}
