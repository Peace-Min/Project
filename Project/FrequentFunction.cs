using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class FrequentFunction
    {
        public static int VaildPassword()
        {
            string r_password = Console.ReadLine();
            int password;
            while (true)
            {
                if (r_password.Length == 4 && int.TryParse(r_password, out password))
                {
                    return password; // 4자리 숫자
                }
                else
                {
                    Console.WriteLine("고객님의 계좌의 비밀번호를 규칙을 지켜 다시 설정해주세요. !! [4자리 숫자] !!");
                }
            }
        }
        public static string FindName(string name)
        {
            Account account = FindAccount(name);
            if (account == null)
            {
                Console.WriteLine("\n존재하지않는 고객님\n");
                ViewOption.Print();
            }
            return name;
        }
        public static Account FindAccount(string name)
        {
            Account account = Account.accounts.Find(s => s.name == name);
            while (true)
            {
                if (account != null)
                {
                    return account;
                }
                Console.WriteLine("존재하지않는 Account입니다.\n다시 한번 이름을 입력해주세요.\n");
                string r_name = Console.ReadLine();
                account = Account.accounts.Find(s => s.name == r_name);
            }
        }
        public static bool LoginPassword(string name)
        {
            for (int i = 0; i < 5; i++)
            {
                if (PasswordCheck(name))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("비밀번호가 다릅니다. {0}회 이상 틀림", i + 1);
                }
            }
            Console.WriteLine("\n5회 이상 비밀번호를 틀리셨습니다.\n");
            return false;
        }
        public static bool PasswordCheck(string name)
        {
            Console.WriteLine("\n[{0}]고객님 비밀번호를 입력해주세요.", name);
            int password = int.Parse(Console.ReadLine());
            if (password == FindAccount(name).GetPassword())
            {
                return true;
            }
            return false;
        }
    }
}
