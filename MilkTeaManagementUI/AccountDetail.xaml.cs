using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for AccountDetail.xaml
    /// </summary>
    public partial class AccountDetail : Window
    {
        private RoleService _roleService = new RoleService();
        private EmployeeService _employeeService = new EmployeeService();
        private LoginServices _loginServices = new LoginServices();
        private LoginRoleService _loginRoleService = new LoginRoleService();

        public Login Login { get; set; }
        public AccountDetail()
        {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {


            Employee employee = new Employee()
            {
                FullName = NameTextBox.Text,
                Address = AddressTextBox.Text,
                PhoneNumber = PhoneTextBox.Text,
                IdCard = CardIdTextBox.Text,
                DateWork = null,
            };

            _employeeService.AddEmployee(employee);


            var lastEmp = _employeeService.GetLastEmployee();


            Login login = new Login()
            {
                UserName = UserNameTextBox.Text,
                Password = PasswordTextBox.Text,
                IdEmployee = lastEmp.Id,
                IsUse = true,
            };

            _loginServices.AddAccount(login);

            var lastLogin = _loginServices.GetLastEmployee();

            LoginRole loginRole = new LoginRole()
            {
                IdLogin = lastLogin.Id,
                IdRole = (int)LoginRolesComboBox.SelectedValue,
            };

            _loginRoleService.AddLoginRole(loginRole);

            this.Close();

        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();

        }

        private void LoadData()
        {
            LoginRolesComboBox.ItemsSource = null;
            LoginRolesComboBox.ItemsSource = _roleService.GetAllRole();
            LoginRolesComboBox.SelectedValuePath = "Id";
            LoginRolesComboBox.DisplayMemberPath = "RoleName";
            LoginRolesComboBox.SelectedIndex = 0;
            if (Login != null)
            {
                UserNameTextBox.Text = Login.UserName;
                PasswordTextBox.Text = Login.Password;
                LoginRolesComboBox.SelectedIndex = Login.LoginRoles.FirstOrDefault().IdRole;


                NameTextBox.Text = Login.IdEmployeeNavigation.FullName;
                AddressTextBox.Text = Login.IdEmployeeNavigation.Address;
                PhoneTextBox.Text = Login.IdEmployeeNavigation.PhoneNumber;
                CardIdTextBox.Text = Login.IdEmployeeNavigation.IdCard;
                DateWorkDatePicker.SelectedDate = DateTime.Now;



            }
        }
    }
}
