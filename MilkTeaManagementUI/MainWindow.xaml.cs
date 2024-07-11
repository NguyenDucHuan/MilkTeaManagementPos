using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;
using System.Windows.Controls;

namespace MilkTeaManagementUI
{
    public partial class MainWindow : Window
    {
        private ProductService _productService;
        private EmployeeService _employeeService;
        private TableService _tableService;
        private TableGroupService _tableGroupService;
        private Dictionary<long, List<TbTable>> _tablesByGroupId;

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
            _tableGroupService = new TableGroupService();
            var tableGroups = _tableGroupService.GetTableGroupList();
            _tableService = new TableService();
            ListViewTable.ItemsSource = _tableService.GetTableList();

            if (tableGroups != null)
            {
                TabControlGroups.Items.Clear();

                foreach (var group in tableGroups)
                {
                    TabItem tabItem = new TabItem();
                    tabItem.Header = group.Name;
                    tabItem.Tag = group.Id;

                    ListView listView = new ListView();
                    listView.Name = $"ListViewTablesGroup{group.Id}";
                    listView.Background = System.Windows.Media.Brushes.White;

                    // Retrieve tables for the current group
                    var tables = _tableService.GetTableByGroup(group.Id);
                    _tablesByGroupId[group.Id] = tables; // Store tables in dictionary for later reference

                    // Set items source to the retrieved tables
                    listView.ItemsSource = tables;

                    // Define how each table item should be displayed
                    listView.ItemTemplate = (DataTemplate)Resources["TableItemTemplate"]; // Ensure "TableItemTemplate" is defined in your resources
                    listView.ItemContainerStyle = (Style)Resources["TableListViewItemStyle"]; // Ensure "TableListViewItemStyle" is defined in your resources

                    tabItem.Content = listView;
                    TabControlGroups.Items.Add(tabItem);
                }

                if (TabControlGroups.Items.Count > 0)
                {
                    TabControlGroups.SelectedIndex = 0; // Select the first tab by default
                }
            }
            else
            {
                MessageBox.Show("Failed to retrieve table groups.");
            }
        }



        private void TabControlGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                TabItem selectedTab = e.AddedItems[0] as TabItem;
                if (selectedTab != null && selectedTab.Tag != null)
                {
                    long groupId = (long)selectedTab.Tag;

                    // Find ListView for the selected group
                    ListView listView = selectedTab.Content as ListView;
                    if (listView != null)
                    {
                        // Set items source to tables of the selected group
                        if (_tablesByGroupId.ContainsKey(groupId))
                        {
                            listView.ItemsSource = _tablesByGroupId[groupId];
                        }
                        else
                        {
                            MessageBox.Show($"Tables for group ID {groupId} not found.");
                        }
                    }
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
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
    }
}
