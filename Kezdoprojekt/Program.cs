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
string uzenet = "";
bool hibas = false;
bool fut = true;

Console.CursorVisible = false;
Console.Clear();
uzenet = "Üdv! Nyilak: mozgás, Szóköz: eszköz, X: kilépés.";

while (fut)
{
    Rajzol();
    ConsoleKeyInfo k = Console.ReadKey(true);
    uzenet = "";
    hibas = false;

    if (k.Key == ConsoleKey.UpArrow) Lep(0, -1);
    else if (k.Key == ConsoleKey.DownArrow) Lep(0, 1);
    else if (k.Key == ConsoleKey.LeftArrow) Lep(-1, 0);
    else if (k.Key == ConsoleKey.RightArrow) Lep(1, 0);
    else if (k.Key == ConsoleKey.Spacebar)
    {
        lent = !lent;
        if (lent) Beir(x, y);      // letételkor az aktuális cellába is kerül jel
    }
    else if (k.Key == ConsoleKey.X) fut = false;
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
        return;                                  // a kurzor és a rajz marad
    }
    x = ujX;
    y = ujY;
    if (lent) Beir(x, y);
}

void Rajzol()
{
    string[] panel = new string[MAG];
    for (int i = 0; i < MAG; i++) panel[i] = "";

    panel[0] = " X: kilépés";
    panel[2] = " Nyilak: mozgás";
    panel[3] = " Szóköz: toll fel/le";

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
    Console.Write("Nyilak: mozgás  Szóköz: eszköz  X: kilépés".PadRight(75));
    Console.SetCursorPosition(0, 22);
    Console.ForegroundColor = hibas ? ConsoleColor.Red : ConsoleColor.Green;
    Console.Write(uzenet.PadRight(75));
    Console.ResetColor();
}