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
    public class Account // Model
    {
        public static List<Account> accounts = new List<Account>();
        public static readonly double rate = 1.04; //c#에서 const대신 씀 const는 버전 관리 문제 발생?
        public string name;
        private int password;
        private int money;
        public Account() { }
        public Account(string name, int password)
        {
            this.name = name;
            this.password = password;
            this.money= 0;
        }
        public int GetMoney()
        {
            return this.money;
        }
        public string GetName()
        {
            return this.name;
        }
        public void SetMoney(int money)
        {
            this.money = money;
        }
        public int GetPassword()
        {
            return this.password;
        }
        
    }
}
