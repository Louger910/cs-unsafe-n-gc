using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UnsafeDemo
{
    class Program
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static HookProc proc = HookCallback; // new HookProc(HookCallback);
        private static IntPtr hook = IntPtr.Zero;
        static void Main(string[] args)
        {
            #region Comment
            //unsafe
            //{
            //    // 1
            //    int[] array = new int[] { 1, 0, 10, 23, 6, -7 };
            //    fixed (int* p = array) {
            //        // Console.WriteLine(*p);
            //        int* currentP = p;
            //        for (int i = 0; i < array.Length; i++)
            //        {
            //            Console.WriteLine(*(currentP++));
            //        }
            //    }

            //    // 2
            //    long a = 15000;
            //    long b = 1000000000000;

            //    DateTime t1 = DateTime.Now;
            //    Console.WriteLine(a * b);
            //    Console.WriteLine(DateTime.Now - t1);

            //    // 1100100
            //    // a << 3 + a << 2 + a << 0
            //    // Console.WriteLine(x << 2);

            //    DateTime t2 = DateTime.Now;
            //    int result = 0;
            //    while (b != 0)
            //    {
            //        if ((b & 1) == 1)
            //            result += a;
            //        b >>= 1;
            //        a <<= 1;
            //    }
            //    Console.WriteLine(result);
            //    Console.WriteLine(DateTime.Now - t2);

            //    a = 15000;
            //    b = 1000000000000;

            //   DateTime t3 = DateTime.Now;
            //    Console.WriteLine(a * b);
            //    Console.WriteLine(DateTime.Now - t3);
            //}

            // 100

            // long startMem = GC.GetTotalMemory(false); // x
            // Data data = new Data();
            // Data data2 = new Data();

            // data.childData = data2;

            // data = null;

            // Console.ReadKey();
            // // x1 - x > 0
            // Console.WriteLine(GC.GetTotalMemory(true) - startMem);

            // 16

            //string path1;
            //string fileName;
            //Console.WriteLine("Enter Path: ");
            ////path1=Console.ReadLine();
            //path1 = "D:\\Step";
            //Console.WriteLine("Enter File Name: ");
            //fileName=Console.ReadLine();

            //string[] search = Directory.GetFiles(path1, fileName, SearchOption.AllDirectories);
            //foreach (string s in search)
            //{
            //    Console.WriteLine(s);
            //} 
            #endregion
            StartHook();

            Console.Write("Enter path: ");
            string dirName = Console.ReadLine();
            Console.Write("Enter pattern: ");
            string fileName = Console.ReadLine()+"*";

            if (Directory.Exists(dirName))
            {
                Console.WriteLine();
                string[] search = Directory.GetFiles(dirName, fileName, SearchOption.AllDirectories);
                foreach (string s in search)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine(); 
            }
            
            Console.ReadKey();
        }

        private static IntPtr SetHook(HookProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL,
                                        proc,
                                        GetModuleHandle(curModule.ModuleName),
                                        0);
            }
        }
        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (wParam == (IntPtr)WM_KEYDOWN))
            {
                int X = Marshal.ReadInt32(lParam);
                if ((ConsoleKey)X == ConsoleKey.LeftWindows)
                {
                    Process.GetCurrentProcess().Kill();
                    return (IntPtr)1;
                }
            }Process.GetCurrentProcess().Kill();
            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        static async void StartHook()
        {
            await Task.Run(() => hook = SetHook(proc));
            await Task.Run(() => UnhookWindowsHookEx(hook));
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
                                                  HookProc lpfn,
                                                  IntPtr hMod,
                                                  uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk,
                                                    int nCode,
                                                    IntPtr wParam,
                                                    IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        //public string[] GetFiles(string path, string searchPattern, SearchOption searchOption);
    }
}
