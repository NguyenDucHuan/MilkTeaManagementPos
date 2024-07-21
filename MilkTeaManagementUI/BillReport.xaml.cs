using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for BillReport.xaml
    /// </summary>
    public partial class BillReport : Window
    {
        private BillService _billService = new BillService();
        public BillReport()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            BillDataGrid.ItemsSource = null;
            BillDataGrid.ItemsSource = _billService.GetAllBills();
        }

        private void ViewDetailButton_Click(object sender, RoutedEventArgs e)
        {
            TbBill? bill = BillDataGrid.SelectedItem as TbBill;
            if (bill != null)
            {
                BillDetail billDetail = new BillDetail();
                billDetail.Bill = bill;
                billDetail.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a bill to view detail");
            }
        }

        private void BillDataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {

        }
    }
}

