using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Newtonsoft.Json.Linq;
using Pathea;
using Pathea.ActionNs;
using Pathea.ActorNs;
using Pathea.ExAnim;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace AnimationSpeed
{
    [BepInPlugin("aedenthorn.AnimationSpeed", "Animation Speed", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<string> reloadKey;

        public static Dictionary<string, float> speedDict;
        public static string filePath = "BepInEx/config/aedenthorn.AnimationSpeed.speeds.txt";

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
            reloadKey = Config.Bind<string>("Options", "ReloadKey", "end", "Reload Key");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");

            LoadSpeeds();

        }

        private static void LoadSpeeds()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "{}");
            }
            speedDict = JObject.Parse(File.ReadAllText(filePath)).ToObject<Dictionary<string, float>>();
            Dbgl($"Loaded {speedDict.Count} animation speeds");

        }

        [HarmonyPatch(typeof(Player), "Update")]
        static class Player_Update_Patch
        {
            public static void Prefix(Player __instance)
            {
                if (!modEnabled.Value)
                    return;

                if (AedenthornUtils.CheckKeyDown(reloadKey.Value))
                {
                    Dbgl($"Reloading animation speeds");
                    LoadSpeeds();
                }
                if (__instance.actor is null)
                    return;
                var viewData = (ViewData)AccessTools.PropertyGetter(typeof(Actor), "viewData").Invoke(__instance.actor, null);
                if (viewData?.animator is null)
                    return;
                var infos = viewData.animator.GetCurrentAnimatorClipInfo(0);
                if (infos is null)
                    return;
                foreach(var info in infos)
                {
                    var name = info.clip?.name;
                    if (string.IsNullOrEmpty(name))
                        continue;

                    if (!speedDict.TryGetValue(name, out float speed))
                    {
                        Dbgl($"playing new anim {name}");
                        speedDict[name] = -1;
                        File.WriteAllText(filePath, JToken.FromObject(speedDict).ToString());
                        continue;
                    }
                    if (speed < 0)
                        continue;
                    //Dbgl($"setting speed to {speed} for anim {name}");
                    viewData.animator.speed = speed;
                    return;

                }
                viewData.animator.speed = 1;
            }
        }

    }
}
