using OxyPlot;
using Xamarin.Forms;
using OxyPlot.Xamarin.Forms;
using System;
using System.IO;
using OxyPlot.Series;
using System.Linq;
using Xamarin.Forms.Internals;
using OxyPlot.Axes;
using System.Collections.Generic;
using OxyPlot.Legends;

namespace Charts
{
    public partial class MainPage : ContentPage
    {
        private PlotModel currentPlotModel;
        private PlotController plotController;
        private PlotModel PlotModel;
        public MainPage()
        {
            InitializeComponent();
            plotController = new PlotController();
            plotView.Controller = plotController;
        }
        private void ConfigurePlotView(PlotModel plotModel)
        {
            currentPlotModel = plotModel;
            plotView.Model = currentPlotModel;
            PlotModel = plotModel;

            // Bind touch event for hit testing
            plotController.BindTouchDown(new DelegatePlotCommand<OxyTouchEventArgs>((view, controller, args) =>
            {
                var hitTestArguments = new HitTestArguments(args.Position, 10);
                var hitResults = view.ActualModel.HitTest(hitTestArguments);

                foreach (var hitResult in hitResults)
                {
                    if (hitResult.Element is OxyPlot.Series.Series series)
                    {
                        HandleSeriesHit(series, hitResult);
                        break; // We handle the first hit result
                    }
                }
            }));

            // Bind zoom events
            plotController.BindMouseDown(OxyMouseButton.Left, PlotCommands.ZoomRectangle);
            plotController.BindMouseWheel(PlotCommands.ZoomWheel);

            plotView.Controller = plotController;
        }
        private void HandleSeriesHit(OxyPlot.Series.Series series, HitTestResult hitResult)
        {
            if (series is BarSeries barSeries)
            {
                var index = (int)hitResult.Index;
                var barItem = barSeries.ActualItems[index] as BarItem;
                if (barItem != null)
                {
                    var roundedValue = Math.Round(barItem.Value, 2); // Round to 2 decimal places
                    DisplayAlert("Bar Selected", $"Value: {roundedValue}", "OK");
                }
            }
            else if (series is LineSeries lineSeries)
            {
                var dataPoint = lineSeries.Points[(int)hitResult.Index];
                var roundedValue = Math.Round(dataPoint.Y, 2); // Round to 2 decimal places
                DisplayAlert("Line Point Selected", $"X: {dataPoint.X}, Y: {roundedValue}", "OK");
            }
            else if (series is PieSeries pieSeries)
            {
                var slice = pieSeries.Slices[(int)hitResult.Index];
                var roundedValue = Math.Round(slice.Value, 2); // Round to 2 decimal places
                DisplayAlert("Pie Slice Selected", $"Label: {slice.Label}, Value: {roundedValue}", "OK");
            }
            else if (series is RectangleBarSeries rectangleSeries)
            {
                var index = (int)hitResult.Index;
                var rectangleItem = rectangleSeries.Items[index];
                DisplayAlert("Rectangle Selected", $"X0: {rectangleItem.X0}, X1: {rectangleItem.X1}, Y0: {rectangleItem.Y0}, Y1: {rectangleItem.Y1}", "OK");
            }
            // Add more else if clauses for other series types as needed
        }
        private PlotModel CreateLineChart()
        {
            var plotModel = new PlotModel { Title = "Line Chart Example" };
            AddLegend(plotModel);
            var valueAxisX = new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 10 ,AxislineColor = OxyColors.Red, TextColor = OxyColors.Blue };
            var valueAxisY = new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 10 };
            plotModel.Axes.Add(valueAxisX);
            plotModel.Axes.Add(valueAxisY);
            //this.LineChartPlotModel_1.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            //this.LineChartPlotModel_1.Series.Add(new FunctionSeries(Math.Sin, 0, 10, 0.1, "Sin(x)"));
            var lineSeries = new LineSeries
            {
                MarkerType = MarkerType.Square,
                // MarkerStroke = OxyColors.Red,
                // MarkerSize = 3,
                Color = OxyColors.Salmon,
                ItemsSource = new List<DataPoint>
                {
                    new DataPoint(0, 0),
                    new DataPoint(1, 2),
                    new DataPoint(2, 4),
                    new DataPoint(3, 6),
                    new DataPoint(4, 8),
                    new DataPoint(5, 10)
                }
            };
            var lineSeries1 = new LineSeries
            {
                MarkerType = MarkerType.Star,
                Color = OxyColors.Aqua,
                //MarkerStroke = OxyColors.Red,
                // MarkerSize = 3,
                ItemsSource = new List<DataPoint>
                {
                    new DataPoint(1, 0),
                    new DataPoint(1,5),
                    new DataPoint(2, 3),
                    new DataPoint(3, 7),
                }
            };

