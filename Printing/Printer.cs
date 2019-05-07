using AlphaChess.Moves;
using System;
using System.Collections.Generic;


namespace AlphaChess.Printing
{


    /// <summary>
    /// 
    /// The Printer class instantiates a Printer instance that provides methods
    /// to print the current board state and possible moves.
    /// 
    /// TODO:
    /// PrintCurrentMoves
    /// 
    /// </summary>


    public partial class Printer
    {


        // Properties
        Board CurrentBoard { get; set; }


        // Constructors
        public Printer(Board BoardToPrint)
        {
            CurrentBoard = BoardToPrint;
        }


        // Instance Methods


        // Returns a tuple represening the zero-based rank and file of a piece
        public static Tuple<int, int> GetLocation(ulong Piece)
        {

            int Rank = 0;
            int File = 0;

            while (Piece >= Math.Pow(2, 8))
            {
                Piece >>= 8;
                Rank++;
            }

            while (Piece >= 2)
            {
                Piece >>= 1;
                File++;
            }

            return Tuple.Create(Rank, File);
        }


        // Returns a Dictionary representing which squares are occupied by which pieces
        public Dictionary<Tuple<int, int>, string> GetOccupiedSquares()
        {
            var OccupiedSquares = new Dictionary<Tuple<int, int>, string>();
            List<Tuple<Tuple<int, int>, string>> SquaresList = new List<Tuple<Tuple<int, int>, string>>();

            SquaresList.Add(GetWhiteKing());
            SquaresList.Add(GetWhiteQueen());
            foreach (var Rook in GetWhiteRooks())
            {
                SquaresList.Add(Rook);
            }
            foreach (var Bishop in GetWhiteBishops())
            {
                SquaresList.Add(Bishop);
            }

            foreach (var Knight in GetWhiteKnights())
            {
                SquaresList.Add(Knight);
            }

            foreach (var Pawn in GetWhitePawns())
            {
                SquaresList.Add(Pawn);
            }

            SquaresList.Add(GetBlackKing());
            SquaresList.Add(GetBlackQueen());
            foreach (var Rook in GetBlackRooks())
            {
                SquaresList.Add(Rook);
            }
            foreach (var Bishop in GetBlackBishops())
            {
                SquaresList.Add(Bishop);
            }
            foreach (var Knight in GetBlackKnights())
            {
                SquaresList.Add(Knight);
            }
            foreach (var Pawn in GetBlackPawns())
            {
                SquaresList.Add(Pawn);
            }


            foreach (var Square in SquaresList)
            {
                OccupiedSquares[Square.Item1] = Square.Item2;
            }


            return OccupiedSquares;

        }


        // Returns two dimensional array representing what to print in each square
        public string[,] GetSquares()
        {
            Dictionary<Tuple<int, int>, string> OccupiedSquares = GetOccupiedSquares();

            string[,] Squares = new string[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var Key = Tuple.Create(i, j);
                    if (OccupiedSquares.ContainsKey(Key))
                    {
                        Squares[i, j] = OccupiedSquares[Key];
                    }
                    else
                    {
                        Squares[i, j] = "  ";
                    }
                }
            }
            return Squares;
        }


        // Prints the current board position to the console 
        public void PrintCurrentBoard()
        {
            string[,] Squares = GetSquares();

            Console.WriteLine("Current Board:");
            Console.WriteLine("     a    b    c    d    e    f    g    h");
            for (int i = 7; i >= 0; i--)
            {
                Console.WriteLine("    ---------------------------------------");
                Console.Write(" {0} |", i + 1);
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(" {0} |", Squares[i, j]);
                }
                Console.WriteLine(" {0}", i + 1);

            }
            Console.WriteLine("    ---------------------------------------");
            Console.WriteLine("     a    b    c    d    e    f    g    h");

            if (CurrentBoard.TurnIsWhite)
            {
                Console.WriteLine("White's move");
            }
            else
            {
                Console.WriteLine("Black's move");
            }


        }

        public static string FormatMoveForBoard(Tuple<ulong, ulong> Move)
        {
            string MoveString = "";

            Tuple<int, int> PieceLocation = GetLocation(Move.Item1);
            string PieceRank = (PieceLocation.Item1 + 1).ToString();
            char PieceFile = (char)(PieceLocation.Item2 + 97);

            MoveString += PieceFile;
            MoveString += PieceRank;


            Tuple<int, int> MoveLocation = GetLocation(Move.Item2);
            string MoveRank = (MoveLocation.Item1 + 1).ToString();
            char MoveFile = (char)(MoveLocation.Item2 + 97);

            MoveString += MoveFile;
            MoveString += MoveRank;

            return MoveString;
        }


        // Formats the move into a string, ex. "Knight b1 to a3"
        public string FormatMoveForPrinting(Tuple<ulong, ulong> move)
        {
            string MoveString = "";

            if (((CurrentBoard.WhiteKing & move.Item1) > 0) || ((CurrentBoard.BlackKing & move.Item1) > 0)) 
            {
                MoveString += "King ";
            }
            else if (((CurrentBoard.WhiteQueen & move.Item1) > 0) || ((CurrentBoard.BlackQueen & move.Item1) > 0))
            {
                MoveString += "Queen ";
            }
            else if (((CurrentBoard.WhiteRooks & move.Item1) > 0) || ((CurrentBoard.BlackRooks & move.Item1) > 0))
            {
                MoveString += "Rook ";
            }
            else if (((CurrentBoard.WhiteBishops & move.Item1) > 0) || ((CurrentBoard.BlackBishops & move.Item1) > 0))
            {
                MoveString += "Bishop ";
            }
            else if (((CurrentBoard.WhiteKnights & move.Item1) > 0) || ((CurrentBoard.BlackKnights & move.Item1) > 0))
            {
                MoveString += "Knight ";
            }
            else if (((CurrentBoard.WhitePawns & move.Item1) > 0) || ((CurrentBoard.BlackPawns & move.Item1) > 0))
            {
                MoveString += "Pawn ";
            }
            else
            {
                throw new Exception();
            }


            Tuple<int, int> PieceLocation = GetLocation(move.Item1);
            string PieceRank = (PieceLocation.Item1+1).ToString();
            char PieceFile = (char)(PieceLocation.Item2+97);

            MoveString += PieceFile;
            MoveString += PieceRank;
            MoveString += " to ";


            Tuple<int, int> MoveLocation = GetLocation(move.Item2);
            string MoveRank = (MoveLocation.Item1 + 1).ToString();
            char MoveFile = (char)(MoveLocation.Item2 + 97);

            MoveString += MoveFile;
            MoveString += MoveRank;


            return MoveString;

        }


        // Prints the current possible moves to the console
        public void PrintCurrentMoves()
        {

            Console.WriteLine("Current Moves:");

            Console.Write($"Move, Visit Number ");
            Console.Write($"{(CurrentBoard.TurnIsWhite ? "WhiteWins" : "BlackWins")}");
            Console.WriteLine($"{(CurrentBoard.TurnIsWhite ? " WhiteUCT" : " BlackUCT")}");
            foreach (var child in CurrentBoard.Children)
            {
                Console.Write($"{FormatMoveForPrinting(child.Move)}");
                Console.WriteLine($" {child.Number}, {(CurrentBoard.TurnIsWhite ? child.WhiteWins : child.BlackWins)}, {(CurrentBoard.TurnIsWhite ? child.WhiteUCT : child.BlackUCT)}");
            }


        }
    }
}
