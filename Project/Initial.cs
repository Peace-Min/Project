using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project
{
    public class Initial
    {
        public static void InitialList()
        {
            int num = int.Parse(Console.ReadLine());
            switch (num)
            {
                case 1:
                    Join();
                    break;
                case 2:
                    Login();
                    break;
                case 3:
                    PrintAccount();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 숫자 1~4만 입력해주세요.\n");
                    break;
            }
        }

        public static void Join() // Case 1
        {
            Console.WriteLine("고객님의 이름을 입력해주세요.");
            string name = Console.ReadLine();

            Console.WriteLine("고객님의 계좌의 비밀번호를 설정해주세요. [4자리 숫자]");
            int password = FrequentFunction.VaildPassword();

            Account account = new Account(name, password);
            Account.accounts.Add(account);
        }

        public static void Login() // Case 2
        {
            Console.WriteLine("\n고객님의 이름을 입력해주세요.");
            string name = Console.ReadLine();
            FrequentFunction.FindName(name);
            if (FrequentFunction.LoginPassword(name))
            {
                ViewOption.LoginMenu(name);
            }
            return; 
            //FindAccont에서 Login Name에 대해 전체 List에서 FInd로 하나씩 훑어가면서 검색한 Account에 대해 매번 Find ??
            //하지만 여기서 Find로 찾은 Account를 계속 사용할 경우 그 다음부턴 코드가 서로 너무 돈독해짐
            //왜냐면 Login 이후로부터 계속 이렇게 account를 넘기다가 추후에 Login 자체에서 수정 사항이 있으면
            //수정할 때도 Account를 무조건 넘길 수 있도록 수정해야함...
            //name을 넘길지 Account를 넘길지 자원을 소중히 여기면 account 개발 편의성 name??
            }

        public static void PrintAccount() //Case 3
        {
            Account.accounts.ForEach(n => Console.WriteLine("\n{0}고객님\t잔액{1}\n", n.GetName(), n.GetMoney()));
        }

    }
}
