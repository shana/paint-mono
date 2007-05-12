/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.SystemLayer;
using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace PaintDotNet.SystemLayer
{
    /// <summary>
    /// Methods for manual profiling and tracing. Only enabled in Debug builds.
    /// </summary>
    /// <remarks>
    /// This class does not rely on any system-specific functionality, but is placed
    /// in the SystemLayer assembly (as opposed to PdnLib) so that classes here may 
    /// also used it.
    /// </remarks>
    public static class Tracing
    {
#if DEBUG
        private static Timing timing = new Timing();
        private static Stack tracePoints = new Stack();

        private class TracePoint
        {
            private string message;
            private ulong timestamp;

            public string Message
            {
                get
                {
                    return message;
                }
            }

            public ulong Timestamp
            {
                get
                {
                    return timestamp;
                }
            }

            public TracePoint(string message, ulong timestamp)
            {
                this.message = message;
                this.timestamp = timestamp;
            }
        }
#endif

        [Conditional("DEBUG")]
        public static void Enter()
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            StackFrame parentFrame = trace.GetFrame(1);
            MethodBase parentMethod = parentFrame.GetMethod();
            ulong now = timing.GetTickCount();
            string msg = new string(' ', 4 * tracePoints.Count) + parentMethod.DeclaringType.Name + "." + parentMethod.Name;
            Debug.WriteLine((now - timing.BirthTick).ToString() + ": " + msg);
            TracePoint tracePoint = new TracePoint(msg, now);
            tracePoints.Push(tracePoint);
#endif
        }

        [Conditional("DEBUG")]
        public static void Enter(string message)
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            StackFrame parentFrame = trace.GetFrame(1);
            MethodBase parentMethod = parentFrame.GetMethod();
            ulong now = timing.GetTickCount();
            string msg = new string(' ', 4 * tracePoints.Count) + parentMethod.DeclaringType.Name + "." + 
                parentMethod.Name + ": " + message;
            Debug.WriteLine((now - timing.BirthTick).ToString() + ": " + msg);
            TracePoint tracePoint = new TracePoint(msg, now);
            tracePoints.Push(tracePoint);
#endif
        }

        [Conditional("DEBUG")]
        public static void Leave()
        {
#if DEBUG
            TracePoint tracePoint = (TracePoint)tracePoints.Pop();
            ulong now = timing.GetTickCount();
            Debug.WriteLine((now - timing.BirthTick).ToString() + ": " + tracePoint.Message + " (" + 
                (now - tracePoint.Timestamp).ToString() + "ms)");
#endif
        }

        [Conditional("DEBUG")]
        public static void Ping(string message, int callerCount)
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            string callerString = "";
            for (int i = 0; i < Math.Min(trace.FrameCount - 1, callerCount); ++i)
            {
                StackFrame frame = trace.GetFrame(1 + i);
                MethodBase method = frame.GetMethod();
                callerString += method.DeclaringType.Name + "." + method.Name;
                if (i != callerCount - 1)
                {
                    callerString += " <- ";
                }
            }
            ulong now = timing.GetTickCount();
            Debug.WriteLine((now - timing.BirthTick).ToString() + ": " + new string(' ', 4 * tracePoints.Count) +
                callerString + (message != null ? (": " + message) : ""));
#endif
        }

        [Conditional("DEBUG")]
        public static void Ping(string message)
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            StackFrame parentFrame = trace.GetFrame(1);
            MethodBase parentMethod = parentFrame.GetMethod();
            ulong now = timing.GetTickCount();
            Debug.WriteLine((now - timing.BirthTick).ToString() + ": " + new string(' ', 4 * tracePoints.Count) + 
                parentMethod.DeclaringType.Name + "." + parentMethod.Name + (message != null ? (": " + message) : ""));
#endif
        }

        [Conditional("DEBUG")]
        public static void Ping()
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            StackFrame parentFrame = trace.GetFrame(1);
            MethodBase parentMethod = parentFrame.GetMethod();
            ulong now = timing.GetTickCount();
            Debug.WriteLine((now - timing.BirthTick).ToString() + ": " + new string(' ', 4 * tracePoints.Count) + 
                parentMethod.DeclaringType.Name + "." + parentMethod.Name);
#endif
        }

    }
}
