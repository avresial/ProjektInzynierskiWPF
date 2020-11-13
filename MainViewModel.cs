using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using System.Collections.ObjectModel;
using System.Threading;
using GalaSoft.MvvmLight.Command;

namespace ProjektInzynierskiWPF
{
    class MainViewModel : ViewModelBase
    {
        private double _CellSize;
        public double CellSize
        {
            get { return _CellSize; }
            set
            {
                _CellSize = value;
                RaisePropertyChanged(nameof(CellSize));
            }
        }

        private double _WindowSize = 600;
        public double WindowSize
        {
            get { return _WindowSize; }
            set
            {
                _WindowSize = value;
                RaisePropertyChanged(nameof(WindowSize));
            }
        }

        private Board _Board;
        public Board Board
        {
            get { return _Board; }
            set
            {
                _Board = value;
                RaisePropertyChanged(nameof(Board));
            }
        }

        private Random _RandomSeed = new Random();
        public Random RandomSeed
        {
            get { return _RandomSeed; }
            set { _RandomSeed = value; }
        }

        private ObservableCollection<BoardElement> _ItemsControllList = new ObservableCollection<BoardElement>();
        public ObservableCollection<BoardElement> ItemsControllList
        {
            get { return _ItemsControllList; }
            private set
            {
                if (_ItemsControllList == value)
                    return;

                _ItemsControllList = value;
                RaisePropertyChanged(nameof(ItemsControllList));
            }
        }

        private RelayCommand _randomisePoints;
        public RelayCommand randomisePoints
        {
            get
            {
                return _randomisePoints ?? (_randomisePoints = new RelayCommand(
                () =>
                {
                    if (Board != null)
                    {
                        Board = new Board(15);
                        Board.randomPoints(40);

                        FactorAlgirithm.IndexFindingPathAlgorithm(Board);
                        drawMatrixOnWindow();
                    }
                },
                () =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand _SetPointsFromExampleNr1Command;
        public RelayCommand SetPointsFromExampleNr1Command
        {
            get
            {
                return _SetPointsFromExampleNr1Command ?? (_SetPointsFromExampleNr1Command = new RelayCommand(
                () =>
                {
                    if (Board != null)
                    {
                        Board = new Board(6);
                        Board.SetPointsFromExampleNr1();
                        FactorAlgirithm.IndexFindingPathAlgorithm(Board);
                        drawMatrixOnWindow();
                    }
                },
                () =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand _SetPointsFromExampleNr2Command;
        public RelayCommand SetPointsFromExampleNr2Command
        {
            get
            {
                return _SetPointsFromExampleNr2Command ?? (_SetPointsFromExampleNr2Command = new RelayCommand(
                () =>
                {
                    if (Board != null)
                    {
                        Board = new Board(6);
                        Board.SetPointsFromExampleNr2();
                        FactorAlgirithm.IndexFindingPathAlgorithm(Board);
                        drawMatrixOnWindow();
                    }
                },
                () =>
                {
                    return true;
                }));
            }
        }

        public MainViewModel()
        {

            Board = new Board(15);
            Board.randomPoints(40);

            FactorAlgirithm.IndexFindingPathAlgorithm(Board);

            drawMatrixOnWindow();
        }
        public void drawMatrixOnWindow()
        {
            ItemsControllList.Clear();
            CellSize = (WindowSize) / Board.Size[0];
            foreach (int value in Board.GetMatrixElements())
            {
                if (value != 0)
                {
                    BoardElement boardElement = new BoardElement(Board.Deserters.Find(x => x.Value == value).Color, value);
                    ItemsControllList.Add(boardElement);
                }
                else
                {
                    BoardElement boardElement = new BoardElement(new SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 242, 242)), value);
                    ItemsControllList.Add(boardElement);
                }
            }
        }
    }
}
