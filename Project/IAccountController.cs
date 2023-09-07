using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public interface IAccountController 
    {
        void RegisterAccount();
        void AccountLogin();
        void PrintAccount();
        void Deposit();
        void Withdraw();
        void YearLate();
        void Transfer();
    }
}
