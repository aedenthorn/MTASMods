using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Commonder;
using HarmonyLib;
using Pathea;
using Pathea.Mtas;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace DevConsole
{
    [BepInPlugin("aedenthorn.DevConsole", "DevConsole", "0.7.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        public static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<KeyCode> hotkey;
        public static ConfigEntry<KeyCode> upKey;
        public static ConfigEntry<KeyCode> downKey;
        public static ConfigEntry<KeyCode> zoomKey;
        public static ConfigEntry<KeyCode> jetPackKey;
        public static ConfigEntry<KeyCode> jumpTimeKey1;
        public static ConfigEntry<KeyCode> jumpTimeKey2;
        

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
            upKey = Config.Bind<KeyCode>("Options", "UpKey", KeyCode.UpArrow, "Hotkey to move up through suggestions");
            downKey = Config.Bind<KeyCode>("Options", "DownKey", KeyCode.DownArrow, "Hotkey to move down through suggestions");
            zoomKey = Config.Bind<KeyCode>("Options", "ZoomKey", KeyCode.LeftControl, "Hotkey to make player zoom");
            jetPackKey = Config.Bind<KeyCode>("Options", "JetPackKey", KeyCode.F4, "Hotkey to toggle jetpack");
            jumpTimeKey1 = Config.Bind<KeyCode>("Options", "JumpTimeKey1", KeyCode.None, "Hotkey to jump time");
            jumpTimeKey2 = Config.Bind<KeyCode>("Options", "JumpTimeKey2", KeyCode.None, "Hotkey to jump time, no sleep");

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
