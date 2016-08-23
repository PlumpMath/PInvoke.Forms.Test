using System;
using PInvoke;
using static ConsoleApplication1.User32Ex;
using static ConsoleApplication1.WindowMessage;
using static PInvoke.Kernel32;
using static PInvoke.User32;

namespace ConsoleApplication1
{
    internal unsafe class Program
    {
        private static DialogProc LpDlgProc = DlgProc;

        /// <summary>
        /// From Raymond Chen blog : https://blogs.msdn.microsoft.com/oldnewthing/20050329-00/?p=36043
        /// </summary>
        private static IntPtr CreateDialogParam(
            SafeLibraryHandle hinst,
            int iTemplate,
            IntPtr hwndParent,
            DialogProc lpDlgProc,
            IntPtr dwInitParam)
        {
            var hrsrc = FindResource(hinst, MAKEINTRESOURCE(iTemplate), RT_DIALOG).Win32ThrowIfZero();
            var hglob = LoadResource(hinst, hrsrc).Win32ThrowIfZero();
            var pTemplate = LockResource(hglob);
            if (pTemplate == null)
            {
                throw new Win32Exception();
            }

            return CreateDialogIndirectParam(hinst,
                (DLGTEMPLATE*)pTemplate, hwndParent, lpDlgProc,
                dwInitParam);
        }

        private static void Main()
        {
            var window = CreateAndShowDialog();
            MessagePump(window);
        }

        private static IntPtr CreateAndShowDialog()
        {
            var window = CreateDialogParam(SafeLibraryHandle.Null, 101, IntPtr.Zero, LpDlgProc, IntPtr.Zero).Win32ThrowIfZero();
            ShowWindow(window, WindowShowStyle.SW_SHOWDEFAULT);
            return window;
        }

        private static void MessagePump(IntPtr window)
        {
            MSG msg;
            int bRet;
            while ((bRet = GetMessage(&msg, IntPtr.Zero, 0, 0)) != 0)
            {
                if (bRet == -1)
                {
                    throw new Win32Exception();
                }

                if (!IsDialogMessage(window, &msg))
                {
                    TranslateMessage(&msg);
                    DispatchMessage(&msg);
                }
            }
        }

        static readonly IntPtr TRUE = (IntPtr)1;

        private static IntPtr DlgProc(IntPtr hwndDlg, WindowMessage uMsg, IntPtr wParam, IntPtr lParam)
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