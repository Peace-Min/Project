using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project
{

    public class AccountViewModel
    {
        private static AccountModel currentaccount = null;
        public static AccountModel Current
        {
            set { currentaccount = value; }
        }
        private const int loginchance = 5;
        public static void InitialList()
        {
            int num = ReadNum();
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
            string name = ReadName();

            Console.WriteLine("고객님의 계좌의 비밀번호를 설정해주세요. [4자리 숫자]");
            int password = VaildPassword();

            AccountModel account = new AccountModel(name, password);
            AccountModel.accounts.Add(account);
        }

        public static void Login() // Case 2
        {
            if (AccountLogin())
            {
                AccountView.LoginMenu();
            }
            return;
        }

        public static void PrintAccount() //Case 3
        {
            AccountModel.accounts.ForEach(account => Console.WriteLine("\n{0}고객님\t잔액{1}\n", account.Name, account.Money));
        }
        public static bool LoginMenuList()
        {
            int listnum = ReadNum();
            switch (listnum)
            {
                case 1:
                    Deposit();
                    return false;
                case 2:
                    Withdraw();
                    return false;
                case 3:
                    Console.WriteLine("[{0}]님의 현재 잔액은{1}원 입니다.", currentaccount.Name, currentaccount.Money);
                    return false;
                case 4:
                    YearLate();
                    return false;
                case 5:
                    Transfer();
                    return false;
                case 6:
                    return true;
                default:
                    Console.WriteLine("\n잘못된 입력입니다. 숫자 1~6까지 입력해주세요\n");
                    return false;
            }
        }
        public static void Deposit() //Login_Case1
        {
            Console.WriteLine("\n얼마를 입금하시겠습니까?\n");
            int money = ReadNum();
            if (money == 0) //숫자입력 x 
                return;
            DepositFunc(currentaccount.Name, money);
        }
        public static void Withdraw() //Login_Case2
        {
            Console.WriteLine("\n얼마를 출금하시겠습니까?\n");
            int money = ReadNum();
            if (money == 0) // 숫자입력 x
                return;
            if (WithdrawFunc(money))
            {
                Console.WriteLine("\n정상적으로 출금되었습니다.\n");
            }
        }
        public static void YearLate() //Login_Case4
        {
            double n_money = (double)currentaccount.Money;
            Console.WriteLine("\n몇 년후의 복리가 궁금하십니까?\n");
            int year = ReadNum();
            if (year <= 0)
                return;
            for (int i = 0; i < year; i++)
            {
                n_money *= AccountModel.rate;
            }
            Console.WriteLine("{0}년 후 잔액은\t{1:0.00}원 입니다.", year, n_money);
        }
        public static void Transfer() //Login_Case5
        {
            Console.WriteLine("\n누구에게 이체하시겠습니까?\n");
            string r_name = Console.ReadLine();
            FindAccount(ref r_name);
            Console.WriteLine("\n얼마를 이체하시겠습니까?\n");
            int money = ReadNum();
            if (money == 0)
                return;
            if (PasswordCheck())
            {
                if (WithdrawFunc(money))
                    ;
                else
                    return;
                DepositFunc(r_name, money);
                Console.WriteLine("\n성공적으로 이체 완료했습니다.\n");
                return;
            }
            else
            {
                return;
            }
        }
        public static int ReadNum()
        {
            try
            {
                int num = int.Parse(Console.ReadLine());
                return num;
            }
            catch (FormatException)
            {
                Console.WriteLine("올바른 숫자 형식이 아닙니다.");
                return 0;
            }
        }
        public static string ReadName()
        {
            Console.WriteLine("고객님의 이름을 입력해주세요.");
            string name = Console.ReadLine();
            return name;
        }
        public static int VaildPassword()
        {
            string r_password;
            int password;
            while (true)
            {
                r_password = Console.ReadLine();
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
        public static bool VaildAccount(ref AccountModel account, string name)
        {
            account = AccountModel.accounts.Find(account => account.Name == name);

            if (account == null)
                return false;
            else
                return true;
        }
        public static AccountModel FindAccount(ref string name)
        {
            AccountModel account = null;
            while (true)
            {
                if (VaildAccount(ref account, name))
                {
                    return account;
                }
                else
                {
                    Console.WriteLine("존재하지않는 Account입니다.\n다시 한번 이름을 입력해주세요.\n");
                    name = Console.ReadLine();
                }
            }
        }
        public static bool PasswordCheck()
        {
            Console.WriteLine("\n[{0}]고객님 비밀번호를 입력해주세요.", currentaccount.Name);
            int password = ReadNum();
            if (password == 0)
                return false;
            if (password == currentaccount.Password)
            {
                return true;
            }
            Console.WriteLine("비밀번호가 일치하지 않습니다.");
            return false;
        }
        public static bool LoginPassword(string name)
        {
            for (int i = 0; i < loginchance; i++) //const loginchance = 5
            {
                if (PasswordCheck())
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
        public static bool AccountLogin()
        {
            string name = ReadName();

            if (currentaccount == null) //singleton
                currentaccount = FindAccount(ref name);
            else
            {
                Console.WriteLine("\nAccount isn't current\n");
                return false;
            }
            if (LoginPassword(name))
                return true;

            return false;
        }
        public static void DepositFunc(string name, int money)
        {
            if (string.Equals(name, currentaccount.Name)) //입금
                currentaccount.Money += money;
            else
            {
                AccountModel receiveaccount = FindAccount(ref name); // 계좌 이체
                receiveaccount.Money += money;
            }
        }
        public static bool WithdrawFunc(int money)
        {
            try
            {
                int result = currentaccount.Money - money;
                if (result < 0)
                {
                    throw new Exception("\n계좌의 잔액이 부족합니다.\n");
                }
                currentaccount.Money = result;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}