using System;
using System.Collections;


namespace AlphaChess
{

    /* This class encapsulates a set of bitboards to represent the board state.
     * It also holds the current turn, and whether or not each player can castle.*/


    public class Board
    {

        // Static Methods
        public static BitArray GetStartingBoard()
        {
            return new BitArray(StartingBoardArray);
        }

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
        UInt64 WhiteKing;
        UInt64 WhiteQueen;
        UInt64 WhiteRooks;
        UInt64 WhiteBishops;
        UInt64 WhiteKnights;
        UInt64 WhitePawns;

        UInt64 WhitePieces;

        UInt64 BlackKing;
        UInt64 BlackQueen;
        UInt64 BlackRooks;
        UInt64 BlackBishops;
        UInt64 BlackKnights;
        UInt64 BlackPawns;

        UInt64 BlackPieces;


        public Board()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
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


            BlackKing = (UInt64)Math.Pow(2, 60);
            BlackQueen = (UInt64)Math.Pow(2, 59);
            BlackRooks = (UInt64)Math.Pow(2, 56) + (UInt64)Math.Pow(2, 63);
            BlackBishops = (UInt64)Math.Pow(2, 58) + (UInt64)Math.Pow(2, 61);
            BlackKnights = (UInt64)Math.Pow(2, 57) + (UInt64)Math.Pow(2, 62);
            BlackPawns = (UInt64)Math.Pow(2, 48) +
                         (UInt64)Math.Pow(2, 49) +
                         (UInt64)Math.Pow(2, 50) +
                         (UInt64)Math.Pow(2, 51) +
                         (UInt64)Math.Pow(2, 52) +
                         (UInt64)Math.Pow(2, 53) +
                         (UInt64)Math.Pow(2, 54) +
                         (UInt64)Math.Pow(2, 45);

            BlackPieces |= BlackKing;
            BlackPieces |= BlackQueen;
            BlackPieces |= BlackRooks;
            BlackPieces |= BlackBishops;
            BlackPieces |= BlackKnights;
            BlackPieces |= BlackPawns;


        }

    }
}
