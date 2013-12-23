using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.esendex.sdk.accounts
{
    public interface IAccountService
    {
        AccountCollection GetAccounts();
    }
}
