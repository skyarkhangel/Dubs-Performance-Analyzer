﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;

namespace DubsAnalyzer
{
    [ProfileMode("Job AI", UpdateMode.Update)]
    class H_JobAI
    {
        //TryFindAndStartJob
        public static bool Active = false;

        public static void ProfilePatch()
        {
            var go = new HarmonyMethod(typeof(H_JobAI), nameof(Prefix));
            var biff = new HarmonyMethod(typeof(H_JobAI), nameof(Postfix));


            Analyzer.harmony.Patch(AccessTools.Method(typeof(Pawn_JobTracker), nameof(Pawn_JobTracker.TryFindAndStartJob)), go, biff);
            Analyzer.harmony.Patch(AccessTools.Method(typeof(Pawn_JobTracker), nameof(Pawn_JobTracker.EndCurrentJob)), go, biff);
            Analyzer.harmony.Patch(AccessTools.Method(typeof(Pawn_JobTracker), nameof(Pawn_JobTracker.TryOpportunisticJob)), go, biff);
            Analyzer.harmony.Patch(AccessTools.Method(typeof(Pawn_JobTracker), nameof(Pawn_JobTracker.StartJob)), go, biff);
            Analyzer.harmony.Patch(AccessTools.Method(typeof(Pawn_JobTracker), nameof(Pawn_JobTracker.EndCurrentOrQueuedJob)), go, biff);
            Analyzer.harmony.Patch(AccessTools.Method(typeof(Pawn_JobTracker), nameof(Pawn_JobTracker.EndCurrentJob)), go, biff);
        }

        public static void Prefix(MethodBase __originalMethod, ref string __state)
        {
            if (!Active || !Analyzer.running) return;

            __state = __originalMethod.Name;

            Analyzer.Start(__state, null, null, null, null, __originalMethod as MethodInfo);
        }
       
        public static void Postfix(string __state)
        {
            Analyzer.Stop(__state);
        }
    }
}
