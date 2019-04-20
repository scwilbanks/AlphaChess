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
        // TODO Other 7 possible moves
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

                //KnightsMoves.AddRange(GetKnightENE(board, Piece));
                //KnightsMoves.AddRange(GetKnightESE(board, Piece));
                //KnightsMoves.AddRange(GetKnightSSE(board, Piece));
                //KnightsMoves.AddRange(GetKnightSSW(board, Piece));
                //KnightsMoves.AddRange(GetKnightWSW(board, Piece));
                //KnightsMoves.AddRange(GetKnightWNW(board, Piece));
                //KnightsMoves.AddRange(GetKnightNNW(board, Piece));
            
            }


            return KnightsMoves;
        }


        // Returns a List of Tuples representing zero or one possible move by 
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


    }
}
