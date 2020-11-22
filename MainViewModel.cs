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

        private int _NewDeserterLocationX;
        public int NewDeserterLocationX
        {
            get { return _NewDeserterLocationX; }
            set
            {
                _NewDeserterLocationX = value;
                RaisePropertyChanged(nameof(NewDeserterLocationX));
            }
        }

        private int _NewDeserterLocationY;
        public int NewDeserterLocationY
        {
            get { return _NewDeserterLocationY; }
            set
            {
                _NewDeserterLocationY = value;
            }
        }

        private int _NewBoardSize;
        public int NewBoardSize
        {
            get { return _NewBoardSize; }
            set
            {
                _NewBoardSize = value;
            }
        }

        private int _AmountOfNewRandomPoints = 1;
        public int AmountOfNewRandomPoints
        {
            get { return _AmountOfNewRandomPoints; }
            set
            {
                _AmountOfNewRandomPoints = value;
            }
        }

        private Board _Board;
        public Board Board
        {
            get { return _Board; }
            set
            {
                if (_Board == value)
                    return;
                _Board = value;               
                RaisePropertyChanged(nameof(Board));
            }
        }

        private StatisticsItemsControlModel _StatisticsItemsControlModel;
        public StatisticsItemsControlModel StatisticsItemsControlModel
        {
            get { return _StatisticsItemsControlModel; }
            set { _StatisticsItemsControlModel = value; }
        }

        private ObservableCollection<StatisticsItemsControlElement> _StatisticsItemsControllList;
        public ObservableCollection<StatisticsItemsControlElement> StatisticsItemsControllList
        {
            get { return _StatisticsItemsControllList; }
            private set
            {
                if (_StatisticsItemsControllList == value)
                    return;

                _StatisticsItemsControllList = value;
                RaisePropertyChanged(nameof(StatisticsItemsControllList));
            }
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
                        Board.randomPoints(AmountOfNewRandomPoints);
                        Board.AlreadyCalculated = false;

                        drawMatrixOnWindow();
                    }
                    else {
                        Board = new Board(15);
                        Board.randomPoints(AmountOfNewRandomPoints);

                        Board.AlreadyCalculated = false;

                        drawMatrixOnWindow();
                    }
                },
                () =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand _FindPaths;
        public RelayCommand FindPaths
        {
            get
            {
                return _FindPaths ?? (_FindPaths = new RelayCommand(
                () =>
                {
                    if (Board != null)
                    {
                        if (Board != null && Board.AlreadyCalculated == false)
                        {
                            FactorAlgirithm.IndexFindingPathAlgorithm(Board);
                            drawMatrixOnWindow();
                        }
                        
                    }
                },
                () =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand _AddDesserter;
        public RelayCommand AddDesserter
        {
            get
            {
                return _AddDesserter ?? (_AddDesserter = new RelayCommand(
                () =>
                {
                    if (Board != null)
                    {
                        Deserter deserter = new Deserter(new Point(NewDeserterLocationX, NewDeserterLocationY), Board);

                        Board.AddNewDeserter(deserter);
                        drawMatrixOnWindow();
                    }
                },
                () =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand _ResizeBoard;
        public RelayCommand ResizeBoard
        {
            get
            {
                return _ResizeBoard ?? (_ResizeBoard = new RelayCommand(
                () =>
                {
                    if (Board != null)
                    {

                        if (NewBoardSize > 0 && NewBoardSize <= Board.MaxSize)
                        {
                            Board = new Board(NewBoardSize);
                        }
                        else 
                        {
                            string message =    "Przekroczono maksymalny rozmiar  \n";
                            message +=          "Poprawny rozmiar mieści się w zakresie \n\n";
                            message +=          "                              0 - " + Board.MaxSize;

                            string title = "Błąd";
                            MessageBox.Show(message, title);
                        }
                        

                  
                        drawMatrixOnWindow();
                    }
                },
                () =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand _CleanBoard;
        public RelayCommand CleanBoard
        {
            get
            {
                return _CleanBoard ?? (_CleanBoard = new RelayCommand(
                () =>
                {
                    if (Board != null)
                    {
                        Board.CleanBoard();
                        Board.AlreadyCalculated = false;
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
            Board.SetStartPoints();

           // FactorAlgirithm.IndexFindingPathAlgorithm(Board);
           
            drawMatrixOnWindow();
        }
        void fillStatisticsList(Board board)
        {
            StatisticsItemsControllList = new ObservableCollection<StatisticsItemsControlElement>();

            foreach (Deserter deserter in board.Deserters)
            {
                StatisticsItemsControllList.Add(new StatisticsItemsControlElement(deserter.Value, deserter.Points.Count, deserter.IsFinished));
            }
        }
        public void drawMatrixOnWindow()
        {
            ItemsControllList.Clear();
            CellSize = (WindowSize) / Board.Size[0];
            fillStatisticsList(Board);
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
