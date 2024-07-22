using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for AccountManagement.xaml
    /// </summary>
    public partial class AccountManagement : Window
    {
        private LoginServices _loginServices = new LoginServices();
        private LoginRoleService _loginRoleService = new LoginRoleService();
        private EmployeeService _employeeService = new EmployeeService();
        public AccountManagement()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();

        }

        private void LoadData()
        {
            AccountDataGrid.ItemsSource = null;
            AccountDataGrid.ItemsSource = _loginServices.GetAllAccount();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            AccountDetail accountDetail = new AccountDetail();
            accountDetail.ShowDialog();

            LoadData();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an account to update");
                return;
            }
            AccountDetail accountDetail = new AccountDetail();
            accountDetail.Login = AccountDataGrid.SelectedItem as Login;
            accountDetail.ShowDialog();

            LoadData();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an account to delete");
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this account?", "Delete Account", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            var login = AccountDataGrid.SelectedItem as Login;

            _loginRoleService.DeleteLoginRole(login.LoginRoles.FirstOrDefault().Id);

            _loginServices.DeleteAccount(login);

            var employee = _employeeService.GetEmployeeById((long)login.IdEmployee);

            _employeeService.DeleteEmployee(employee);

            LoadData();
        }
    }
}
