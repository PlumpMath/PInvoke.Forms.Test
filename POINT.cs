using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}