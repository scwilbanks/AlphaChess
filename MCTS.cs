using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaChess
{

    /// <summary>
    /// 
    /// The MCTS class provides static methods to conduct Monte Carlo Tree
    /// Search on a Board instance.
    /// 
    /// </summary>


    public partial class Game
    {
        // Initiates the Monte Carlo Tree Search, develops the tree
        // TODO
        public void MCTS()
        {
            Console.WriteLine("Conducting MCTS");


            Board LeafBoard = MCTSDown(CurrentBoard);
            MCTSUp(LeafBoard);

        }

        // TODO
        public Board MCTSDown(Board CurrentBoard)
        {

            

            

            while (CurrentBoard.Children != null)
            {
                Board HighestUCTBoard = null;

                foreach (Board Child in CurrentBoard.Children)
                {
                    if (HighestUCTBoard == null || Child.UCT > HighestUCTBoard.UCT)
                    {
                        HighestUCTBoard = Child;
                    }
                }

                CurrentBoard = HighestUCTBoard;

            }


            return CurrentBoard;

        }

        // TODO
        // TODO: remove child if still in check
        public void MCTSUp(Board LeafBoard)
        {

            LeafBoard.InitializeChildren();

            Board CurrentBoard = LeafBoard;

            while (CurrentBoard != null)
            {
                CurrentBoard.VisitNumber++;

                CurrentBoard.UpdateUCT();

                CurrentBoard = CurrentBoard.Parent;
            }
            

            

        }

    }
}
