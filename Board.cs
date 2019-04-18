using System;
using System.Collections;
using System.Collections.Generic;

namespace AlphaChess
{

    /// <summary>
    /// 
    /// This class encapsulates a set of bitboards to represent the board state.
    /// It also holds the current turn, and whether or not each player can castle.
    /// 
    /// </summary>


    public class Board
    {

        // Static Methods
        // This will likely be deleted, pending the bitboard implementation
        public static BitArray GetStartingBoard()
        {
            return new BitArray(StartingBoardArray);
        }


        // This will likely be deleted, pending the bitboard implementation
        public static bool[] StartingBoardArray = new bool[]
        {

            // Taken
            // White Pawns, index 0
            false, false, false, false, false, false, false, false,

            // White Other Pieces, index 8
            false, false, false, false, false, false, false, false,

            // Black Pawns, index 16
            false, false, false, false, false, false, false, false,

            // Black Other Pieces, index 24
            false, false, false, false, false, false, false, false,


            // Locations
            // First three bools represent A-H columns
            // Second three bools represent 1-8 rows
            //
            // WP1, index 32
            false, false, false, false, false, true,

            // WP2, index 38
            false, false, true, false, false, true,

            // WP3, index 44
            false, true, false, false, false, true,

            // WP4, index 50
            false, true, true, false, false, true,

            // WP5, index 56
            true, false, false, false, false, true,

            // WP6, index 62
            true, false, true, false, false, true,

            // WP7, index 68
            true, true, false, false, false, true,

            // WP8, index 74
            true, true, true, false, false, true,

            // White Other Pieces, 1-8
            // WR1, index 80
            false, false, false, false, false, false,

            // WN1, index 86
            false, false, true, false, false, false,

            // WB1, index 92
            false, true, false, false, false, false,

            // WQ, index 98
            false, true, true, false, false, false,

            // WK, index 104
            true, false, false, false, false, false,

            // WB2, index 110
            true, false, true, false, false, false,

            // WN2, index 116
            true, true, false, false, false, false,

            // WR2, index 122
            true, true, true, false, false, false,

            // Black Pawns, 1-8
            // BP1, index 128
            false, false, false, true, true, false,

            // BP2, index 134
            false, false, true, true, true, false,

            // BP3, index 140
            false, true, false, true, true, false,

            // BP4, index 146
            false, true, true, true, true, false,

            // BP5, index 152
            true, false, false, true, true, false,

            // BP6, index 158
            true, false, true, true, true, false,

            // BP7, index 164
            true, true, false, true, true, false,

            // BP8, index 170
            true, true, true, true, true, false,

            // Black Other Pieces, 1-8
            // BR1, index 176
            false, false, false, true, true, true,

            // BN1, index 182
            false, false, true, true, true, true,

            // BB1, index 188
            false, true, false, true, true, true,

            // BQ, index 194
            false, true, true, true, true, true,

            // BK, index 200
            true, false, false, true, true, true,

            // BB2, index 206
            true, false, true, true, true, true,

            // BN2, index 212
            true, true, false, true, true, true,

            // BR2, index 218
            true, true, true, true, true, true,

            // Turn, index 224
            true,

            // Can White castle, index 225
            true,

            // Can Black castle, index 226
            true

        };


        // Properties
        // Tree references
        Board Parent { get; set; }
        Board[] Children { get; set; }

        //White Pieces
        ulong WhiteKing { get; set; }
        ulong WhiteQueen { get; set; }
        ulong WhiteRooks { get; set; }
        ulong WhiteBishops { get; set; }
        ulong WhiteKnights { get; set; }
        ulong WhitePawns { get; set; }
        ulong WhitePieces { get; set; }

        // Black Pieces
        ulong BlackKing { get; set; }
        ulong BlackQueen { get; set; }
        ulong BlackRooks { get; set; }
        ulong BlackBishops { get; set; }
        ulong BlackKnights { get; set; }
        ulong BlackPawns { get; set; }
        ulong BlackPieces { get; set; }

        // Various Board Data
        public bool TurnIsWhite { get; set; }
        bool CanWhiteCastle { get; set; }
        bool CanBlackCastle { get; set; }
        bool IsWhiteInCheck { get; set; }
        bool IsBlackInCheck { get; set; }

        // MCTS Data
        int VisitNumber { get; set; }
        int Value { get; set; }
        float UCT { get; set; }
        

        // Constructors

        // Constructor for starting board
        public Board()
        {
            InitializeBoard();
            SetStartingBoardData();
        }


        // Constructor for all other boards, takes current board and move and returns new Board object after that move is made
        public Board(Board board, Int16 move)
        {
            MakeMove(move);

        }


        // Initializers

        // Initializes the White pieces bitboards properties
        private void InitializeWhitePieces()
        {
            WhiteKing = 16;
            WhiteQueen = 8;
            WhiteRooks = 129;
            WhiteBishops = 36;
            WhiteKnights = 66;
            WhitePawns = 65280;

            WhitePieces |= WhiteKing;
            WhitePieces |= WhiteQueen;
            WhitePieces |= WhiteRooks;
            WhitePieces |= WhiteBishops;
            WhitePieces |= WhiteKnights;
            WhitePieces |= WhitePawns;

        }


        // Initializes the Black pieces bitboards properties
        private void InitializeBlackPieces()
        {
            BlackKing = (ulong)Math.Pow(2, 60);
            BlackQueen = (ulong)Math.Pow(2, 59);
            BlackRooks = (ulong)Math.Pow(2, 56) + (ulong)Math.Pow(2, 63);
            BlackBishops = (ulong)Math.Pow(2, 58) + (ulong)Math.Pow(2, 61);
            BlackKnights = (ulong)Math.Pow(2, 57) + (ulong)Math.Pow(2, 62);
            BlackPawns = (ulong)Math.Pow(2, 48) +
                         (ulong)Math.Pow(2, 49) +
                         (ulong)Math.Pow(2, 50) +
                         (ulong)Math.Pow(2, 51) +
                         (ulong)Math.Pow(2, 52) +
                         (ulong)Math.Pow(2, 53) +
                         (ulong)Math.Pow(2, 54) +
                         (ulong)Math.Pow(2, 45);

            BlackPieces |= BlackKing;
            BlackPieces |= BlackQueen;
            BlackPieces |= BlackRooks;
            BlackPieces |= BlackBishops;
            BlackPieces |= BlackKnights;
            BlackPieces |= BlackPawns;

        }


        // Sets values for initial board position
        private void SetStartingBoardData()
        {
            TurnIsWhite = true;
            CanWhiteCastle = true;
            CanBlackCastle = true;
            IsWhiteInCheck = false;
            IsBlackInCheck = false;
            VisitNumber = 0;
            Value = 0;
        }


        // Initializes White and Black pieces bitboards
        private void InitializeBoard()
        {

            InitializeWhitePieces();
            InitializeBlackPieces();


        }


        // Update Methods

        // Changes all board data to coorespond to the state after the move is made
        // TODO
        private void MakeMove(Int16 move)
        {

        }


        // Initializes child boards and sets the Children Property
        // TODO
        private void InitializeChildren()
        {

        }
    }
}
