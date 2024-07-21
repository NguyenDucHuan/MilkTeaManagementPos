using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaManagement.DAL.ViewModelEntities
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        private int _totalBill;
        private double _totalAmount;
        private int _sellProductOnMonth;
        private MilkTeaContext _context;

        public int TotalBill
        {
            get => _totalBill;
            set
            {
                _totalBill = value;
                OnPropertyChanged(nameof(TotalBill));
            }
        }

        public double TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }

        public int SellProductOnMonth
        {
            get => _sellProductOnMonth;
            set
            {
                _sellProductOnMonth = value;
                OnPropertyChanged(nameof(SellProductOnMonth));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
