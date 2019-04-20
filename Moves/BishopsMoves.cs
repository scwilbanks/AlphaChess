using System;
using System.Collections.Generic;


namespace AlphaChess.Moves
{
    static partial class MoveGenerator
    {

        // Returns an array of Tuples representing all possible moves for the 
        // current player's Bishops from the current board position
        public static List<Tuple<ulong, ulong>> GetBishopsMoves(Board board)
        {

            ulong[] Pieces;

            if (board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.WhiteBishops);
            }
            else if (!board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.BlackBishops);
            }
            else
            {
                throw new Exception();
            }

            List<Tuple<ulong, ulong>> BishopsMoves = new List<Tuple<ulong, ulong>>();

            foreach (ulong Piece in Pieces)
            {
                BishopsMoves.AddRange(GetNorthEast(board, Piece, 7));
                BishopsMoves.AddRange(GetSouthEast(board, Piece, 7));
                BishopsMoves.AddRange(GetSouthWest(board, Piece, 7));
                BishopsMoves.AddRange(GetNorthWest(board, Piece, 7));
            }

            return BishopsMoves;
        }


    }
}
