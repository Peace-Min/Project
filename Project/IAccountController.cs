using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public interface IAccountController // Account의 대표적 기능 추상화
    {
        void RegisterAccount();
        bool AccountLogin();
        void PrintAccountList();
        void AccountDeposit();
        void AccountWithdraw();
        void AccountInformation();
        void AccountOfInterest(); //n년 후 잔액 확인
        void AccountTransfer();
    }
}
