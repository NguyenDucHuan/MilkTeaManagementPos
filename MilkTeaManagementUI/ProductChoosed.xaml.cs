using MilkTeaManagement.DAL.Entities;
using System.Windows;

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for ProductChoosed.xaml
    /// </summary>
    public partial class ProductChoosed : Window
    {
        public TbProduct Product { get; set; } = null;
        public ProductChoosed()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductNameLabel.Content = Product.Name;
            QuantityTextBox.Text = "1";
            ProductPriceLabel.Content = Product.UnitPrice;
        }

        private void DecresingButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(QuantityTextBox.Text) > 1)
            {
                QuantityTextBox.Text = (int.Parse(QuantityTextBox.Text) - 1).ToString();
            }
            else
            {
                QuantityTextBox.Text = "1";
            }

        }

        private void IncresingButton_Click(object sender, RoutedEventArgs e)
        {
            QuantityTextBox.Text = (int.Parse(QuantityTextBox.Text) + 1).ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Product.Unit = QuantityTextBox.Text;
            Product.Description = DescriptionTextBox.Text;
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Products.Add(Product);
        }
    }
}
