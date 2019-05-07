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
        public void MCTS()
        {
            int max = 1000;
            Console.WriteLine($"Conducting {max} MCTS iterations");
            for (int i= 0; i < max; i++)
            {
                MCTSIteration();
            }
        }


        public void MCTSIteration()
        {
            


            Board LeafBoard = MCTSDown(CurrentBoard);
            MCTSProcessLeaf(LeafBoard);
            MCTSUp(LeafBoard);

        }

        // TODO
        public Board MCTSDown(Board CurrentBoard)
        {



            while (CurrentBoard.Number > 1 && CurrentBoard.Children.Count() > 0)
            {


                Board HighestUCTBoard = null;

                foreach (Board Child in CurrentBoard.Children)
                {

                    if (CurrentBoard.TurnIsWhite)
                    {
                        if (HighestUCTBoard == null || Child.WhiteUCT > HighestUCTBoard.WhiteUCT)
                        {
                            HighestUCTBoard = Child;
                        }
                    }
                    else if (!CurrentBoard.TurnIsWhite)
                    {
                        if (HighestUCTBoard == null || Child.BlackUCT > HighestUCTBoard.BlackUCT)
                        {
                            HighestUCTBoard = Child;
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }



                }
                


                CurrentBoard = HighestUCTBoard;

            }

            
            return CurrentBoard;

        }


        public void MCTSProcessLeaf(Board LeafBoard)
        {

            if (LeafBoard.Children.Count() > 0)
            {
                LeafBoard.Number = 0;
                LeafBoard.WhiteWins = 0;
                LeafBoard.BlackWins = 0;

                foreach (Board Child in LeafBoard.Children)
                {
                    Child.InitializeChildren();

                    LeafBoard.Number += 1;
                    LeafBoard.WhiteWins += Child.WhiteWins;
                    LeafBoard.BlackWins += Child.BlackWins;
                }
            }
            else
            {
                LeafBoard.Number += 1;
                LeafBoard.WhiteWins += LeafBoard.WhiteWins > 0 ? 1 : 0;
                LeafBoard.BlackWins += LeafBoard.BlackWins > 0 ? 1 : 0;
            }

        }

        // TODO
        public void MCTSUp(Board LeafBoard)
        {

            int WhiteWin = LeafBoard.WhiteWins;
            int BlackWin = LeafBoard.BlackWins;
            int NewBoards = LeafBoard.Children.Length;

            Board CurrentBoard = LeafBoard.Parent;

            while (CurrentBoard != null)
            {

                CurrentBoard.Number += LeafBoard.Number-1;
                CurrentBoard.WhiteWins += LeafBoard.WhiteWins;
                CurrentBoard.BlackWins += LeafBoard.BlackWins;

                foreach(Board Child in CurrentBoard.Children)
                {
                    //Child.WhiteWins += WhiteWin;
                    //Child.BlackWins += BlackWin;

                    //if (Child.Children.Length == 0)
                    //{
                    //}
                    Child.UpdateUCTs();
                }

                
                // Go up
                CurrentBoard = CurrentBoard.Parent;
            }
            

            

        }

    }
}
