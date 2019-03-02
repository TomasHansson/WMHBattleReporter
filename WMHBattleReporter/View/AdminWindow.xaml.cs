using System;
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
using System.Windows.Shapes;
using WMHBattleReporter.ViewModel;

namespace WMHBattleReporter.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddFactionButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewFactionTextBox.Text))
            {
                MessageBox.Show("You must supply a name for the faction.");
                return;
            }

            if (DatabaseServices.FactionNameExists(NewFactionTextBox.Text))
            {
                MessageBox.Show("A faction with that name already exists in the database.");
                return;
            }
        }

        private void DeleteFactionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentFactions.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a faction to delete.");
                return;
            }
        }

        private void AddCasterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewCasterTextBox.Text))
            {
                MessageBox.Show("You must supply a name for the caster.");
                return;
            }

            if (DatabaseServices.CasterNameExists(NewCasterTextBox.Text))
            {
                MessageBox.Show("A caster with that name already exists.");
                return;
            }

            if (CurrentFactions.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a faction to which the new caster belongs.");
                return;
            }
        }

        private void DeleteCasterButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentFactionCasters.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a caster to delete.");
                return;
            }
        }
    }
}
