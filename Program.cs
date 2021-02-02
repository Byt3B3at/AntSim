using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AntSim
{
    /// <summary>
    /// Klasse mit der Main()-Methode
    /// </summary>
    class Program
    {
        // copy paste: https://stackoverflow.com/questions/22053112/maximizing-console-window-c-sharp
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }

        static void Main()
        {
            Maximize();
            new World();

            Console.Write("Simulation ended.. Press the any-key to close the application...");
            Console.ReadKey();
        }
    }
}