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
        // Pawns from the current board position
        // TODO: add attacks, en passant, and promotion
        public static List<Tuple<ulong, ulong>> GetPawnsMoves(Board board)
        {

            ulong[] Pieces;

            if (board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.WhitePawns);
            }
            else if (!board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.BlackPawns);
            }
            else
            {
                throw new Exception();
            }


            List<Tuple<ulong, ulong>> PawnsMoves = new List<Tuple<ulong, ulong>>();

            foreach (ulong Piece in Pieces)
            {
                PawnsMoves.AddRange(GetNorth(board, Piece, 1));
            }

            return PawnsMoves;
        }
    }
}
