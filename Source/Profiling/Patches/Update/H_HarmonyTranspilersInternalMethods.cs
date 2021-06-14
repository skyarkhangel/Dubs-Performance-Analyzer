﻿using HarmonyLib;
using RimWorld.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Analyzer.Profiling
{
    [Entry("entry.update.harmonytranspilers", Category.Update)]
    public static class H_HarmonyTranspilersInternalMethods
    {
        public static void ProfilePatch()
        {
            var patches = Harmony.GetAllPatchedMethods().ToList();

            var filteredTranspilers = patches
                .Where(m => Harmony.GetPatchInfo(m).Transpilers.Any(p => Utility.IsNotAnalyzerPatch(p.owner)) && m is MethodInfo)
                .Select(m => m as MethodInfo)
                .ToList();

            foreach (var meth in filteredTranspilers)
            {
                try
                {
                    MethodTransplanting.ProfileInsertedMethods(meth);
                }
                catch (Exception e)
                {
#if DEBUG
                        ThreadSafeLogger.ReportException(e, $"[Analyzer] Failed to patch transpiler {Utility.GetSignature(meth, false)}");
#endif
#if NDEBUG
                    if (Settings.verboseLogging)
                        ThreadSafeLogger.ReportException(e, $"[Analyzer] Failed to patch transpiler {Utility.GetSignature(meth, false)}");
#endif
                }
            }
        }
    }
}




