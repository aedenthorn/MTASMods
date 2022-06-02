using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.ActorNs;
using System;
using System.Reflection;

namespace MovementSpeed
{
    [BepInPlugin("aedenthorn.MovementSpeed", "MovementSpeed", "0.2.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<float> runFastSpeed;
        public static ConfigEntry<float> runSpeed;
        public static ConfigEntry<float> walkSpeed;

        public static float healthTimeElapsed;
        public static float staminaTimeElapsed;

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
            runFastSpeed = Config.Bind<float>("Options", "RunFastSpeed", 20, "Sprint speed");
            runSpeed = Config.Bind<float>("Options", "RunSpeed", 13, "Run speed");
            walkSpeed = Config.Bind<float>("Options", "WalkSpeed", 4, "Walk speed");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");

        }
        [HarmonyPatch(typeof(Player), "InitActor", new Type[]{})]
        static class Player_InitActor_Patch
        {
            public static void Postfix(Player __instance)
            {
                if (!modEnabled.Value)
                    return;
                FieldInfo attrCmptF = AccessTools.Field(typeof(Actor), "attrCmpt");
                FieldInfo actorProtoF = AccessTools.Field(typeof(AttrCmpt), "actorProto");
                ActorProto actorProto = (ActorProto)actorProtoF.GetValue(attrCmptF.GetValue(__instance.actor));

                Dbgl($"walk: {actorProto.walkSpeed}, run: {actorProto.runSpeed}, run fast: {actorProto.runFastSpeed}");

                actorProto.walkSpeed = walkSpeed.Value;
                actorProto.runSpeed = runSpeed.Value;
                actorProto.runFastSpeed = runFastSpeed.Value;
                actorProtoF.SetValue(attrCmptF.GetValue(__instance.actor), actorProto);
            }
        }
    }
}
