using System.Runtime.InteropServices;

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
}