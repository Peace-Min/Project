using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class ViewOption
    {
        public static void Print()
        {
            Console.WriteLine("\n1.계좌 생성\t2.계좌 로그인\t3.계좌 목록\t4.종료");
            Initial.InitialList();
        }
        public static void LoginMenu(string name)
        {
            while (true)
            {
                Console.WriteLine("\n1.입금\t2.출금\t3.잔액 확인\t4.복리 확인\t5.계좌 이체\t6.로그아웃\n");
                if (Login.LoginMenuList(name)) 
                    break;
            }
        }
    }
}
