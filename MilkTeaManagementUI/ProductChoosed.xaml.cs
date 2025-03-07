﻿using MilkTeaManagement.DAL.Entities;
using System.Windows;
using System.Diagnostics;
using System.Windows.Navigation;

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
            if (Product != null)
            {
                ProductNameLabel.Content = Product.Name;
                QuantityTextBox.Text = "1";
                ProductPriceLabel.Content = Product.UnitPrice;
            }
            else
            {
                HiddenPiece.Visibility = Visibility.Visible;
                Information.Visibility = Visibility.Hidden;
            }
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
            TbBillDetailt billDetailt = new TbBillDetailt() { Description = Product.Description, IdProduct = Product.Id, Quantity = int.Parse(Product.Unit), UnitPrice = Product.UnitPrice, IntoMoney = Product.UnitPrice * int.Parse(Product.Unit), IdProductNavigation = Product };
            if (Application.Current.Properties["CurBill"] == null)
            {
                TbBill curBill = new TbBill();
                curBill.TbBillDetailts.Add(billDetailt);
                Application.Current.Properties["CurBill"] = curBill;
            }
            else
            {
                TbBill curBill = (TbBill)Application.Current.Properties["CurBill"];

                foreach (var item in curBill.TbBillDetailts)
                {
                    if (item.IdProduct == Product.Id)
                    {
                        item.Quantity += int.Parse(Product.Unit);
                        item.IntoMoney = item.Quantity * item.UnitPrice;
                        this.Close();
                        return;
                    }
                }

                curBill.TbBillDetailts.Add(billDetailt);
                Application.Current.Properties["CurBill"] = curBill;
            }
            this.Close();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true 
            };
            Process.Start(psi);
            e.Handled = true;
        }
    }
}
