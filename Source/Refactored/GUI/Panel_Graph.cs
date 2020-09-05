﻿using ColourPicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Analyzer
{
    public static class Panel_Graph
    {
        private static int entryCount = 300;
        private static int hoverVal;
        private static string hoverValStr = string.Empty;
        private static int ResetRange;

        private static float WindowMax;

        private static double max;
        private static string MaxStr;
        private static string totalBytesStr;
        public static int Entries = 0;

        public static void DisplayColorPicker(Rect rect, bool LineCol)
        {
            Widgets.DrawBoxSolid(rect, (LineCol) ? Modbase.Settings.LineCol : Modbase.Settings.GraphCol);

            if (Widgets.ButtonInvisible(rect, true))
            {
                if (Find.WindowStack.WindowOfType<colourPicker>() != null) // if we already have a colour window open, close it
                    Find.WindowStack.RemoveWindowsOfType(typeof(colourPicker));

                else
                {
                    colourPicker cp = new colourPicker();
                    if (LineCol) cp.Setcol = () => Modbase.Settings.LineCol = colourPicker.CurrentCol;
                    else cp.Setcol = () => Modbase.Settings.GraphCol = colourPicker.CurrentCol;

                    cp.SetColor((LineCol) ? Modbase.Settings.LineCol : Modbase.Settings.GraphCol);

                    Find.WindowStack.Add(cp);
                }
            }
        }

        public static void DrawSettings(Rect rect, int entries)
        {
            Rect sliderRect = rect.RightPartPixels(200f);
            sliderRect.x -= 15;
            entryCount = (int)Widgets.HorizontalSlider(sliderRect, entryCount, 10, 2000, true, string.Intern($"{entryCount} Entries"));
            sliderRect = new Rect(sliderRect.xMax + 5, sliderRect.y + 2, 10, 10);

            DisplayColorPicker(sliderRect, true);
            sliderRect.y += 12;
            DisplayColorPicker(sliderRect, false);

            rect.width -= 220;
            Text.Anchor = TextAnchor.MiddleRight;
            Widgets.Label(rect, hoverValStr);
            Text.Anchor = TextAnchor.UpperLeft;
        }

        public static void Draw(Rect position)
        {
            //ResetRange++;
            //if (ResetRange >= 500)
            //{
            //    ResetRange = 0;
            //    WindowMax = 0;
            //}

            //Text.Font = GameFont.Small;

            //Profiler prof = GUIController.GetCurrentProfiler;
            //if (prof == null || prof.times.Length <= 0) return;

            //int entries = (prof.times.Length > entryCount) ? entryCount : prof.times.Length;

            //DrawSettings(position.TopPartPixels(30f).ContractedBy(2f), entries);
            //position = position.BottomPartPixels(position.height - 30f);

            //Widgets.DrawBoxSolid(position, Modbase.Settings.GraphCol);

            //GUI.color = Color.grey;
            //Widgets.DrawBox(position, 2);
            //GUI.color = Color.white;

            //float gap = position.width / entries;

            //GUI.BeginGroup(position);
            //position = position.AtZero();

            //double LastMax = max;
            //max = prof.times[0];

            //for (int i = 1; i < entries; i++)
            //{
            //    double entry = prof.times[i];

            //    if (entry > max) max = entry;
            //}

            //if (max > WindowMax)
            //    WindowMax = (float)max;

            //Vector2 last = new Vector2();

            //for (int i = 0; i < entries; i++)
            //{
            //    float entry = (float)prof.times[i];
            //    float y = GenMath.LerpDoubleClamped(0, WindowMax, position.height, position.y, entry);

            //    Vector2 screenPoint = new Vector2(position.xMax - (gap * i), y);

            //    if (i != 0)
            //    {
            //        Widgets.DrawLine(last, screenPoint, Modbase.Settings.LineCol, 1f);

            //        Rect relevantArea = new Rect(screenPoint.x - gap / 2f, position.y, gap, position.height);
            //        if (Mouse.IsOver(relevantArea))
            //        {
            //            //DoHover = true;
            //            if (i != hoverVal)
            //            {
            //                hoverVal = i;
            //                hoverValStr = $"{entry:0.00000}ms {prof.hits[i]} calls";
            //            }
            //            SimpleCurveDrawer.DrawPoint(screenPoint);
            //        }
            //    }

            //    last = screenPoint;
            //}

            //if (LastMax != max) MaxStr = $" Max: {max:0.0000}ms";

            //float LogMaxY = GenMath.LerpDoubleClamped(0, WindowMax, position.height, position.y, (float)max);
            //Rect crunt = position;
            //crunt.y = LogMaxY;
            //Widgets.Label(crunt, MaxStr);
            //Widgets.DrawLine(new Vector2(position.x, LogMaxY), new Vector2(position.xMax, LogMaxY), Color.red, 1f);

            //last = Vector2.zero;

            //GUI.EndGroup();

            //Entries = entries;
        }
    }
}
