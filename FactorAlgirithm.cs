using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektInzynierskiWPF
{
    public class FactorAlgirithm
    {
        public static void moveAccordingToFactors(Board board, Deserter deserter)
        {
            deserter.ProssessFactors();
            List<Factor> lista = new List<Factor>();
            lista.Add(new Factor(deserter.BottomFactor, "BottomFactor"));
            lista.Add(new Factor(deserter.TopFactor, "TopFactor"));
            lista.Add(new Factor(deserter.LeftFactor, "LeftFactor"));
            lista.Add(new Factor(deserter.RightFactor, "RightFactor"));

            lista = lista.OrderBy(o => o.Value).ToList();

            if (lista[0].Value != lista[3].Value)
            {
                switch (lista[0].Type)
                {

                    case "BottomFactor":
                        deserter.Move(0, -1);
                        break;
                    case "TopFactor":
                        deserter.Move(0, 1);
                        break;
                    case "LeftFactor":
                        deserter.Move(-1, 0);
                        break;
                    case "RightFactor":
                        deserter.Move(1, 0);
                        break;
                }
            }
            if (lista[0].Value == lista[3].Value && lista[0].Value != 0 && lista[0].Value != 1)
            {
                switch (lista[0].Type)
                {

                    case "BottomFactor":
                        deserter.Move(0, -1);
                        break;
                    case "TopFactor":
                        deserter.Move(0, 1);
                        break;
                    case "LeftFactor":
                        deserter.Move(-1, 0);
                        break;
                    case "RightFactor":
                        deserter.Move(1, 0);
                        break;
                }
            }
        }

        public static void IndexFindingPathAlgorithm(Board board)
        {
            foreach (var deserter in board.Deserters)
            {
                int tryies = 0;
                bool IsGoing = true;
                while (IsGoing)
                {
                    FactorAlgirithm.moveAccordingToFactors(board, deserter);
                    tryies++;

                    if (deserter.RightFactor != 1 || deserter.LeftFactor != 1 || deserter.TopFactor != 1 || deserter.BottomFactor != 1)
                    {
                        IsGoing = true;
                        if (deserter.RightFactor == 0 && deserter.LeftFactor == 0 && deserter.TopFactor == 0 && deserter.BottomFactor == 0)
                        {
                            IsGoing = false;

                        }
                        if (tryies > 20)
                        {
                            IsGoing = false;
                        }
                    }
                    else
                    {
                        IsGoing = false;
                    }
                }
            }
            if (board.Deserters.Count() > 0)
                board.AlreadyCalculated = true;
        }
    }
    public class Factor
    {
        public Double Value;
        public string Type;
        public Factor(Double value, string type)
        {
            Value = value;
            Type = type;
        }
    }
}
