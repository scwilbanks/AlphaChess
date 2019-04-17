using System;
using System.Collections;


namespace AlphaChess
{

    /* This class encapsulates a BitArray that represents a unique board state.
     * Each unique board will be used as a key in the Game's hashtable.
     * 
     * Values that Board will hold:
     * If a piece is taken: 1 bit x 32 pieces
     * Pieces' places on board 6 bits x 32 pieces = 192 bits
     * Turn = 1 bit
     * If white can castle: 1 bit
     * If black can castle: 1 bit */


    public static class Board
    {

        // Static Methods
        public static BitArray GetStartingBoard()
        {
            return new BitArray(StartingBoardArray);
        }


        public static bool[] StartingBoardArray = new bool[]
        {

            // Taken
            // White Pawns
            false, false, false, false, false, false, false, false,

            // White Other Pieces
            false, false, false, false, false, false, false, false,

            // Black Pawns
            false, false, false, false, false, false, false, false,

            // Black Other Pieces
            false, false, false, false, false, false, false, false,


            // Locations
            // First three bools represent A-H columns
            // Second three bools represent 1-8 rows
            //
            // WP1
            false, false, false, false, false, true,

            // WP2
            false, false, true, false, false, true,

            // WP3
            false, true, false, false, false, true,

            // WP4
            false, true, true, false, false, true,

            // WP5
            true, false, false, false, false, true,

            // WP6
            true, false, true, false, false, true,

            // WP7
            true, true, false, false, false, true,

            // WP8
            true, true, true, false, false, true,

            // White Other Pieces, 1-8
            // WR1
            false, false, false, false, false, false,

            // WN1
            false, false, true, false, false, false,

            // WB1
            false, true, false, false, false, false,

            // WQ
            false, true, true, false, false, false,

            // WK
            true, false, false, false, false, false,

            // WB2
            true, false, true, false, false, false,

            // WN2
            true, true, false, false, false, false,

            // WR2
            true, true, true, false, false, false,

            // Black Pawns, 1-8
            // BP1
            false, false, false, true, true, false,

            // BP2
            false, false, true, true, true, false,

            // BP3
            false, true, false, true, true, false,

            // BP4
            false, true, true, true, true, false,

            // BP5
            true, false, false, true, true, false,

            // BP6
            true, false, true, true, true, false,

            // BP7
            true, true, false, true, true, false,

            // BP8
            true, true, true, true, true, false,

            // Black Other Pieces, 1-8
            // BR1
            false, false, false, true, true, true,

            // BN1
            false, false, true, true, true, true,

            // BB1
            false, true, false, true, true, true,

            // BQ
            false, true, true, true, true, true,

            // BK
            true, false, false, true, true, true,

            // BB2
            true, false, true, true, true, true,

            // BN2
            true, true, false, true, true, true,

            // BR2
            true, true, true, true, true, true,

            // Turn
            true,

            // Can White castle
            true,

            // Can Black castle
            true

        };

    }
}
