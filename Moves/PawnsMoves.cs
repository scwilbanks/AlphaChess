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
        // TODO: en passant
        // TODO: promotion
        public static List<Tuple<ulong, ulong>> GetPawnsMoves(Board board)
        {

            ulong[] Pieces;
            ulong EligibleSquares;

            if (board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.WhitePawns);
                EligibleSquares = board.WhiteEligibleSquares;
            }
            else if (!board.TurnIsWhite)
            {
                Pieces = ParsePieces(board.BlackPawns);
                EligibleSquares = board.BlackEligibleSquares;
            }
            else
            {
                throw new Exception();
            }


            List<Tuple<ulong, ulong>> PawnsMoves = new List<Tuple<ulong, ulong>>();

            foreach (ulong Piece in Pieces)
            {
                if (board.TurnIsWhite)
                {
                    if (Piece >= Math.Pow(2, 8) && Piece <= Math.Pow(2, 15))
                    {
                        PawnsMoves.AddRange(GetPawnNorth(board, Piece, 2));
                    }
                    else if (Piece < Math.Pow(2, 8) || Piece > Math.Pow(2, 15))
                    {
                        PawnsMoves.AddRange(GetPawnNorth(board, Piece, 1));
                    }
                    else
                    {
                        throw new Exception();
                    }

                    PawnsMoves.AddRange(GetPawnAttackNorthEast(board, Piece));
                    PawnsMoves.AddRange(GetPawnAttackNorthWest(board, Piece));

                }
                else if (!board.TurnIsWhite)
                {
                    if (Piece >= Math.Pow(2, 48) && Piece <= Math.Pow(2, 55))
                    {
                        PawnsMoves.AddRange(GetPawnSouth(board, Piece, 2));
                    }
                    else if (Piece < Math.Pow(2, 48) || Piece > Math.Pow(2, 55))
                    {
                        PawnsMoves.AddRange(GetPawnSouth(board, Piece, 1));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                
            }

            return PawnsMoves;
        }


        // Returns a List of tuples representing zero or one moves for a white 
        // pawn to attack the square up one and right one 
        public static List<Tuple<ulong, ulong>> GetPawnAttackNorthEast(Board board, ulong Piece)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
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

            // If on 8th Rank or h file
            if ((Piece < Math.Pow(2, 56)) && ((Current & HFile) == 0))
            {
                Candidate = Current << 9;

                if ((Candidate & board.BlackPieces) > 0)
                {
                    MoveList.Add(Tuple.Create(Piece, Candidate));
                }
            }


            return MoveList;

        }


        // Returns a List of tuples representing zero or one moves for a white 
        // pawn to attack the square up one and left one 
        public static List<Tuple<ulong, ulong>> GetPawnAttackNorthWest(Board board, ulong Piece)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
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

            // If on 8th Rank or a file
            if ((Piece < Math.Pow(2, 56)) && ((Current & AFile) == 0))
            {
                Candidate = Current << 7;

                if ((Candidate & board.BlackPieces) > 0)
                {
                    MoveList.Add(Tuple.Create(Piece, Candidate));
                }
            }


            return MoveList;

        }


        // Returns a List of tuples representing zero or one moves for a black 
        // pawn to attack the square down one and right one 
        public static List<Tuple<ulong, ulong>> GetPawnAttackSouthEast(Board board, ulong Piece)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
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

            // If not on 1st Rank or H File
            if ((Piece >= Math.Pow(2, 8)) && ((Current & HFile) == 0))
            {
                Candidate = Current >> 7;

                if ((Candidate & board.WhitePieces) > 0)
                {
                    MoveList.Add(Tuple.Create(Piece, Candidate));
                }
            }


            return MoveList;

        }


        // Returns a List of tuples representing zero or one moves for a black 
        // pawn to attack the square down one and left one 
        public static List<Tuple<ulong, ulong>> GetPawnAttackSouthWest(Board board, ulong Piece)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
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

            // If not on 1st Rank or A File
            if ((Piece >= Math.Pow(2, 8)) && ((Current & AFile) == 0))
            {
                Candidate = Current >> 9;

                if ((Candidate & board.WhitePieces) > 0)
                {
                    MoveList.Add(Tuple.Create(Piece, Candidate));
                }
            }


            return MoveList;

        }

        // Returns a List of tuples representing the set of moves to the North
        // for the current Pawn
        public static List<Tuple<ulong, ulong>> GetPawnNorth(Board board, ulong Piece, int MaxMoves)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
            ulong Candidate;

            for (int i = 1; i <= MaxMoves; i++)
            {
                // If on 8th Rank
                if (Current >= Math.Pow(2, 56))
                {
                    break;
                }
                else
                {
                    Candidate = Current << 8;

                    // If square occupied by any piece
                    if (((Candidate & board.WhitePieces) > 0) || ((Candidate & board.BlackPieces) > 0))
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Piece, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }

        // Returns a List of tuples representing the set of moves to the South
        // for the current Pawn
        public static List<Tuple<ulong, ulong>> GetPawnSouth(Board board, ulong Piece, int MaxMoves)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
            ulong Candidate;

            for (int i = 1; i <= MaxMoves; i++)
            {
                // If on 1st Rank
                if (Current <= Math.Pow(2, 7))
                {
                    break;
                }
                else
                {
                    Candidate = Current >> 8;

                    // If square occupied by any piece
                    if (((Candidate & board.WhitePieces) > 0) || ((Candidate & board.BlackPieces) > 0))
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Piece, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }



    }
}
