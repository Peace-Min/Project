using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Function
    {
        private static List<Account> accounts = new List<Account>();
        
        public static void Func()
        {
            Print();
            String r_num = Console.ReadLine();
            int.TryParse(r_num, out int num);

            active(num);
        }
        public static void Print()
        {
            Console.WriteLine("\n1.계좌 생성\t2.계좌 로그인\t3.계좌 목록\t4.종료");
        }
        public static void Print_Account()
        {
            /*foreach(Account account in accounts)
            {
                Console.WriteLine("\n{0}고객님\t잔액{1}\n", account.GetName(), account.GetMoney());
            }*/
            accounts.ForEach( n => Console.WriteLine("\n{0}고객님\t잔액{1}\n", n.GetName(), n.GetMoney()));
        }
        public static void active(int num)
        {
            Account account = new Account();
            switch (num)
            {
                case 1:
                    accounts.Add(account.join());   break;
                case 2:
                    accounts=account.login(accounts);    break;
                case 3:
                    Print_Account();    break;
                case 4:
                    Environment.Exit(0);    break;
            }
        }
    }
}
