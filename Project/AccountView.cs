using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public static class AccountView
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("\t안녕하세요 Net-Bank입니다.\t\n");
            while (true)
            {
                MenuScreenUI.InitialMenuScreen();
            }
        }
    }
}

