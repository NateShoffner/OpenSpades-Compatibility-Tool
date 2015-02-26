#region

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace OpenSpadesCompatibilityTool
{
    public class SDL : IDisposable
    {
        #region Constants

        public const int SDL_INIT_VIDEO = 0x00000020;
        public const int SDL_OPENGL = 0x00000002;
        public const int SDL_FULLSCREEN = unchecked((int) 0x80000000);
        public const int SDL_DOUBLEBUF = 0X40000000;

        #endregion

        private const string DLL_NAME = "SDL.dll";
        private const CallingConvention CALLING_CONVENTION = CallingConvention.Cdecl;

        #region Constructor

        public SDL(int initFlags)
        {
            SDL_Init(initFlags);
        }

        #endregion

        #region P/Invokes

        [DllImport(DLL_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Init"), SuppressUnmanagedCodeSecurity]
        private static extern int SDL_Init(int flags);

        [DllImport(DLL_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Quit"), SuppressUnmanagedCodeSecurity]
        private static extern int SDL_Quit();

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

        #region Implementation of IDisposable

        public void Dispose()
        {
            SDL_Quit();
        }

        #endregion
    }
}