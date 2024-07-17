using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;
using System.Windows.Controls;
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
        public List<TbProduct> Products { get; set; }
        public TbBill CurBill { get; set; } = null;



        public MainWindow()
        {
            InitializeComponent();
            _tablesByGroupId = new Dictionary<long, List<TbTable>>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMenu();
            LoadLoginUser();
            LoadTableGroups();
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
            var loginEmp = _employeeService.GetLoginEmployee(loggedInEmpID);
            LoginEmpNameTextBox.Text = loginEmp.FullName;
        }
        public void LoadCurOrder()
        {
            ListViewOrder.ItemsSource = null;
            if (CurBill != null)
            {
                ListViewOrder.ItemsSource = CurBill.TbBillDetailts;

            }
        }

        public void LoadTableGroups()
        {
            var tableGroups = _tableGroupService.GetTableGroupList();
            //ListViewTables.ItemsSource = _tableService.GetTableList();
            ListViewTable.ItemsSource = _tableService.GetTableList();

            //if (tableGroups != null)
            //{
            //    TabControlGroups.Items.Clear();

            //    foreach (var group in tableGroups)
            //    {
            //        TabItem tabItem = new TabItem();
            //        tabItem.Header = group.Name;
            //        tabItem.Tag = group.Id;


            //        ListView listView = new ListView();
            //        listView.Name = $"ListViewTablesGroup{group.Id}";
            //        listView.Background = System.Windows.Media.Brushes.White;
            //        // Retrieve tables for the current group
            //        var tables = _tableService.GetTableByGroup(group.Id);
            //        _tablesByGroupId[group.Id] = tables; // Store tables in dictionary for later reference

            //        //Set items source to the retrieved tables
            //        listView.ItemsSource = tables;

            //        // Define how each table item should be displayed
            //        listView.ItemTemplate = (DataTemplate)Resources["TableItemTemplate"]; // Ensure "TableItemTemplate" is defined in your resources
            //        listView.ItemContainerStyle = (Style)Resources["TableListViewItemStyle"]; // Ensure "TableListViewItemStyle" is defined in your resources

            //        tabItem.Content = listView;
            //        TabControlGroups.Items.Add(tabItem);
            //    }

            //    if (TabControlGroups.Items.Count > 0)
            //    {
            //        TabControlGroups.SelectedIndex = 0; // Select the first tab by default
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Failed to retrieve table groups.");
            //}
        }
        public void ListViewTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurBill == null)
                CurBill = new();
            long loggedInEmpID = (long)Application.Current.Properties["LoggedInEmpID"];
            CurBill.IdTable = (ListViewTable.SelectedItem as TbTable).Id;
            CurBill.IdUser = loggedInEmpID;
            Application.Current.Properties["CurBill"] = CurBill;
            TableChoosedTextBlock.Content = "Table: " + (ListViewTable.SelectedItem as TbTable).NameTb;
        }

        public void Product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductChoosed productChoosed = new ProductChoosed();
            productChoosed.Product = ListViewProduct.SelectedItem as TbProduct;
            productChoosed.ShowDialog();
            CurBill = (TbBill)Application.Current.Properties["CurBill"];
            UpdateTotalMoney();
            TotalMoneyTextBlock.Text = "Total money: " + CurBill.TotalMoney.ToString() + " VND";
            LoadCurOrder();


        }



        //private void TabControlGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var selectedGroup = TabControlGroups.SelectedItem as TableGroupService;
        //    if (selectedGroup != null)
        //    {
        //        ListViewTable.ItemsSource = selectedGroup.GetTableGroupList();
        //    }
        //}

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

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (CurBill.IdTable == null)
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
                MessageBoxResult answer = MessageBox.Show("Check out complete", "Complete!!", MessageBoxButton.OK, MessageBoxImage.Information);
                CurBill = null;
                LoadCurOrder();
                TableChoosedTextBlock.Content = "";
                TotalMoneyTextBlock.Text = "";
            }
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
            LoadCurOrder();
            TableChoosedTextBlock.Content = "";
            TotalMoneyTextBlock.Text = "";
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.ShowDialog();

            LoadMenu();
        }
    }
}
