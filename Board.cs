﻿using AlphaChess.Moves;
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
    /// TODO: Castling, in check
    /// 
    /// </summary>


    public class Board
    {


        // Properties


        // Tree references
        public Board Parent { get; set; }
        public Board[] Children { get; set; }
        public Tuple<ulong, ulong> Move { get; set; }
        Tuple<ulong, ulong>[] WhiteMoves { get; set; }
        Tuple<ulong, ulong>[] BlackMoves { get; set; }

        //White Pieces
        public ulong WhiteKing { get; set; }
        public ulong WhiteQueen { get; set; }
        public ulong WhiteRooks { get; set; }
        public ulong WhiteBishops { get; set; }
        public ulong WhiteKnights { get; set; }
        public ulong WhitePawns { get; set; }
        public ulong WhitePieces { get; set; }
        public ulong WhiteAttacks { get; set; }

        // Black Pieces
        public ulong BlackKing { get; set; }
        public ulong BlackQueen { get; set; }
        public ulong BlackRooks { get; set; }
        public ulong BlackBishops { get; set; }
        public ulong BlackKnights { get; set; }
        public ulong BlackPawns { get; set; }
        public ulong BlackPieces { get; set; }
        public ulong BlackAttacks { get; set; }

        // Various Board Data
        public ulong WhiteEligibleSquares { get; set; }
        public ulong BlackEligibleSquares { get; set; }

        public bool TurnIsWhite { get; set; }
        public bool CanWhiteCastle { get; set; }
        public bool CanBlackCastle { get; set; }
        public bool WhiteInCheck { get; set; }
        public bool BlackInCheck { get; set; }
        public bool WhiteInCheckMate { get; set; }
        public bool BlackInCheckMate { get; set; }

        // MCTS Data
        public int Number { get; set; }
        public int Value { get; set; }
        public int WhiteWins {get; set;}
        public int BlackWins { get; set; }
        public double WhiteUCT { get; set; }
        public double BlackUCT { get; set; }


        // Constructors


        // Constructor for starting board
        public Board()
        {
            InitializeBoard();
            SetStartingBoardData();
            InitializeChildren();
        }


        // Constructor for all other boards, takes current board and move and returns new Board object after that move is made
        public Board(Board board, Tuple<ulong, ulong> Move)
        {

            if (board.TurnIsWhite)
            {
                InitializeWhitesChildBoard(board, Move);
            }
            else if (!board.TurnIsWhite)
            {
                InitializeBlacksChildBoard(board, Move);
            }
            else
            {
                throw new Exception();
            }
            

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


        private ulong GetWhiteAttacks()
        {
            ulong Attacks = 0;
            

            foreach (Tuple<ulong, ulong> Move in WhiteMoves)
            {
                Attacks |= Move.Item2;
            }

            return Attacks;

        }


        private ulong GetBlackAttacks()
        {
            ulong Attacks = 0;

            foreach (Tuple<ulong, ulong> Move in BlackMoves)
            {
                Attacks |= Move.Item2;
            }


            return Attacks;

        }


        // Sets values for initial board position
        private void SetStartingBoardData()
        {
            Parent = null;


            
            WhiteEligibleSquares = ~WhitePieces;
            BlackEligibleSquares = ~BlackPieces;


            BlackMoves = MoveGenerator.GetMoves(this);
            TurnIsWhite = true;
            WhiteMoves = MoveGenerator.GetMoves(this);
            

            BlackAttacks = GetBlackAttacks();
            WhiteAttacks = GetWhiteAttacks();

            this.WhiteInCheck = IsWhiteInCheck();
            this.BlackInCheck = IsBlackInCheck();



            CanWhiteCastle = true;
            CanBlackCastle = true;
            WhiteInCheck = false;
            BlackInCheck = false;
            Number = 1;
            Value = 0;
        }


        // Initializes White and Black pieces bitboards
        private void InitializeBoard()
        {

            InitializeWhitePieces();
            InitializeBlackPieces();


        }


        // Update Methods

        

        // Changes all board data to coorespond to the state after white moves
        private void InitializeWhitesChildBoard(Board Parent, Tuple<ulong, ulong> Move)
        {
            this.Parent = Parent;

            this.Move = Move;



            this.WhiteKing = (Parent.WhiteKing & Move.Item1) > 0 ? Parent.WhiteKing ^ Move.Item1 | Move.Item2 : Parent.WhiteKing;
            this.WhiteQueen = (Parent.WhiteQueen & Move.Item1) > 0 ? Parent.WhiteQueen ^ Move.Item1 | Move.Item2 : Parent.WhiteQueen;
            this.WhiteRooks = (Parent.WhiteRooks & Move.Item1) > 0 ? Parent.WhiteRooks ^ Move.Item1 | Move.Item2 : Parent.WhiteRooks;
            this.WhiteBishops = (Parent.WhiteBishops & Move.Item1) > 0 ? Parent.WhiteBishops ^ Move.Item1 | Move.Item2 : Parent.WhiteBishops;
            this.WhiteKnights = (Parent.WhiteKnights & Move.Item1) > 0 ? Parent.WhiteKnights ^ Move.Item1 | Move.Item2 : Parent.WhiteKnights;
            this.WhitePawns = (Parent.WhitePawns & Move.Item1) > 0 ? Parent.WhitePawns ^ Move.Item1 | Move.Item2 : Parent.WhitePawns;
            this.WhitePieces |= this.WhiteKing;
            this.WhitePieces |= this.WhiteQueen;
            this.WhitePieces |= this.WhiteRooks;
            this.WhitePieces |= this.WhiteBishops;
            this.WhitePieces |= this.WhiteKnights;
            this.WhitePieces |= this.WhitePawns;


            this.BlackKing = (Parent.BlackKing & Move.Item2) > 0 ? Parent.BlackKing ^ Move.Item2: Parent.BlackKing;
            this.BlackQueen = (Parent.BlackQueen & Move.Item2) > 0 ? Parent.BlackQueen ^ Move.Item2 : Parent.BlackQueen;
            this.BlackRooks = (Parent.BlackRooks & Move.Item2) > 0 ? Parent.BlackRooks ^ Move.Item2 : Parent.BlackRooks;
            this.BlackBishops = (Parent.BlackBishops & Move.Item2) > 0 ? Parent.BlackBishops ^ Move.Item2 : Parent.BlackBishops;
            this.BlackKnights = (Parent.BlackKnights & Move.Item2) > 0 ? Parent.BlackKnights ^ Move.Item2 : Parent.BlackKnights;
            this.BlackPawns = (Parent.BlackPawns & Move.Item2) > 0 ? Parent.BlackPawns ^ Move.Item2 : Parent.BlackPawns;
            this.BlackPieces |= this.BlackKing;
            this.BlackPieces |= this.BlackQueen;
            this.BlackPieces |= this.BlackRooks;
            this.BlackPieces |= this.BlackBishops;
            this.BlackPieces |= this.BlackKnights;
            this.BlackPieces |= this.BlackPawns;

            WhiteEligibleSquares = ~WhitePieces;
            BlackEligibleSquares = ~BlackPieces;

            
            WhiteMoves = MoveGenerator.GetMoves(this);
            this.TurnIsWhite = false;
            BlackMoves = MoveGenerator.GetMoves(this);

            this.BlackAttacks = GetBlackAttacks();
            this.WhiteAttacks = GetWhiteAttacks();

            this.WhiteInCheck = IsWhiteInCheck();
            this.BlackInCheck = IsBlackInCheck();


            this.CanWhiteCastle = true;
            this.CanBlackCastle = true;

            this.Value = this.CalcValue();


        }

        // Changes all board data to coorespond to the state after Black moves
        private void InitializeBlacksChildBoard(Board Parent, Tuple<ulong, ulong> Move)
        {
            this.Parent = Parent;

            this.Move = Move;


            this.WhiteKing = (Parent.WhiteKing & Move.Item2) > 0 ? Parent.WhiteKing ^ Move.Item2 : Parent.WhiteKing;
            this.WhiteQueen = (Parent.WhiteQueen & Move.Item2) > 0 ? Parent.WhiteQueen ^ Move.Item2 : Parent.WhiteQueen;
            this.WhiteRooks = (Parent.WhiteRooks & Move.Item2) > 0 ? Parent.WhiteRooks ^ Move.Item2 : Parent.WhiteRooks;
            this.WhiteBishops = (Parent.WhiteBishops & Move.Item2) > 0 ? Parent.WhiteBishops ^ Move.Item2 : Parent.WhiteBishops;
            this.WhiteKnights = (Parent.WhiteKnights & Move.Item2) > 0 ? Parent.WhiteKnights ^ Move.Item2 : Parent.WhiteKnights;
            this.WhitePawns = (Parent.WhitePawns & Move.Item2) > 0 ? Parent.WhitePawns ^ Move.Item2 : Parent.WhitePawns;
            this.WhitePieces |= this.WhiteKing;
            this.WhitePieces |= this.WhiteQueen;
            this.WhitePieces |= this.WhiteRooks;
            this.WhitePieces |= this.WhiteBishops;
            this.WhitePieces |= this.WhiteKnights;
            this.WhitePieces |= this.WhitePawns;


            this.BlackKing = (Parent.BlackKing & Move.Item1) > 0 ? Parent.BlackKing ^ Move.Item1 | Move.Item2 : Parent.BlackKing;
            this.BlackQueen = (Parent.BlackQueen & Move.Item1) > 0 ? Parent.BlackQueen ^ Move.Item1 | Move.Item2 : Parent.BlackQueen;
            this.BlackRooks = (Parent.BlackRooks & Move.Item1) > 0 ? Parent.BlackRooks ^ Move.Item1 | Move.Item2 : Parent.BlackRooks;
            this.BlackBishops = (Parent.BlackBishops & Move.Item1) > 0 ? Parent.BlackBishops ^ Move.Item1 | Move.Item2 : Parent.BlackBishops;
            this.BlackKnights = (Parent.BlackKnights & Move.Item1) > 0 ? Parent.BlackKnights ^ Move.Item1 | Move.Item2 : Parent.BlackKnights;
            this.BlackPawns = (Parent.BlackPawns & Move.Item1) > 0 ? Parent.BlackPawns ^ Move.Item1 | Move.Item2 : Parent.BlackPawns;
            this.BlackPieces |= this.BlackKing;
            this.BlackPieces |= this.BlackQueen;
            this.BlackPieces |= this.BlackRooks;
            this.BlackPieces |= this.BlackBishops;
            this.BlackPieces |= this.BlackKnights;
            this.BlackPieces |= this.BlackPawns;

            WhiteEligibleSquares = ~WhitePieces;
            BlackEligibleSquares = ~BlackPieces;

            
            


            BlackMoves = MoveGenerator.GetMoves(this);
            this.TurnIsWhite = true;
            WhiteMoves = MoveGenerator.GetMoves(this);

            this.BlackAttacks = GetBlackAttacks();
            this.WhiteAttacks = GetWhiteAttacks();

            this.WhiteInCheck = IsWhiteInCheck();
            this.BlackInCheck = IsBlackInCheck();


            this.CanWhiteCastle = true;
            this.CanBlackCastle = true;
            this.Value = this.CalcValue();


        }


        // Initializes child boards and sets the Children Property
        public void InitializeChildren()
        {

            List<Board> ChildrenList = new List<Board>();
            Tuple<ulong, ulong>[] Moves = this.TurnIsWhite ? this.WhiteMoves : this.BlackMoves;

            foreach (Tuple<ulong, ulong> move in Moves)
            {
                Board ChildBoard = new Board(this, move);
                if (this.TurnIsWhite)
                {
                    if (!ChildBoard.WhiteInCheck)
                    {
                        ChildrenList.Add(ChildBoard);
                    }
                    else
                    {
                        
                    }
                }  
                else if (!this.TurnIsWhite)
                {
                    if (!ChildBoard.BlackInCheck)
                    {
                        ChildrenList.Add(ChildBoard);
                    }
                    else
                    {

                    }
                }



            }
            if (ChildrenList.Count == 0)
            {

            }


            this.Children = ChildrenList.ToArray();

            this.Number = 1;
            this.WhiteInCheckMate = IsWhiteInCheckMate();
            this.BlackInCheckMate = IsBlackInCheckMate();
            this.WhiteWins = this.BlackInCheckMate ? 1 : 0;
            this.BlackWins = this.WhiteInCheckMate ? 1 : 0;

        }



        public bool IsWhiteInCheck()
        {
            bool IsInCheck = false;


            if ((this.WhiteKing & this.BlackAttacks) > 0)
            {
                IsInCheck = true;
            }

            return IsInCheck;


        }
        


        public bool IsBlackInCheck()
        {
            bool IsInCheck = false;

            if ((this.BlackKing & this.WhiteAttacks) > 0)
            {
                IsInCheck = true;
            }

            return IsInCheck;

        }


        public bool IsWhiteInCheckMate()
        {
            bool InCheckMate = false;

            if (this.TurnIsWhite && this.WhiteInCheck && (this.Children.Length == 0))
            {
                InCheckMate = true;
            }

            return InCheckMate;
        }

        
        public bool IsBlackInCheckMate()
        {
            bool InCheckMate = false;

            if (!this.TurnIsWhite && this.BlackInCheck && (this.Children.Length == 0))
            {
                InCheckMate = true;
            }

            return InCheckMate;
        }


        public int CalcBitboardValue(ulong Piece, int PieceValue)
        {
            int Value = 0;
            ulong CurrentPiece = Piece;

            while (CurrentPiece > 0)
            {
                if (CurrentPiece % 2 == 1)
                {
                    Value += PieceValue;
                }
                CurrentPiece >>= 1;
            }

            return Value;
        }

        public int CalcValue()
        {
            int Value = 0;

            // If parent is white's turn, if parent is black, they will swap
            Value += CalcBitboardValue(this.WhiteQueen, 9);
            Value += CalcBitboardValue(this.WhiteRooks, 5);
            Value += CalcBitboardValue(this.WhiteBishops, 3);
            Value += CalcBitboardValue(this.WhiteKnights, 3);
            Value += CalcBitboardValue(this.WhitePawns, 1);

            Value -= CalcBitboardValue(this.BlackQueen, 9);
            Value -= CalcBitboardValue(this.BlackRooks, 5);
            Value -= CalcBitboardValue(this.BlackBishops, 3);
            Value -= CalcBitboardValue(this.BlackKnights, 3);
            Value -= CalcBitboardValue(this.BlackPawns, 1);

            // If parent's turn is black, the values swap
            if (!this.Parent.TurnIsWhite)
            {
                Value = -Value;
            }

            return Value;
        }

        // TODO
        public void UpdateUCTs()
        {

            if (this.WhiteWins > 0 || this.BlackWins > 0)
            {

            }
            double VisitedRate = Constants.C * Math.Sqrt(Math.Log(this.Parent.Number) / this.Number);

            double WhiteWinRate = (double)this.WhiteWins / (double)this.Number;
            double BlackWinRate = (double)this.BlackWins / (double)this.Number;

            this.WhiteUCT = VisitedRate + WhiteWinRate;
            this.BlackUCT = VisitedRate + BlackWinRate;

        }
    }
}
