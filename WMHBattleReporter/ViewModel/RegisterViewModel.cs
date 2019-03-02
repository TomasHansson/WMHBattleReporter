using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMHBattleReporter.ViewModel.Commands;

namespace WMHBattleReporter.ViewModel
{
    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public RegisterCommand RegisterCommand { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
        }
    }
}
