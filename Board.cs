using AlphaChess.Moves;
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


        // Properties


        // Tree references
        Board Parent { get; set; }
        Board[] Children { get; set; }

        //White Pieces
        public ulong WhiteKing { get; set; }
        public ulong WhiteQueen { get; set; }
        public ulong WhiteRooks { get; set; }
        public ulong WhiteBishops { get; set; }
        public ulong WhiteKnights { get; set; }
        public ulong WhitePawns { get; set; }
        public ulong WhitePieces { get; set; }

        // Black Pieces
        public ulong BlackKing { get; set; }
        public ulong BlackQueen { get; set; }
        public ulong BlackRooks { get; set; }
        public ulong BlackBishops { get; set; }
        public ulong BlackKnights { get; set; }
        public ulong BlackPawns { get; set; }
        public ulong BlackPieces { get; set; }

        // Various Board Data
        public ulong EligibleSquares { get; set; }
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
            InitializeChildren();
        }


        // Constructor for all other boards, takes current board and move and returns new Board object after that move is made
        public Board(Board board, Tuple<ulong, ulong> move)
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
                         (ulong)Math.Pow(2, 55);

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
            Parent = null;
            EligibleSquares = TurnIsWhite ? ~BlackPieces : ~WhitePieces;
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
        private void MakeMove(Tuple<ulong, ulong> move)
        {
            // To be implemented
        }


        // Initializes child boards and sets the Children Property
        private void InitializeChildren()
        {
            Tuple<ulong, ulong>[] MovesArray = MoveGenerator.GetMoves(this);
            List<Board> ChildrenList = new List<Board>();

            foreach (Tuple<ulong, ulong> move in MovesArray)
            {
                Board ChildBoard = new Board(this, move);
                ChildrenList.Add(ChildBoard);
            }

            Children = ChildrenList.ToArray();

        }
    }
}
