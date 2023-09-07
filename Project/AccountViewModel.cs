﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project
{

    public static class AccountViewModel // Account 인스턴스 처리
    {
        public static AccountModel currentaccount = null;
        private const int loginchance = 5;
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
            //FindAccont에서 Login Name에 대해 전체 List에서 FInd로 하나씩 훑어가면서 검색한 Account에 대해 매번 Find ??
            //하지만 여기서 Find로 찾은 Account를 계속 사용할 경우 그 다음부턴 코드가 서로 너무 돈독해짐 => Current Accunt 인스턴스 생성
        }

        public static void PrintAccount() //Case 3
        {
            AccountModel.accounts.ForEach(n => Console.WriteLine("\n{0}고객님\t잔액{1}\n", n.GetName(), n.GetMoney()));
        }
        public static bool LoginMenuList()
        {
            int list_num = int.Parse(Console.ReadLine());
            switch (list_num)
            {
                case 1:
                    Deposit();
                    return false;
                case 2:
                    Withdraw();
                    return false;
                case 3:
                    Console.WriteLine("[{0}]님의 현재 잔액은{1}원 입니다.", currentaccount.GetName(), currentaccount.GetMoney());
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
        public static string ReadName()
        {
            Console.WriteLine("고객님의 이름을 입력해주세요.");
            string name = Console.ReadLine();
            return name;
        }
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
        public static bool VaildAccount(ref AccountModel account, string name)
        {
            account = AccountModel.accounts.Find(account => account.name == name);

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
            currentaccount = FindAccount(ref name);
            if (LoginPassword(name))
            {
                return true;
            }
            return false;
        }
        public static bool PasswordCheck()
        {
            Console.WriteLine("\n[{0}]고객님 비밀번호를 입력해주세요.", currentaccount.GetName());
            int password = int.Parse(Console.ReadLine());
            if (password == currentaccount.GetPassword())
            {
                return true;
            }
            return false;
        }

        public static void Deposit()
        {
            Console.WriteLine("\n얼마를 입금하시겠습니까?\n");
            int money = int.Parse(Console.ReadLine());
            DepositFunc(currentaccount.GetName(), money);
        }
        public static void DepositFunc(string name, int money)
        {
            if (string.Equals(name, currentaccount.GetName())) //입금
                currentaccount.SetMoney(currentaccount.GetMoney() + money);
            else
            {
                AccountModel receiveaccount = FindAccount(ref name); // 계좌 이체
                receiveaccount.SetMoney(receiveaccount.GetMoney() + money);
            }
        }

        public static void Withdraw()
        {
            Console.WriteLine("\n얼마를 출금하시겠습니까?\n");
            int money = int.Parse(Console.ReadLine());
            if (WithdrawFunc(money))
            {
                Console.WriteLine("\n정상적으로 출금되었습니다.\n");
            }
        }
        public static bool WithdrawFunc(int money)
        {
            try
            {
                int result = currentaccount.GetMoney() - money;
                if (result < 0)
                {
                    throw new Exception("\n계좌의 잔액이 부족합니다.\n");
                }
                currentaccount.SetMoney(result);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static void Transfer()
        {
            Console.WriteLine("\n누구에게 이체하시겠습니까?\n");
            string r_name = Console.ReadLine();
            FindAccount(ref r_name);
            Console.WriteLine("\n얼마를 이체하시겠습니까?\n");
            int money = int.Parse(Console.ReadLine());

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
                Console.WriteLine("비밀번호가 일치하지 않습니다.");
                return;
            }
        }
        public static void YearLate()
        {
            double n_money = (double)currentaccount.GetMoney();
            Console.WriteLine("\n몇 년후의 복리가 궁금하십니까?\n");
            int year = int.Parse(Console.ReadLine());

            for (int i = 0; i < year; i++)
            {
                n_money *= AccountModel.rate;
            }
            Console.WriteLine("{0}년 후 잔액은\t{1:0.00}원 입니다.", year, n_money);
        }
    }
}

