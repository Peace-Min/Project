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
        private static AccountController accountController = new AccountController(); //인터페이스로 구현해서 static 메소드 X
        public static void Main(string[] args)
        {
            Console.WriteLine("\t안녕하세요 Net-Bank입니다.\t\n");
            while (true)
            {
                InitialPrint();
            }
        }
        public static void InitialPrint()
        {
            Console.WriteLine("\n1.계좌 생성\t2.계좌 로그인\t3.계좌 목록\t4.종료");
            accountController.InitialList();
        }
        public static void LoginMenu()
        {
            while (true)
            {
                Console.WriteLine("\n1.입금\t2.출금\t3.잔액 확인\t4.복리 확인\t5.계좌 이체\t6.로그아웃\n");
                if (accountController.LoginMenuList())
                    break;
            }
        }
    }
}

