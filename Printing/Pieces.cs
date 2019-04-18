using System;
using System.Collections.Generic;



namespace AlphaChess.Printing
{


    public partial class Printer
    {

        // Instance Methods


        // Returns a tuple representing the location and string to print for the White King
        public Tuple<Tuple<int, int>, string> GetWhiteKing()
        {
            Tuple<int, int> Square = GetLocation(CurrentBoard.WhiteKing);
            string _String = "WK ";

            Tuple<Tuple<int, int>, string> Result = Tuple.Create(Square, _String);

            return Result;
        }


        // Returns a tuple representing the location and string to print for the White Queen
        public Tuple<Tuple<int, int>, string> GetWhiteQueen()
        {
            Tuple<int, int> Square = GetLocation(CurrentBoard.WhiteQueen);
            string _String = "WQ ";

            Tuple<Tuple<int, int>, string> Result = Tuple.Create(Square, _String);

            return Result;
        }


        // Returns a tuple representing the location and string to print for the White
        public List<Tuple<Tuple<int, int>, string>> GetWhiteRooks()
        {
            List<Tuple<Tuple<int, int>, string>> Results = new List<Tuple<Tuple<int, int>, string>>();

            ulong Rooks = CurrentBoard.WhiteRooks;

            int Count = 1;

            ulong Position = 1;
            while (Rooks > 0)
            {
                if (Rooks % 2 == 1)
                {
                    Tuple<int, int> Square = GetLocation(Position);
                    string _String = $"WR{Count++}";


                    Results.Add(Tuple.Create(Square, _String));

                    Rooks -= 1;

                }
                else
                {
                    Position *= 2;
                    Rooks >>= 1;
                }

            }


            return Results;
        }


        // Returns a tuple representing the location and string to print for the White
        public List<Tuple<Tuple<int, int>, string>> GetWhiteBishops()
        {
            List<Tuple<Tuple<int, int>, string>> Results = new List<Tuple<Tuple<int, int>, string>>();

            ulong Bishops = CurrentBoard.WhiteBishops;

            int Count = 1;

            ulong Position = 1;
            while (Bishops > 0)
            {
                if (Bishops % 2 == 1)
                {
                    Tuple<int, int> Square = GetLocation(Position);
                    string _String = $"WB{Count++}";


                    Results.Add(Tuple.Create(Square, _String));

                    Bishops -= 1;

                }
                else
                {
                    Position *= 2;
                    Bishops >>= 1;
                }

            }


            return Results;
        }


        // Returns a tuple representing the location and string to print for the White
        public List<Tuple<Tuple<int, int>, string>> GetWhiteKnights()
        {
            List<Tuple<Tuple<int, int>, string>> Results = new List<Tuple<Tuple<int, int>, string>>();

            ulong Knights = CurrentBoard.WhiteKnights;

            int Count = 1;

            ulong Position = 1;
            while (Knights > 0)
            {
                if (Knights % 2 == 1)
                {
                    Tuple<int, int> Square = GetLocation(Position);
                    string _String = $"WK{Count++}";


                    Results.Add(Tuple.Create(Square, _String));

                    Knights -= 1;

                }
                else
                {
                    Position *= 2;
                    Knights >>= 1;
                }

            }


            return Results;
        }


        // Returns a tuple representing the location and string to print for the White
        public List<Tuple<Tuple<int, int>, string>> GetWhitePawns()
        {
            List<Tuple<Tuple<int, int>, string>> Results = new List<Tuple<Tuple<int, int>, string>>();

            ulong Pawns = CurrentBoard.WhitePawns;

            int Count = 1;

            ulong Position = 1;
            while (Pawns > 0)
            {
                if (Pawns % 2 == 1)
                {
                    Tuple<int, int> Square = GetLocation(Position);
                    string _String = $"WP{Count++}";


                    Results.Add(Tuple.Create(Square, _String));

                    Pawns -= 1;

                }
                else
                {
                    Position *= 2;
                    Pawns >>= 1;
                }

            }


            return Results;
        }


