using AlphaChess.Printing;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AlphaChess
{
    public class Game
    {

        /// <summary>
        /// 
        /// The Game instance represents the main Global object that
        /// encapsulates all global information.
        /// 
        /// </summary>


        // Properties
        public bool PlayerColorIsWhite { get; set; }
        public Board CurrentBoard { get; set; }


        // Static Methods
        // Returns whether or not the input for player's color is valid
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


        // Returns true for White and false for Black
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


        // Takes console input from the player to choose player's color
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
                ExecuteTurn();
            }

            
        }

        // Returns whether or not the game should end based on the current 
        // board state
        // TODO
        public bool IsGameOver()
        {
            return false;
        }

        // TODO
        public bool IsMoveValid(string move)
        {
            return false;
        }

        // Takes player input for their move, and updates Game object with the
        // corresponding child board.
        // TODO
        private void MakePlayerMove()
        {
            string Move;

            do
            {
                Console.WriteLine("Making the Player's move");
                Move = Console.ReadLine();
            }
            while (!IsMoveValid(Move));


        }

        // Chooses best move for the Computer AI opponent, makes its move and
        // updates the Game object with the correspond child board.
        // TODO
        private void MakeOpponentMove()
        {
            Console.WriteLine("Making the Opponent's move");
        }

        // Initiates the Monte Carlo Tree Search, develops the tree
        // TODO
        private void MCTS()
        {
            Console.WriteLine("Conducting MCTS");
        }

        // Executes a turn of the game
        private void ExecuteTurn()
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

        // Executes the player's turn by displaying information about the board
        // and making his move
        private void ExecutePlayerTurn()
        {
            Console.WriteLine("Executing Player Turn:");
            MCTS();
            Printer printer = new Printer(CurrentBoard);
            printer.PrintCurrentBoard();
            printer.PrintCurrentMoves();
            MakePlayerMove();
            return;
        }

        // Executes the Computer AI opponent's turn by displaying information
        //about the board and making its move
        private void ExecuteOpponentTurn()
        {
            Console.WriteLine("Executing Opponent Turn:");
            MCTS();
            Printer printer = new Printer(CurrentBoard);
            printer.PrintCurrentBoard();
            printer.PrintCurrentMoves();
            return;
        }

    }
}
