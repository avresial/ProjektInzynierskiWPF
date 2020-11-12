using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ProjektInzynierskiWPF
{
    class Program
    {
        public Program()
        {
            Board board = new Board();
            SetPointsFromExampleNr1(board);
            FactorAlgirithm.IndexFindingPathAlgorithm(board);
           
        }
        static void SetingCustomPoints(Board board)
        {

            int amountOfPoints = 1;


            for (int i = 0; i < amountOfPoints; i++)
            {
                int X = 0;
                int Y = 0;

                if (X >= 0 && X < board.Size[0] && Y >= 0 && Y < board.Size[1])
                {
                   // board.AddNewDeserter(new Deserter(new Point(X, Y), board));
                }

            }
        }
        static void SetPointsFromExampleNr1(Board board)
        {
            //board.AddNewDeserter(new Deserter(new Point(0, 3), board,));

            //board.AddNewDeserter(new Deserter(new Point(1, 2), board));
            //board.AddNewDeserter(new Deserter(new Point(1, 3), board));
            //board.AddNewDeserter(new Deserter(new Point(1, 4), board));

            //board.AddNewDeserter(new Deserter(new Point(3, 2), board));
            //board.AddNewDeserter(new Deserter(new Point(3, 3), board));
            //board.AddNewDeserter(new Deserter(new Point(3, 4), board));

            //board.AddNewDeserter(new Deserter(new Point(5, 2), board));
            //board.AddNewDeserter(new Deserter(new Point(5, 3), board));
            //board.AddNewDeserter(new Deserter(new Point(5, 4), board));
        }
        static void SetPointsFromExampleNr2(Board board)
        {
            //board.AddNewDeserter(new Deserter(new Point(0, 3), board));

            //board.AddNewDeserter(new Deserter(new Point(1, 2), board));
            //board.AddNewDeserter(new Deserter(new Point(1, 3), board));
            //board.AddNewDeserter(new Deserter(new Point(1, 4), board));

            //board.AddNewDeserter(new Deserter(new Point(3, 2), board));
            //board.AddNewDeserter(new Deserter(new Point(3, 3), board));
            //board.AddNewDeserter(new Deserter(new Point(3, 4), board));

            //board.AddNewDeserter(new Deserter(new Point(5, 2), board));
            //board.AddNewDeserter(new Deserter(new Point(5, 3), board));
            //board.AddNewDeserter(new Deserter(new Point(5, 4), board));
            //board.AddNewDeserter(new Deserter(new Point(4, 3), board));
        }
        static void randomPoints(Board board, int size, int amount = 0)
        {

            if (amount == 0)
            {
                amount = size;
            }

            for (int i = 0; i < amount; i++)
            {
                Random rnd = new Random();
                int rndX = rnd.Next(0, size - 1);
                int rndY = rnd.Next(0, size - 1);

                int tries = 0, maxTries = 10;
                while (board.Matrix[rndX, rndY] != 0 && tries < maxTries)
                {
                    rndX = rnd.Next(0, size - 1);
                    rndY = rnd.Next(0, size - 1);
                    tries++;
                }

                if (tries < maxTries)
                {
                    //board.AddNewDeserter(new Deserter(new Point(rndX, rndY), board));
                }


            }
        }
    }



}
