using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private Random _RandomSeed = new Random();
        public Random RandomSeed
        {
            get { return _RandomSeed; }
            set { _RandomSeed = value; }
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
                    list.Add(Matrix[j, i]);
            }
            return list;
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
        public ObservableCollection<String> DrawCountOfEachDeserter()
        {
            ObservableCollection<String> PathCount = new ObservableCollection<string>();
            for (int i = 0; i < Deserters.Count - 1; i++)
            {
                int deserterNumber = i + 1;
                PathCount.Add("\n Deserter nr. " + deserterNumber + " posiada ścieżkę o długości - " + CountPathOfOneDeserter(deserterNumber) + "\n");               
            }
            return PathCount;
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
        public void randomPoints(int amount = 0)
        {
            int size = Size[0];
            if (amount == 0)
            {
                amount = size;
            }

            for (int i = 0; i < amount; i++)
            {
                Point rndPoint = new Point(RandomSeed.Next(0, size - 1), RandomSeed.Next(0, size - 1));

                int rndX = RandomSeed.Next(0, size - 1);
                int rndY = RandomSeed.Next(0, size - 1);

                int tries = 0, maxTries = 10000;
                while (Matrix[rndX, rndY] != 0 && tries < maxTries)
                {
                    rndX = RandomSeed.Next(0, size - 1);
                    rndY = RandomSeed.Next(0, size - 1);
                    tries++;
                }

                if (tries < maxTries)
                {
                    AddNewDeserter(new Deserter(new Point(rndX, rndY), this));
                }
            }
        }

        public void SetPointsFromExampleNr1()
        {

            AddNewDeserter(new Deserter(new Point(0, 3), this));

            AddNewDeserter(new Deserter(new Point(1, 2), this));
            AddNewDeserter(new Deserter(new Point(1, 3), this));
            AddNewDeserter(new Deserter(new Point(1, 4), this));

            AddNewDeserter(new Deserter(new Point(3, 2), this));
            AddNewDeserter(new Deserter(new Point(3, 3), this));
            AddNewDeserter(new Deserter(new Point(3, 4), this));

            AddNewDeserter(new Deserter(new Point(5, 2), this));
            AddNewDeserter(new Deserter(new Point(5, 3), this));
            AddNewDeserter(new Deserter(new Point(5, 4), this));
        }

        public void SetPointsFromExampleNr2()
        {
            AddNewDeserter(new Deserter(new Point(0, 3), this));

            AddNewDeserter(new Deserter(new Point(1, 2), this));
            AddNewDeserter(new Deserter(new Point(1, 3), this));
            AddNewDeserter(new Deserter(new Point(1, 4), this));

            AddNewDeserter(new Deserter(new Point(3, 2), this));
            AddNewDeserter(new Deserter(new Point(3, 3), this));
            AddNewDeserter(new Deserter(new Point(3, 4), this));

            AddNewDeserter(new Deserter(new Point(5, 2), this));
            AddNewDeserter(new Deserter(new Point(5, 3), this));
            AddNewDeserter(new Deserter(new Point(5, 4), this));
            AddNewDeserter(new Deserter(new Point(4, 3), this));
        }

    }
}

