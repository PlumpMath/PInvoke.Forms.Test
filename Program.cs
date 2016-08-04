using System;
using System.Runtime.InteropServices;
using PInvoke;
using static ConsoleApplication1.MorePinvoke;
using static ConsoleApplication1.WindowMessage;
using static PInvoke.Kernel32;
using static PInvoke.User32;

namespace ConsoleApplication1
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct DLGTEMPLATE
    {
        public int style;
        public int dwExtendedStyle;
        public short cdit;
        public byte x;
        public byte y;
        public byte cx;
        public byte cy;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct POINT
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// Specifies the width and height of a rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct SIZE
    {
        /// <summary>
        /// Specifies the rectangle's width. The units depend on which function uses this.
        /// </summary>
        public int cx;

        /// <summary>
        /// Specifies the rectangle's height. The units depend on which function uses this.
        /// </summary>
        public int cy;
    }

    /// <summary>
    /// Contains message information from a thread's message queue.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct MSG
    {
        /// <summary>
        /// A handle to the window whose window procedure receives the message. This member is NULL when the message is a thread message.
        /// </summary>
        public IntPtr hwnd;

        /// <summary>
        /// The message identifier. Applications can only use the low word; the high word is reserved by the system.
        /// </summary>
        public WindowMessage message;

        /// <summary>
        /// Additional information about the message. The exact meaning depends on the value of the message member.
        /// </summary>
        public IntPtr wParam;

        /// <summary>
        /// Additional information about the message. The exact meaning depends on the value of the message member.
        /// </summary>
        public IntPtr lParam;

        /// <summary>
        /// The time at which the message was posted.
        /// </summary>
        public int time;

        /// <summary>
        /// The cursor position, in screen coordinates, when the message was posted.
        /// </summary>
        public POINT pt;
    }

    [Flags]
    internal enum QueueStatusFlags
    {
        QS_KEY = 0x0001,
        QS_MOUSEMOVE = 0x0002,
        QS_MOUSEBUTTON = 0x0004,
        QS_POSTMESSAGE = 0x0008,
        QS_TIMER = 0x0010,
        QS_PAINT = 0x0020,
        QS_SENDMESSAGE = 0x0040,
        QS_HOTKEY = 0x0080,
        QS_ALLPOSTMESSAGE = 0x0100,

        QS_RAWINPUT = 0x0400,
        QS_TOUCH = 0x0800,
        QS_POINTER = 0x1000,

        QS_MOUSE = QS_MOUSEMOVE |
                   QS_MOUSEBUTTON,

        QS_INPUT = QS_MOUSE |
                   QS_KEY |
                   QS_RAWINPUT |
                   QS_TOUCH |
                   QS_POINTER,

        QS_ALLEVENTS = QS_INPUT |
                       QS_POSTMESSAGE |
                       QS_TIMER |
                       QS_PAINT |
                       QS_HOTKEY,

        QS_ALLINPUT = QS_INPUT |
                      QS_POSTMESSAGE |
                      QS_TIMER |
                      QS_PAINT |
                      QS_HOTKEY |
                      QS_SENDMESSAGE
    }

    [Flags]
    internal enum PeekMessageRemoveFlags
    {
        /// <summary>
        ///     Messages are not removed from the queue after processing by <see cref="PeekMessage" />.
        /// </summary>
        PM_NOREMOVE = 0x0000,

        /// <summary>
        ///     Messages are removed from the queue after processing by <see cref="PeekMessage" />.
        /// </summary>
        PM_REMOVE = 0x0001,

        /// <summary>
        ///     Prevents the system from releasing any thread that is waiting for the caller to go idle (see WaitForInputIdle).
        ///     Combine this value with either <see cref="PM_NOREMOVE" /> or <see cref="PM_REMOVE" />.
        /// </summary>
        PM_NOYIELD = 0x0002,

        /// <summary>
        ///     Process mouse and keyboard messages.
        /// </summary>
        PM_QS_INPUT = QueueStatusFlags.QS_INPUT << 16,

        /// <summary>
        ///     Process paint messages.
        /// </summary>
        PM_QS_PAINT = QueueStatusFlags.QS_PAINT << 16,

        /// <summary>
        ///     Process all posted messages, including timers and hotkeys.
        /// </summary>
        PM_QS_POSTMESSAGE =
            (QueueStatusFlags.QS_POSTMESSAGE | QueueStatusFlags.QS_HOTKEY | QueueStatusFlags.QS_TIMER) << 16,

        /// <summary>
        ///     Process all sent messages.
        /// </summary>
        PM_QS_SENDMESSAGE = QueueStatusFlags.QS_SENDMESSAGE << 16
    }

    internal static unsafe class MorePinvoke
    {
        public delegate IntPtr DialogProc(IntPtr hwndDlg, WindowMessage uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport(nameof(User32), SetLastError = true)]
        public static extern IntPtr CreateDialogIndirectParam(
            SafeLibraryHandle hInstance,
            DLGTEMPLATE* lpTemplate,
            IntPtr hWndParent,
            DialogProc lpDialogFunc,
            IntPtr lParamInit);

        [DllImport(nameof(User32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMessage(
            MSG* lpMsg,
            IntPtr hWnd,
            int wMsgFilterMin,
            int wMsgFilterMax);

        [DllImport(nameof(User32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(
            MSG* lpMsg,
            IntPtr hWnd,
            int wMsgFilterMin,
            int wMsgFilterMax,
            PeekMessageRemoveFlags wRemoveMsg);

        [DllImport(nameof(User32), SetLastError = true)]
        public static extern IntPtr DefWindowProc(
            IntPtr hWnd,
            WindowMessage Msg,
            IntPtr wParam,
            IntPtr lParam);

        /// <summary>
        ///     Translates virtual-key messages into character messages. The character messages are posted to the calling thread's
        ///     message queue, to be read the next time the thread calls the <see cref="GetMessage" /> or
        ///     <see cref="PeekMessage" /> function.
        /// </summary>
        /// <param name="lpMsg">
        ///     A pointer to an <see cref="MSG" /> structure that contains message information retrieved from the
        ///     calling thread's message queue by using the <see cref="GetMessage" /> or <see cref="PeekMessage" /> function.
        /// </param>
        /// <returns>
        ///     If the message is translated (that is, a character message is posted to the thread's message queue), the return
        ///     value is nonzero.
        ///     <para>
        ///         If the message is <see cref="WM_KEYDOWN" />, <see cref="WM_KEYUP" />, <see cref="WM_SYSKEYDOWN" />, or
        ///         <see cref="WM_SYSKEYUP" />, the return value is nonzero, regardless of the translation.
        ///     </para>
        ///     <para>
        ///         If the message is not translated (that is, a character message is not posted to the thread's message queue),
        ///         the return value is zero.
        ///     </para>
        /// </returns>
        [DllImport(nameof(User32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TranslateMessage(MSG* lpMsg);

        /// <summary>
        ///     Dispatches a message to a window procedure. It is typically used to dispatch a message retrieved by the
        ///     <see cref="GetMessage" /> function.
        /// </summary>
        /// <param name="lpMsg">A pointer to a structure that contains the message.</param>
        /// <returns>
        ///     The return value specifies the value returned by the window procedure. Although its meaning depends on the
        ///     message being dispatched, the return value generally is ignored.
        /// </returns>
        [DllImport(nameof(User32), SetLastError = true)]
        public static extern IntPtr DispatchMessage(MSG* lpMsg);

        /// <summary>
        ///     Indicates to the system that a thread has made a request to terminate (quit). It is typically used in response to a
        ///     <see cref="WM_DESTROY" /> message.
        /// </summary>
        /// <param name="nExitCode">
        ///     The application exit code. This value is used as the wParam parameter of the
        ///     <see cref="WM_QUIT" /> message.
        /// </param>
        [DllImport(nameof(User32), SetLastError = true)]
        public static extern void PostQuitMessage(int nExitCode);
    }

    public static class Win32ErrorCheckExtensions
    {
        public static IntPtr Win32ThrowIfZero(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            return ptr;
        }
    }

    internal unsafe class Program
    {
        private static IntPtr CreateDialogParam(
            SafeLibraryHandle hinst,
            int iTemplate,
            IntPtr hwndParent,
            DialogProc lpDlgProc,
            IntPtr dwInitParam)
        {
            IntPtr hdlg;
            var hrsrc = FindResource(hinst, MAKEINTRESOURCE(iTemplate), RT_DIALOG).Win32ThrowIfZero();
            var hglob = LoadResource(hinst, hrsrc).Win32ThrowIfZero();
            var pTemplate = LockResource(hglob); // fixed 1pm
            if (pTemplate == null)
            {
                throw new Win32Exception();
            }

            hdlg = CreateDialogIndirectParam(hinst,
                (DLGTEMPLATE*) pTemplate, hwndParent, lpDlgProc,
                dwInitParam);
            return hdlg;
        }

        private static void Main(string[] args)
        {
            var window = CreateDialogParam(SafeLibraryHandle.Null, 101, IntPtr.Zero, LpDlgProc, IntPtr.Zero);
            if (window == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            ShowWindow(window, WindowShowStyle.SW_SHOWDEFAULT);

            MSG msg;
            while (GetMessage(&msg, IntPtr.Zero, 0, 0)) {
                TranslateMessage(&msg);
                DispatchMessage(&msg);
            }
        }

        static readonly IntPtr TRUE = new IntPtr(1);
        static readonly IntPtr FALSE = new IntPtr(0);

        private static IntPtr LpDlgProc(IntPtr hwndDlg, WindowMessage uMsg, IntPtr wParam, IntPtr lParam)
        {
            switch (uMsg)
            {
                case WM_CREATE:
                    return TRUE;

                case WM_DESTROY:
                    PostQuitMessage(0);
                    return TRUE;

                default:
                    return DefWindowProc(hwndDlg, uMsg, wParam, lParam);
            }
        }
    }
}