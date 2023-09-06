using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class LoginFunction
    {
        public static void Deposit(string name)
        {
            Console.WriteLine("\n얼마를 입금하시겠습니까?\n");
            int money = int.Parse(Console.ReadLine());
            DepositFunc(name, money);
        }
        public static void DepositFunc(string name, int money)
        {
            Account account = FrequentFunction.FindAccount(name);
            account.SetMoney(account.GetMoney() + money);
        }

        public static void Withdraw(string name)
        {
            Console.WriteLine("\n얼마를 출금하시겠습니까?\n");
            int money = int.Parse(Console.ReadLine());
            if (WithdrawFunc(name, money))
            {
                Console.WriteLine("\n정상적으로 출금되었습니다.\n");
            }
        }
        public static bool WithdrawFunc(string name, int money)
        {
            Account account = FrequentFunction.FindAccount(name);
            try
            {
                int result = account.GetMoney() - money;
                if (result < 0)
                {
                    throw new Exception("\n계좌의 잔액이 부족합니다.\n");
                }
                account.SetMoney(result);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static void Transfer(string name)
        {
            Console.WriteLine("\n누구에게 이체하시겠습니까?\n");
            string r_name = Console.ReadLine();
            Account account = FrequentFunction.FindAccount(r_name);
            Console.WriteLine("\n얼마를 이체하시겠습니까?\n");
            int money = int.Parse(Console.ReadLine());

            if (FrequentFunction.PasswordCheck(name))
            {
                if (WithdrawFunc(name, money)) Console.WriteLine("\n성공적으로 이체 완료했습니다.\n");
                else return;
                DepositFunc(r_name, money);
            }
            else
            {
                Console.WriteLine("비밀번호가 일치하지 않습니다.");
                return;
            }
        }
        public static void YearLate(string name)
        {
            double n_money = (double)FrequentFunction.FindAccount(name).GetMoney();
            Console.WriteLine("\n몇 년후의 복리가 궁금하십니까?\n");
            int year = int.Parse(Console.ReadLine());

            for (int i = 0; i < year; i++)
            {
                n_money *= Account.rate;
            }
            Console.WriteLine("{0}년 후 잔액은\t{1:0.00}원 입니다.", year, n_money);
        }
    }
}
