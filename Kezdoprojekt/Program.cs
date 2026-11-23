// Konzolos rajzoló – a lehető legegyszerűbb változat, egyetlen fájlban.

Console.OutputEncoding = System.Text.Encoding.UTF8;

if (Console.IsInputRedirected || Console.IsOutputRedirected)
{
    Console.WriteLine("A programhoz nyiss interaktív konzolablakot.");
    return;
}
if (Console.WindowWidth < 76 || Console.WindowHeight < 23)
{
    Console.WriteLine("Nagyítsd a konzolablakot legalább 76 oszloposra és 23 sorosra.");
    return;
}

// ----- A rajzlap: 52 oszlop (a 76 kétharmadánál több) és 20 sor. Konzolpozíció: 24 + x, 1 + y -----
const int SZEL = 52;
const int MAG = 20;
char[,] rajz = new char[MAG, SZEL];
ConsoleColor[,] rajzSzin = new ConsoleColor[MAG, SZEL];
for (int i = 0; i < MAG; i++)
    for (int j = 0; j < SZEL; j++)
        rajz[i, j] = ' ';

ConsoleColor[] szinek = { ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow,
                          ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.Gray };
string[] szinNevek = { "cián", "piros", "zöld", "sárga", "kék", "magenta", "fehér", "szürke" };

int x = SZEL / 2;          // kurzor helye
int y = MAG / 2;
char karakter = '#';       // aktuális rajzkarakter
int szinSzam = 0;          // az aktuális szín sorszáma a szinek tömbben
bool lent = false;         // false = felemelve, true = letéve
int mod = 0;               // 0 = rajzolás, 1 = menü, 2 = színválasztás, 3 = karakterválasztás
string uzenet = "";
bool hibas = false;
bool fut = true;

Console.CursorVisible = false;
Console.Clear();
uzenet = "Üdv! Nyilak: mozgás, Szóköz: eszköz, M: menü.";

while (fut)
{
    Rajzol();
    ConsoleKeyInfo k = Console.ReadKey(true);
    uzenet = "";
    hibas = false;

    if (mod == 0)                                  // ---------- rajzolás ----------
    {
        if (k.Key == ConsoleKey.UpArrow) Lep(0, -1);
        else if (k.Key == ConsoleKey.DownArrow) Lep(0, 1);
        else if (k.Key == ConsoleKey.LeftArrow) Lep(-1, 0);
        else if (k.Key == ConsoleKey.RightArrow) Lep(1, 0);
        else if (k.Key == ConsoleKey.Spacebar)
        {
            lent = !lent;
            if (lent) Beir(x, y);
        }
        else if (k.Key >= ConsoleKey.F1 && k.Key <= ConsoleKey.F12)
        {
            int sorszam = k.Key - ConsoleKey.F1;
            Nyomda(sorszam / 3, sorszam % 3 + 1);
        }
        else if (k.Key == ConsoleKey.M || k.Key == ConsoleKey.Escape) mod = 1;
        else if (k.Key == ConsoleKey.X) fut = false;
    }
    else if (mod == 1)                             // ---------- menü ----------
    {
        if (k.KeyChar == '1') mod = 3;
        else if (k.KeyChar == '2') mod = 2;
        else if (k.KeyChar == '6') fut = false;
        else if (k.Key == ConsoleKey.Escape || k.Key == ConsoleKey.M) mod = 0;
        else Hiba("Hibás választás! Nyomj érvényes számot, vagy Esc-et.");
    }
    else if (mod == 2)                             // ---------- színválasztás ----------
    {
        if (k.KeyChar >= '1' && k.KeyChar <= '8')
        {
            szinSzam = k.KeyChar - '1';
            mod = 0;
        }
        else if (k.Key == ConsoleKey.Escape) mod = 1;
        else Hiba("Hibás választás! Nyomj 1-8 közötti számot, vagy Esc-et.");
    }
    else                                           // ---------- karakterválasztás ----------
    {
        if (k.Key == ConsoleKey.Escape) mod = 1;
        else if (!char.IsControl(k.KeyChar))
        {
            karakter = k.KeyChar;
            mod = 0;
        }
        else Hiba("Ezt a billentyűt nem lehet karakternek használni.");
    }
}

Console.ResetColor();
Console.Clear();
Console.CursorVisible = true;
Console.WriteLine("A program véget ért.");

// ======================= függvények =======================

void Hang()
{
    if (OperatingSystem.IsWindows()) Console.Beep(800, 120);
    else Console.Write("\a");
}

void Hiba(string szoveg)
{
    Hang();
    uzenet = szoveg;
    hibas = true;
}

