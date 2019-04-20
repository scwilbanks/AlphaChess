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

            if (board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.WhiteRooks);
            }
            else if (!board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.BlackRooks);
            }
            else
            {
                throw new Exception();
            }

            List<Tuple<ulong, ulong>> RooksMoves = new List<Tuple<ulong, ulong>>();

            foreach (ulong Piece in Pieces)
            {
                RooksMoves.AddRange(GetNorth(board, Piece, 7));
                RooksMoves.AddRange(GetEast(board, Piece, 7));
                RooksMoves.AddRange(GetSouth(board, Piece, 7));
                RooksMoves.AddRange(GetWest(board, Piece, 7));
            }

            return RooksMoves;
        }


    }
}
