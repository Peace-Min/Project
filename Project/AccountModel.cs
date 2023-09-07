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
        public static List<AccountModel> accounts = new List<AccountModel>();
        public const double rate = 1.04; 
        private string name;
        private int password;
        private int money;
        private static AccountModel currentAccount = null; //클래스,메서드,인터페이스,네임스페이스,상수=> 파스칼  변수,인수=>카멜
        public static AccountModel Current
        {
            get
            {
                if (currentAccount == null)
                {
                    currentAccount = new AccountModel();
                }
                return currentAccount;
            }
            set { currentAccount = value; }
        }
        public AccountModel() { }
        public AccountModel(string name, int password)
        {
            this.name = name;
            this.password = password;
            this.money= 0;
        }
        public int Money
        {
            get{    return money;}
            set {   money = value; }
        }
        public string Name
        {
            get {   return name; }
        }
       public int Password
       {
            get {   return password; }
       }
        
    }
}
