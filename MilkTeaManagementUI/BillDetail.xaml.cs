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
        public BillDetail()
        {
            InitializeComponent();
        }
    }
}