            plotModel.Series.Add(lineSeries);
            plotModel.Series.Add(lineSeries1);
            return plotModel;
        }
        private PlotModel CreateBarChart()
        {
            var plotModel = new PlotModel { Title = "Bar Chart Example" };
            AddLegend(plotModel);
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left, TextColor = OxyColors.Beige, AxislineColor = OxyColors.Gray };
            categoryAxis.Labels.AddRange(new[] { "Category A", "Category B", "Category C" });
            plotModel.Axes.Add(categoryAxis);

            var valueAxis = new LinearAxis { Position = AxisPosition.Top };
            plotModel.Axes.Add(valueAxis);

            var barSeries = new BarSeries
            {
                FillColor = OxyColors.Green,
                ItemsSource = new List<BarItem>
                {
                    new BarItem { Value = 25 },
                    new BarItem { Value = 37 },
                    new BarItem { Value = 18 }
                }
            };

            plotModel.Series.Add(barSeries);
            return plotModel;
        }
        private PlotModel CreatePieChart()
        {
            var plotModel = new PlotModel { Title = "Pie Chart Example" };
            AddLegend(plotModel);
            var pieSeries = new PieSeries
            {
                Slices = new List<PieSlice>
                {
                    new PieSlice("Category A", 25){ Fill = OxyColors.Blue },
                    new PieSlice("Category B", 37){ Fill = OxyColors.Green },
                    new PieSlice("Category C", 18) { Fill = OxyColors.Red }
                }
            };

            plotModel.Series.Add(pieSeries);
            return plotModel;
        }
        private PlotModel CreateGroupedBarChart()
        {
            var plotModel = new PlotModel { Title = "Grouped Bar Chart Example" };
            AddLegend(plotModel);
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left, TextColor = OxyColors.Pink, AxislineColor = OxyColors.Black };
            categoryAxis.Labels.AddRange(new[] { "Category A", "Category B", "Category C" });
            plotModel.Axes.Add(categoryAxis);

            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom };
            plotModel.Axes.Add(valueAxis);

            var barSeries1 = new BarSeries { Title = "Group 1", FillColor = OxyColors.Blue, };
            barSeries1.Items.AddRange(new List<BarItem>
            {
                new BarItem { Value = 25 },
                new BarItem { Value = 37 },
                new BarItem { Value = 18 }
            });

            var barSeries2 = new BarSeries { Title = "Group 2" , FillColor = OxyColors.Orange, };
            barSeries2.Items.AddRange(new List<BarItem>
            {
                new BarItem { Value = 30 },
                new BarItem { Value = 40 },
                new BarItem { Value = 20 }
            });

            plotModel.Series.Add(barSeries1);
            plotModel.Series.Add(barSeries2);
            return plotModel;
        }
        private PlotModel CreateRectangleChart()
        {
            var plotModel = new PlotModel { Title = "Rectangle Chart Example" };
            AddLegend(plotModel);
            // Weekday axis (horizontal)
            plotModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Bottom,

                // Key used for specifying this axis in the HeatMapSeries
                Key = "WeekdayAxis",

                // Array of Categories (see above), mapped to one of the coordinates of the 2D-data array
                ItemsSource = new[]
                {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"
    }
            });

            // Cake type axis (vertical)
            plotModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "CakeAxis",
                ItemsSource = new[]
                {
            "Apple cake",
            "Baumkuchen",
            "Bundt cake",
            "Chocolate cake",
            "Carrot cake"
    }
            });

            // Color axis
            plotModel.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalettes.Hot(200)
            });

            var rand = new Random();
            var data = new double[7, 5];
            for (int x = 0; x < 5; ++x)
            {
                for (int y = 0; y < 7; ++y)
                {
                    data[y, x] = rand.Next(0, 200) * (0.13 * (y + 1));
                }
            }

            var heatMapSeries = new HeatMapSeries
            {
                X0 = 0,
                X1 = 6,
                Y0 = 0,
                Y1 = 4,
                XAxisKey = "WeekdayAxis",
                YAxisKey = "CakeAxis",
                RenderMethod = HeatMapRenderMethod.Rectangles,
                LabelFontSize = 0.2, // neccessary to display the label
                Data = data
            };

            plotModel.Series.Add(heatMapSeries);
            return plotModel;
        }
        private PlotModel CreateBitmapChart()
        {
            var plotModel = new PlotModel { Title = "Trigonometric functions" };
            AddLegend(plotModel);
            var start = -Math.PI;
            var end = Math.PI;
            var step = 0.1;
            int steps = (int)((Math.Abs(start) + Math.Abs(end)) / step);

            //generate points for functions
            var sinData = new DataPoint[steps];
            for (int i = 0; i < steps; ++i)
            {
                var x = (start + step * i);
                sinData[i] = new DataPoint(x, Math.Sin(x));
            }

            //sin(x)
            var sinStemSeries = new StemSeries
            {
                MarkerStroke = OxyColors.Green,
                MarkerType = MarkerType.Circle
            };
            sinStemSeries.Points.AddRange(sinData);

            plotModel.Series.Add(sinStemSeries);
            return plotModel;
        }
        private void OnShowLineChartClicked(object sender, EventArgs e)
        {
            var lineChart = CreateLineChart();
            ConfigurePlotView(lineChart);
        }
        private void OnShowBarChartClicked(object sender, EventArgs e)
        {
            var barChart = CreateBarChart();
            ConfigurePlotView(barChart);
        }
        private void OnShowPieChartClicked(object sender, EventArgs e)
        {
            var pieChart = CreatePieChart();
            ConfigurePlotView(pieChart);
        }
        private void OnShowGroupedBarChartClicked(object sender, EventArgs e)
        {
            var groupedBarChart = CreateGroupedBarChart();
            ConfigurePlotView(groupedBarChart);
        }
        private void OnShowRectangleChartClicked(object sender, EventArgs e)
        {
            var rectangleChart = CreateRectangleChart();
            ConfigurePlotView(rectangleChart);
        }
        private void OnShowBitmapChartClicked(object sender, EventArgs e)
        {
            var bitmapChart = CreateBitmapChart();
            ConfigurePlotView(bitmapChart);
        }
        private async void OnExportToPdfClicked(object sender, EventArgs e)
        {
            try
            {
                

                // Define the path to save the PDF
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var pdfPath = Path.Combine(documentsPath, "chart.pdf");

                // Export to PDF
                using (var stream = File.Create(pdfPath))
                {
                    var exporter = new PdfExporter { Width = 600, Height = 400 };
                    exporter.Export(PlotModel, stream);
                }
                DependencyService.Get<IPdfOpener>().OpenPdf(pdfPath);
               // await DisplayAlert("Success", $"Chart exported to PDF at {pdfPath}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        private void OnZoomInClicked(object sender, EventArgs e)
        {
            Zoom(1.5);
        }
        private void OnZoomOutClicked(object sender, EventArgs e)
        {
            Zoom(1 / 1.5);
        }
        private void AddLegend(PlotModel plotModel)
        {
            var legend = new Legend
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.TopRight,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorderThickness = 0
            };
            plotModel.Legends.Add(legend);
        }
        private void Zoom(double factor)
        {
            foreach (var axis in currentPlotModel.Axes)
            {
                var range = axis.ActualMaximum - axis.ActualMinimum;
                var mid = (axis.ActualMaximum + axis.ActualMinimum) / 2;
                var newMin = mid - (range / 2) / factor;
                var newMax = mid + (range / 2) / factor;

                axis.Zoom(newMin, newMax);
            }
            currentPlotModel.InvalidatePlot(false); // Correct call on the model, not plotView
        }
    }
}
