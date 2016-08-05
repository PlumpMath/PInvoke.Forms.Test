using System;
using System.Runtime.InteropServices;
using PInvoke;

namespace ConsoleApplication1
{
    internal static unsafe class User32Ex
    {
        public delegate IntPtr DialogProc(IntPtr hwndDlg, WindowMessage uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport(nameof(User32), SetLastError = true)]
        public static extern IntPtr CreateDialogIndirectParam(
            Kernel32.SafeLibraryHandle hInstance,
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
        ///     Retrieves the type of messages found in the calling thread's message queue.
        /// </summary>
        /// <param name="flags">The types of messages for which to check</param>
        /// <returns>
        ///     The high-order word of the return value indicates the types of messages currently in the queue. The low-order word
        ///     indicates the types of messages that have been added to the queue and that are still in the queue since the last
        ///     call to the <see cref="GetQueueStatus" />, <see cref="GetMessage" />, or <see cref="PeekMessage" /> function.
        /// </returns>
        [DllImport(nameof(User32))]
        public static extern int GetQueueStatus(QueueStatusFlags flags);

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
        ///         If the message is <see cref="WindowMessage.WM_KEYDOWN" />, <see cref="WindowMessage.WM_KEYUP" />, <see cref="WindowMessage.WM_SYSKEYDOWN" />, or
        ///         <see cref="WindowMessage.WM_SYSKEYUP" />, the return value is nonzero, regardless of the translation.
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
        ///     <see cref="WindowMessage.WM_DESTROY" /> message.
        /// </summary>
        /// <param name="nExitCode">
        ///     The application exit code. This value is used as the wParam parameter of the
        ///     <see cref="WindowMessage.WM_QUIT" /> message.
        /// </param>
        [DllImport(nameof(User32), SetLastError = true)]
        public static extern void PostQuitMessage(int nExitCode);
    }
}