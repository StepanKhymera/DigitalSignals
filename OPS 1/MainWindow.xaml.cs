using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace OPS_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           InitializeComponent();


            Chart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Fourier",
                    Fill = Brushes.Transparent,
                    PointGeometry = null,
                    Stroke = Brushes.Green
                },
                new LineSeries
                {

                    Title = "Original",
                    Fill = Brushes.Transparent,
                    //PointGeometry = null,
                    Stroke = Brushes.Blue
                },
                new LineSeries
                {

                    Title = "Original",
                    Fill = Brushes.Transparent,
                    PointGeometry = null,
                    Stroke = Brushes.Black
                }   


            };
            Chart.Series[0].Values = new ChartValues<ObservablePoint>();
            Chart.Series[2].Values = new ChartValues<ObservablePoint>();

            Chart.Series[1].Values = new ChartValues<ObservablePoint>();
           
        }

    private void Button_Click(object sender, RoutedEventArgs e)
        {
            FunctionBuilder fuc = new FunctionBuilder();
            Chart.Series[0].Values.Clear();
            Chart.Series[1].Values.Clear();
            Chart.Series[2].Values.Clear();
            double delta = 0;

            for (int j = 0; j < 6; ++j)
            {
                Chart.Series[1].Values.Add(new ObservablePoint
                {
                    X = fuc.X[j],
                    Y = fuc.Y[j]
                });
                delta += Math.Abs( fuc.Y[j] - fuc.Fourier(fuc.X[j] + fuc.x_step / 2.0, int.Parse(N.Text)));
                Chart.Series[1].Values.Add(new ObservablePoint
                {
                    X = double.NaN,
                    Y = double.NaN
                });
            }

            Delta.Content = $"Delta = { Math.Round( delta /= 6, 5)}";
            int i = 0;
            for (double x = -1.2 * Math.PI; x <= Math.PI; x += 0.1, ++i)
            {
                double y2 = fuc.Fourier(x, int.Parse(N.Text));
                double y3 = fuc.MNK(x);

                Chart.Series[0].Values.Add(new ObservablePoint
                {
                    X = x - fuc.x_step / 2.0,
                    Y = y2
                });

                //РОЗКОМЕНТУЙ ЯКЩО ПОТРІБНО ОБИДВА ГРАФІКИ ОДНАЧАСНО
                //Chart.Series[2].Values.Add(new ObservablePoint
                //{
                //    X = x,
                //    Y = y3

                //});

            }

        }
    

    private void Button_Click_MNK(object sender, RoutedEventArgs e)
    {
        FunctionBuilder fuc = new FunctionBuilder();
        Chart.Series[0].Values.Clear();
        Chart.Series[1].Values.Clear();
        Chart.Series[2].Values.Clear();
            double delta = 0;

            for (int j = 0; j < 6; ++j)
            {
                Chart.Series[1].Values.Add(new ObservablePoint
                {
                    X = fuc.X[j],
                    Y = fuc.Y[j]
                });
                delta += Math.Abs(fuc.Y[j] - fuc.MNK(fuc.X[j]));
                Chart.Series[1].Values.Add(new ObservablePoint
                {
                    X = double.NaN,
                    Y = double.NaN
                });
            }

            Delta.Content = $"Delta = { Math.Round(delta /= 6, 5)}";

            int i = 0;
        for (double x = -1.2 * Math.PI; x <= Math.PI; x += 0.1, ++i)
        {
            double y3 = fuc.MNK(x);

           
            Chart.Series[2].Values.Add(new ObservablePoint
            {
                X = x,
                Y = y3

            });

        }

    }
    }
}
