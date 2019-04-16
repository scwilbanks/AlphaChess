using System.Collections;

namespace AlphaChess
{
    // This class encapsulates a BitArray that represents a unique board state.
    // Each unique board will be used as a key in the Game's hashtable.
    // 
    // Values that Board will hold:
    // If a piece is taken: 1 bit x 32 pieces
    // Pieces' places on board 6 bits x 32 pieces = 192 bits
    // Turn = 1 bit
    // If white can castle: 1 bit
    // If black can castle: 1 bit

    public class Board
    {

        bool[] StateStartingValue = new bool[]
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
        // TODO
        // WR1
        false, false, false, false, false, false,

        // WN1
        false, false, false, false, false, false,

        // WB1
        false, false, false, false, false, false,

        // WQ
        false, false, false, false, false, false,

        // WK
        false, false, false, false, false, false,

        // WB2
        false, false, false, false, false, false,

        // WN2
        false, false, false, false, false, false,

        // WR2
        false, false, false, false, false, false,

        // Black Pawns, 1-8
        // TODO
        // BP1
        false, false, false, false, false, false,

        // BP2
        false, false, false, false, false, false,

        // BP3
        false, false, false, false, false, false,

        // BP4
        false, false, false, false, false, false,

        // BP5
        false, false, false, false, false, false,

        // BP6
        false, false, false, false, false, false,

        // BP7
        false, false, false, false, false, false,

        // BP8
        false, false, false, false, false, false,

        // Black Other Pieces, 1-8
        // TODO
        // BR1
        false, false, false, false, false, false,

        // BN1
        false, false, false, false, false, false,

        // BB1
        false, false, false, false, false, false,

        // BQ
        false, false, false, false, false, false,

        // BK
        false, false, false, false, false, false,

        // BB2
        false, false, false, false, false, false,

        // BN2
        false, false, false, false, false, false,

        // BR2
        false, false, false, false, false, false,

        };


        static BitArray GetStartingState()
        {
            return new BitArray(0);
        }

    }
}
