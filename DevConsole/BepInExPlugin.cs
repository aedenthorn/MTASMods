using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Commonder;
using HarmonyLib;
using Pathea;
using Pathea.DesignerConfig;
using Pathea.FrameworkNs;
using Pathea.ItemNs;
using Pathea.MonsterNs;
using Pathea.Mtas;
using Pathea.NpcNs;
using Pathea.RideNs;
using Pathea.StoryScript;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevConsole
{
    [BepInPlugin("aedenthorn.DevConsole", "DevConsole", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        public static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<KeyCode> hotkey;

        //public static ConfigEntry<int> nexusID;

        public static void Dbgl(string str = "", LogLevel logLevel = LogLevel.Debug)
        {
            if (isDebug.Value)
                context.Logger.Log(logLevel, str);
        }
        public void Awake()
        {

            context = this;
            modEnabled = Config.Bind<bool>("General", "Enabled", true, "Enable this mod");
            isDebug = Config.Bind<bool>("General", "IsDebug", true, "Enable debug logs");
            hotkey = Config.Bind<KeyCode>("Options", "MenuKey", KeyCode.F1, "Hotkey to toggle debug console");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");

        }
        public void Update()
        {
            if (Input.GetKeyDown(hotkey.Value))
            {
                Dbgl("Pressed hotkey");
                var cmd = FindObjectOfType<CmdCtr>();
                if (cmd != null)
                {
                    var cg = AccessTools.FieldRefAccess<CmdCtr, CanvasGroup>(cmd, "canvasGroup");
                    if (cg.alpha != 1)
                    {
                        Dbgl("Starting debug tools");
                        cg.alpha = 1;
                        cg.interactable = true;
                        AccessTools.FieldRefAccess<CmdCtr, GraphicRaycaster>(cmd, "graphicRaycaster").enabled = true;
                    }
                    else
                    {
                        Dbgl("Closing debug tools");
                        cg.alpha = 0;
                        cg.interactable = false;
                        AccessTools.FieldRefAccess<CmdCtr, GraphicRaycaster>(cmd, "graphicRaycaster").enabled = false;
                    }
                }
            }
        }


        [HarmonyPatch(typeof(WorldLauncher), "Register")]
        static class WorldLauncher_Register_Patch
        {
            public static void Postfix()
            {
                Dbgl($"Starting debug tools");
                Singleton<GameMgr>.Instance.Append(new DebugToolModule());
            }
        }
    }
}
