using HandyControl.Tools.Extension;
using LiveCharts;
using LiveCharts.Wpf;
using MilkTeaManagement.BLL.Services;
using System.Windows;

namespace MilkTeaManagementUI
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private BillService _BillService = new();
        public ReportWindow()
        {
            InitializeComponent();
            LoadChart();
            ReportDetail.DataContext = _BillService.GetReportViewModelLast30Day();

        }
        public void LoadChart()
        {
            var List = _BillService.GetLast30DayTotalMoney();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "TotalMoneyPer7Days",
                    Values = new ChartValues<double> ()
                }
            };
            foreach (var item in List)
            {
                SeriesCollection[0].Values.Add(item);
            }

            var labels = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                string dateStart = DateTime.Now.AddDays(-7 * (i + 1)).ToString("dd/MM");
                string dateEnd = DateTime.Now.AddDays(-7 * i).ToString("dd/MM");
                labels.Add($"{dateStart}-{dateEnd}");
            }

            Labels = labels.ToArray();
            Formatter = value => value.ToString("N");
            DataContext = this;
        }
    }
}
