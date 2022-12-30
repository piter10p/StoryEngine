using Microsoft.Win32.SafeHandles;
using StoryEngine.Core.Configuration;
using System.Runtime.InteropServices;

namespace StoryEngine.Core
{
    public class WindowsGameConsole : IGameConsole
    {
        private readonly EngineConfiguration _engineConfiguration;

        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;

        public WindowsGameConsole(EngineConfiguration engineConfiguration)
        {
            _engineConfiguration = engineConfiguration;
        }

        public void Initialize(int width, int height)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);

            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
        }

        public ConsoleKey[] GetInput()
        {
            var keys = new List<ConsoleKey>();

            while (Console.KeyAvailable)
            {
                keys.Add(Console.ReadKey(true).Key);
            }

            return keys.ToArray();
        }

        public void WriteToBuffer(char[] content)
        {
            var h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

            if (!h.IsInvalid)
            {
                CharInfo[] buf = new CharInfo[content.Length];
                SmallRect rect = new SmallRect()
                {
                    Left = 0,
                    Top = 0,
                    Right = Convert.ToInt16(_engineConfiguration.WindowSize.Width),
                    Bottom = Convert.ToInt16(_engineConfiguration.WindowSize.Height)
                };

                for(var i = 0; i < content.Length; i++)
                {
                    buf[i].Attributes = 7;
                    buf[i].Char.UnicodeChar = Convert.ToUInt16(content[i]);
                }

                WriteConsoleOutputW(
                    h,
                    buf,
                    new Coord()
                    {
                        X = Convert.ToInt16(_engineConfiguration.WindowSize.Width),
                        Y = Convert.ToInt16(_engineConfiguration.WindowSize.Height)
                    },
                    new Coord()
                    {
                        X = 0,
                        Y = 0
                    },
                    ref rect);
            }
        }

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutputW(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        private struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        private struct CharUnion
        {
            [FieldOffset(0)] public ushort UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }
    }
}
