using System;
using System.Collections;

namespace AlphaChess
{
    public class Game
    {


        // Properties
        public bool PlayerColorIsWhite { get; set; }
        public Board CurrentBoard { get; set; }


        // Static Methods
        public static bool ValidateColorInput(string Input)
        {


            bool Result;


            switch (Input.ToLower())
            {
                case "":
                case "w":
                case "white":
                case "b":
                case "black":
                    Result = true;
                    break;
                default:
                    Result = false;
                    break;
            }


            return Result;


        }


        public static bool ParseColorInput(string Input)
        {
            bool Result;

            Input.ToLower();

            switch (Input.ToLower())
            {
                case "":
                case "w":
                case "white":
                    Result = true;
                    break;
                case "b":
                case "black":
                    Result = false;
                    break;
                default:
                    Result = true;
                    break;
            }

            return Result;
        }


        private static bool GetColor()
        {
            bool PlayerColor = false;
            bool ColorDecided = false;


            Console.WriteLine("What color would you like to be?");

            while (!ColorDecided)
            {
                string Input = Console.ReadLine();

                if (ValidateColorInput(Input))
                {
                    PlayerColor = ParseColorInput(Input);
                    ColorDecided = true;
                }
                else
                {
                    Console.WriteLine("Please choose White or Black");
                }


            }

            return PlayerColor;
        }


        private static Hashtable GetStartingBoards()
        {
            return new Hashtable();
        }


        private static BitArray GetStartingBoard()
        {
            return Board.GetStartingBoard();
        }


        // Constructors
        public Game()
        {
            PlayerColorIsWhite = GetColor();
            CurrentBoard = new Board();

        }


        // Instance Methods
        public void Start()
        {

            while (!IsGameOver())
            {
                ExecuteNextTurn();
            }

            
        }


        public bool IsGameOver()
        {
            return false;
        }

        // Returns two dimensional array representing what to print in each square
        // TODO
        private string[,] GetSquares()
        {
            string[,] Squares = new string[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Squares[i, j] = "   ";
                }
            }
            return Squares;
        }


        // Prints the current board position to the console 
        // TODO
        private void PrintCurrentBoard()
        {
            string[,] Squares = GetSquares();

            Console.WriteLine("Current Board:");
            Console.WriteLine("      a     b     c     d     e     f     g     h");
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("   " + " -----" + " -----" + " -----" + " -----" + " -----" + " -----" + " -----" + " -----");
                Console.Write(" {0} |", 8-i);
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(" {0} |", Squares[i, j]);
                }
                Console.WriteLine(" {0}", 8-i);

            }
            Console.WriteLine("   " + " -----" + " -----" + " -----" + " -----" + " -----" + " -----" + " -----" + " -----");
            Console.WriteLine("      a     b     c     d     e     f     g     h");


        }


        private void PrintCurrentMoves()
        {
            Console.WriteLine("Printing current moves");
        }


        private void MakePlayerMove()
        {
            Console.WriteLine("Making the Player's move");
        }


        private void MakeOpponentMove()
        {
            Console.WriteLine("Making the Opponent's move");
        }


        private void MCTS()
        {
            Console.WriteLine("Conducting MCTS");
        }


        private void ExecuteNextTurn()
        {

            if (PlayerColorIsWhite == CurrentBoard.TurnIsWhite)
            {
                ExecutePlayerTurn();
            }
            else if (PlayerColorIsWhite != CurrentBoard.TurnIsWhite)
            {
                ExecuteOpponentTurn();
            }


            Console.WriteLine();
            Console.ReadKey();
            CurrentBoard.TurnIsWhite = !CurrentBoard.TurnIsWhite;
        }


        private void ExecutePlayerTurn()
        {
            Console.WriteLine("Executing Player Turn:");
            MCTS();
            PrintCurrentBoard();
            PrintCurrentMoves();
            MakePlayerMove();
            return;
        }


        private void ExecuteOpponentTurn()
        {
            Console.WriteLine("Executing Opponent Turn:");
            MCTS();
            PrintCurrentBoard();
            PrintCurrentMoves();
            MakeOpponentMove();
            return;
        }

    }
}
