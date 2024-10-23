using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace Animation3D.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient
    {
        public MainWindowViewModel()
        {
            FirstBlockPositions = "0,0,1 4,0,1 4,1,1 0,1,1 0,0,-2 4,0,-2 4,1,-2 0,1,-2";
            SecondBlockPositions = "1.5,1,0 2.5,1,0 2.5,4,0 1.5,4,0 1.5,1,-1 2.5,1,-1 2.5,4,-1 1.5,4,-1";
            ThirdBlockPositions = "1.5,3,1 2.5,3,1 2.5,4,1 1.5,4,1 1.5,3,0 2.5,3,0 2.5,4,0 1.5,4,0";

            FirstBlockEdges = SplitPosition(FirstBlockPositions);
            SecondBlockEdges = SplitPosition(SecondBlockPositions);
            ThirdBlockEdges = SplitPosition(ThirdBlockPositions);

            MoveUpCommand = new RelayCommand(MoveUp);
            MoveLeftCommand = new RelayCommand(MoveLeft);
            MoveDownCommand = new RelayCommand(MoveDown);
            MoveRightCommand = new RelayCommand(MoveRight);
            MoveCloserCommand = new RelayCommand(MoveCloser);
            MoveFurtherCommand = new RelayCommand(MoveFurther);
        }

        public IRelayCommand MoveUpCommand { get; }
        public IRelayCommand MoveLeftCommand { get; }
        public IRelayCommand MoveDownCommand { get; }
        public IRelayCommand MoveRightCommand { get; }
        public IRelayCommand MoveCloserCommand { get; }
        public IRelayCommand MoveFurtherCommand { get; }

        [ObservableProperty]
        private string _firstBlockPositions;

        [ObservableProperty]
        private string _secondBlockPositions;

        [ObservableProperty]
        private string _thirdBlockPositions;

        private string[]? _firstBlockEdges;

        private string[]? _secondBlockEdges;

        private string[]? _thirdBlockEdges;

        public string[]? FirstBlockEdges
        {
            get => _firstBlockEdges;
            set => SetProperty(ref _firstBlockEdges, value);
        }

        public string[]? SecondBlockEdges
        {
            get => _secondBlockEdges;
            set => SetProperty(ref _secondBlockEdges, value);
        }

        public string[]? ThirdBlockEdges
        {
            get => _thirdBlockEdges;
            set => SetProperty(ref _thirdBlockEdges, value);
        }

        public static string[]? SplitPosition(string position)
        {
            return position.Split(" ");
        }

        public static string UpdatePosition(string[] edges)
        {
            string? var = "";
            for (int i = 0; i < edges.Length; i++)
            {
                var += edges[i] + " ";
            }
            return var.Substring(0, var.Length - 1);
        }

        public double GetXValue(string edge)
        {
            string[] values = edge.Split(",");
            return double.Parse(values[0], CultureInfo.InvariantCulture);
        }

        public double GetYValue(string edge)
        {
            string[] values = edge.Split(",");
            return double.Parse(values[1], CultureInfo.InvariantCulture);
        }
        public double GetZValue(string edge)
        {
            string[] values = edge.Split(",");
            return double.Parse(values[2], CultureInfo.InvariantCulture);
        }

        public void MoveUp()
        {
            if (double.Parse(_thirdBlockEdges![0].Split(",")[1], CultureInfo.InvariantCulture) >= 3)
            {
                return;
            }
            MoveYPlus(ThirdBlockEdges!);
            ThirdBlockPositions = UpdatePosition(ThirdBlockEdges!);
        }

        public void MoveLeft()
        {
            if (double.Parse(_thirdBlockEdges![0].Split(",")[0], CultureInfo.InvariantCulture) <= 0)
            {
                return;
            }
            MoveXMinus(ThirdBlockEdges!);
            MoveXMinus(SecondBlockEdges!);
            ThirdBlockPositions = UpdatePosition(ThirdBlockEdges!);
            SecondBlockPositions = UpdatePosition(SecondBlockEdges!);
        }

        public void MoveDown()
        {
            if (double.Parse(_thirdBlockEdges![0].Split(",")[1], CultureInfo.InvariantCulture) <= 1)
            {
                return;
            }
            MoveYMinus(ThirdBlockEdges!);
            ThirdBlockPositions = UpdatePosition(ThirdBlockEdges!);
        }

        public void MoveRight()
        {
            if (double.Parse(_thirdBlockEdges![0].Split(",")[0], CultureInfo.InvariantCulture) >= 3)
            {
                return;
            }
            MoveXPlus(ThirdBlockEdges!);
            MoveXPlus(SecondBlockEdges!);
            ThirdBlockPositions = UpdatePosition(ThirdBlockEdges!);
            SecondBlockPositions = UpdatePosition(SecondBlockEdges!);
        }

        public void MoveCloser()
        {
            if (double.Parse(_thirdBlockEdges![0].Split(",")[2], CultureInfo.InvariantCulture) >= 2)
            {
                return;
            }
            MoveZPlus(ThirdBlockEdges!);
            MoveZPlus(SecondBlockEdges!);
            ThirdBlockPositions = UpdatePosition(ThirdBlockEdges!);
            SecondBlockPositions = UpdatePosition(SecondBlockEdges!);
        }

        public void MoveFurther()
        {
            if (double.Parse(_thirdBlockEdges![0].Split(",")[2], CultureInfo.InvariantCulture) <= 0)
            {
                return;
            }
            MoveZMinus(ThirdBlockEdges!);
            MoveZMinus(SecondBlockEdges!);
            ThirdBlockPositions = UpdatePosition(ThirdBlockEdges!);
            SecondBlockPositions = UpdatePosition(SecondBlockEdges!);
        }

        public string[] MoveXPlus(string[] edges)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = (GetXValue(edges[i]) + 0.1).ToString(CultureInfo.InvariantCulture) + "," + GetYValue(edges[i]).ToString(CultureInfo.InvariantCulture) + "," + GetZValue(edges[i]).ToString(CultureInfo.InvariantCulture);
            }
            return edges;
        }

        public string[] MoveXMinus(string[] edges)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = (GetXValue(edges[i]) - 0.1).ToString(CultureInfo.InvariantCulture) + "," + GetYValue(edges[i]).ToString(CultureInfo.InvariantCulture) + "," + GetZValue(edges[i]).ToString(CultureInfo.InvariantCulture);
            }
            return edges;
        }

        public string[] MoveYPlus(string[] edges)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = GetXValue(edges[i]).ToString(CultureInfo.InvariantCulture) + "," + (GetYValue(edges[i]) + 0.1).ToString(CultureInfo.InvariantCulture) + "," + GetZValue(edges[i]).ToString(CultureInfo.InvariantCulture);
            }
            return edges;
        }

        public string[] MoveYMinus(string[] edges)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = GetXValue(edges[i]).ToString(CultureInfo.InvariantCulture) + "," + (GetYValue(edges[i]) - 0.1).ToString(CultureInfo.InvariantCulture) + "," + GetZValue(edges[i]).ToString(CultureInfo.InvariantCulture);
            }
            return edges;
        }

        public string[] MoveZPlus(string[] edges)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = GetXValue(edges[i]).ToString(CultureInfo.InvariantCulture) + "," + GetYValue(edges[i]).ToString(CultureInfo.InvariantCulture) + "," + (GetZValue(edges[i]) + 0.1).ToString(CultureInfo.InvariantCulture);
            }
            return edges;
        }

        public string[] MoveZMinus(string[] edges)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = GetXValue(edges[i]).ToString(CultureInfo.InvariantCulture) + "," + GetYValue(edges[i]).ToString(CultureInfo.InvariantCulture) + "," + (GetZValue(edges[i]) - 0.1).ToString(CultureInfo.InvariantCulture);
            }
            return edges;
        }
    }
}
