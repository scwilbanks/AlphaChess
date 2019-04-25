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

            if (CurrentBoard.Children == null)
            {
                CurrentBoard.InitializeChildren();
            }

            return CurrentBoard.Children[0];

        }

        // TODO
        public void MCTSUp(Board LeafBoard)
        {

        }

    }
}
