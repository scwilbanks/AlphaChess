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
        // current player's Rooks from the current board position
        public static List<Tuple<ulong, ulong>> GetRooksMoves(Board board)
        {

            ulong[] Pieces;
            ulong EligibleSquares;

            if (board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.WhiteRooks);
                EligibleSquares = board.WhiteEligibleSquares;
            }
            else if (!board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.BlackRooks);
                EligibleSquares = board.BlackEligibleSquares;
            }
            else
            {
                throw new Exception();
            }

            List<Tuple<ulong, ulong>> RooksMoves = new List<Tuple<ulong, ulong>>();

            foreach (ulong Piece in Pieces)
            {
                RooksMoves.AddRange(GetNorth(board, EligibleSquares, Piece, 7));
                RooksMoves.AddRange(GetEast(board, EligibleSquares, Piece, 7));
                RooksMoves.AddRange(GetSouth(board, EligibleSquares, Piece, 7));
                RooksMoves.AddRange(GetWest(board, EligibleSquares, Piece, 7));
            }

            return RooksMoves;
        }


    }
}
