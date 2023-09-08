using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public static class MenuScreenUI //Interface ScreenUI ?? => 클래스 갯수 많아짐 객체 선언도 해야함
    {
        private static AccountController accountController = new AccountController();
        public static void InitialMenuScreen()
        {
            Console.WriteLine("\n1.계좌 생성\t2.계좌 로그인\t3.계좌 목록\t4.종료");

            int listOfNum = AccountControllerService.ReadNum();
            switch (listOfNum)
            {
                case 1:
                    accountController.RegisterAccount();
                    break;
                case 2:
                    if(accountController.AccountLogin())
                        LoginMenuScreen();
                    break;
                case 3:
                    accountController.PrintAccountList();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 숫자 1~4만 입력해주세요.\n");
                    break;
            }
        }

        public static void LoginMenuScreen()
        {
            while (true)
            {
                Console.WriteLine("\n1.입금\t2.출금\t3.잔액 확인\t4.복리 확인\t5.계좌 이체\t6.로그아웃\n");
                int listOfNum = AccountControllerService.ReadNum();
                switch (listOfNum)
                {
                    case 1:
                        accountController.AccountDeposit();
                        break;
                    case 2:
                        accountController.AccountWithdraw();
                        break;
                    case 3:
                        accountController.AccountInformation();
                        break;
                    case 4:
                        accountController.AccountOfInterest();
                        break;
                    case 5:
                        accountController.AccountTransfer();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다. 숫자 1~6까지 입력해주세요\n");
                        break;
                }
            }
        }
    }
}
