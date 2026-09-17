using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2 {
     class Game {
        public int size;
        public Player Cat { get; }
        public Player Mouse { get; }
        public bool gameStatus;


        String inputFile { get; }
        String outputFile { get; }

        public Game(Player First, Player Second, String inp, String outp) {
            Cat = First;
            Mouse = Second;
            gameStatus = true;
            inputFile = inp;
            outputFile = outp;
        }

        public void Run() {
            using (StreamReader sr = new StreamReader(inputFile)) {
                String? line;
                while (gameStatus && (line = sr.ReadLine()) != null) {
                    String[] arr = line.Split(' ');
                    char com = arr[0].Trim()[0];
                    int steps;
                    int.TryParse(arr[1], out steps);
                        ReadCommanD(com, steps);

                 
                }
            }
        }

        private void ReadCommanD(char command, int steps) {
            switch (command) {
                case 'C':
                    Cat.Move(steps, size);
                    break;
                case 'M':
                    Mouse.Move(steps, size);
                    break;
                case 'P':
                    PrintInf();
                    break;
                default:
                    break;
            }
        }
        private void PrintInf() {
            string catPos = Cat.state == State.NotInGame ? "??" : Cat.location.ToString();

            string mousePos = Mouse.state == State.NotInGame ? "??" : Mouse.location.ToString();

            int dist = GetDist();
            string distStr = dist == -1 ? "" : dist.ToString();

            PrintHeader();
            Console.WriteLine($"{catPos,5}{mousePos,7}{distStr,10}");
            
            

        }
        public void PrintSummary() {
            Console.WriteLine("-----------------------");
            Console.WriteLine();
            Console.WriteLine($"Distance traveled:   Mouse    Cat");
            Console.WriteLine($"{Mouse.DistanceTraveled,22}{Cat.DistanceTraveled,7}");
            Console.WriteLine();

        }
        public void PrintHeader() {
            Console.WriteLine("Cat and Mouse\n");
            Console.WriteLine("  Cat  Mouse   Distance");
            Console.WriteLine("-----------------------");
        }
        private int GetDist() {
            if (Mouse.state == State.NotInGame || Cat.state == State.NotInGame) {
                return -1;
            }
            return Math.Abs(Cat.location - Mouse.location);
            
        }
        
    }
}
