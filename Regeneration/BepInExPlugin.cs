﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.ActionNs;
using Pathea.ActorNs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace Regeneration
{
    [BepInPlugin("aedenthorn.Regeneration", "Regeneration", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;
        public static ConfigEntry<float> staminaRegenRate;
        public static ConfigEntry<float> healthRegenRate;

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
            staminaRegenRate = Config.Bind<float>("Options", "StaminaRegenRate", 5, "Number of seconds per stamina point regen (can be decimal)");
            healthRegenRate = Config.Bind<float>("Options", "HealthRegenRate", 5, "Number of seconds per health point regen (can be decimal)");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
        }

        [HarmonyPatch(typeof(Player), "Update")]
        static class Player_Update_Patch
        {
            public static void Prefix(Player __instance)
            {
                if (!modEnabled.Value || __instance.actor is null)
                    return;
                healthTimeElapsed += Time.deltaTime;
                staminaTimeElapsed += Time.deltaTime;
                if (staminaTimeElapsed > staminaRegenRate.Value)
                {
                    __instance.actor.ApplyAttrChange(ActorRunTimeAttrType.Sp, 1);
                    staminaTimeElapsed = 0;
                }
                if (healthTimeElapsed > healthRegenRate.Value)
                {
                    healthTimeElapsed = 0;
                    float health = __instance.actor.GetAttr(ActorRunTimeAttrType.Hp);
                    float max = __instance.actor.GetAttr(ActorAttrType.HpMax);
                    if (health < max)
                    {
                        float value = Math.Min(max, health + 1);
                        __instance.actor.ApplyAttrChange(ActorRunTimeAttrType.Hp, value);
                        __instance.actor.ShowHpChangeUI(value);
                    }
                }
            }
        }

    }
}
