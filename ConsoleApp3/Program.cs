using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int width = 17;  // Šířka plinko desky
        int height = 15; // Výška plinko desky
        int totalScore = 0;

        // Inicializace plinko desky
        char[,] board = InitializeBoard(width, height);

        Console.WriteLine("Vítejte ve hře Plinko!");
        Console.WriteLine("Zadejte pozici, kde chcete pustit disk (0-16) nebo stiskněte mezerník pro náhodnou pozici:");

        while (true)
        {
            int startPosition = GetStartPosition(width);
            int currentCol = startPosition;
            Random random = new Random();

            Console.Clear();
            DrawBoard(board);

            // Simulace pádu disku
            for (int row = 0; row < height; row++)
            {
                // Předchozí pozici disku vymažeme
                if (row > 0) DrawChar(row - 1, currentCol, board[row - 1, currentCol]);

                // Posun disku doleva nebo doprava při narazení na kolík
                if (currentCol > 0 && currentCol < width - 1)
                {
                    if (board[row, currentCol - 1] == 'o' && board[row, currentCol + 1] == 'o')
                    {
                        currentCol += random.Next(0, 2) * 2 - 1;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Yellow; // Nastavení barvy textu na žlutou
                DrawChar(row, currentCol, '#');  // Zaznamenání pozice disku
                Console.ResetColor(); // Obnovení výchozí barvy textu
                Thread.Sleep(50); // Pauza pro plynulý pád
            }

            // Bodovací systém
            int[] scores = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 800, 700, 600, 500, 400, 300, 200, 100 };
            int finalScore = scores[currentCol];
            totalScore += finalScore;

            Console.SetCursorPosition(0, height + 2);
            Console.WriteLine($"Disk dopadl na pozici: {currentCol}");
            Console.WriteLine($"Vaše skóre je: {finalScore}");
            Console.WriteLine($"Celkové skóre: {totalScore}");
            Console.WriteLine("Stiskněte mezerník pro další hru nebo 'q' pro ukončení.");

            // Čekání na mezerník pro další hru nebo 'q' pro ukončení
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    return;
                }
            }
        }
    }

    static char[,] InitializeBoard(int width, int height)
    {
        char[,] board = new char[height, width];

        // Umístění kolíků do desky
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (row % 2 == 0)
                {
                    board[row, col] = col % 2 == 0 ? ' ' : 'o';
                }
                else
                {
                    board[row, col] = col % 2 == 0 ? 'o' : ' ';
                }
            }
        }

        return board;
    }

    static int GetStartPosition(int width)
    {
        int startPosition = -1;
        Random random = new Random();
        while (startPosition < 0 || startPosition >= width)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                startPosition = random.Next(0, width);
            }
            else if (char.IsDigit(keyInfo.KeyChar))
            {
                startPosition = int.Parse(keyInfo.KeyChar.ToString());
                if (startPosition < 0 || startPosition >= width)
                {
                    Console.WriteLine($"Zadejte platnou pozici (0-{width - 1}) nebo stiskněte mezerník pro náhodnou pozici:");
                    startPosition = -1;
                }
            }
            else
            {
                Console.WriteLine($"Neplatný vstup. Zadejte číslo (0-{width - 1}) nebo stiskněte mezerník pro náhodnou pozici:");
            }
        }
        return startPosition;
    }

    static void DrawBoard(char[,] board)
    {
        int height = board.GetLength(0);
        int width = board.GetLength(1);
        int startX = (Console.WindowWidth - width) / 2;

        for (int r = 0; r < height; r++)
        {
            Console.SetCursorPosition(startX, r);
            for (int c = 0; c < width; c++)
            {
                Console.Write(board[r, c]);
            }
            Console.WriteLine();
        }
    }

    static void DrawChar(int row, int col, char ch)
    {
        int startX = (Console.WindowWidth - 17) / 2; // Adjust based on the board width
        Console.SetCursorPosition(startX + col, row);
        Console.Write(ch);
        Console.SetCursorPosition(0, 15); // Reset cursor position to prevent overwriting the last line
    }
}