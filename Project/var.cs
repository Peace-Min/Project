using System;
using System.Collections.Generic;

namespace Project
{
    public class var<T>
    {
        public static implicit operator var<T>(List<Account> v)
        {
            throw new NotImplementedException();
        }
    }
}