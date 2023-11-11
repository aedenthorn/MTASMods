using Commonder;
using HarmonyLib;
using Pathea;
using Pathea.DesignerConfig;
using Pathea.FrameworkNs;
using Pathea.ItemNs;
using Pathea.MonsterNs;
using Pathea.NpcNs;
using Pathea.RideNs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DevConsole
{
    public class ModCmd : MonoBehaviour, ICmd
    {
        public void Awake()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Mod", "Dump", "Dump various IDs to files", false)]
        public void Dump()
        {

            string path = AedenthornUtils.GetAssetPath(BepInExPlugin.context, true);
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

            BepInExPlugin.Dbgl($"Dumped data to {path}");
        }

    }
}