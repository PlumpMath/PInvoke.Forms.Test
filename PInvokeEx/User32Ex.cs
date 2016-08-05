using System;
using System.Runtime.InteropServices;
using PInvoke;
using static PInvoke.Kernel32;
using static PInvoke.User32;

namespace ConsoleApplication1
{
    internal static unsafe class User32Ex
    {
        /// <summary>
        ///     Application-defined callback function used with the CreateDialog and DialogBox families of functions. It processes
        ///     messages sent to a modal or modeless dialog box.
        /// </summary>
        /// <param name="hwndDlg">A handle to the dialog box.</param>
        /// <param name="uMsg">The message.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>
        ///     Typically, the dialog box procedure should return TRUE if it processed the message, and FALSE if it did not. If the
        ///     dialog box procedure returns FALSE, the dialog manager performs the default dialog operation in response to the
        ///     message.
        ///     <para>
        ///         If the dialog box procedure processes a message that requires a specific return value, the dialog box
        ///         procedure should set the desired return value by calling <see cref="SetWindowLong" /> with
        ///         <see cref="WindowLongIndexFlags.DWLP_MSGRESULT" /> immediately before returning TRUE. Note that you must call
        ///         SetWindowLong immediately before returning TRUE; doing so earlier may result in the
        ///         <see cref="WindowLongIndexFlags.DWLP_MSGRESULT" /> value being overwritten by a nested dialog box message.
        ///     </para>
        /// </returns>
        public delegate IntPtr DialogProc(IntPtr hwndDlg, WindowMessage uMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        ///     Creates a modeless dialog box from a dialog box template in memory. Before displaying the dialog box, the function
        ///     passes an application-defined value to the dialog box procedure as the lParam parameter of the
        ///     <see cref="WindowMessage.WM_INITDIALOG" /> message. An application can use this value to initialize dialog box
        ///     controls.
        /// </summary>
        /// <param name="hInstance">
        ///     A handle to the module which contains the dialog box template. If this parameter is
        ///     <see cref="SafeLibraryHandle.Null" />, then the current executable is used.
        /// </param>
        /// <param name="lpTemplate">
        ///     The template CreateDialogIndirectParam uses to create the dialog box. A dialog box template consists of a header
        ///     that describes the dialog box, followed by one or more additional blocks of data that describe each of the controls
        ///     in the dialog box. The template can use either the standard format or the extended format.
        ///     <para>
        ///         In a standard template, the header is a <see cref="DLGTEMPLATE" /> structure followed by additional
        ///         variable-length arrays. The data for each control consists of a <see cref="DLGITEMTEMPLATE" /> structure
        ///         followed by additional variable-length arrays.
        ///     </para>
        ///     <para>
        ///         In an extended dialog box template, the header uses the DLGTEMPLATEEX format and the control definitions use
        ///         the DLGITEMTEMPLATEEX format.
        ///     </para>
        ///     <para>
        ///         After CreateDialogIndirectParam returns, you can free the template, which is only used to get the dialog box
        ///         started.
        ///     </para>
        /// </param>
        /// <param name="hWndParent">A handle to the window that owns the dialog box.</param>
        /// <param name="lpDialogFunc">A pointer to the dialog box procedure.</param>
        /// <param name="lParamInit">
        ///     The value to pass to the dialog box in the lParam parameter of the
        ///     <see cref="WindowMessage.WM_INITDIALOG" /> message.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the window handle to the dialog box.
        ///     <para>
        ///         If the function fails, the return value is <see cref="IntPtr.Zero" />. To get extended error information,
        ///         call <see cref="GetLastError" />.
        ///     </para>
        /// </returns>
        [DllImport(nameof(User32), SetLastError = true)]
        public static extern IntPtr CreateDialogIndirectParam(
            SafeLibraryHandle hInstance,
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
        ///         call <see cref="GetLastError" />.
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
        ///     Posts a message to the message queue of the specified thread. It returns without waiting for the thread to process
        ///     the message.
        /// </summary>
        /// <param name="idThread">
        ///     The identifier of the thread to which the message is to be posted.
        ///     <para>
        ///         The function fails if the specified thread does not have a message queue. The system creates a thread's
        ///         message queue when the thread makes its first call to one of the User or GDI functions.
        ///     </para>
        ///     <para>
        ///         Message posting is subject to UIPI. The thread of a process can post messages only to posted-message queues
        ///         of threads in processes of lesser or equal integrity level.
        ///     </para>
        ///     <para>
        ///         This thread must have the SE_TCB_NAME privilege to post a message to a thread that belongs to a process with
        ///         the same locally unique identifier (LUID) but is in a different desktop. Otherwise, the function fails and
        ///         returns <see cref="Win32ErrorCode.ERROR_INVALID_THREAD_ID" />.
        ///     </para>
        ///     <para>
        ///         This thread must either belong to the same desktop as the calling thread or to a process with the same LUID.
        ///         Otherwise, the function fails and returns <see cref="Win32ErrorCode.ERROR_INVALID_THREAD_ID" />.
        ///     </para>
        /// </param>
        /// <param name="Msg">The type of message to be posted.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call
        ///         <see cref="GetLastError" />. GetLastError returns
        ///         <see cref="Win32ErrorCode.ERROR_INVALID_THREAD_ID" /> if idThread is not a valid thread identifier, or if the
        ///         thread specified by idThread does not have a message queue. GetLastError returns
        ///         <see cref="Win32ErrorCode.ERROR_NOT_ENOUGH_QUOTA" /> when the message limit is hit.
        ///     </para>
        /// </returns>
        [DllImport(nameof(User32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostThreadMessage(
            int idThread,
            WindowMessage Msg,
            IntPtr wParam,
            IntPtr lParam);

        public static readonly IntPtr HWND_BROADCAST = (IntPtr) 0xffff;

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     <para>
        ///         To post a message in the message queue associated with a thread, use the <see cref="PostThreadMessage" />
        ///         function.
        ///     </para>
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message. The following values have special
        ///     meanings.
        ///     <para>
        ///         <see cref="HWND_BROADCAST" />: The message is posted to all top-level windows in the system, including
        ///         disabled or invisible unowned windows, overlapped windows, and pop-up windows. The message is not posted to
        ///         child windows.
        ///     </para>
        ///     <para>
        ///         <see cref="IntPtr.Zero" />: The function behaves like a call to <see cref="PostThreadMessage" /> with the
        ///         dwThreadId parameter set to the identifier of the current thread.
        ///     </para>
        ///     <para>
        ///         Starting with Windows Vista, message posting is subject to UIPI. The thread of a process can post messages
        ///         only to message queues of threads in processes of lesser or equal integrity level.
        ///     </para>
        /// </param>
        /// <param name="Msg">The type of message to be posted.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     <para>
        ///         If the function fails, the return value is zero. To get extended error information, call
        ///         <see cref="GetLastError" />. GetLastError returns <see cref="Win32ErrorCode.ERROR_NOT_ENOUGH_QUOTA" />
        ///         when the limit is hit.
        ///     </para>
        /// </returns>
        [DllImport(nameof(User32), SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(
            IntPtr hWnd,
            WindowMessage Msg,
            IntPtr wParam,
            IntPtr lParam);

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

        /// <summary>
        ///     Determines whether a message is intended for the specified dialog box and, if it is, processes the message.
        /// </summary>
        /// <param name="hDlg">A handle to the dialog box.</param>
        /// <param name="lpMsg">A pointer to an <see cref="MSG" /> structure that contains the message to be checked.</param>
        /// <returns>
        ///     If the message has been processed, the return value is nonzero.
        ///     <para>If the message has not been processed, the return value is zero.</para>
        /// </returns>
        [DllImport(nameof(User32))]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsDialogMessage(IntPtr hDlg, MSG* lpMsg);
        
    }
}