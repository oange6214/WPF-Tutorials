using NavigationMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationMVVM.Stores
{
    public class AccountStore
    {
		private Account _currentAccount;

		public Account CurrentAccount
		{
			get => _currentAccount;
            set 
			{ 
				_currentAccount = value;
			}
		}


	}
}
