using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Commonder;
using HarmonyLib;
using Pathea;
using Pathea.DebugToolNs;
using Pathea.DesignerConfig;
using Pathea.FrameworkNs;
using Pathea.ItemNs;
using Pathea.MonsterNs;
using Pathea.Mtas;
using Pathea.NpcNs;
using Pathea.RideNs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DevConsole
{
    [BepInPlugin("aedenthorn.DevConsole", "DevConsole", "0.3.1")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<string> hotkey;

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
            hotkey = Config.Bind<string>("Options", "Hotkey", "f1", "Hotkey to toggle debug console");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");

        }
        private void Update()
        {
            if (AedenthornUtils.CheckKeyDown(hotkey.Value))
            {
                Dbgl("Pressed hotkey");
                var cmd = FindObjectOfType<CmdCtr>();
                if(cmd != null)
                {
                    var cg = AccessTools.FieldRefAccess<CmdCtr, CanvasGroup>(cmd, "canvasGroup");
                    if(cg.alpha != 1)
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

        [HarmonyPatch(typeof(SelectorCtr), nameof(SelectorCtr.FreshList))]
        static class SelectorCtr_FreshList_Patch
        {
            public static void Postfix(Transform ___container, TMP_InputField ___inputText)
            {
                Dbgl($"making fresh list");
                foreach(Transform c in ___container)
                {
                    string text = c.GetComponentInChildren<Text>()?.text.Split(' ')[0];
                    if(text != null)
                    {
                        ClickAction a = c.gameObject.GetComponent<ClickAction>();
                        if(a is null)
                            a = c.gameObject.AddComponent<ClickAction>();
                        a.text = text;
                        a.input = ___inputText;
                        
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

                Singleton<GameMgr>.Instance.Register(new IModule[] { new DebugToolModule() });
            }
        }

        [HarmonyPatch(typeof(Cmd), nameof(Cmd.Commit))]
        static class Cmd_Commit_Patch
        {
            public static void Prefix(ref string str, Dictionary<int, MethodTarget> ___methodDic)
            {
                if (!modEnabled.Value)
                    return;
                var array = str.Split(' ');
                if (___methodDic.ContainsKey(array[0].GetHashCode()))
                    return;

                if (array[0] == "dump")
                {
                    string path = AedenthornUtils.GetAssetPath(context, true);
                    var methods = Cmd.Instance.Dics.Values.Select(m => m.methodInfo.Name).ToList();
                    methods.Sort();
                    File.WriteAllLines(Path.Combine(path, "commands.txt"), methods);

                    var items = AccessTools.FieldRefAccess<ItemPrototypeModule, ConfigReaderId<ItemPrototype>>(Module<ItemPrototypeModule>.Self, "items").Select(i => TextMgr.GetStr(i.nameId) + ": " + i.id).ToList();
                    items.Sort();
                    File.WriteAllLines(Path.Combine(path, "items.txt"), items);

                    var npcs = AccessTools.FieldRefAccess<NpcMgr, ConfigReaderId<NpcProtoData>>(Module<NpcMgr>.Self, "npcProtos").Select(i => TextMgr.GetStr(i.nameID) + ": " + i.id).ToList();
                    npcs.Sort();
                    File.WriteAllLines(Path.Combine(path, "npcs.txt"), npcs);

                    var monsters = ((Dictionary<int, MonsterProto>)AccessTools.Field(typeof(MonsterDB), "MonsterProtos").GetValue(null)).Values.Select(i => TextMgr.GetStr(i.nameId) + ": " + i.id).ToList();
                    monsters.Sort();
                    File.WriteAllLines(Path.Combine(path, "monsters.txt"), monsters);

                    var rides = AccessTools.FieldRefAccess<RideModule, ConfigReaderId<RidableProtoData>>(Module<RideModule>.Self, "rideProtos").Select(i => TextMgr.GetStr(i.nameId) + ": " + i.id).ToList();
                    rides.Sort();
                    File.WriteAllLines(Path.Combine(path, "ridables.txt"), rides); 
                    
                    Dbgl($"Dumped data to {path}");
                    return;
                }

                Dbgl($"Commit command: {str}, not in dic");
                foreach(var m in ___methodDic.Values)
                {
                    if (m.attr.CMD.ToLower() == array[0].ToLower() && ___methodDic.ContainsKey(m.attr.CMD.GetHashCode()))
                    {
                        array[0] = m.attr.CMD;
                        str = string.Join(" ", array);
                        Dbgl($"Found command: {str}");
                        return;
                    }
                }
                Singleton<GameMgr>.Instance.Register(new IModule[] { new DebugToolModule() });
            }
        }
    }
}
