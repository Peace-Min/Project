using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class Login
    {
        public static bool LoginMenuList(string name)
        {
            int list_num = int.Parse(Console.ReadLine());
            switch (list_num)
            {
                case 1:
                    LoginFunction.Deposit(name);
                    return false;
                case 2:
                    LoginFunction.Withdraw(name);
                    return false;
                case 3:
                    Account account = FrequentFunction.FindAccount(name);
                    Console.WriteLine("[{0}]님의 현재 잔액은{1}원 입니다.", account.GetName(), account.GetMoney());
                    return false;
                case 4:
                    LoginFunction.YearLate(name);
                    return false;
                case 5:
                    LoginFunction.Transfer(name);
                    return false;
                case 6:
                    return true;
                default:
                    Console.WriteLine("\n잘못된 입력입니다. 숫자 1~6까지 입력해주세요\n");
                    return false;
            }
        }
        
    }
}
