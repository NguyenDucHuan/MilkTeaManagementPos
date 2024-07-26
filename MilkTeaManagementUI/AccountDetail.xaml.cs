using Microsoft.Win32;
using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        private byte[] Img { get; set; }
        public Login Login { get; set; }
        public AccountDetail()
        {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (Login == null)
            {
                Employee employee = new Employee()
                {
                    FullName = NameTextBox.Text,
                    Address = AddressTextBox.Text,
                    PhoneNumber = PhoneTextBox.Text,
                    IdCard = CardIdTextBox.Text,
                    DateWork = null,
                    Img = this.Img
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
            else
            {

                var oldLoginRole = _loginRoleService.GetLoginRole(Login.Id);

                _loginRoleService.UpdateLoginRole(oldLoginRole);
                var oldLog = _loginServices.GetLoginNoHash(Login.UserName, Login.Password);
                oldLog.UserName = UserNameTextBox.Text;
                oldLog.Password = PasswordTextBox.Text;
                _loginServices.UpdateAccount(oldLog);
                var oldemp = _employeeService.GetEmployeeById((long)Login.IdEmployee);
                oldemp.FullName = NameTextBox.Text;
                oldemp.Address = AddressTextBox.Text;
                oldemp.PhoneNumber = PhoneTextBox.Text;
                oldemp.IdCard = CardIdTextBox.Text;
                oldemp.Img = this.Img;
                _employeeService.UpdateEmployee(oldemp);
                this.Close();
            }


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
                LoginRolesComboBox.SelectedValue = Login.LoginRoles.FirstOrDefault().IdRole;
                NameTextBox.Text = Login.IdEmployeeNavigation.FullName;
                AddressTextBox.Text = Login.IdEmployeeNavigation.Address;
                PhoneTextBox.Text = Login.IdEmployeeNavigation.PhoneNumber;
                CardIdTextBox.Text = Login.IdEmployeeNavigation.IdCard;
                DateWorkDatePicker.SelectedDate = DateTime.Now;
                Img = Login.IdEmployeeNavigation.Img;
                DataContext = this;
            }
        }
        private void EmpImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                Img = File.ReadAllBytes(openFileDialog.FileName);
                DisplayImage(openFileDialog.FileName);
                MessageBox.Show("Image uploaded successfully!");
            }
        }
        private void DisplayImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            EmpImage.Source = bitmap;
        }
        private void UpdateImgButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                Img = File.ReadAllBytes(openFileDialog.FileName);
                DisplayImage(openFileDialog.FileName);
                MessageBox.Show("Image uploaded successfully!");
            }
        }
    }
}
