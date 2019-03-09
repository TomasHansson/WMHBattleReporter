﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WMHBattleReporter.Model;

namespace WMHBattleReporter.ViewModel.Commands
{
    public class AddCasterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public AdminViewModel ViewModel { get; set; }

        public AddCasterCommand(AdminViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(ViewModel.NewCaster) || DatabaseServices.CasterNameExists(ViewModel.NewCaster)
                || ViewModel.NewCastersFaction == null)
                return;

            Caster newCaster = new Caster()
            {
                Faction = ViewModel.NewCastersFaction.Name,
                Name = ViewModel.NewCaster
            };

            DatabaseServices.InsertItem(newCaster);
            ViewModel.RefillFactionCastersCollection();
        }
    }
}
