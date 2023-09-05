using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project
{
    public class Account
    {
        public static readonly double rate = 1.04; //c#에서 const대신 씀 const는 버전 관리 문제 발생?
        public string Name { get; set; }
        public int Password { get; set; }
        public int Money { get; set; }
        public Account() { }
        public Account(string name, int password, int money = 0)
        {
            Name = name;
            Password = password;
            Money = money;
        }

        public Account join()
        {
            Console.WriteLine("고객님의 이름을 입력해주세요.");
            string Name = Console.ReadLine();
            int Password;
            Console.WriteLine("고객님의 계좌의 비밀번호를 설정해주세요. [4자리 숫자]");
            while (true)
            {
                string r_Password = Console.ReadLine();
                if (r_Password.Length == 4 && int.TryParse(r_Password, out Password))
                {
                    break; // 4자리 숫자
                }
                else
                {
                    Console.WriteLine("고객님의 계좌의 비밀번호를 규칙을 지켜 다시 설정해주세요. !! [4자리 숫자] !!");
                }
            }
            Account person_account = new Account(Name, Password);
            return person_account;
        }
        public List<Account> login(List<Account> accounts)
        {
            Console.WriteLine("\n고객님의 이름을 입력해주세요.");
            string Name = Console.ReadLine();

            var passive = Find_account(accounts, Name);
            if (passive == null) return accounts;

            Console.WriteLine("\n[{0}]고객님 비밀번호를 입력해주세요.", passive.Name);
            for (int i = 0; i < 5; i++)
            {
                int password = int.Parse(Console.ReadLine());
                if (password == passive.Password)
                {
                    login_menu(passive, accounts);
                    return accounts;
                }
                else
                {
                    Console.WriteLine("비밀번호가 다릅니다. {0}회 이상 틀림", i + 1);
                }
            }
            Console.WriteLine("\n5회 이상 비밀번호를 틀리셨습니다.\n");
          
            return accounts;
        }
        public Account Find_account(List<Account> accounts, string name)
        {
            var passive = accounts.Find(s => s.Name == name); //List + 람다식 + Find메서드 사용
            if (passive == null)
            {
                Console.WriteLine("\n존재하지않는 회원입니다.\n");
                return null;
            }
            return passive;
        }
        public void login_menu(Account account, List<Account> accounts)
        {
            while (true)
            {
                Console.WriteLine("\n1.입금\t2.출금\t3.잔액 확인\t4.복리 확인\t5.계좌 이체\t6.로그아웃\n");
                int list_num = int.Parse(Console.ReadLine());
                switch (list_num)
                {
                    case 1: deposit(account); break;
                    case 2: withdraw(account); break;
                    case 3: Console.WriteLine("[{0}]님의 현재 잔액은{1}원 입니다.", account.Name, account.Money); break;
                    case 4: year_late(account); break;
                    case 5: transfer(account, accounts); break;
                    case 6: return;
                }
            }
        }
        public void deposit(Account account)
        {
            Console.WriteLine("\n얼마를 입금하시겠습니까?\n");
            int money = int.Parse(Console.ReadLine());
            deposit_func(account, money);
        }
        public void deposit_func(Account account, int money)
        {
            account.Money += money;
        }

        public void withdraw(Account account) //예외처리 사용예정
        {
            Console.WriteLine("\n얼마를 출금하시겠습니까?\n");
            int money = int.Parse(Console.ReadLine());
            if (withdraw_func(account, money))
            {
                Console.WriteLine("\n정상적으로 출금되었습니다.\n");
            }
        }
        public bool withdraw_func(Account account, int money)
        {
            try
            {
                int result = account.Money - money;
                if (result < 0)
                {
                    throw new Exception("\n계좌의 잔액이 부족합니다.\n");
                }
                account.Money -= money;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public void transfer(Account account, List<Account> accounts)
        {
            Console.WriteLine("\n누구에게 이체하시겠습니까?\n");
            string name = Console.ReadLine();

            var passive = Find_account(accounts, name);
            if (passive == null) return ;
            Console.WriteLine("\n얼마를 이체하시겠습니까?\n");
            int money = int.Parse(Console.ReadLine());

            Console.WriteLine("\n비밀번호를 입력해주세요.\n");
            int password = int.Parse(Console.ReadLine());

            if (password == account.Password)
            {
                if (withdraw_func(account, money)) Console.WriteLine("\n성공적으로 이체 완료했습니다.\n");
                else return;
                deposit_func(passive, money);
            }
            else
            {
                Console.WriteLine("비밀번호가 일치하지 않습니다.");
                return;
            }
        }
        public void year_late(Account account)
        {
            double n_money = account.Money;
            Console.WriteLine("\n몇 년후의 복리가 궁금하십니까?\n");
            int year = int.Parse(Console.ReadLine());

            for (int i = 0; i < year; i++)
            {
                n_money *= rate;
            }
            Console.WriteLine("{0}년 후 잔액은\t{1:0.00}원 입니다.", year, n_money);
        }
    }
}
