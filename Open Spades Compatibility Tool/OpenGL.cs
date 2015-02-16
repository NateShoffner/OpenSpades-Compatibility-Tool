#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

#endregion

namespace OpenSpadesCompatibilityTool
{
    public class OpenGL : IDisposable
    {
        private const string DLL_NAME = "opengl32.dll";
        private const CallingConvention CALLING_CONVENTION = CallingConvention.Winapi;
        private readonly IntPtr _hWnd;
        private IntPtr contextHandle;

        public OpenGL(IntPtr hWnd)
        {
            _hWnd = hWnd;
        }

        #region P/Invokes

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport(DLL_NAME, CallingConvention = CALLING_CONVENTION)]
        private static extern IntPtr glGetString(StringName name);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int ChoosePixelFormat(IntPtr hdc, [In] ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern bool SetPixelFormat(IntPtr hdc, int iPixelFormat, ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr wglCreateContext(IntPtr hdc);

        [DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int wglDeleteContext(IntPtr context);

        [DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int wglMakeCurrent(IntPtr hdc, IntPtr context);

        [DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern void glGetIntegerv(uint pname, ref int @params);

        #endregion

        public void StartContext()
        {
            var hdc = GetDC(_hWnd);

            contextHandle = wglCreateContext(hdc);

            if (contextHandle == IntPtr.Zero)
            {
                var pixelformatdescriptor = new PIXELFORMATDESCRIPTOR();
                pixelformatdescriptor.Init();

                var pixelFormat = ChoosePixelFormat(hdc, ref pixelformatdescriptor);

                if (SetPixelFormat(hdc, pixelFormat, ref pixelformatdescriptor) == false)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                contextHandle = wglCreateContext(hdc);
                if (contextHandle == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            wglMakeCurrent(hdc, contextHandle);
        }

        private static string GetString(StringName name)
        {
            var strPtr = glGetString(name);
            return strPtr == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(strPtr);
        }

        #region Public Methods

        public string GetVendor()
        {
            return GetString(StringName.GL_VENDOR);
        }

        public string GetRenderer()
        {
            return GetString(StringName.GL_RENDERER);
        }

        public string GetVersion()
        {
            return GetString(StringName.GL_VERSION);
        }

        public List<string> GetExtensions()
        {
            var extensions = new List<string>();

            var extStr = GetString(StringName.GL_EXTENSIONS);

            if (extStr != null)
            {
                extensions.AddRange(extStr.Split(' '));
            }

            return extensions;
        }

        public string GetGLSLVersion()
        {
            return GetString(StringName.GL_SHADING_LANGUAGE_VERSION);
        }

        public int GetMaxTextureSize()
        {
            var i = 0;
            glGetIntegerv(0x0D33, ref i); //GL_MAX_TEXTURE_SIZE
            return i;
        }

        public int GetMax3DTextureSize()
        {
            var i = 0;
            glGetIntegerv(0x8073, ref i); //GL_MAX_3D_TEXTURE_SIZE
            return i;
        }

        public int GetMaxCombinedTextureUnits()
        {
            var i = 0;
            glGetIntegerv(0x8B4D, ref i); //GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS
            return i;
        }

        public int GetMaxVertexTextureImageUnits()
        {
            var i = 0;
            glGetIntegerv(0x8B4C, ref i); //GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS 
            return i;
        }

        public int GetMaxVaryingComponents()
        {
            var i = 0;
            glGetIntegerv(0x8B4B, ref i); //GL_MAX_VARYING_COMPONENTS 
            return i;
        }

        #endregion

        #region Nested type: PFD_FLAGS

        [Flags]
        private enum PFD_FLAGS : uint
        {
            PFD_DOUBLEBUFFER = 0x00000001,
            PFD_STEREO = 0x00000002,
            PFD_DRAW_TO_WINDOW = 0x00000004,
            PFD_DRAW_TO_BITMAP = 0x00000008,
            PFD_SUPPORT_GDI = 0x00000010,
            PFD_SUPPORT_OPENGL = 0x00000020,
            PFD_GENERIC_FORMAT = 0x00000040,
            PFD_NEED_PALETTE = 0x00000080,
            PFD_NEED_SYSTEM_PALETTE = 0x00000100,
            PFD_SWAP_EXCHANGE = 0x00000200,
            PFD_SWAP_COPY = 0x00000400,
            PFD_SWAP_LAYER_BUFFERS = 0x00000800,
            PFD_GENERIC_ACCELERATED = 0x00001000,
            PFD_SUPPORT_DIRECTDRAW = 0x00002000,
            PFD_DIRECT3D_ACCELERATED = 0x00004000,
            PFD_SUPPORT_COMPOSITION = 0x00008000,
            PFD_DEPTH_DONTCARE = 0x20000000,
            PFD_DOUBLEBUFFER_DONTCARE = 0x40000000,
            PFD_STEREO_DONTCARE = 0x80000000
        }

        #endregion

        #region Nested type: PFD_LAYER_TYPES

        private enum PFD_LAYER_TYPES : byte
        {
            PFD_MAIN_PLANE = 0,
            PFD_OVERLAY_PLANE = 1,
            PFD_UNDERLAY_PLANE = 255
        }

        #endregion

        #region Nested type: PFD_PIXEL_TYPE

        private enum PFD_PIXEL_TYPE : byte
        {
            PFD_TYPE_RGBA = 0,
            PFD_TYPE_COLORINDEX = 1
        }

        #endregion

        #region Nested type: PIXELFORMATDESCRIPTOR

        [StructLayout(LayoutKind.Sequential)]
        public struct PIXELFORMATDESCRIPTOR
        {
            public void Init()
            {
                nSize = (ushort) Marshal.SizeOf(typeof (PIXELFORMATDESCRIPTOR));
                nVersion = 1;
                dwFlags = PFD_FLAGS.PFD_DRAW_TO_WINDOW | PFD_FLAGS.PFD_SUPPORT_OPENGL | PFD_FLAGS.PFD_DOUBLEBUFFER | PFD_FLAGS.PFD_SUPPORT_COMPOSITION;
                iPixelType = PFD_PIXEL_TYPE.PFD_TYPE_RGBA;
                cColorBits = 24;
                cRedBits = cRedShift = cGreenBits = cGreenShift = cBlueBits = cBlueShift = 0;
                cAlphaBits = cAlphaShift = 0;
                cAccumBits = cAccumRedBits = cAccumGreenBits = cAccumBlueBits = cAccumAlphaBits = 0;
                cDepthBits = 32;
                cStencilBits = cAuxBuffers = 0;
                iLayerType = PFD_LAYER_TYPES.PFD_MAIN_PLANE;
                bReserved = 0;
                dwLayerMask = dwVisibleMask = dwDamageMask = 0;
            }

            private ushort nSize;
            private ushort nVersion;
            private PFD_FLAGS dwFlags;
            private PFD_PIXEL_TYPE iPixelType;
            private byte cColorBits;
            private byte cRedBits;
            private byte cRedShift;
            private byte cGreenBits;
            private byte cGreenShift;
            private byte cBlueBits;
            private byte cBlueShift;
            private byte cAlphaBits;
            private byte cAlphaShift;
            private byte cAccumBits;
            private byte cAccumRedBits;
            private byte cAccumGreenBits;
            private byte cAccumBlueBits;
            private byte cAccumAlphaBits;
            private byte cDepthBits;
            private byte cStencilBits;
            private byte cAuxBuffers;
            private PFD_LAYER_TYPES iLayerType;
            private byte bReserved;
            private uint dwLayerMask;
            private uint dwVisibleMask;
            private uint dwDamageMask;
        }

        #endregion

        #region Nested type: StringName

        private enum StringName
        {
            GL_VENDOR = 0x1F00,
            GL_RENDERER = 0x1F01,
            GL_VERSION = 0x1F02,
            GL_EXTENSIONS = 0x1F03,
            GL_SHADING_LANGUAGE_VERSION = 0x8B8C
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (contextHandle != IntPtr.Zero)
                wglDeleteContext(contextHandle);
        }

        #endregion
    }
}