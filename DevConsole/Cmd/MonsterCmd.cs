using System;
using System.Runtime.CompilerServices;
using Commonder;
using Pathea.ActorNs;
using Pathea.FrameworkNs;
using Pathea.MonsterNs;
using Pathea.ScenarioNs;
using UnityEngine;

namespace DevConsole
{
    public class MonsterCmd : MonoBehaviour, ICmd
    {
        public void Start()
        {
            Cmd.Instance.Register(this);
        }

        public void OnDestroy()
        {
            Cmd.Instance.ClearAll();
        }

        [Command("Monster", "MonsterKill", "杀死指定怪物", false)]
        public void MonsterKill(int id)
        {
            Monster monster = Module<MonsterMgr>.Self.GetMonster(id);
            if (monster != null)
            {
                monster.actor.SetAttr(ActorRunTimeAttrType.Hp, 0f);
            }
        }

        [Command("Monster", "MonsterKillAll", "杀死所有怪物", false)]
        public void MonsterKillAll()
        {
            Module<MonsterMgr>.Self.ForeachMonsters(delegate (Monster ret)
            {
                ret.actor.SetAttr(ActorRunTimeAttrType.Hp, 0f);
            });
        }

        [Command("Monster", "MonsterDestroy", "删除指定怪物", false)]
        public void MonsterDestroy(int id)
        {
            Module<MonsterMgr>.Self.DestroyMonster(id, false, false);
        }

        [Command("Monster", "MonsterCreate", "创建怪物", false)]
        public void MonsterCreate(int protoId, int lvl)
        {
            Vector3 gamePos = Module<Player>.Self.GamePos;
            Module<MonsterMgr>.Self.CreateMonster(protoId, lvl, gamePos, Vector3.zero, Module<ScenarioModule>.Self.CurScene, -1, true, null);
        }

        [Command("Monster", "MonsterCreateAll", "创建所有怪物", false)]
        public void MonsterCreateAll(int lvl)
        {
            Vector3 playerPos = Module<Player>.Self.GamePos;
            MonsterDB.ForeachIds(delegate (int id)
            {
                Module<MonsterMgr>.Self.CreateMonster(id, lvl, playerPos, Vector3.zero, Module<ScenarioModule>.Self.CurScene, -1, true, null);
            });
        }

        [Command("Monster", "MonsterCreateAndStopAi", "创建怪物并停止Ai", false)]
        public void MonsterCreateAndStopAi(int protoId, int lvl)
        {
            Vector3 gamePos = Module<Player>.Self.GamePos;
            Module<MonsterMgr>.Self.CreateMonster(protoId, lvl, gamePos, Vector3.zero, Module<ScenarioModule>.Self.CurScene, -1, true, null).StopAI(this);
        }

        public MonsterCmd()
        {
        }

    }
}
