Tento kód představuje konzolovou hru Plinko, kde disk padá přes desku s kolíky a nakonec dopadne do jedné z pozic, čímž hráč získá určité skóre. Níže je popis jednotlivých částí kódu:

### 1. Deklarace a inicializace proměnných

```csharp
int width = 17;  // Šířka plinko desky
int height = 15; // Výška plinko desky
int totalScore = 0;
```
- `width` a `height` určují rozměry plinko desky.
- `totalScore` ukládá celkové skóre hráče.

### 2. Inicializace plinko desky

```csharp
char[,] board = InitializeBoard(width, height);
```
- `board` je dvourozměrné pole znaků, které představuje plinko desku.
- `InitializeBoard` je metoda, která naplní desku kolíky ('o') a prázdnými místy (' ').

### 3. Uvítání hráče a získání počáteční pozice

```csharp
Console.WriteLine("Vítejte ve hře Plinko!");
Console.WriteLine("Zadejte pozici, kde chcete pustit disk (0-16) nebo stiskněte mezerník pro náhodnou pozici:");
```
- Vypíše uvítací zprávu a instrukce pro hráče.

### 4. Hlavní herní smyčka

```csharp
while (true)
{
    int startPosition = GetStartPosition(width);
    int currentCol = startPosition;
    Random random = new Random();

    Console.Clear();
    DrawBoard(board);
```
- Hlavní smyčka hry, která se opakuje, dokud hráč nezvolí ukončení.
- `GetStartPosition` získá počáteční pozici disku od hráče nebo náhodně.

### 5. Simulace pádu disku

```csharp
for (int row = 0; row < height; row++)
{
    if (row > 0) DrawChar(row - 1, currentCol, board[row - 1, currentCol]);

    if (currentCol > 0 && currentCol < width - 1)
    {
        if (board[row, currentCol - 1] == 'o' && board[row, currentCol + 1] == 'o')
        {
            currentCol += random.Next(0, 2) * 2 - 1;
        }
    }

    Console.ForegroundColor = ConsoleColor.Yellow;
    DrawChar(row, currentCol, '#');
    Console.ResetColor();
    Thread.Sleep(50);
}
```
- Smyčka, která simuluje pád disku přes desku.
- Disk může změnit směr při narazení na kolík.
- `DrawChar` vykresluje disk na aktuální pozici.

### 6. Výpočet skóre

```csharp
int[] scores = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 800, 700, 600, 500, 400, 300, 200, 100 };
int finalScore = scores[currentCol];
totalScore += finalScore;
```
- Skóre závisí na konečné pozici disku (`currentCol`).
- `scores` je pole s hodnotami skóre pro každou pozici.

### 7. Zobrazení výsledků a volba další hry nebo ukončení

```csharp
Console.SetCursorPosition(0, height + 2);
Console.WriteLine($"Disk dopadl na pozici: {currentCol}");
Console.WriteLine($"Vaše skóre je: {finalScore}");
Console.WriteLine($"Celkové skóre: {totalScore}");
Console.WriteLine("Stiskněte mezerník pro další hru nebo 'q' pro ukončení.");

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
```
- Výpis výsledků a celkového skóre.
- Hráč může zvolit pokračování (mezerník) nebo ukončení ('q').

### 8. Metoda pro inicializaci desky

```csharp
static char[,] InitializeBoard(int width, int height)
{
    char[,] board = new char[height, width];
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
```
- Inicializuje desku s kolíky ('o') a prázdnými místy (' ').

### 9. Metoda pro získání počáteční pozice

```csharp
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
```
- Získává počáteční pozici od hráče nebo náhodně.

### 10. Metoda pro vykreslení desky

```csharp
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
```
- Vykresluje celou desku na konzoli.

### 11. Metoda pro vykreslení znaku

```csharp
static void DrawChar(int row, int col, char ch)
{
    int startX = (Console.WindowWidth - 17) / 2;
    Console.SetCursorPosition(startX + col, row);
    Console.Write(ch);
    Console.SetCursorPosition(0, 15);
}
```
- Vykresluje jednotlivý znak na určenou pozici na desce.

Tento kód tvoří kompletní hru Plinko v konzolové aplikaci, kde hráč může spouštět disky z různých pozic a sledovat, jak padají přes desku s kolíky, aby dosáhli různých skóre.
