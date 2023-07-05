using Arm64.DesktopApp.WPF.Helpers;
using Arm64.DesktopApp.WPF.Models;
using System.Collections.ObjectModel;

namespace Arm64.DesktopApp.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string computationTime = "";

        public string ComputationTime
        {
            get => computationTime;
            set => SetProperty(ref computationTime, value);
        }

        private readonly List<DataPoint2d> computationTimeHistory = new();

        public ObservableCollection<DataPoint2d> DataPoints { get; set; } =
            new ObservableCollection<DataPoint2d>();

        private SimpleCommand? runCalculationsCommand;
        public SimpleCommand RunCalculationsCommand
        {
            get => runCalculationsCommand ??= new SimpleCommand((object? parameter) =>
            {
                var computationTime = PerformanceHelper.MeasurePerformance(
                    MatrixHelper.SquareMatrixMultiplication, executionCount: 2);

                ComputationTime = string.Format($"Platform: " +
                        $"{Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")}\n" +
                        $"Computation time: {computationTime:f2} ms");

                // Add computation time to the history
                computationTimeHistory.Add(new DataPoint2d
                {
                    X = computationTimeHistory.Count + 1,
                    Y = computationTime
                });
            });
        }

        private SimpleCommand? plotResultsCommand;
        public SimpleCommand PlotResultsCommand
        {
            get => plotResultsCommand ??= new SimpleCommand((object? parameter) =>
            {
                DataPoints.Clear();

                // Copy computation time history to DataPoints collection
                foreach (DataPoint2d point in computationTimeHistory)
                {
                    DataPoints.Add(point);
                }
            });
        }
    }
}
