using System;

namespace Lab2 {
    class Program {
        static void Main(string[] args) {
            Player pl1 = new Player("Cat");
            Player pl2 = new Player("Mouse");

            String inpF = "1.ChaseData.txt";
            String outF = "1.PureSuitLog.txt";



            Game game = new Game(pl1, pl2, inpF, outF);
            game.Run();
        }
    }
}