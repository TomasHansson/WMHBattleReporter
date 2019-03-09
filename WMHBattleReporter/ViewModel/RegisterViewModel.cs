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
        public RegisterCommand RegisterCommand { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Region { get; set; }

        public List<string> RegionOptions { get; } = new List<string>{ "North America" , "South America", "Europa", "Africa", "Asia", "Oceania" };

        public RegisterViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
        }
    }
}