void Beir(int bx, int by)
{
    rajz[by, bx] = karakter;
    rajzSzin[by, bx] = szinek[szinSzam];
}

void Lep(int dx, int dy)
{
    int ujX = x + dx;
    int ujY = y + dy;
    if (ujX < 0 || ujX >= SZEL || ujY < 0 || ujY >= MAG)
    {
        Hiba("A rajzlap szélén vagy, tovább nem léphetsz.");
        return;
    }
    x = ujX;
    y = ujY;
    if (lent) Beir(x, y);
}

void Nyomda(int forma, int sugar)
{
    if (x - sugar < 0 || x + sugar >= SZEL || y - sugar < 0 || y + sugar >= MAG)
    {
        Hiba("A nyomda kilógna a rajzlapról, nem rajzoltam semmit.");
        return;
    }
    for (int dy = -sugar; dy <= sugar; dy++)
    {
        for (int dx = -sugar; dx <= sugar; dx++)
        {
            bool jelolt;
            if (forma == 0) jelolt = Math.Abs(dx) == Math.Abs(dy);
            else if (forma == 1) jelolt = dx == 0 || dy == 0;
            else if (forma == 2) jelolt = Math.Abs(dx) == sugar || Math.Abs(dy) == sugar;
            else jelolt = Math.Abs(dx) + Math.Abs(dy) == sugar;

            if (jelolt) Beir(x + dx, y + dy);
        }
    }
}

void Rajzol()
{
    string[] panel = new string[MAG];
    for (int i = 0; i < MAG; i++) panel[i] = "";

    if (mod == 0)
    {
        panel[0] = " M: menü";
        panel[1] = " X: kilépés";
        panel[3] = " Nyilak: mozgás";
        panel[4] = " Szóköz: toll fel/le";
        panel[5] = " F1-F12: nyomdák";
    }
    else if (mod == 1)
    {
        panel[0] = " MENÜ (nyomj számot)";
        panel[1] = " 1 Karakter választása";
        panel[2] = " 2 Szín választása";
        panel[6] = " 6 Kilépés";
        panel[8] = " Esc: vissza a rajzhoz";
    }
    else if (mod == 2)
    {
        panel[0] = " Szín (nyomj számot)";
        for (int i = 0; i < szinNevek.Length; i++)
            panel[1 + i] = " " + (i + 1) + " " + szinNevek[i];
    }
    else
    {
        panel[0] = " Üss le egy billentyűt,";
        panel[1] = " ez lesz a karakter.";
        panel[2] = " (Esc: mégse)";
    }
    panel[12] = " Pozíció   (" + x + ", " + y + ")";
    panel[13] = " Karakter  " + karakter;
    panel[14] = " Szín      " + szinNevek[szinSzam];
    panel[15] = " Eszköz    " + (lent ? "letéve" : "felemelve");

    Console.SetCursorPosition(0, 0);
    Console.BackgroundColor = ConsoleColor.Cyan;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Write((" KONZOLOS RAJZOLÓ").PadRight(23) + "│" + " RAJZLAP".PadRight(52));
    Console.ResetColor();

    for (int i = 0; i < MAG; i++)
    {
        Console.SetCursorPosition(0, 1 + i);
        Console.ForegroundColor = (i == 14) ? szinek[szinSzam] : ConsoleColor.Gray;
        Console.Write(panel[i].PadRight(23) + "│");
        Console.ResetColor();
    }

    // rajzlap
    for (int i = 0; i < MAG; i++)
    {
        Console.SetCursorPosition(24, 1 + i);
        for (int j = 0; j < SZEL; j++)
        {
            if (i == y && j == x)
            {
                Console.BackgroundColor = lent ? ConsoleColor.Green : ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(rajz[i, j]);
            }
            else if (rajz[i, j] == ' ')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write('·');
            }
            else
            {
                Console.ForegroundColor = rajzSzin[i, j];
                Console.Write(rajz[i, j]);
            }
            Console.ResetColor();
        }
    }

    // alsó két sor
    Console.SetCursorPosition(0, 21);
    Console.ForegroundColor = ConsoleColor.DarkGray;
    if (mod == 0) Console.Write("Nyilak: mozgás  Szóköz: eszköz  F1-F12: nyomda  M: menü  X: kilépés".PadRight(75));
    else Console.Write("Nyomj számot a választáshoz   Esc: vissza".PadRight(75));
    Console.SetCursorPosition(0, 22);
    Console.ForegroundColor = hibas ? ConsoleColor.Red : ConsoleColor.Green;
    Console.Write(uzenet.PadRight(75));
    Console.ResetColor();
}