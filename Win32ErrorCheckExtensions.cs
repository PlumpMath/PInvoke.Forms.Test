using System;
using PInvoke;

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
}