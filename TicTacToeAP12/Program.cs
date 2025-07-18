
class Program
{
    const int feldAnzahl = 3;
    static readonly char[,] spielfeld = new char[feldAnzahl, feldAnzahl];
    static int spielZug = 1;
    static int status;

    static void Main()
    {

        do
        {
            Console.Clear();
            Console.WriteLine("TicTacToe");
            SpielfeldZeichnen();

            char spielerZeichen = GetSpielZug();
            Console.Write($"Spieler {spielerZeichen}, geben Sie die Kooordinaten für Zeile und Spalte im Format ZeileSpalte, Z.B. 00: ");

            string? eingabe = Console.ReadLine()?.Trim();
            // Extrachieren von Werten (aus Format nn)
            var strZeile = eingabe?.Length > 0 ? eingabe.Substring(0, 1) : string.Empty;
            var strSpalte = eingabe?.Length > 1 ? eingabe.Substring(1, 1) : string.Empty;

            if (int.TryParse(strZeile, out var intZeile) &&
                int.TryParse(strSpalte, out var intSpalte) &&
                intZeile >= 0 && intZeile < feldAnzahl &&
                intSpalte >= 0 && intSpalte < feldAnzahl)
            {
                if (spielfeld[intZeile, intSpalte] == default(char))
                {
                    spielfeld[intZeile, intSpalte] = spielerZeichen;
                    spielZug++;
                }
                else
                {
                    Console.WriteLine("Feld ist bereits belegt. Taste drücken...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe. Taste drücken...");
                Console.ReadKey();
            }

            status = SpielStatus();

        } while (status == 0);

        Console.Clear();
        SpielfeldZeichnen();


        if (status == 1)
            Console.WriteLine($"Spieler {GetSpielZug()} hat gewonnen!");
        else
            Console.WriteLine("Unentschieden!");

        Console.WriteLine("Drücken Sie eine Taste zum Beenden...");
        Console.ReadKey();
    }

    private static char GetSpielZug()
    {
        return (spielZug % 2 == 0 ? 'X' : 'O');
    }

    static void SpielfeldZeichnen()
    {
        Console.WriteLine("   0   1   2");
        for (int zeile = 0; zeile < feldAnzahl; zeile++)
        {
            Console.Write($"{zeile}  ");
            for (int spalte = 0; spalte < feldAnzahl; spalte++)
            {
                char feld = spielfeld[zeile, spalte];
                Console.Write(feld == default(char) ? ' ' : feld);
                if (spalte < 2) Console.Write(" | ");
            }
            if (zeile < 2) Console.WriteLine("\n  ---+---+---");
            else Console.WriteLine();
        }
    }

   

    static int SpielStatus()
    {
        for (int i = 0; i < feldAnzahl; i++)
        {
            // Reihen
            if (spielfeld[i, 0] != default(char) && spielfeld[i, 0] == spielfeld[i, 1] && spielfeld[i, 1] == spielfeld[i, 2])
                return 1;
            // Spalten
            if (spielfeld[0, i] != default(char) && spielfeld[0, i] == spielfeld[1, i] && spielfeld[1, i] == spielfeld[2, i])
                return 1;
        }

        // Diagonalen
        if (spielfeld[0, 0] != default(char) && spielfeld[0, 0] == spielfeld[1, 1] && spielfeld[1, 1] == spielfeld[2, 2])
            return 1;
        if (spielfeld[0, 2] != default(char) && spielfeld[0, 2] == spielfeld[1, 1] && spielfeld[1, 1] == spielfeld[2, 0])
            return 1;

        // Noch freie Felder?
        foreach (char feld in spielfeld)
        {
            if (feld == default(char)) return 0;
        }

        return -1; // Unentschieden
    }
}
