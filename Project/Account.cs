using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Account
    {
        public static readonly double rate = 1.04; //c#에서 const대신 씀 const는 버전 관리 문제 발생?
        public string Name { get; set; }
        public int Password { get; set; }
        public int Money { get; set; }
        public Account() { }
        public Account(string name, int password,int money=0)
        {
            Name = name;
            Password = password;
            Money = money;
        }

        public Account join()
        {
            Console.WriteLine("고객님의 이름을 입력해주세요.");
            string Name=Console.ReadLine();
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
            Account person_account=new Account(Name, Password);
            return person_account;
        }
        public void login(List<Account> accounts)
        {   
            Console.WriteLine("\n고객님의 이름을 입력해주세요.");
            string Name = Console.ReadLine();
            foreach (Account account in accounts)
            {
                if (account.Name == Name)
                {
                    Console.WriteLine("\n[{0}]고객님 비밀번호를 입력해주세요.",account.Name);
                    for(int i=0;i<5;i++)
                    {
                        string r_password = Console.ReadLine();
                        int.TryParse(r_password, out int password);

                        if (password == account.Password)
                        {
                            login_menu(account);
                            return ;
                        }
                        else
                        {
                            Console.WriteLine("비밀번호가 다릅니다. {0}회 이상 틀림", i + 1);
                        }
                    }
                        Console.WriteLine("\n5회 이상 비밀번호를 틀리셨습니다.\n");
                        Environment.Exit(0);
                }
            }
            Console.WriteLine("\n등록된 이름이 존재하지 않습니다.\n");
        }
        public void login_menu(Account account)
        {
            while (true)
            {
                Console.WriteLine("\n1.입금\t2.출금\t3.잔액 확인\t4.복리 확인\t5.로그아웃\n");
                string r_list = Console.ReadLine();
                int.TryParse(r_list, out int list_num);
                switch (list_num)
                {
                    case 1: deposit(account); break;
                    case 2: withdraw(account); break;
                    case 3: Console.WriteLine("[{0}]님의 현재 잔액은{1}원 입니다.",account.Name,account.Money); break;
                    case 4: year_late(account); break;
                    case 5: return;
                }
            }
        }
        public void deposit(Account account)
        {
            int money;
            Console.WriteLine("\n얼마를 입금하시겠습니까?\n");
            string r_money = Console.ReadLine();
            int.TryParse(r_money, out money) ;
            account.Money += money;
        }
        public void withdraw(Account account) //예외처리 사용예정
        {
            Console.WriteLine("얼마를 출금하시겠습니까? ");
            string r_money = Console.ReadLine();
            int.TryParse(r_money, out int money);
            account.Money -= money;
        }
        public void year_late(Account account)
        {
            double n_money=account.Money;
            Console.WriteLine("\n몇 년후의 복리가 궁금하십니까?\n");
            string r_year = Console.ReadLine();
            int.TryParse(r_year, out int year);
            for(int i= 0; i < year; i++)
            {
                n_money *= rate;
            }
            Console.WriteLine("{0}년 후 잔액은\t{1:0.00}원 입니다.",year,n_money);
        }
    }
}
