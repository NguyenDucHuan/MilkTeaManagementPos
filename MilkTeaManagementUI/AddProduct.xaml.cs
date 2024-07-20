using Microsoft.Win32;
using MilkTeaManagement.BLL.Services;
using MilkTeaManagement.DAL.Entities;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private byte[] _imageData;
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
            tbProduct.Img = _imageData;

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

        private void UpdateImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _imageData = File.ReadAllBytes(openFileDialog.FileName);
                DisplayImage(openFileDialog.FileName);
                MessageBox.Show("Image uploaded successfully!");
            }
        }
        private void DisplayImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            ProductImage.Source = bitmap;
        }
    }
}
