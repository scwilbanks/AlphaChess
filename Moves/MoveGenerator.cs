﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace AlphaChess.Moves
{


    /// <summary>
    /// 
    /// MoveGenerator is a static class for generating all possible moves for 
    /// the player from the current position.
    /// 
    /// </summary>


    static class MoveGenerator
    {

        // Returns a List of tuples representing the set of moves to the North
        // for the current piece
        public static List<Tuple<ulong, ulong>> GetNorth(Board board, ulong Piece, int MaxMoves)
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

                    // If square occupied by player's piece
                    if ((Candidate & board.EligibleSquares) == 0)
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Current, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }


        // Returns a List of tuples representing the set of moves to the NorthEast
        // for the current piece
        public static List<Tuple<ulong, ulong>> GetNorthEast(Board board, ulong Piece, int MaxMoves)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
            ulong Candidate;

            for (int i = 1; i <= MaxMoves; i++)
            {
                
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
                if ((Current >= Math.Pow(2, 56)) || ((Current & HFile) > 0))
                {
                    break;
                }
                else
                {
                    Candidate = Current << 9;

                    // If square occupied by player's piece
                    if ((Candidate & board.EligibleSquares) == 0)
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Current, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }


        // Returns a List of tuples representing the set of moves to the East
        // for the current piece
        public static List<Tuple<ulong, ulong>> GetEast(Board board, ulong Piece, int MaxMoves)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
            ulong Candidate;

            for (int i = 1; i <= MaxMoves; i++)
            {
                // If on h file
                ulong HFile =
                    (ulong)Math.Pow(2, 7)
                  + (ulong)Math.Pow(2, 15)
                  + (ulong)Math.Pow(2, 23)
                  + (ulong)Math.Pow(2, 31)
                  + (ulong)Math.Pow(2, 39)
                  + (ulong)Math.Pow(2, 47)
                  + (ulong)Math.Pow(2, 55)
                  + (ulong)Math.Pow(2, 63);

                if ((Current & HFile) > 0)
                {
                    break;
                }
                else
                {
                    Candidate = Current << 1;

                    // If square occupied by player's piece
                    if ((Candidate & board.EligibleSquares) == 0)
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Current, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }

        // Returns a List of tuples representing the set of moves to the SouthEast
        // for the current piece
        public static List<Tuple<ulong, ulong>> GetSouthEast(Board board, ulong Piece, int MaxMoves)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
            ulong Candidate;

            for (int i = 1; i <= MaxMoves; i++)
            {

                ulong HFile =
                        (ulong)Math.Pow(2, 7)
                      + (ulong)Math.Pow(2, 15)
                      + (ulong)Math.Pow(2, 23)
                      + (ulong)Math.Pow(2, 31)
                      + (ulong)Math.Pow(2, 39)
                      + (ulong)Math.Pow(2, 47)
                      + (ulong)Math.Pow(2, 55)
                      + (ulong)Math.Pow(2, 63);

                // If on 1st Rank or h file
                if ((Current <= Math.Pow(2, 7)) || ((Current & HFile) > 0))
                {
                    break;
                }
                else
                {
                    Candidate = Current >> 7;

                    // If square occupied by player's piece
                    if ((Candidate & board.EligibleSquares) == 0)
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Current, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }


        // Returns a List of tuples representing the set of moves to the South
        // for the current piece
        public static List<Tuple<ulong, ulong>> GetSouth(Board board, ulong Piece, int MaxMoves)
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

                    // If square occupied by player's piece
                    if ((Candidate & board.EligibleSquares) == 0)
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Current, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }


        // Returns a List of tuples representing the set of moves to the SouthWest
        // for the current piece
        public static List<Tuple<ulong, ulong>> GetSouthWest(Board board, ulong Piece, int MaxMoves)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
            ulong Candidate;

            for (int i = 1; i <= MaxMoves; i++)
            {

                ulong AFile =
                    (ulong)Math.Pow(2, 0)
                  + (ulong)Math.Pow(2, 8)
                  + (ulong)Math.Pow(2, 16)
                  + (ulong)Math.Pow(2, 24)
                  + (ulong)Math.Pow(2, 32)
                  + (ulong)Math.Pow(2, 40)
                  + (ulong)Math.Pow(2, 48)
                  + (ulong)Math.Pow(2, 56);

                // If on 1st Rank or a file
                if ((Current <= Math.Pow(2, 7)) || ((Current & AFile) > 0))
                {
                    break;
                }
                else
                {
                    Candidate = Current >> 9;

                    // If square occupied by player's piece
                    if ((Candidate & board.EligibleSquares) == 0)
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Current, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }


        // Returns a List of tuples representing the set of moves to the West
        // for the current piece
        public static List<Tuple<ulong, ulong>> GetWest(Board board, ulong Piece, int MaxMoves)


        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
            ulong Candidate;

            for (int i = 1; i <= MaxMoves; i++)
            {
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

                if ((Current & AFile) > 0)
                {
                    break;
                }
                else
                {
                    Candidate = Current >> 1;

                    // If square occupied by player's piece
                    if ((Candidate & board.EligibleSquares) == 0)
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Current, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }


        // Returns a List of tuples representing the set of moves to the NorthWest
        // for the current piece
        public static List<Tuple<ulong, ulong>> GetNorthWest(Board board, ulong Piece, int MaxMoves)
        {
            List<Tuple<ulong, ulong>> MoveList = new List<Tuple<ulong, ulong>>();

            ulong Current = Piece;
            ulong Candidate;

            for (int i = 1; i <= MaxMoves; i++)
            {

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
                if ((Current >= Math.Pow(2, 8)) || ((Current & AFile) > 0))
                {
                    break;
                }
                else
                {
                    Candidate = Current << 7;

                    // If square occupied by player's piece
                    if ((Candidate & board.EligibleSquares) == 0)
                    {
                        break;
                    }
                    else
                    {

                        MoveList.Add(Tuple.Create(Current, Candidate));
                        Current = Candidate;
                    }
                }
            }

            return MoveList;

        }


        // Returns an array of Tuples representing all possible moves for the 
        // White King from the current board position
        public static Tuple<ulong, ulong>[] GetWhiteKingMoves(Board board)
        {
            ulong Piece = board.WhiteKing;

            List<Tuple<ulong, ulong>> WhiteKingMoves = new List<Tuple<ulong, ulong>>();

            WhiteKingMoves.Concat(GetNorth(board, Piece, 1));

            WhiteKingMoves.Concat(GetNorthEast(board, Piece, 1));

            WhiteKingMoves.Concat(GetEast(board, Piece, 1));

            WhiteKingMoves.Concat(GetSouthEast(board, Piece, 1));

            WhiteKingMoves.Concat(GetSouth(board, Piece, 1));

            WhiteKingMoves.Concat(GetSouthWest(board, Piece, 1));

            WhiteKingMoves.Concat(GetWest(board, Piece, 1));

            WhiteKingMoves.Concat(GetNorthWest(board, Piece, 1));

            return WhiteKingMoves.ToArray();

        }


        // Splits a ulong representing multiple pieces into an array of ulongs
        public static ulong[] ParsePieces(ulong Pieces)
        {
            List<ulong> PiecesList = new List<ulong>();

            int Place = 0;
            ulong Current = Pieces;
            while (Current > 0)
            {
                if (Current % 2 == 1)
                {
                    PiecesList.Add((ulong)Math.Pow(2, Place));
                    
                }
                Place++;
                Current >>= 1;
            }
            return PiecesList.ToArray();
        }


        // Returns an array of Tuples representing all possible moves for the 
        // White Pawns from the current board position
        public static Tuple<ulong, ulong>[] GetWhitePawnsMoves(Board board)
        {
            ulong[] Pieces = ParsePieces(board.WhitePawns);

            List<Tuple<ulong, ulong>> WhitePawnsMoves = new List<Tuple<ulong, ulong>>();

            foreach (ulong Piece in Pieces)
            {
                foreach (Tuple<ulong, ulong> Move in GetNorth(board, Piece, 1))
                {
                    WhitePawnsMoves.Add(Move);
                }
            }

            return WhitePawnsMoves.ToArray();
        }

        // Returns an array of Tuples representing all possible moves for all 
        // pieces from the current board position
        public static Tuple<ulong, ulong>[] GetMoves(Board board)
        {

            List<Tuple<ulong, ulong>> MovesList = new List<Tuple<ulong, ulong>>();
            ulong EligibleSquares;


            if (board.TurnIsWhite)
            {
                EligibleSquares = ~board.WhitePieces;
                MovesList.Concat(GetWhiteKingMoves(board));

                foreach (var Move in GetWhitePawnsMoves(board))
                {
                    MovesList.Add(Move);
                }

                // build rest of white movelist
            }
            else if (!board.TurnIsWhite)
            {
                EligibleSquares = ~board.BlackPieces;
                // Build black movelist
            }
            else
            {
                throw new Exception();
            }


            return MovesList.ToArray();
        }

    }
}
