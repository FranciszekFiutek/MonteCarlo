using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using MonteCarloLibrary;

namespace PiNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<double> numCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            precisionBox.Focus();
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int precision = Int32.Parse(precisionBox.Text);

                numCollection = new List<double>();
                numList.Items.Clear();
                resultBlock.Text = null;

                for (int i = circleCanva.Children.Count - 1; i > 0; i--)
                {
                    circleCanva.Children.RemoveAt(i);
                }

                if (operationList.SelectedIndex == 0)
                {
                    CallPiCount(precision);
                }
                else
                {
                    if (formulaBox.Text == "")
                    {
                        formulaBox.Text = "-x*x+1";
                    }
                    CallCountIntegral(precision);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Błędne dane wejściowe");
            }

        }

        private async void CallPiCount(int precision)
        {
            Pi piCounter = new Pi(precision);

            piCounter.Randomize();
            piCounter.Count(numCollection);

            if (showOperationsCheckbox.IsChecked == true)
            {
                for (int i = 0; i < precision; i++) 
                {
                    await Task.Delay(1);
                    numList.Items.Add(numCollection[i]);
                    DrawPoint(piCounter, i);
                }
            }
            resultBlock.Text = numCollection[numCollection.Count - 1].ToString();
        }

        private void CallCountIntegral(int precision)
        {
            string input = xStartBox.Text;
            input.Replace('.', ',');
            double xStart = Double.Parse(xStartBox.Text), xEnd = Double.Parse(xEndBox.Text);
            Integral integralCounter = new Integral(precision, xStart, xEnd, formulaBox.Text);

            integralCounter.Count(numCollection);

            if (showOperationsCheckbox.IsChecked == true)
            {
                for (int i = 0; i < precision-1; i++)
                {
                    numList.Items.Add(numCollection[i].ToString("F20"));
                }
            }
            resultBlock.Text = numCollection[numCollection.Count - 1].ToString("F20");
        }

        private void DrawPoint(Pi piCounter, int index)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = Brushes.Red;
            ellipse.StrokeThickness = 0;
            ellipse.Height = 5;
            ellipse.Width = 5;

            circleCanva.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, piCounter.Points[index].XCoordinate * 400 > 395 ? 395 : piCounter.Points[index].XCoordinate * 400);
            Canvas.SetBottom(ellipse, piCounter.Points[index].YCoordinate * 400 > 395 ? 395 : piCounter.Points[index].YCoordinate * 400);
        }

        private void operationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (operationList.SelectedIndex == 1)
            {
                formulaBox.IsEnabled = true;

                precisionBox.SetValue(Grid.ColumnSpanProperty, 1);

                xStartBlock.Visibility = Visibility.Visible;
                xStartBox.Visibility = Visibility.Visible;

                xEndBlock.Visibility = Visibility.Visible;
                xEndBox.Visibility = Visibility.Visible;

                mainGrid.ColumnDefinitions[9].Width = new GridLength(0);
                mainGrid.ColumnDefinitions[10].Width = new GridLength(0);

                circleCanva.Visibility = Visibility.Hidden;
                Width = 900;
            }
            else if (operationList.SelectedIndex == 0)
            {
                formulaBox.IsEnabled = false;

                Ellipse ellipse = new Ellipse();
                ellipse.StrokeThickness = 1;
                ellipse.Stroke = Brushes.White;
                ellipse.Height = 400;
                ellipse.Width = 400;

                circleCanva.Children.Add(ellipse);

                precisionBox.SetValue(Grid.ColumnSpanProperty, 5);

                xStartBlock.Visibility = Visibility.Hidden;
                xStartBox.Visibility = Visibility.Hidden;

                xEndBlock.Visibility = Visibility.Hidden;
                xEndBox.Visibility = Visibility.Hidden;

                mainGrid.ColumnDefinitions[9].Width = new GridLength(10, GridUnitType.Star);
                mainGrid.ColumnDefinitions[10].Width = new GridLength(1, GridUnitType.Star);

                circleCanva.Visibility = Visibility.Visible;
                Width = 1180;
            }
        }
    }
}