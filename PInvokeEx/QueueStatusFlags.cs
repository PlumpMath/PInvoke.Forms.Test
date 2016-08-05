using System;

namespace ConsoleApplication1
{
    /// <summary>
    /// Flags to be passed to the <code>flags</code> parameter of <see cref="User32Ex.GetQueueStatus"/>
    /// </summary>
    [Flags]
    internal enum QueueStatusFlags
    {
        /// <summary>
        ///     A <see cref="WindowMessage.WM_KEYUP" />, <see cref="WindowMessage.WM_KEYDOWN" />,
        ///     <see cref="WindowMessage.WM_SYSKEYUP" />, or <see cref="WindowMessage.WM_SYSKEYDOWN" /> message is in the queue.
        /// </summary>
        QS_KEY = 0x0001,

        /// <summary>
        ///     A <see cref="WindowMessage.WM_MOUSEMOVE" /> message is in the queue.
        /// </summary>
        QS_MOUSEMOVE = 0x0002,

        /// <summary>
        ///     A mouse-button message (<see cref="WindowMessage.WM_LBUTTONUP" />, <see cref="WindowMessage.WM_RBUTTONDOWN" />, and
        ///     so on).
        /// </summary>
        QS_MOUSEBUTTON = 0x0004,

        /// <summary>
        ///     A posted message (other than those listed here) is in the queue.
        /// </summary>
        QS_POSTMESSAGE = 0x0008,

        /// <summary>
        ///     A <see cref="WindowMessage.WM_TIMER" /> message is in the queue.
        /// </summary>
        QS_TIMER = 0x0010,

        /// <summary>
        ///     A <see cref="WindowMessage.WM_PAINT" /> message is in the queue.
        /// </summary>
        QS_PAINT = 0x0020,

        /// <summary>
        ///     A message sent by another thread or application is in the queue.
        /// </summary>
        QS_SENDMESSAGE = 0x0040,

        /// <summary>
        ///     A <see cref="WindowMessage.WM_HOTKEY" /> message is in the queue.
        /// </summary>
        QS_HOTKEY = 0x0080,

        /// <summary>
        ///     A posted message (other than those listed here) is in the queue.
        /// </summary>
        QS_ALLPOSTMESSAGE = 0x0100,

        /// <summary>
        ///     A raw input message is in the queue.
        /// </summary>
        QS_RAWINPUT = 0x0400,

        QS_TOUCH = 0x0800,

        QS_POINTER = 0x1000,

        /// <summary>
        ///     A <see cref="WindowMessage.WM_MOUSEMOVE" /> message or mouse-button message (
        ///     <see cref="WindowMessage.WM_LBUTTONUP" />, <see cref="WindowMessage.WM_RBUTTONDOWN" />, and so on).
        /// </summary>
        QS_MOUSE = QS_MOUSEMOVE |
                   QS_MOUSEBUTTON,

        /// <summary>
        ///     An input message is in the queue.
        /// </summary>
        QS_INPUT = QS_MOUSE |
                   QS_KEY |
                   QS_RAWINPUT |
                   QS_TOUCH |
                   QS_POINTER,

        /// <summary>
        ///     An input, <see cref="WindowMessage.WM_TIMER" />, <see cref="WindowMessage.WM_PAINT" />,
        ///     <see cref="WindowMessage.WM_HOTKEY" />, or posted message is in the queue.
        /// </summary>
        QS_ALLEVENTS = QS_INPUT |
                       QS_POSTMESSAGE |
                       QS_TIMER |
                       QS_PAINT |
                       QS_HOTKEY,

        /// <summary>
        ///     Any message is in the queue.
        /// </summary>
        QS_ALLINPUT = QS_INPUT |
                      QS_POSTMESSAGE |
                      QS_TIMER |
                      QS_PAINT |
                      QS_HOTKEY |
                      QS_SENDMESSAGE
    }
}