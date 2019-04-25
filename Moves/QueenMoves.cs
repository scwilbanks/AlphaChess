using System;
using System.Collections.Generic;


namespace AlphaChess.Moves
{
    static partial class MoveGenerator
    {
        // Returns an array of Tuples representing all possible moves for the 
        // current player's Queen from the current board position
        public static List<Tuple<ulong, ulong>> GetQueenMoves(Board board)
        {

            ulong Piece;
            ulong EligibleSquares;

            if (board.TurnIsWhite)
            {
                Piece = board.WhiteQueen;
                EligibleSquares = board.WhiteEligibleSquares;
            }
            else if (!board.TurnIsWhite)
            {
                Piece = board.BlackQueen;
                EligibleSquares = board.BlackEligibleSquares;
            }
            else
            {
                throw new Exception();
            }

            List<Tuple<ulong, ulong>> WhiteQueenMoves = new List<Tuple<ulong, ulong>>();

            WhiteQueenMoves.AddRange(GetNorth(board, EligibleSquares, Piece, 7));
            WhiteQueenMoves.AddRange(GetNorthEast(board, EligibleSquares, Piece, 7));
            WhiteQueenMoves.AddRange(GetEast(board, EligibleSquares, Piece, 7));
            WhiteQueenMoves.AddRange(GetSouthEast(board, EligibleSquares, Piece, 7));
            WhiteQueenMoves.AddRange(GetSouth(board, EligibleSquares, Piece, 7));
            WhiteQueenMoves.AddRange(GetSouthWest(board, EligibleSquares, Piece, 7));
            WhiteQueenMoves.AddRange(GetWest(board, EligibleSquares, Piece, 7));
            WhiteQueenMoves.AddRange(GetNorthWest(board, EligibleSquares, Piece, 7));

            return WhiteQueenMoves;

        }
    }
}
