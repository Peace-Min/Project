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
        public static readonly double rate = 1.04; //c#에서 const대신 씀 const는 버전 관리 문제 발생?
        private string name;
        private int password;
        private int money;
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
