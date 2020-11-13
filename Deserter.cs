using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;

namespace ProjektInzynierskiWPF
{
    public class Deserter : ViewModelBase
    {
        private Board _board;
        public Board Board
        {
            get { return _board; }
            set { _board = value; }
        }

        private Point _OriginPoint;
        public Point OriginPoint
        {
            get { return _OriginPoint; }
            set { _OriginPoint = value; }
        }

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        
        public bool IsFinished
        {
            get {
                if (TopFactor == 0 && BottomFactor == 0)
                    return true;               
                return false;
            }         
        }    

        private List<Point> _Points;
        public List<Point> Points
        {
            get { return _Points; }
            set
            {
                _Points = value;
            }
        }

        private System.Windows.Media.Brush _Color;
        public System.Windows.Media.Brush Color
        {
            get
            {
                return _Color;
            }

            set
            {
                _Color = value;
                RaisePropertyChanged(nameof(Color));
            }
        }

        public Double TopFactor;
        public Double BottomFactor;
        public Double LeftFactor;
        public Double RightFactor;

        public Deserter(Point location, Board board, Brush color = null, int value = 0)
        {
            Board = board;
            Points = new List<Point>();
            OriginPoint = location;
            Color = color;

            if (Color == null)
                Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)Board.RandomSeed.Next(256), (byte)Board.RandomSeed.Next(256), (byte)Board.RandomSeed.Next(256))); 
            
            if (value == 0)
            {
                Value = Board.Deserters.Count + 1;
            }
            else
            {
                Value = value;
            }

            Points.Add(location);
            ProssessFactors();
        }

        public void ProssessFactors()
        {
            Point lastPoint = Points[Points.Count - 1];

            if (lastPoint.Y != 0 && lastPoint.Y < Board.Size[0] - 1 && lastPoint.X != 0 && lastPoint.X < Board.Size[1] - 1)
            {

                if (Board.Matrix[lastPoint.X, lastPoint.Y + 1] != 0)
                {
                    TopFactor = 1;
                }
                else
                {
                   // TopFactor = Convert.ToDouble(lastPoint.Y) / Convert.ToDouble(Board.Size[0] - 1);
                    TopFactor = (Convert.ToDouble(Board.Size[0] - 1) - Convert.ToDouble(Points[Points.Count - 1].Y)) / Convert.ToDouble(Board.Size[0] - 1);
                }

                if (Board.Matrix[lastPoint.X, lastPoint.Y - 1] != 0)
                {
                    BottomFactor = 1;
                }
                else
                {
               //     BottomFactor = (Convert.ToDouble(Board.Size[0] - 1) - Convert.ToDouble(Points[Points.Count - 1].Y)) / Convert.ToDouble(Board.Size[0] - 1);
                    BottomFactor = Convert.ToDouble(lastPoint.Y) / Convert.ToDouble(Board.Size[0] - 1);
                }

                if (Board.Matrix[lastPoint.X + 1, lastPoint.Y] != 0)
                {
                    RightFactor = 1;
                }
                else
                {
              //      RightFactor = Convert.ToDouble(Points[Points.Count - 1].X) / Convert.ToDouble(Board.Size[1] - 1);
                    RightFactor = (Convert.ToDouble(Board.Size[1] - 1) - Convert.ToDouble(Points[Points.Count - 1].X)) / Convert.ToDouble(Board.Size[1] - 1);

                }

                if (Board.Matrix[lastPoint.X - 1, lastPoint.Y] != 0)
                {
                    LeftFactor = 1;
                }
                else
                {
              //      LeftFactor = (Convert.ToDouble(Board.Size[1] - 1) - Convert.ToDouble(Points[Points.Count - 1].X)) / Convert.ToDouble(Board.Size[1] - 1);
                    LeftFactor = Convert.ToDouble(Points[Points.Count - 1].X) / Convert.ToDouble(Board.Size[1] - 1);
                }

            }
            else
            {
                TopFactor = 0;
                BottomFactor = 0;
                LeftFactor = 0;
                RightFactor = 0;
            }

        }
        public void Move(int x, int y)
        {
            Point lastPoint = Points[Points.Count - 1];
            if (lastPoint.Y + y >= 0 && lastPoint.Y + y <= Board.Size[0] - 1 && lastPoint.X + x >= 0 && lastPoint.X + x <= Board.Size[1] - 1)
            {
                if (Board.Matrix[lastPoint.X + x, lastPoint.Y + y] == 0)
                {

                    Points.Add(new Point(lastPoint.X + x, lastPoint.Y + y));
                    Board.Matrix[lastPoint.X + x, lastPoint.Y + y] = Value;
                    Board.Iteration++;
                    ProssessFactors();
                }
            }
        }      
    }
}

