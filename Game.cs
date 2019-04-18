using AlphaChess.Printing;
using System;
using System.Collections;
using System.Collections.Generic;

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

        // TODO
        public bool IsGameOver()
        {
            return false;
        }


        // TODO
        private void MakePlayerMove()
        {
            Console.WriteLine("Making the Player's move");
        }


        // TODO
        private void MakeOpponentMove()
        {
            Console.WriteLine("Making the Opponent's move");
        }


        // TODO
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
            Printer printer = new Printer(CurrentBoard);
            printer.PrintCurrentBoard();
            printer.PrintCurrentMoves();
            MakePlayerMove();
            return;
        }


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
