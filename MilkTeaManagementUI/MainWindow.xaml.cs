using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MilkTeaManagementUI
{
    public partial class MainWindow : Window
    {
        private ProductService _productService = new();
        private EmployeeService _employeeService = new();
        private TableService _tableService = new();
        private TableGroupService _tableGroupService = new();
        private Dictionary<long, List<TbTable>> _tablesByGroupId;
        private BillService _billService = new();
        private BillDetailService _billDetailService = new();
        private LoginServices _loginServices = new();
        public List<TbTable> TablesOnUse { get; set; } = null;
        public TbBill CurBill { get; set; } = null;

        public TbBill OldBill { get; set; } = null;


        public MainWindow()
        {
            InitializeComponent();
            _tablesByGroupId = new Dictionary<long, List<TbTable>>();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMenu();
            LoadLoginUser();
            LoadTable();
            LoadCurOrder();
        }

        public void LoadMenu()
        {
            ListViewProduct.ItemsSource = null;
            _productService = new ProductService();
            var products = _productService.GetAllProductList();
            DataContext = this;
            ListViewProduct.ItemsSource = products;
        }
        public void LoadLoginUser()
        {
            _employeeService = new EmployeeService();
            long loggedInEmpID = (long)Application.Current.Properties["LoggedInEmpID"];
            var loginrole = _loginServices.GetLoginByEmpID(loggedInEmpID).LoginRoles.FirstOrDefault().IdLogin;
            var loginEmp = _employeeService.GetLoginEmployee(loggedInEmpID);
            if (loginrole != 1)
            {
                GridMenu.Visibility = Visibility.Hidden;
            }
            LoginEmpNameTextBox.Text = loginEmp.FullName;
        }
        public void LoadCurOrder()
        {
            ListViewOrder.ItemsSource = null;
            if (CurBill != null)
            {
                ListViewOrder.ItemsSource = CurBill.TbBillDetailts;
                if (CurBill.IdTable != null)
                {
                    TableChoosedTextBlock.Content = "Table: " + _tableService.GetTableList().Where(t => t.Id == CurBill.IdTable).FirstOrDefault().NameTb;
                }
                TotalMoneyTextBlock.Text = "Total money:\n " + CurBill.TotalMoney.ToString() + " VND";
            }
            CheckoutOrderButton.Visibility = Visibility.Visible;
            ClearOrderButton.Visibility = Visibility.Visible;
            RefreshTableButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
        }
        public void LoadTable()
        {
            if (TablesOnUse != null && TablesOnUse.Count > 0)
            {
                ListViewTableOnUse.ItemsSource = null;
                ListViewTable.ItemsSource = null;
                var tables = _tableService.GetTableList();
                ListViewTable.ItemsSource = tables.Where(t => !TablesOnUse.Any(p2 => p2.Id == t.Id));
                ListViewTableOnUse.ItemsSource = TablesOnUse;
            }
            else
            {
                ListViewTableOnUse.ItemsSource = null;
                ListViewTable.ItemsSource = null;
                ListViewTable.ItemsSource = _tableService.GetTableList();
            }

        }
        public void Product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewOrder.ItemsSource = null;
            ProductChoosed productChoosed = new ProductChoosed();
            if (ListViewProduct.SelectedItem != null)
            {
                productChoosed.Product = ListViewProduct.SelectedItem as TbProduct;
                productChoosed.ShowDialog();

                if (Application.Current.Properties["CurBill"] != null)
                {
                    CurBill = (TbBill)Application.Current.Properties["CurBill"];
                    UpdateTotalMoney();
                    if (CurBill.TotalMoney > 0)
                    {
                        TotalMoneyTextBlock.Text = "Total money:\n " + CurBill.TotalMoney.ToString() + " VND";
                    }
                }
            }
            ListViewProduct.SelectedItem = null;
            LoadCurOrder();
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
            Application.Current.Shutdown();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            var storyboard = (Storyboard)FindResource("OpenMenu");
            storyboard.Begin();
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            var storyboard = (Storyboard)FindResource("CloseMenu");
            storyboard.Begin();
        }
        private void UpdateTotalMoney()
        {
            double totalMoney = 0;
            if (CurBill != null)
            {
                if (CurBill.TbBillDetailts.Count != 0)
                {
                    foreach (var billDetail in CurBill.TbBillDetailts)
                    {
                        totalMoney = totalMoney + (double)billDetail.IntoMoney;
                    }
                    CurBill.TotalMoney = totalMoney;
                }
            }
        }
        private void ClearOrderButton_Click(object sender, RoutedEventArgs e)
        {
            CurBill = null;
            Application.Current.Properties["CurBill"] = null;
            LoadCurOrder();
            TableChoosedTextBlock.Content = "";
            TotalMoneyTextBlock.Text = "";
        }
        private void ProductStackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.ShowDialog();
            LoadMenu();
        }
        private void RefreshTableButton_Click(object sender, RoutedEventArgs e)
        {
            if (OldBill != null)
            {
                var g = TablesOnUse.Where(c => c.Id == OldBill.IdTable).FirstOrDefault();
                TablesOnUse.Remove(g);
                ListViewOrder.ItemsSource = null;
                LoadTable();
                TableChoosedTextBlock.Content = "";
                TotalMoneyTextBlock.Text = "";

            }
            CheckoutOrderButton.Visibility = Visibility.Visible;
            ClearOrderButton.Visibility = Visibility.Visible;
            RefreshTableButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
        }
        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurBill == null || CurBill.IdTable == null)
            {
                MessageBoxResult answer = MessageBox.Show("Please choose table!!", "No choose table!!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (CurBill != null && (CurBill.TbBillDetailts.Count == 0 || CurBill.TbBillDetailts == null))
            {
                MessageBoxResult answer = MessageBox.Show("No have order menu now!! Please choose drink!!", "Error ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (CurBill != null && (CurBill.TbBillDetailts.Count > 0 || CurBill.TbBillDetailts != null))
            {
                CurBill.BillDate = DateTime.Now;
                _billService.CreateNewBill(CurBill);
                _billDetailService.CreateNewBillDetails((List<TbBillDetailt>)CurBill.TbBillDetailts);
                MessageBoxResult answer = MessageBox.Show("Check out complete", "Complete!!",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                if (TablesOnUse != null)
                {
                    TablesOnUse.Add(_tableService.GetTableList().Where(c => c.Id == CurBill.IdTable).FirstOrDefault());
                }
                else
                {
                    TablesOnUse = new();
                    TablesOnUse.Add(_tableService.GetTableList().Where(c => c.Id == CurBill.IdTable).FirstOrDefault());
                }
                Application.Current.Properties["CurBill"] = null;
                CurBill = null;
                LoadCurOrder();
                ListViewTable.SelectedItem = null;
                LoadTable();
                TableChoosedTextBlock.Content = "";
                TotalMoneyTextBlock.Text = "";
            }
        }
        private void ListViewTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurBill == null)
                CurBill = new();
            else
            {
                ListViewOrder.ItemsSource = null;
            }
            long loggedInEmpID = (long)Application.Current.Properties["LoggedInEmpID"];
            if (ListViewTable.SelectedItem != null)
            {
                CurBill.IdTable = (ListViewTable.SelectedItem as TbTable).Id;
                CurBill.IdUser = loggedInEmpID;
                TableChoosedTextBlock.Content = "Table: " + (ListViewTable.SelectedItem as TbTable).NameTb;
            }
            LoadCurOrder();
            ListViewTable.SelectedItem = null;
        }
        private void ListViewTableOnUse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewTableOnUse.SelectedItem != null)
            {
                long tableID = (ListViewTableOnUse.SelectedItem as TbTable).Id;
                var lastBillByTable = _billService.GetLastestBillFromTableID(tableID);
                ListViewOrder.ItemsSource = null;
                if (lastBillByTable != null)
                {
                    ListViewOrder.ItemsSource = lastBillByTable.TbBillDetailts;
                    TableChoosedTextBlock.Content = "Ordering On Table: " + (ListViewTableOnUse.SelectedItem as TbTable).NameTb;
                    TotalMoneyTextBlock.Text = "Total money:\n " + lastBillByTable.TotalMoney.ToString() + " VND";
                }
                OldBill = lastBillByTable;
            }
            ListViewTableOnUse.SelectedItem = null;
            LoadTable();
            CheckoutOrderButton.Visibility = Visibility.Hidden;
            ClearOrderButton.Visibility = Visibility.Hidden;
            RefreshTableButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LoadCurOrder();
            TableChoosedTextBlock.Content = "";

            CheckoutOrderButton.Visibility = Visibility.Visible;
            ClearOrderButton.Visibility = Visibility.Visible;
            RefreshTableButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
        }

        private void BillsStackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BillReport billReport = new BillReport();
            billReport.ShowDialog();
        }

        private void Accounts_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AccountManagement accountManagement = new AccountManagement();
            accountManagement.ShowDialog();
        }

        private void ListViewItem_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ReportWindow reportWindow = new ReportWindow();
            reportWindow.ShowDialog();
        }

    }
}
