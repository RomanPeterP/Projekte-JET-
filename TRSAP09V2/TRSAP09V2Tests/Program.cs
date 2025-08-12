using TRSAP09V2.Data;

namespace TRSAP09V2Tests
{
    public class Program
	{
		static void Main(string[] args)
		{
            BasicHandling.Clean();
            BasicHandling.SeedData();

            var testVorbereitung = new Modul4TestVorbereitung();
            testVorbereitung.Aufgabe01();
            testVorbereitung.Aufgabe02();
            testVorbereitung.Aufgabe03();
            testVorbereitung.Aufgabe04();
            testVorbereitung.Aufgabe05();
            testVorbereitung.Aufgabe06();
            testVorbereitung.Aufgabe07();
            testVorbereitung.Aufgabe08();
            testVorbereitung.Aufgabe09();
            testVorbereitung.Aufgabe10();
            testVorbereitung.Aufgabe11();
            testVorbereitung.Aufgabe12();
            testVorbereitung.Aufgabe13();
            testVorbereitung.Aufgabe14();
            testVorbereitung.Aufgabe15();
            testVorbereitung.Aufgabe16();
            testVorbereitung.Aufgabe17();
            testVorbereitung.Aufgabe18();
            testVorbereitung.Aufgabe19();
            testVorbereitung.Aufgabe20();
        }
    }

}
