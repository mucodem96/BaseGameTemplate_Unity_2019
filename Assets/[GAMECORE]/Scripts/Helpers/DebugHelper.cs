﻿using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Scripts.Helpers
{
    public static class DebugHelper
    {
        [Conditional("ENABLE_LOG")]
        public static void LogRed(string message)
        {
            LogWithColor(message, "red");
        }

        [Conditional("ENABLE_LOG")]
        public static void LogGreen(string message)
        {
            LogWithColor(message, "green");
        }

        [Conditional("ENABLE_LOG")]
        public static void LogYellow(string message)
        {
            LogWithColor(message, "yellow");
        }

        [Conditional("ENABLE_LOG")]
        private static void LogWithColor(string message, string color)
        {
            Debug.Log("<b><color=" + color + ">" + message + "</color></b>");
        }
    }
}