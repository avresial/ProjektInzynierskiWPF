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
                        randomPoints(Board, 40);
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
                        SetPointsFromExampleNr1(Board);
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
                        SetPointsFromExampleNr2(Board);
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
            Random rnd = new Random();

            
            //SetPointsFromExampleNr1(board);

            randomPoints(Board, 40);
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
;
        }

        public void randomPoints(Board board, int amount = 0)
        {
            int size = board.Size[0];
            if (amount == 0)
            {
                amount = size;
            }
            Random rnd = RandomSeed;
            for (int i = 0; i < amount; i++)
            {
                Point rndPoint = new Point(rnd.Next(0, size - 1), rnd.Next(0, size - 1));

                int rndX = rnd.Next(0, size - 1);
                int rndY = rnd.Next(0, size - 1);

                int tries = 0, maxTries = 10000;
                while (board.Matrix[rndX, rndY] != 0 && tries < maxTries)
                {
                    rndX = rnd.Next(0, size - 1);
                    rndY = rnd.Next(0, size - 1);
                    tries++;
                }

                if (tries < maxTries)
                {
                    //Brush Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
                    Brush Color = getRandomColor();
                    board.AddNewDeserter(new Deserter(new Point(rndX, rndY), board, Color));
                }
            }
        }

       
        public void SetPointsFromExampleNr1(Board board)
        {

            board.AddNewDeserter(new Deserter(new Point(0, 3), board, getRandomColor()));

            board.AddNewDeserter(new Deserter(new Point(1, 2), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(1, 3), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(1, 4), board, getRandomColor()));

            board.AddNewDeserter(new Deserter(new Point(3, 2), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(3, 3), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(3, 4), board, getRandomColor()));

            board.AddNewDeserter(new Deserter(new Point(5, 2), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(5, 3), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(5, 4), board, getRandomColor()));
        }
        public void SetPointsFromExampleNr2(Board board)
        {
            board.AddNewDeserter(new Deserter(new Point(0, 3), board, getRandomColor()));

            board.AddNewDeserter(new Deserter(new Point(1, 2), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(1, 3), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(1, 4), board, getRandomColor()));

            board.AddNewDeserter(new Deserter(new Point(3, 2), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(3, 3), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(3, 4), board, getRandomColor()));

            board.AddNewDeserter(new Deserter(new Point(5, 2), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(5, 3), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(5, 4), board, getRandomColor()));
            board.AddNewDeserter(new Deserter(new Point(4, 3), board, getRandomColor()));
        }
        public Brush getRandomColor()
        {
            
            Brush brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)RandomSeed.Next(256), (byte)RandomSeed.Next(256), (byte)RandomSeed.Next(256)));
            return brush;


        }
    }
}
