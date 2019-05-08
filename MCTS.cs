using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Task[] tasks = new Task[max];

            Console.WriteLine($"Conducting {max} MCTS iterations");
            for (int i = 0; i < max; i++)
            {
                //Console.WriteLine(i);
                tasks[i] = Task.Run( () => MCTSIteration());
            }
            Task.WaitAll(tasks);
        }


        public void MCTSIteration()
        {
            Board LeafBoard = MCTSDown(CurrentBoard);
            MCTSProcessLeaf(LeafBoard);
            MCTSUp(LeafBoard);
        }

        // TODO: Shouldn't choose a leaf that has already been entered
        public Board MCTSDown(Board CurrentBoard)
        {
            while (CurrentBoard.Number > 1 && CurrentBoard.Children.Count() > 0)
            {

                Board HighestUCTBoard = null;
                object HighestUCTBoardLock = new object();
                Monitor.Enter(HighestUCTBoardLock);

                while (HighestUCTBoard == null)
                {
                    foreach (Board Child in CurrentBoard.Children)
                    {
                        if (CurrentBoard.TurnIsWhite)
                        {
                            if (HighestUCTBoard == null || Child.WhiteUCT > HighestUCTBoard.WhiteUCT)
                            {
                                if (Monitor.TryEnter(Child))
                                {
                                    if (HighestUCTBoard == null)
                                    {
                                        Monitor.Exit(HighestUCTBoardLock);
                                    }
                                    else
                                    {
                                        Monitor.Exit(HighestUCTBoard);
                                    }
                                    
                                    HighestUCTBoard = Child;
                                    Monitor.Enter(HighestUCTBoard);
                                    Monitor.Exit(Child);
                                }
                                
                            }
                        }
                        else if (!CurrentBoard.TurnIsWhite)
                        {
                            if (HighestUCTBoard == null || Child.BlackUCT > HighestUCTBoard.BlackUCT)
                            {
                                if (Monitor.TryEnter(Child))
                                {
                                    if (HighestUCTBoard == null)
                                    {
                                        Monitor.Exit(HighestUCTBoardLock);
                                    }
                                    else
                                    {
                                        Monitor.Exit(HighestUCTBoard);
                                    }
                                    HighestUCTBoard = Child;
                                    Monitor.Enter(HighestUCTBoard);
                                    Monitor.Exit(Child);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }


                    }
                }


                CurrentBoard = HighestUCTBoard;

            }
            
            return CurrentBoard;

        }

        // Lock here, to prevent another task from selecting this lea
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

            int WhiteWins = LeafBoard.WhiteWins;
            int BlackWins = LeafBoard.BlackWins;
            int NewBoards = LeafBoard.Number - 1;

            
            Board CurrentBoard = LeafBoard.Parent;

            while (CurrentBoard != null)
            {
                Monitor.Enter(CurrentBoard);
                CurrentBoard.Number += NewBoards;
                CurrentBoard.WhiteWins += WhiteWins;
                CurrentBoard.BlackWins += BlackWins;
                Monitor.Exit(CurrentBoard);

                foreach (Board Child in CurrentBoard.Children)
                {
                    if (Monitor.IsEntered(Child))
                    {
                        Child.UpdateUCTs();
                    }
                    else
                    {
                        Monitor.Enter(Child);
                        Child.UpdateUCTs();
                    }
                    Monitor.Exit(Child);
                }


                // Go up
                CurrentBoard = CurrentBoard.Parent;
            }
            

            

        }

    }
}
