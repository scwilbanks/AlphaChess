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

            if (board.TurnIsWhite)
            {
                Piece = board.WhiteQueen;
            }
            else if (!board.TurnIsWhite)
            {
                Piece = board.BlackQueen;
            }
            else
            {
                throw new Exception();
            }

            List<Tuple<ulong, ulong>> WhiteQueenMoves = new List<Tuple<ulong, ulong>>();

            WhiteQueenMoves.AddRange(GetNorth(board, Piece, 7));
            WhiteQueenMoves.AddRange(GetNorthEast(board, Piece, 7));
            WhiteQueenMoves.AddRange(GetEast(board, Piece, 7));
            WhiteQueenMoves.AddRange(GetSouthEast(board, Piece, 7));
            WhiteQueenMoves.AddRange(GetSouth(board, Piece, 7));
            WhiteQueenMoves.AddRange(GetSouthWest(board, Piece, 7));
            WhiteQueenMoves.AddRange(GetWest(board, Piece, 7));
            WhiteQueenMoves.AddRange(GetNorthWest(board, Piece, 7));

            return WhiteQueenMoves;

        }
    }
}
