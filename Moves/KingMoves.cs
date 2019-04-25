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
        // current player's King from the current board position
        public static List<Tuple<ulong, ulong>> GetKingMoves(Board board)
        {

            ulong Piece;
            ulong EligibleSquares;

            if (board.TurnIsWhite)
            {
                Piece = board.WhiteKing;
                EligibleSquares = board.WhiteEligibleSquares;
            }
            else if (!board.TurnIsWhite)
            {
                Piece = board.BlackKing;
                EligibleSquares = board.BlackEligibleSquares;
            }
            else
            {
                throw new Exception();
            }

            List<Tuple<ulong, ulong>> WhiteKingMoves = new List<Tuple<ulong, ulong>>();

            WhiteKingMoves.AddRange(GetNorth(board, EligibleSquares, Piece, 1));
            WhiteKingMoves.AddRange(GetNorthEast(board, EligibleSquares, Piece, 1));
            WhiteKingMoves.AddRange(GetEast(board, EligibleSquares, Piece, 1));
            WhiteKingMoves.AddRange(GetSouthEast(board, EligibleSquares, Piece, 1));
            WhiteKingMoves.AddRange(GetSouth(board, EligibleSquares, Piece, 1));
            WhiteKingMoves.AddRange(GetSouthWest(board, EligibleSquares, Piece, 1));
            WhiteKingMoves.AddRange(GetWest(board, EligibleSquares, Piece, 1));
            WhiteKingMoves.AddRange(GetNorthWest(board, EligibleSquares, Piece, 1));

            return WhiteKingMoves;

        }

    }
}
