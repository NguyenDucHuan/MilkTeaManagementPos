using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for BillDetail.xaml
    /// </summary>
    public partial class BillDetail : Window
    {
        public TbBill Bill { get; set; }
        private BillService _billService = new();
        public BillDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Bill != null)
            {
                TbBill bill = _billService.GetBill(Bill.Id);
                if (bill != null)
                {
                    BillDetailDataGrid.ItemsSource = new List<TbBill> { bill };
                    BillDetailsDataGrid.ItemsSource = _billService.GetBillDetailsByBillId(Bill.Id);
                }
                else
                {
                    MessageBox.Show("Bill not found.");
                }
            }
        }


    }
}
