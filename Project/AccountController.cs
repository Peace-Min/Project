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
    public class AccountController : IAccountController // 인터페이스 구현
    {
        private const int LoginChance = 5; // 상수 => 파스칼
        private static AccountModel currentAccount = new AccountModel();
        private static AccountModel accountModel = new AccountModel();
        public void RegisterAccount() // Case 1
        {
            string name = AccountControllerService.ReadName();

            Console.WriteLine("고객님의 계좌의 비밀번호를 설정해주세요. [4자리 숫자]");
            int password = AccountControllerService.VaildPassword();

            AccountModel account = new AccountModel(name, password);
            account.Accounts.Add(account);

        }
        public bool AccountLogin() // Case 2
        {
            if (AccountControllerService.AccountLoginOfCheck())
            {
                currentAccount = currentAccount.Current;
                return true;
            }

            return false;
        }
        public void PrintAccountList() //Case 3
        {
            accountModel.Accounts.ForEach(account => Console.WriteLine("\n{0}고객님\t잔액{1}\n", account.Name, account.Money));
        }
        public void AccountDeposit() //Login_Case1
        {
            Console.WriteLine("\n얼마를 입금하시겠습니까?\n");
            int money = AccountControllerService.ReadNum();
            if (money == 0) //숫자입력 x 
                return;
            
            AccountControllerService.DepositFunc(currentAccount.Name, money, currentAccount);
        }
        public void AccountWithdraw() //Login_Case2
        {
            Console.WriteLine("\n얼마를 출금하시겠습니까?\n");
            int money = AccountControllerService.ReadNum();
            if (money == 0) // 숫자입력 x
                return;

            if (AccountControllerService.WithdrawFunc(money, currentAccount))
                Console.WriteLine("\n정상적으로 출금되었습니다.\n");
        }
        public void AccountInformation()
        {
            Console.WriteLine("[{0}]님의 현재 잔액은{1}원 입니다.", currentAccount.Name, currentAccount.Money);
        }
        public void AccountOfInterest() //Login_Case4
        {
            double futureMoney = (double)currentAccount.Money;
            Console.WriteLine("\n몇 년후의 복리가 궁금하십니까?\n");
            int readYear = AccountControllerService.ReadNum();

            if (readYear <= 0)
                return;

            for (int i = 0; i < readYear; i++)
                futureMoney *= currentAccount.rate;

            Console.WriteLine("{0}년 후 잔액은\t{1:0.00}원 입니다.", readYear, futureMoney);
        }
        public void AccountTransfer() //Login_Case5
        {
            Console.WriteLine("\n누구에게 이체하시겠습니까?\n");
            string readName = Console.ReadLine();
            AccountControllerService.FindAccount(ref readName);
            Console.WriteLine("\n얼마를 이체하시겠습니까?\n");
            int money = AccountControllerService.ReadNum();
            if (money == 0)
                return;

            if (AccountControllerService.PasswordCheck())
            {
                if (!AccountControllerService.WithdrawFunc(money,currentAccount))
                    return;
                AccountControllerService.DepositFunc(readName, money,currentAccount);
                Console.WriteLine("\n성공적으로 이체 완료했습니다.\n");
            }
        }
    }
}
