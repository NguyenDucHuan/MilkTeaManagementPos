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
        public List<TbProduct> Products { get; set; }
        public String Description { get; set; }

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
        }

        public void LoadMenu()
        {
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
            TableChoosedTextBlock.Content = "Table: " + (ListViewTable.SelectedItem as TbTable).NameTb;
        }

        public void Product_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductChoosed productChoosed = new ProductChoosed();
            productChoosed.Product = ListViewProduct.SelectedItem as TbProduct;
            productChoosed.ShowDialog();

            ListViewOrder.ItemsSource = Products;
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

    }
}
