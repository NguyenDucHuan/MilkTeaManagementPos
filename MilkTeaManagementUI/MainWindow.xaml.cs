using MilkTeaManagement.BLL.Services;
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

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductService _productService;
        private EmployeeService _employeeService;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _productService = new ProductService();
            _employeeService = new EmployeeService();
            if (Application.Current.Properties.Contains("LoggedInEmpID"))
            {
                long loggedInEmpID = (long)Application.Current.Properties["LoggedInEmpID"];
                var loginEmp = _employeeService.GetLoginEmployee(loggedInEmpID);
                LoginEmpNameTextBox.Text = loginEmp.FullName;
                string vvvv = loginEmp.FullName;
                var products = _productService.GetAllProductList();
                DataContext = this;
                ListViewProduct.ItemsSource = products;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
