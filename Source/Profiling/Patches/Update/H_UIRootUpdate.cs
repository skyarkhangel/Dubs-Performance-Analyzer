﻿using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;
using Verse.Sound;

namespace Analyzer.Profiling
{
    [Entry("UIRootUpdate", Category.Update)]
    internal static class H_UIRootUpdate
    {
        public static bool Active = false;

        public static void ProfilePatch()
        {

            HarmonyMethod go = new HarmonyMethod(typeof(H_UIRootUpdate), nameof(Start));
            HarmonyMethod biff = new HarmonyMethod(typeof(H_UIRootUpdate), nameof(Stop));

            void slop(Type e, string s)
            {
                Modbase.Harmony.Patch(AccessTools.Method(e, s), go, biff);
            }

            slop(typeof(ScreenshotTaker), nameof(ScreenshotTaker.Update));
            slop(typeof(DragSliderManager), nameof(DragSliderManager.DragSlidersUpdate));
            slop(typeof(WindowStack), nameof(WindowStack.WindowsUpdate));
            slop(typeof(MouseoverSounds), nameof(MouseoverSounds.ResolveFrame));
            slop(typeof(UIHighlighter), nameof(UIHighlighter.UIHighlighterUpdate));
            slop(typeof(Messages), nameof(Messages.Update));
            slop(typeof(WorldInterface), nameof(WorldInterface.WorldInterfaceUpdate));
            slop(typeof(MapInterface), nameof(MapInterface.MapInterfaceUpdate));
            slop(typeof(AlertsReadout), nameof(AlertsReadout.AlertsReadoutUpdate));
            slop(typeof(LessonAutoActivator), nameof(LessonAutoActivator.LessonAutoActivatorUpdate));
            slop(typeof(Tutor), nameof(Tutor.TutorUpdate));
        }

        [HarmonyPriority(Priority.Last)]
        public static void Start(MethodInfo __originalMethod, ref Profiler __state)
        {
            if (Active)
            {
                __state = ProfileController.Start($"{__originalMethod.DeclaringType} - {__originalMethod.Name}", null, null, null, null, __originalMethod);
            }
        }

        [HarmonyPriority(Priority.First)]
        public static void Stop(Profiler __state)
        {
            if (Active)
            {
                __state?.Stop();
            }
        }
    }
}