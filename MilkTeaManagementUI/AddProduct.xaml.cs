using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.Windows;

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private ProductGroupService _productGroupService = new ProductGroupService();
        private ProductService _productService = new ProductService();
        public AddProduct()
        {
            InitializeComponent();
        }

        private void AddProductGroupButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductGroup addProductGroup = new AddProductGroup();
            addProductGroup.ShowDialog();

            LoadDataComboBox();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "" || UnitTextBox.Text == "" || PriceTextBox.Text == "" || DescriptionTextBox.Text == "")
            {
                MessageBox.Show("Please fill in all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            TbProduct tbProduct = new TbProduct();
            tbProduct.Name = NameTextBox.Text;
            tbProduct.Unit = UnitTextBox.Text;
            tbProduct.UnitPrice = int.Parse(PriceTextBox.Text);
            tbProduct.Description = DescriptionTextBox.Text;
            tbProduct.IdGroupProduct = (long)ProductGroupComboBox.SelectedValue;

            _productService.AddProduct(tbProduct);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataComboBox();
        }

        private void LoadDataComboBox()
        {
            ProductGroupComboBox.ItemsSource = null;
            ProductGroupComboBox.ItemsSource = _productGroupService.GetAllProductGroup();
            ProductGroupComboBox.DisplayMemberPath = "NameGr";
            ProductGroupComboBox.SelectedValuePath = "IdGr";
            ProductGroupComboBox.SelectedIndex = 0;
        }
    }
}
