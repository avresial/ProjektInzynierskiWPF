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


        private List<BoardElement> _ItemsControllList = new List<BoardElement>();
        public List<BoardElement> ItemsControllList
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


        public MainViewModel()
        {

            Board = new Board(15);
            Random rnd = new Random();

            CellSize = (WindowSize) / Board.Size[0];
            //SetPointsFromExampleNr1(board);

            randomPoints(Board, 30);
            FactorAlgirithm.IndexFindingPathAlgorithm(Board);
            drawMatrixOnWindow();


        }
        void drawMatrixOnWindow()
        {
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

        static void randomPoints(Board board, int amount = 0)
        {
            int size = board.Size[0];
            if (amount == 0)
            {
                amount = size;
            }
            Random rnd = new Random();
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
                    Brush Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
                    board.AddNewDeserter(new Deserter(new Point(rndX, rndY), board, Color));
                }


            }
        }

        public Brush getRandomColor()
        {
            Random rnd = new Random();
            return new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
        }
        void SetPointsFromExampleNr1(Board board)
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
    }
}
