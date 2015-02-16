#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace OpenSpadesCompatibilityTool
{
    public class SDL
    {
        #region Constants

        public const int SDL_INIT_VIDEO = 0x00000020;
        public const int SDL_OPENGL = 0x00000002;
        public const int SDL_FULLSCREEN = unchecked((int) 0x80000000);
        public const int SDL_DOUBLEBUF = 0X40000000;

        #endregion

        private const string DLL_NAME = "SDL.dll";
        private const CallingConvention CALLING_CONVENTION = CallingConvention.Cdecl;

        private readonly string _dllPath;

        #region Constructor

        public SDL(string dllPath, int initFlags)
        {
            _dllPath = dllPath;

            SetDllDirectory(Path.GetDirectoryName(_dllPath));

            __SDL_Init(initFlags);
        }

        #endregion

        #region P/Invokes

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetDllDirectory(string lpPathName);

        [DllImport(DLL_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Init"), SuppressUnmanagedCodeSecurity]
        private static extern int __SDL_Init(int flags);

        [DllImport(DLL_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ListModes"), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr SDL_ListModesInternal(IntPtr format, int flags);

        #endregion

        [CLSCompliant(false)]
        public unsafe SDLRect[] ListModes(IntPtr format, int flags)
        {
            var rectPtr = SDL_ListModesInternal(format, flags);

            if (rectPtr == IntPtr.Zero)
                return null;

            if (rectPtr == new IntPtr(-1))
            {
                return new SDLRect[0];
            }

            var rects = (SDLRect**) rectPtr.ToPointer();

            var i = 0;

            var modes = new List<SDLRect>();

            while (rects[i] != null)
            {
                var rect = (SDLRect) Marshal.PtrToStructure(new IntPtr(rects[i]), typeof (SDLRect));
                modes.Add(rect);
                i++;
            }

            return modes.ToArray();
        }

        #region Nested type: SDLRect

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct SDLRect
        {
            public short X;
            public short Y;
            public short W;
            public short H;

            public SDLRect(short x, short y, short w, short h)
            {
                X = x;
                Y = y;
                W = w;
                H = h;
            }
        }

        #endregion
    }
}