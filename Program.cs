using System;
using PInvoke;
using static ConsoleApplication1.User32Ex;
using static ConsoleApplication1.WindowMessage;
using static PInvoke.Kernel32;
using static PInvoke.User32;

namespace ConsoleApplication1
{
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