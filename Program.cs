using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game
{
    class Program
    {
        private static int[,] board;
        private static int p1Score = 0;
        private static int p2Score = 0;
        private static int chosenRow1 = -1;
        private static int chosenCol1 = -1;
        private static int chosenRow2 = -1;
        private static int chosenCol2 = -1;
        private static int boardHeight;
        private static int boardWidth;
        private static int pairs;
        private static bool turn = true; //true = player 1, false = player 2

        static void Main(string[] args)
        {
            BuildBoard();

            // ---------------------cheating-zone-------------------
            //Console.Clear();
            //Console.WriteLine("Cheat Sheet: ");
            //DrawVisibleBoard(board);
            //Console.ReadLine();
            // -----------------------------------------------------

            while (pairs > 0)
            {
                PickFirstCard();
                PickSecondCard();
                MatchResult();
            }
            GameOver();
        }

        private static void GameOver()
        {
            Console.Clear();
            DrawPoints(p1Score, p2Score);
            DrawBoard(board);
            Console.WriteLine();
            Console.WriteLine("Game Over!");
            if (p1Score == p2Score)
            {
                Console.WriteLine("It's a tie!");
            }
            else if (p1Score > p2Score)
            {
                Console.WriteLine("Player 1 wins!");
            }
            else
            {
                Console.WriteLine("Player 2 wins!");
            }
        }

        //END OF MAIN ----------------------------------------------------------------------------------------------------------------------------------------------------------------

        private static void BuildBoard()
        {
            Random rngsus = new Random();
            int rowrng = 0;
            int colrng = 0;

            do
            {
                Console.Clear();
                Console.Write("Enter the board's height (2, 4, 6, 8): ");
            }
            while (int.TryParse(Console.ReadLine(), out boardHeight) == false || boardHeight > 8 || boardHeight < 2 || boardHeight % 2 != 0);

            do
            {
                Console.Clear();
                Console.WriteLine("Enter the board's height (2, 4, 6, 8): " + boardHeight);
                Console.Write("Enter the board's width (2, 4, 6, 8): ");
            }
            while (int.TryParse(Console.ReadLine(), out boardWidth) == false || boardWidth > 8 || boardWidth < 2 || boardWidth % 2 != 0);

            board = new int[boardHeight, boardWidth];
            pairs = (boardHeight * boardWidth) / 2;
            for (int i = 1; i <= pairs; i++)
            {
                //first card
                do
                {
                    rowrng = rngsus.Next(0, boardHeight);
                    colrng = rngsus.Next(0, boardWidth);
                }
                while (board[rowrng, colrng] != 0);
                board[rowrng, colrng] = i;
                //second card
                do
                {
                    rowrng = rngsus.Next(0, boardHeight);
                    colrng = rngsus.Next(0, boardWidth);
                }
                while (board[rowrng, colrng] != 0);
                board[rowrng, colrng] = i;
            }
        }

        private static void PickFirstCard()
        {
            do //picking the first card
            {
                if (chosenRow1 - 1 >= 0 && chosenCol1 - 1 >= 0)
                {
                    if (board[chosenRow1 - 1, chosenCol1 - 1] == 0)
                    {
                        Console.Clear();
                        DrawPoints(p1Score, p2Score);
                        DrawBoard(board);
                        DrawTurn(turn);
                        Console.WriteLine("There is no card in this spot!");
                        Console.ReadLine();
                    }
                }

                do
                {
                    Console.Clear();
                    DrawPoints(p1Score, p2Score);
                    DrawBoard(board);
                    DrawTurn(turn);
                    Console.Write($"Enter row number of your first card (1 - {boardHeight}): ");
                }
                while (int.TryParse(Console.ReadLine(), out chosenRow1) == false || chosenRow1 > boardHeight || chosenRow1 <= 0);

                do
                {
                    Console.Clear();
                    DrawPoints(p1Score, p2Score);
                    DrawBoard(board);
                    DrawTurn(turn);
                    Console.WriteLine($"Enter row number of your first card (1 - {boardHeight}): {chosenRow1}");
                    Console.WriteLine($"Enter column number of your first card (1 - {boardWidth}): ");
                }
                while (int.TryParse(Console.ReadLine(), out chosenCol1) == false || chosenCol1 > boardWidth || chosenCol1 <= 0);
            }
            while (board[chosenRow1 - 1, chosenCol1 - 1] == 0);
        }

        private static void PickSecondCard()
        {
            do // picking the second card
            {
                if (chosenRow2 - 1 >= 0 && chosenCol2 - 1 >= 0)
                {

                    if (board[chosenRow2 - 1, chosenCol2 - 1] == 0)
                    {
                        Console.Clear();
                        DrawPoints(p1Score, p2Score);
                        Draw1CardBoard(board, chosenRow1 - 1, chosenCol1 - 1);
                        DrawTurn(turn);
                        Console.WriteLine("There is no card in this spot!");
                        Console.ReadLine();
                    }
                }

                if (chosenRow2 - 1 == chosenRow1 - 1 && chosenCol2 - 1 == chosenCol1 - 1)
                {
                    Console.Clear();
                    DrawPoints(p1Score, p2Score);
                    Draw1CardBoard(board, chosenRow1 - 1, chosenCol1 - 1);
                    DrawTurn(turn);
                    Console.WriteLine("This card's face is already up! Choose different card");
                    Console.ReadLine();
                }

                do
                {
                    Console.Clear();
                    DrawPoints(p1Score, p2Score);
                    Draw1CardBoard(board, chosenRow1 - 1, chosenCol1 - 1);
                    DrawTurn(turn);
                    Console.Write($"Enter row number of your second card (1 - {boardHeight}): ");
                }
                while (int.TryParse(Console.ReadLine(), out chosenRow2) == false || chosenRow2 > boardHeight || chosenRow2 <= 0);

                do
                {
                    Console.Clear();
                    DrawPoints(p1Score, p2Score);
                    Draw1CardBoard(board, chosenRow1 - 1, chosenCol1 - 1);
                    DrawTurn(turn);
                    Console.WriteLine($"Enter row number of your second card (1 - {boardHeight}): {chosenRow2}");
                    Console.WriteLine($"Enter column number of your second card (1 - {boardWidth})");
                }
                while (int.TryParse(Console.ReadLine(), out chosenCol2) == false || chosenCol2 > boardWidth || chosenCol2 <= 0);
            }
            while (board[chosenRow2 - 1, chosenCol2 - 1] == 0 || (chosenRow2 - 1 == chosenRow1 - 1 && chosenCol2 - 1 == chosenCol1 - 1));
        }

        private static void MatchResult()
        {
            Console.Clear();
            DrawPoints(p1Score, p2Score);
            Draw2CardsBoard(board, chosenRow1 - 1, chosenCol1 - 1, chosenRow2 - 1, chosenCol2 - 1);

            if (board[chosenRow1 - 1, chosenCol1 - 1] == board[chosenRow2 - 1, chosenCol2 - 1])
            {
                Console.WriteLine($"Player {(turn ? "1" : "2")} has found a match!");
                if (turn)
                {
                    p1Score++;
                }
                else
                {
                    p2Score++;
                }
                pairs--;
                board[chosenRow1 - 1, chosenCol1 - 1] = 0;
                board[chosenRow2 - 1, chosenCol2 - 1] = 0;
            }
            else
            {
                Console.WriteLine("Cards don't match!");
                turn = !turn;
            }
            chosenRow1 = -1;
            chosenCol1 = -1;
            chosenRow2 = -1;
            chosenCol2 = -1;
            Console.ReadLine();
            Console.Clear();
        }

        private static void DrawPoints(int p1Score, int p2Score)
        {
            Console.WriteLine($"Player 1: {p1Score} | Player 2: {p2Score}");
        }

        private static void DrawTurn(bool turn)
        {
            Console.WriteLine($"Player {(turn ? "1" : "2")}'s turn");
        }

        private static void DrawVisibleBoard(int[,] board)
        {
            Console.WriteLine("----------------------------");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] < 10)
                    {
                        Console.Write($" {board[i, j]}|");
                    }
                    else
                    {
                        Console.Write($"{board[i, j]}|");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------");
        }

        private static void DrawBoard(int[,] board)
        {
            Console.WriteLine("----------------------------");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] > 0)
                    {
                        Console.Write(" 0|");
                    }
                    else
                    {
                        Console.Write(" X|");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------");
        }

        private static void Draw1CardBoard(int[,] board, int realChosenRow1, int realChosenCol1)
        {
            Console.WriteLine("----------------------------");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] > 0)
                    {
                        if(i == realChosenRow1 && j == realChosenCol1)
                        {
                            if (board[i, j] < 10)
                            {
                                Console.Write($" {board[i, j]}|");
                            }
                            else
                            {
                                Console.Write($"{board[i, j]}|");
                            }
                        }
                        else
                        {
                            Console.Write(" 0|");
                        }
                    }
                    else
                    {
                        Console.Write(" X|");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------");
        }

        private static void Draw2CardsBoard(int[,] board, int realChosenRow1, int realChosenCol1, int realChosenRow2, int realChosenCol2)
        {
            Console.WriteLine("----------------------------");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] > 0)
                    {
                        if ((i == realChosenRow1 && j == realChosenCol1) || (i == realChosenRow2 && j == realChosenCol2))
                        {
                            if (board[i, j] < 10)
                            {
                                Console.Write($" {board[i, j]}|");
                            }
                            else
                            {
                                Console.Write($"{board[i, j]}|");
                            }
                        }
                        else
                        {
                            Console.Write(" 0|");
                        }
                    }
                    else
                    {
                        Console.Write(" X|");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------");
        }
    }
}
