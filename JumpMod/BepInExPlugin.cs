using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.ActionNs;
using Pathea.ActorNs;
using Pathea.FrameworkNs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace JumpMod
{
    [BepInPlugin("aedenthorn.JumpMod", "Jump Mod", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<float> heightModifier;

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
            heightModifier = Config.Bind<float>("Options", "HeightModifier", 2, "Jump height multiplier");

            //nexusID = Config.Bind<int>("General", "NexusID", 1, "Nexus mod ID for updates");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(Player), nameof(Player.SetJump))]
        static class Player_SetJump_Patch
        {
            public static void Prefix(bool isPress, ref bool ___canApplyJump) 
            {
                Dbgl($"set jump; press {isPress}, can {___canApplyJump}");
                if (modEnabled.Value)
                    ___canApplyJump = true;
            }
        }
        
        [HarmonyPatch(typeof(ActionJump), nameof(ActionJump.OnStart))]
        static class ActionJump_OnStart_Patch
        {
            public static void xPrefix(ActionJump __instance, bool ___isFall)
            {
                Dbgl($"start jump; is fall {___isFall}");
            }
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                Dbgl("Transpiling ActionJump.OnStart");
                List<CodeInstruction> codes = instructions.ToList();
                MethodInfo get_JumpHieght = AccessTools.PropertyGetter(typeof(IActionAgent), nameof(IActionAgent.JumpHeight));
                int index = codes.FindIndex((CodeInstruction c) => c.operand != null && c.operand is MethodInfo && (MethodInfo)c.operand == get_JumpHieght);
                if (index > -1)
                {
                    Dbgl("Inserting jump height modifier");
                    codes.Insert(index + 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(BepInExPlugin), nameof(BepInExPlugin.ModifyJumpHeight))));
                }
                return codes.AsEnumerable();
            }
        }
        
        //[HarmonyPatch(typeof(ActionCmpt), nameof(ActionCmpt.TryDoAction))]
        static class ActionCmpt_TryDoAction_Patch
        {
            public static void Postfix(ActionCmpt __instance, ActionType actiontype, IData actionData, bool __result)
            {
                if (actiontype != ActionType.Jump)
                    return;
                Dbgl($"try jump cmpt; is fall {((JumpData)actionData).isFall}, result {__result}");
            }
        }
        
        
        //[HarmonyPatch(typeof(ActionCmpt), "CanDo")]
        static class ActionCmpt_CanDo_Patch
        {
            public static void Postfix(ActionCmpt __instance, ActionType actiontype, ref bool __result)
            {
                if (actiontype != ActionType.Jump)
                    return;
                Dbgl($"cando jump cmpt; result {__result}");
                __result = true;
            }
        }
        
        //[HarmonyPatch(typeof(ActionContainer), "CanDoAction", new Type[] { typeof(int) })]
        static class ActionContainer_CanDoAction_Patch
        {
            public static void Postfix(ActionContainer __instance, int idx, ref bool __result)
            {
                if (idx != (int)ActionType.Jump)
                    return;
                Dbgl($"can jump container, is jump {idx == (int)ActionType.Jump}, result {__result}");
                if (!__result)
                {
                    __result = true;
                    PhysicData pd = (PhysicData)AccessTools.PropertyGetter(typeof(Actor), "physicData").Invoke(Module<Player>.Self.actor, null);
                    pd.isGrounded = true;
                    //AccessTools.Method(typeof(ActionContainer), "StopAction", new Type[] { typeof(int), typeof(bool) }).Invoke(__instance, new object[] { idx, true });
                    AccessTools.Method(typeof(ActionContainer), "RemoveAction", new Type[] { typeof(int) }).Invoke(__instance, new object[] { idx });
                    AccessTools.PropertySetter(typeof(Actor), "physicData").Invoke(Module<Player>.Self.actor, new object[] { pd });
                    //__instance.RemoveMask(ActionMask.JumpUp);

                }
            }
        }
        
        //[HarmonyPatch(typeof(ActionContainer), "DoInterrupting", new Type[] { typeof(int) })]
        static class ActionContainer_DoInterrupting_Patch
        {
            public static void Postfix(ActionContainer __instance, int idx, bool __result)
            {
                if (idx != (int)ActionType.Jump)
                    return;
                AccessTools.Method(typeof(ActionContainer), "StopAction").Invoke(__instance, new object[] { idx, true });
                Dbgl($"DoInterrupting container, is jump {idx == (int)ActionType.Jump}, result {__result}");
                __result = false;
            }
        }

        private static float ModifyJumpHeight(float height)
        {
            return modEnabled.Value ? height * heightModifier.Value : height;
        }
    }
}
