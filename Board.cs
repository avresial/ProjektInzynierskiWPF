using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjektInzynierskiWPF
{
    public class Board
    {

        private int _Iteration;
        public int Iteration
        {
            get { return _Iteration; }
            set { _Iteration = value; }
        }


        private int[,] _Matrix;
        public int[,] Matrix
        {
            get { return _Matrix; }
            set { _Matrix = value; }
        }

        private int[] _Size;
        public int[] Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        private List<Deserter> _Deserters;
        public List<Deserter> Deserters
        {
            get { return _Deserters; }
            set
            {
                _Deserters = value;

            }
        }

        public Board(int size)
        {
            Iteration = 0;
            if (size <= 3)
                size = 3;

            Deserters = new List<Deserter>();

            Size = new int[2];
            Size[0] = size;
            Size[1] = size;

            Matrix = new int[Size[0], Size[1]];
        }
        public Board()
        {
            Iteration = 0;

            Deserters = new List<Deserter>();

            Size = new int[2];
            Size[0] = 3;
            Size[1] = 3;

            Matrix = new int[Size[0], Size[1]];
        }

        public void AddNewDeserter(Deserter deserter)
        {
            Iteration++;
            Deserters.Add(deserter);
            foreach (var item in _Deserters)
            {
                this.Matrix[item.OriginPoint.X, item.OriginPoint.Y] = item.Value;
            }
        }
        public List<int> GetMatrixElements()
        {
            List<int> list = new List<int>();
           
            for (int i = Size[0] - 1; i >= 0; i--)
            {
                for (int j = 0; j < Size[1]; j++)
                {
                    list.Add(Matrix[j, i]);
                  
                }
          
            }



            return list;
        }
        public void DrawMatrix()
        {
            Console.WriteLine("---- - - - - - - - - - - - - - - - - -- -  - -- " + Iteration);
            for (int i = Size[0] - 1; i >= 0; i--)
            {
                for (int j = 0; j < Size[1]; j++)
                {
                    Console.Write("   ");
                    Console.Write(Matrix[j, i]);
                }
                Console.WriteLine(); Console.WriteLine();
            }
        }
        public void DrawMatrixMessagebox()
        {
            string konsola = "";
            for (int i = Size[0] - 1; i >= 0; i--)
            {
                for (int j = 0; j < Size[1]; j++)
                {
                    konsola += "   ";
                    konsola += Matrix[j, i];

                }

                konsola += "\n";
            }


        }

        public int CountPathOfOneDeserter(int value)
        {
            List<Deserter> listDeserers = Deserters.Where(x => x.Value == value).ToList();
            Deserter deserter = null;

            if (listDeserers.Count > 0)
            {
                deserter = listDeserers[0];
                return deserter.Points.Count();
            }
            else
            {
                return 0;
            }
        }
        public void DrawCountOfEachDeserter()
        {
            for (int i = 0; i < Deserters.Count - 1; i++)
            {
                int deserterNumber = i + 1;
                Console.WriteLine("\n Deserter nr. " + deserterNumber + " posiada ścieżkę o długości - " + CountPathOfOneDeserter(deserterNumber) + "\n");
            }
        }

        public List<Deserter> GetListOfDeserterWhoNeverReachTheirDestination()
        {
            List<Deserter> loosers = new List<Deserter>();
            foreach (Deserter deserter in Deserters)
            {
                if (deserter.BottomFactor + deserter.LeftFactor + deserter.RightFactor + deserter.TopFactor != 0)
                {
                    Console.WriteLine("Deserter - " + deserter.Value + " nigdy nie odnalazł krawędzi i skończył w punkcie - " + deserter.Points[deserter.Points.Count - 1]);
                    loosers.Add(deserter);
                }
            }

            return loosers;
        }

    }
}

