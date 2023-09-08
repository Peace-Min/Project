using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project
{
    public class AccountModel // Model 
    {
        private static List<AccountModel> accounts = new List<AccountModel>();
        private double rate { get { return rate; } set { rate = 1.04; } } // 이율 
        public string name { get { return name; } set { name = value; } }
        public int password { get { return password; } set { password = value; } }
        public int money;
        public static AccountModel currentAccount { get { return currentAccount; } set { currentAccount= value; } } 
        //클래스,메서드,인터페이스,네임스페이스,상수=> 파스칼  변수,인수=>카멜


        public List<AccountModel> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }
        public AccountModel() { }
        public AccountModel(string name, int password)
        {
            this.name = name;
            this.password = password;
            this.money = 0;
            Console.WriteLine(this.name + this.password);
            currentAccount = this;
        }
      
    }
}
