using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using OpsCore.UI.Views;

namespace OpsCore.UI
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            NavView.SelectedItem = NavView.MenuItems[0];
            ContentFrame.Navigate(typeof(DashboardPage));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var tag = (args.SelectedItem as NavigationViewItem)?.Tag?.ToString();

            switch (tag)
            {
                case "Dashboard":
                    ContentFrame.Navigate(typeof(DashboardPage));
                    break;
                case "Assets":
                    ContentFrame.Navigate(typeof(AssetsPage));
                    break;
                case "Employees":
                    ContentFrame.Navigate(typeof(EmployeesPage));
                    break;
                case "Maintenance":
                    ContentFrame.Navigate(typeof(MaintenancePage));
                    break;
            }
        }
    }
}