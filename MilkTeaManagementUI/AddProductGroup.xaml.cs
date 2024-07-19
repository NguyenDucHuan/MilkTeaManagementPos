using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for AddProductGroup.xaml
    /// </summary>
    public partial class AddProductGroup : Window
    {
        private ProductGroupService _productGroupService = new ProductGroupService();
        public AddProductGroup()
        {
            InitializeComponent();
        }

        private void AddProductGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGroupNameTextBox.Text == "" || DescriptionProductGroupTextBox.Text == "")
            {
                MessageBox.Show("Please fill in all fields");
                return;

            }
            TbGroupProduct tbGroupProduct = new TbGroupProduct();

            tbGroupProduct.NameGr = ProductGroupNameTextBox.Text;
            tbGroupProduct.DescriptionGr = DescriptionProductGroupTextBox.Text;


            _productGroupService.AddProductGroup(tbGroupProduct);
            MessageBox.Show("Add Product Group Success");
            this.Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
