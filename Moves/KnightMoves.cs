using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaChess.Moves
{
    static partial class MoveGenerator
    {
        // Returns an array of Tuples representing all possible moves for the 
        // current player's Knights from the current board position
        public static List<Tuple<ulong, ulong>> GetKnightsMoves(Board board)
        {
            ulong[] Pieces;

            if (board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.WhiteKnights);
            }
            else if (!board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.BlackKnights);
            }
            else
            {
                throw new Exception();
            }


            List<Tuple<ulong, ulong>> KnightsMoves = new List<Tuple<ulong, ulong>>();

            foreach (ulong Piece in Pieces)
            {
                KnightsMoves.AddRange(GetKnightNNE(board, Piece));
                KnightsMoves.AddRange(GetKnightENE(board, Piece));
                KnightsMoves.AddRange(GetKnightESE(board, Piece));
                KnightsMoves.AddRange(GetKnightSSE(board, Piece));
                KnightsMoves.AddRange(GetKnightSSW(board, Piece));
                KnightsMoves.AddRange(GetKnightWSW(board, Piece));
                KnightsMoves.AddRange(GetKnightWNW(board, Piece));
                KnightsMoves.AddRange(GetKnightNNW(board, Piece));
            
            }


            return KnightsMoves;
        }


        // Returns a List of Tuples representing zero or one possible moves by 
        // the Knight to the square up two and right one.
        public static List<Tuple<ulong, ulong>> GetKnightNNE(Board board, ulong Piece)
        {

            List<Tuple<ulong, ulong>> Result = new List<Tuple<ulong, ulong>>();

            ulong Candidate;

            ulong HFile =
                (ulong)Math.Pow(2, 7)
              + (ulong)Math.Pow(2, 15)
              + (ulong)Math.Pow(2, 23)
              + (ulong)Math.Pow(2, 31)
              + (ulong)Math.Pow(2, 39)
              + (ulong)Math.Pow(2, 47)
              + (ulong)Math.Pow(2, 55)
              + (ulong)Math.Pow(2, 63);

            // If on Ranks 1-6, and Files a-g
            if (Piece < Math.Pow(2, 48) && ((Piece & HFile) == 0))
            {
                Candidate = Piece << 17;

                if ((Candidate & board.EligibleSquares) > 0)
                {
                    Result.Add(Tuple.Create(Piece, Candidate));
                }
                

            }

            return Result;


        }


        // Returns a List of Tuples representing zero or one possible moves by 
        // the Knight to the square up one and right two.
        public static List<Tuple<ulong, ulong>> GetKnightENE(Board board, ulong Piece)
        {

            List<Tuple<ulong, ulong>> Result = new List<Tuple<ulong, ulong>>();

            ulong Candidate;

            ulong GAndHFiles =
                (ulong)Math.Pow(2, 6)
              + (ulong)Math.Pow(2, 7)
              + (ulong)Math.Pow(2, 14)
              + (ulong)Math.Pow(2, 15)
              + (ulong)Math.Pow(2, 22)
              + (ulong)Math.Pow(2, 23)
              + (ulong)Math.Pow(2, 30)
              + (ulong)Math.Pow(2, 31)
              + (ulong)Math.Pow(2, 38)
              + (ulong)Math.Pow(2, 39)
              + (ulong)Math.Pow(2, 46)
              + (ulong)Math.Pow(2, 47)
              + (ulong)Math.Pow(2, 54)
              + (ulong)Math.Pow(2, 55)
              + (ulong)Math.Pow(2, 62)
              + (ulong)Math.Pow(2, 63);

            // If on Ranks 1-7, and Files a-f
            if (Piece < Math.Pow(2, 56) && ((Piece & GAndHFiles) == 0))
            {
                Candidate = Piece << 10;

                if ((Candidate & board.EligibleSquares) > 0)
                {
                    Result.Add(Tuple.Create(Piece, Candidate));
                }


            }

            return Result;


        }


        // Returns a List of Tuples representing zero or one possible moves by 
        // the Knight to the square down one and right two.
        public static List<Tuple<ulong, ulong>> GetKnightESE(Board board, ulong Piece)
        {

            List<Tuple<ulong, ulong>> Result = new List<Tuple<ulong, ulong>>();

            ulong Candidate;

            ulong GAndHFiles =
                (ulong)Math.Pow(2, 6)
              + (ulong)Math.Pow(2, 7)
              + (ulong)Math.Pow(2, 14)
              + (ulong)Math.Pow(2, 15)
              + (ulong)Math.Pow(2, 22)
              + (ulong)Math.Pow(2, 23)
              + (ulong)Math.Pow(2, 30)
              + (ulong)Math.Pow(2, 31)
              + (ulong)Math.Pow(2, 38)
              + (ulong)Math.Pow(2, 39)
              + (ulong)Math.Pow(2, 46)
              + (ulong)Math.Pow(2, 47)
              + (ulong)Math.Pow(2, 54)
              + (ulong)Math.Pow(2, 55)
              + (ulong)Math.Pow(2, 62)
              + (ulong)Math.Pow(2, 63);

            // If on Ranks 2-8, and Files a-f
            if ((Piece >= Math.Pow(2, 8)) && ((Piece & GAndHFiles) == 0))
            {
                Candidate = Piece >> 6;

                if ((Candidate & board.EligibleSquares) > 0)
                {
                    Result.Add(Tuple.Create(Piece, Candidate));
                }


            }

            return Result;


        }


        // Returns a List of Tuples representing zero or one possible moves by 
        // the Knight to the square down two and right one.
        public static List<Tuple<ulong, ulong>> GetKnightSSE(Board board, ulong Piece)
        {

            List<Tuple<ulong, ulong>> Result = new List<Tuple<ulong, ulong>>();

            ulong Candidate;

            ulong HFile =
                (ulong)Math.Pow(2, 7)
              + (ulong)Math.Pow(2, 15)
              + (ulong)Math.Pow(2, 23)
              + (ulong)Math.Pow(2, 31)
              + (ulong)Math.Pow(2, 39)
              + (ulong)Math.Pow(2, 47)
              + (ulong)Math.Pow(2, 55)
              + (ulong)Math.Pow(2, 63);

            // If on Ranks 3-8, and Files a-g
            if ((Piece >= Math.Pow(2, 16)) && ((Piece & HFile) == 0))
            {
                Candidate = Piece >> 15
;

                if ((Candidate & board.EligibleSquares) > 0)
                {
                    Result.Add(Tuple.Create(Piece, Candidate));
                }


            }

            return Result;


        }


        // Returns a List of Tuples representing zero or one possible moves by 
        // the Knight to the square down two and left one.
        public static List<Tuple<ulong, ulong>> GetKnightSSW(Board board, ulong Piece)
        {

            List<Tuple<ulong, ulong>> Result = new List<Tuple<ulong, ulong>>();

            ulong Candidate;

            // If on a file
            ulong AFile =
                (ulong)Math.Pow(2, 0)
              + (ulong)Math.Pow(2, 8)
              + (ulong)Math.Pow(2, 16)
              + (ulong)Math.Pow(2, 24)
              + (ulong)Math.Pow(2, 32)
              + (ulong)Math.Pow(2, 40)
              + (ulong)Math.Pow(2, 48)
              + (ulong)Math.Pow(2, 56);

            // If on Ranks 3-8, and Files b-h
            if ((Piece >= Math.Pow(2, 16)) && ((Piece & AFile) == 0))
            {
                Candidate = Piece >> 17
;

                if ((Candidate & board.EligibleSquares) > 0)
                {
                    Result.Add(Tuple.Create(Piece, Candidate));
                }


            }

            return Result;


        }


        // Returns a List of Tuples representing zero or one possible moves by 
        // the Knight to the square down two and left one.
        public static List<Tuple<ulong, ulong>> GetKnightWSW(Board board, ulong Piece)
        {

            List<Tuple<ulong, ulong>> Result = new List<Tuple<ulong, ulong>>();

            ulong Candidate;

            // If on a or b file
            ulong AAndBFiles =
                (ulong)Math.Pow(2, 0)
              + (ulong)Math.Pow(2, 1)
              + (ulong)Math.Pow(2, 8)
              + (ulong)Math.Pow(2, 9)
              + (ulong)Math.Pow(2, 16)
              + (ulong)Math.Pow(2, 17)
              + (ulong)Math.Pow(2, 24)
              + (ulong)Math.Pow(2, 25)
              + (ulong)Math.Pow(2, 32)
              + (ulong)Math.Pow(2, 33)
              + (ulong)Math.Pow(2, 40)
              + (ulong)Math.Pow(2, 41)
              + (ulong)Math.Pow(2, 48)
              + (ulong)Math.Pow(2, 49)
              + (ulong)Math.Pow(2, 56)
              + (ulong)Math.Pow(2, 57);

            // If on Ranks 2-8, and Files b-h
            if ((Piece >= Math.Pow(2, 8)) && ((Piece & AAndBFiles) == 0))
            {
                Candidate = Piece >> 10
;

                if ((Candidate & board.EligibleSquares) > 0)
                {
                    Result.Add(Tuple.Create(Piece, Candidate));
                }


            }

            return Result;


        }


        // Returns a List of Tuples representing zero or one possible moves by 
        // the Knight to the square up one and left two.
        public static List<Tuple<ulong, ulong>> GetKnightWNW(Board board, ulong Piece)
        {

            List<Tuple<ulong, ulong>> Result = new List<Tuple<ulong, ulong>>();


            ulong Candidate;

            // If on a or b file
            ulong AAndBFiles =
                (ulong)Math.Pow(2, 0)
              + (ulong)Math.Pow(2, 1)
              + (ulong)Math.Pow(2, 8)
              + (ulong)Math.Pow(2, 9)
              + (ulong)Math.Pow(2, 16)
              + (ulong)Math.Pow(2, 17)
              + (ulong)Math.Pow(2, 24)
              + (ulong)Math.Pow(2, 25)
              + (ulong)Math.Pow(2, 32)
              + (ulong)Math.Pow(2, 33)
              + (ulong)Math.Pow(2, 40)
              + (ulong)Math.Pow(2, 41)
              + (ulong)Math.Pow(2, 48)
              + (ulong)Math.Pow(2, 49)
              + (ulong)Math.Pow(2, 56)
              + (ulong)Math.Pow(2, 57);

            // If on Ranks 1-7, and Files c-h
            if (Piece < Math.Pow(2, 56) && ((Piece & AAndBFiles) == 0))
            {
                Candidate = Piece << 6;

                if ((Candidate & board.EligibleSquares) > 0)
                {
                    Result.Add(Tuple.Create(Piece, Candidate));
                }


            }

            return Result;


        }


        // Returns a List of Tuples representing zero or one possible moves by 
        // the Knight to the square up two and left one.
        public static List<Tuple<ulong, ulong>> GetKnightNNW(Board board, ulong Piece)
        {

            List<Tuple<ulong, ulong>> Result = new List<Tuple<ulong, ulong>>();


            ulong Candidate;

            // If on a file
            ulong AFile =
                (ulong)Math.Pow(2, 0)
              + (ulong)Math.Pow(2, 8)
              + (ulong)Math.Pow(2, 16)
              + (ulong)Math.Pow(2, 24)
              + (ulong)Math.Pow(2, 32)
              + (ulong)Math.Pow(2, 40)
              + (ulong)Math.Pow(2, 48)
              + (ulong)Math.Pow(2, 56);

            // If on Ranks 1-6, and Files b-h
            if (Piece < Math.Pow(2, 48) && ((Piece & AFile) == 0))
            {
                Candidate = Piece << 15;

                if ((Candidate & board.EligibleSquares) > 0)
                {
                    Result.Add(Tuple.Create(Piece, Candidate));
                }


            }

            return Result;


        }




    }
}
