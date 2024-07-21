using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;
using System.Windows.Controls;

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
        private void SearchByDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = DateTime.Parse(SearchByDatePicker.Text);
            _billService.GetByDate(date);
            if (date != null)
            {
                BillDataGrid.ItemsSource = null;
                BillDataGrid.ItemsSource = _billService.GetByDate(date);
            }
            else
            {
                MessageBox.Show("Please select a date to search");
            }
        }

    }
}

