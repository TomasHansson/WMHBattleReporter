﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WMHBattleReporter.ViewModel;

namespace WMHBattleReporter.View
{
    /// <summary>
    /// Interaction logic for RegisterUserControl.xaml
    /// </summary>
    public partial class RegisterUserControl : UserControl
    {
        private RegisterViewModel _registerVM;

        public RegisterUserControl()
        {
            InitializeComponent();
            _registerVM = Resources["RegisterVM"] as RegisterViewModel;
            _registerVM.RegisterCommand.Message += DisplayMessage;
        }

        private void DisplayMessage(string message) => MessageBox.Show(message);
    }
}
