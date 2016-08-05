using System;
using System.Runtime.InteropServices;
using PInvoke;
using static PInvoke.User32;

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

        /// <summary>
        ///     Retrieves a message from the calling thread's message queue. The function dispatches incoming sent messages until a
        ///     posted message is available for retrieval.
        ///     <para>
        ///         Unlike <see cref="GetMessage" />, the <see cref="PeekMessage" /> function does not wait for a message to be
        ///         posted before returning.
        ///     </para>
        /// </summary>
        /// <param name="lpMsg">A pointer to an <see cref="MSG" /> structure that receives message information.</param>
        /// <param name="hWnd">
        ///     A handle to the window whose messages are to be retrieved. The window must belong to the current thread.
        ///     <para>
        ///         If hWnd is <see cref="IntPtr.Zero" />, PeekMessage retrieves messages for any window that belongs to the
        ///         current thread, and any messages on the current thread's message queue whose hwnd value is NULL (see the MSG
        ///         structure). Therefore if hWnd is <see cref="IntPtr.Zero" />, both window messages and thread messages are
        ///         processed.
        ///     </para>
        ///     <para>
        ///         If hWnd is -1, PeekMessage retrieves only messages on the current thread's message queue whose hwnd value is
        ///         NULL, that is, thread messages as posted by <see cref="PostMessage" /> (when the hWnd parameter is
        ///         <see cref="IntPtr.Zero" />) or
        ///         <see cref="PostThreadMessage" />.
        ///     </para>
        /// </param>
        /// <param name="wMsgFilterMin">
        ///     <para>
        ///         The value of the first message in the range of messages to be examined. Use
        ///         <see cref="WindowMessage.WM_KEYFIRST" /> to specify the first keyboard message or
        ///         <see cref="WindowMessage.WM_MOUSEFIRST" /> to specify the first mouse message.
        ///     </para>
        ///     <para>
        ///         If wMsgFilterMin and wMsgFilterMax are both <see cref="WindowMessage.WM_NULL" />, PeekMessage returns all
        ///         available messages (that is, no range filtering is performed).
        ///     </para>
        /// </param>
        /// <param name="wMsgFilterMax">
        ///     <para>
        ///         The value of the last message in the range of messages to be examined. Use
        ///         <see cref="WindowMessage.WM_KEYLAST" /> to specify the last keyboard message or
        ///         <see cref="WindowMessage.WM_MOUSELAST" /> to specify the last mouse message.
        ///     </para>
        ///     <para>
        ///         If wMsgFilterMin and wMsgFilterMax are both <see cref="WindowMessage.WM_NULL" />, PeekMessage returns all
        ///         available messages (that is, no range filtering is performed).
        ///     </para>
        /// </param>
        /// <returns>
        ///     If the function retrieves a message other than <see cref="WindowMessage.WM_QUIT" />, the return value is nonzero.
        ///     <para>If the function retrieves the <see cref="WindowMessage.WM_QUIT" /> message, the return value is zero.</para>
        ///     <para>
        ///         If there is an error, the return value is -1. For example, the function fails if <paramref name="hWnd" /> is
        ///         an invalid window handle or <paramref name="lpMsg" /> is an invalid pointer. To get extended error information,
        ///         call <see cref="Kernel32.GetLastError" />.
        ///     </para>
        /// </returns>
        [DllImport(nameof(User32), SetLastError = true)]
        public static extern int GetMessage(
            MSG* lpMsg,
            IntPtr hWnd,
            WindowMessage wMsgFilterMin,
            WindowMessage wMsgFilterMax);

        /// <summary>
        ///     Dispatches incoming sent messages, checks the thread message queue for a posted message, and retrieves the message
        ///     (if any exist).
        /// </summary>
        /// <param name="lpMsg">A pointer to an <see cref="MSG" /> structure that receives message information.</param>
        /// <param name="hWnd">
        ///     A handle to the window whose messages are to be retrieved. The window must belong to the current thread.
        ///     <para>
        ///         If hWnd is <see cref="IntPtr.Zero" />, PeekMessage retrieves messages for any window that belongs to the
        ///         current thread, and any messages on the current thread's message queue whose hwnd value is NULL (see the MSG
        ///         structure). Therefore if hWnd is <see cref="IntPtr.Zero" />, both window messages and thread messages are
        ///         processed.
        ///     </para>
        ///     <para>
        ///         If hWnd is -1, PeekMessage retrieves only messages on the current thread's message queue whose hwnd value is
        ///         NULL, that is, thread messages as posted by <see cref="PostMessage" /> (when the hWnd parameter is
        ///         <see cref="IntPtr.Zero" />) or
        ///         <see cref="PostThreadMessage" />.
        ///     </para>
        /// </param>
        /// <param name="wMsgFilterMin">
        ///     <para>
        ///         The value of the first message in the range of messages to be examined. Use
        ///         <see cref="WindowMessage.WM_KEYFIRST" /> to specify the first keyboard message or
        ///         <see cref="WindowMessage.WM_MOUSEFIRST" /> to specify the first mouse message.
        ///     </para>
        ///     <para>
        ///         If wMsgFilterMin and wMsgFilterMax are both <see cref="WindowMessage.WM_NULL" />, PeekMessage returns all
        ///         available messages (that is, no range filtering is performed).
        ///     </para>
        /// </param>
        /// <param name="wMsgFilterMax">
        ///     <para>
        ///         The value of the last message in the range of messages to be examined. Use
        ///         <see cref="WindowMessage.WM_KEYLAST" /> to specify the last keyboard message or
        ///         <see cref="WindowMessage.WM_MOUSELAST" /> to specify the last mouse message.
        ///     </para>
        ///     <para>
        ///         If wMsgFilterMin and wMsgFilterMax are both <see cref="WindowMessage.WM_NULL" />, PeekMessage returns all
        ///         available messages (that is, no range filtering is performed).
        ///     </para>
        /// </param>
        /// <param name="wRemoveMsg">Specifies how messages are to be handled</param>
        /// <returns>
        ///     If a message is available, the return value is true.
        ///     <para>If no messages are available, the return value is false.</para>
        /// </returns>
        [DllImport(nameof(User32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(
            MSG* lpMsg,
            IntPtr hWnd,
            WindowMessage wMsgFilterMin,
            WindowMessage wMsgFilterMax,
            PeekMessageRemoveFlags wRemoveMsg);

        /// <summary>
        ///     Calls the default window procedure to provide default processing for any window messages that an application does
        ///     not process. This function ensures that every message is processed. DefWindowProc is called with the same
        ///     parameters received by the window procedure.
        /// </summary>
        /// <param name="hWnd">A handle to the window procedure that received the message.</param>
        /// <param name="Msg">The message.</param>
        /// <param name="wParam">
        ///     Additional message information. The content of this parameter depends on the value of the
        ///     <paramref name="Msg" /> parameter.
        /// </param>
        /// <param name="lParam">
        ///     Additional message information. The content of this parameter depends on the value of the
        ///     <paramref name="Msg" /> parameter.
        /// </param>
        /// <returns>The return value is the result of the message processing and depends on the message.</returns>
        [DllImport(nameof(User32))]
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