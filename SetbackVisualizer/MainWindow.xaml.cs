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

namespace SetbackVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var inputUnitType = "feet";

            //double inputLotWidthInUnits = 13.41;
            //double inputLotHeightInUnits = 30.18;

            double inputLotWidthInUnits = 150;
            double inputLotHeightInUnits = 120;

            //double? inputMinimumFrontYardDepthInUnits = 6.1;
            //double? inputMinimumFrontYardDepthInPercent = null;

            //double? inputMinimumSideYardDepthInUnits = null;
            //double? inputMinimumSideYardDepthInPercent = 20;

            //double? inputMinimumBackYardDepthInUnits = 10.7;
            //double? inputMinimumBackYardDepthInPercent = null;

            double? inputMinimumFrontYardDepthInUnits = null;
            double? inputMinimumFrontYardDepthInPercent = 20;

            double? inputMinimumSideYardDepthInUnits = null;
            double? inputMinimumSideYardDepthInPercent = 20;

            double? inputMinimumBackYardDepthInUnits = null;
            double? inputMinimumBackYardDepthInPercent = 45;

            double minimumFrontYardInUnits = inputMinimumFrontYardDepthInUnits ?? inputMinimumFrontYardDepthInPercent.Value / 100 * inputLotHeightInUnits;
            double minimumSideYardInUnits = inputMinimumSideYardDepthInUnits ?? inputMinimumSideYardDepthInPercent.Value / 100 * inputLotWidthInUnits;
            double minimumBackYardInUnits = inputMinimumBackYardDepthInUnits ?? inputMinimumBackYardDepthInPercent.Value / 100 * inputLotHeightInUnits;

            double buildingEnvelopeWidthInUnits = (inputLotWidthInUnits - minimumSideYardInUnits - minimumSideYardInUnits);
            double buildingEnvelopeHeightInUnits = (inputLotHeightInUnits - minimumFrontYardInUnits - minimumBackYardInUnits);

            double lotAreaInSquareUnits = inputLotHeightInUnits * inputLotWidthInUnits;
            double buildingEnvelopeAreaInSquareUnits = buildingEnvelopeWidthInUnits * buildingEnvelopeHeightInUnits;

            double lotDrawWidth = 0;
            double lotDrawHeight = 0;

            //if (mainCanvas.Width != mainCanvas.Height)
            //    throw new Exception("canvas is not a square");

            if(inputLotWidthInUnits > inputLotHeightInUnits)
            {
                lotDrawWidth = mainCanvas.Width;
                lotDrawHeight = mainCanvas.Height * inputLotHeightInUnits / inputLotWidthInUnits;
            }
            else
            {
                lotDrawWidth = mainCanvas.Width * inputLotWidthInUnits / inputLotHeightInUnits;
                lotDrawHeight = mainCanvas.Height;
            }

            var canvasWidth = mainCanvas.Width;

            Rectangle lotRectangle = new Rectangle();
            lotRectangle.Width = lotDrawWidth;
            lotRectangle.Height = lotDrawHeight;
     
            lotRectangle.Stroke = Brushes.Blue;
            lotRectangle.StrokeThickness = 2;
            //exampleRectangle.Fill;
            mainCanvas.Children.Insert(0, lotRectangle);

            

            Rectangle buildingEnvelopeRectangle = new Rectangle();

            buildingEnvelopeRectangle.Width = lotDrawWidth * buildingEnvelopeWidthInUnits / inputLotWidthInUnits;
            buildingEnvelopeRectangle.Height = lotDrawHeight * buildingEnvelopeHeightInUnits / inputLotHeightInUnits;

            buildingEnvelopeRectangle.Stroke = Brushes.Blue;
            buildingEnvelopeRectangle.StrokeThickness = 2;
            SolidColorBrush myBrush = new SolidColorBrush(Colors.LightBlue);
            buildingEnvelopeRectangle.Fill = myBrush;

            mainCanvas.Children.Insert(1, buildingEnvelopeRectangle);

            Canvas.SetLeft(buildingEnvelopeRectangle, lotDrawWidth* minimumSideYardInUnits / inputLotWidthInUnits);
            Canvas.SetTop(buildingEnvelopeRectangle, lotDrawHeight * minimumFrontYardInUnits / inputLotHeightInUnits);


            var lotCoverage = (buildingEnvelopeRectangle.Width * buildingEnvelopeRectangle.Height) / (lotRectangle.Width * lotRectangle.Height);

            var outputBuilder = new StringBuilder();
            outputBuilder.AppendLine($"Lot size: {lotAreaInSquareUnits:N} square {inputUnitType}");
            outputBuilder.AppendLine($"Building envelope size: {buildingEnvelopeAreaInSquareUnits:N} square {inputUnitType}");
            outputBuilder.AppendLine($"Lot coverage: {buildingEnvelopeAreaInSquareUnits / lotAreaInSquareUnits:P}");

            textOutputBlock.Text = outputBuilder.ToString();

            Console.WriteLine($"Lot size: {lotAreaInSquareUnits} square units");
            Console.WriteLine($"Building envelope size: {buildingEnvelopeAreaInSquareUnits} square units");
            Console.WriteLine($"Lot coverage: {buildingEnvelopeAreaInSquareUnits / lotAreaInSquareUnits:P}%");

        }
    }
}
