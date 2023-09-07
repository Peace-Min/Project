using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public static class AccountControllerService // 구체적 기능 메소드 구현
    {
        private const int LoginChance = 5;
        private static AccountModel tryLoginAccount = new AccountModel();
        public static int ReadNum()
        {
            string readNumber = Console.ReadLine();
            if (int.TryParse(readNumber, out int readNum))
                return readNum;
            else
            {
                Console.WriteLine("올바른 숫자 형식이 아닙니다.");
                return 0;
            }
        }
        public static string ReadName()
        {
            Console.WriteLine("고객님의 이름을 입력해주세요.");
            string readName = Console.ReadLine();
            return readName;
        }
        public static int VaildPassword()
        {
            string readPassword;
            int password;
            while (true)
            {
                readPassword = Console.ReadLine();
                if (readPassword.Length == 4 && int.TryParse(readPassword, out password))
                {
                    return password; // 4자리 숫자
                }
                else
                {
                    Console.WriteLine("고객님의 계좌의 비밀번호를 규칙을 지켜 다시 설정해주세요. !! [4자리 숫자] !!");
                }
            }
        }
        public static AccountModel VaildAccount(string name)
        {
            AccountModel account = AccountModel.accounts.Find(account => account.Name == name);
            return account;
        }
        public static AccountModel FindAccount(ref string name) //참조형 가독성? 유효하지않는 이름인 경우 name 변경
        {
            AccountModel account = null;
            while (true)
            {
                account = VaildAccount(name);
                if (account != null)
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
            //AccountModel currentAccount = AccountModel.Current;
            Console.WriteLine("\n[{0}]고객님 비밀번호를 입력해주세요.", tryLoginAccount.Name);
            int password = ReadNum();
            if (password == 0)
                return false;
            if (password == tryLoginAccount.Password)
            {
                return true;
            }
            Console.WriteLine("비밀번호가 일치하지 않습니다.");
            return false;
        }
        public static bool LoginPassword()
        {
            for (int i = 0; i < LoginChance; i++) //const LoginChance = 5
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
        public static bool AccountLoginOfCheck()
        {
            string name = ReadName();
            //AccountModel currentAccount = AccountModel.Current;
            tryLoginAccount = FindAccount(ref name);
            //currentAccount = FindAccount(ref name);

            if (LoginPassword())
            {

                AccountModel.Current = tryLoginAccount;
                return true;
            }
            return false;
        }
        public static void DepositFunc(string name, int money)
        {
            AccountModel currentAccount = AccountModel.Current;
            if (string.Equals(name, currentAccount.Name))
                currentAccount.Money += money;
            else
            {
                AccountModel receiveaccount = FindAccount(ref name); // 계좌 이체
                receiveaccount.Money += money;
            }
        }
        public static bool WithdrawFunc(int money)
        {
            AccountModel currentAccount = AccountModel.Current;
            try
            {
                int result = currentAccount.Money - money;
                if (result < 0)
                {
                    throw new Exception("\n계좌의 잔액이 부족합니다.\n");
                }
                currentAccount.Money = result;
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
