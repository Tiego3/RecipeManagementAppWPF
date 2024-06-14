using LiveCharts.Wpf.Points;
using LiveCharts.Wpf;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;
using LiveCharts.Definitions.Charts;

namespace RecipeManagementAppWPF
{
    /// <summary>
    /// Interaction logic for PieChart.xaml
    /// </summary>
    public partial class PieChart : Window
    {
        public PieChart(Dictionary<string, double> foodGroupPercentages)
        {
            InitializeComponent();
            DisplayPieChart(foodGroupPercentages);
        }

        private void DisplayPieChart(Dictionary<string, double> foodGroupPercentages)
        {
            var pieSeries = new OxyPlot.Series.PieSeries // Use fully qualified name here
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.5,
                AngleSpan = 360,
                StartAngle = 0
            };

            foreach (var kvp in foodGroupPercentages)
            {
                pieSeries.Slices.Add(new OxyPlot.Series.PieSlice(kvp.Key, kvp.Value) { IsExploded = false });
            }

            var plotModel = new PlotModel { Title = "Food Group Distribution" };
            plotModel.Series.Add(pieSeries);

            PieChartView.Model = plotModel;
        }
    }
}
