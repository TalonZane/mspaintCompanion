using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mspaintCompanion.Native
{
    /// <summary>
    /// Provides functions from the USER32 Dynamic Link Library.
    /// </summary>
    internal static class User32
    {
        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.
        /// </summary>
        /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
        /// <returns>If the window was brought to the foreground, the return value is nonzero. If the window was not brought to the foreground, the return value is zero.</returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified window or for the entire screen. You can use the returned handle in subsequent GDI functions to draw in the DC. The device context is an opaque data structure, whose values are used internally by GDI.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose DC is to be retrieved. If this value is NULL, GetDC retrieves the DC for the entire screen.</param>
        /// <returns>If the function succeeds, the return value is a handle to the DC for the specified window's client area. If the function fails, the return value is NULL.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        /// <summary>
        /// The ReleaseDC function releases a device context (DC), freeing it for use by other applications. The effect of the ReleaseDC function depends on the type of DC. It frees only common and window DCs. It has no effect on class or private DCs.
        /// </summary>
        /// <param name="hwnd">A handle to the window whose DC is to be released.</param>
        /// <param name="dc">A handle to the DC to be released.</param>
        /// <returns>The return value indicates whether the DC was released. If the DC was released, the return value is 1. If the DC was not released, the return value is zero.</returns>
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        /// <summary>
        /// The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
        /// </summary>
        public const int WS_EX_TRANSPARENT = 0x00000020;
    }
}