        // Returns a tuple representing the location and string to print for the Black
        public Tuple<Tuple<int, int>, string> GetBlackKing()
        {
            Tuple<int, int> Square = GetLocation(CurrentBoard.BlackKing);
            string _String = "BK ";

            Tuple<Tuple<int, int>, string> Result = Tuple.Create(Square, _String);

            return Result;
        }


        // Returns a tuple representing the location and string to print for the Black
        public Tuple<Tuple<int, int>, string> GetBlackQueen()
        {
            Tuple<int, int> Square = GetLocation(CurrentBoard.BlackQueen);
            string _String = "BQ ";

            Tuple<Tuple<int, int>, string> Result = Tuple.Create(Square, _String);

            return Result;
        }


        // Returns a tuple representing the location and string to print for the Black
        public List<Tuple<Tuple<int, int>, string>> GetBlackRooks()
        {
            List<Tuple<Tuple<int, int>, string>> Results = new List<Tuple<Tuple<int, int>, string>>();

            ulong Rooks = CurrentBoard.BlackRooks;

            int Count = 1;

            ulong Position = 1;
            while (Rooks > 0)
            {
                if (Rooks % 2 == 1)
                {
                    Tuple<int, int> Square = GetLocation(Position);
                    string _String = $"BR{Count++}";


                    Results.Add(Tuple.Create(Square, _String));

                    Rooks -= 1;

                }
                else
                {
                    Position *= 2;
                    Rooks >>= 1;
                }

            }


            return Results;
        }


        // Returns a tuple representing the location and string to print for the Black
        public List<Tuple<Tuple<int, int>, string>> GetBlackBishops()
        {
            List<Tuple<Tuple<int, int>, string>> Results = new List<Tuple<Tuple<int, int>, string>>();

            ulong Bishops = CurrentBoard.BlackBishops;

            int Count = 1;

            ulong Position = 1;
            while (Bishops > 0)
            {
                if (Bishops % 2 == 1)
                {
                    Tuple<int, int> Square = GetLocation(Position);
                    string _String = $"BB{Count++}";


                    Results.Add(Tuple.Create(Square, _String));

                    Bishops -= 1;

                }
                else
                {
                    Position *= 2;
                    Bishops >>= 1;
                }

            }


            return Results;
        }


        // Returns a tuple representing the location and string to print for the Black
        public List<Tuple<Tuple<int, int>, string>> GetBlackKnights()
        {
            List<Tuple<Tuple<int, int>, string>> Results = new List<Tuple<Tuple<int, int>, string>>();

            ulong Knights = CurrentBoard.BlackKnights;

            int Count = 1;

            ulong Position = 1;
            while (Knights > 0)
            {
                if (Knights % 2 == 1)
                {
                    Tuple<int, int> Square = GetLocation(Position);
                    string _String = $"BK{Count++}";


                    Results.Add(Tuple.Create(Square, _String));

                    Knights -= 1;

                }
                else
                {
                    Position *= 2;
                    Knights >>= 1;
                }

            }


            return Results;
        }


        // Returns a tuple representing the location and string to print for the Black
        public List<Tuple<Tuple<int, int>, string>> GetBlackPawns()
        {
            List<Tuple<Tuple<int, int>, string>> Results = new List<Tuple<Tuple<int, int>, string>>();

            ulong Pawns = CurrentBoard.BlackPawns;

            int Count = 1;

            ulong Position = 1;
            while (Pawns > 0)
            {
                if (Pawns % 2 == 1)
                {
                    Tuple<int, int> Square = GetLocation(Position);
                    string _String = $"BP{Count++}";


                    Results.Add(Tuple.Create(Square, _String));

                    Pawns -= 1;

                }
                else
                {
                    Position *= 2;
                    Pawns >>= 1;
                }

            }


            return Results;
        }


    }
}
