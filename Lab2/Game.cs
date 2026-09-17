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
            using (StreamWriter writer = new StreamWriter("output.txt", append: false)) {
                PrintHeader(writer);

                using (StreamReader sr = new StreamReader(inputFile)) {
                    String? line;
                    int.TryParse(sr.ReadLine(), out size);
                    while (gameStatus && (line = sr.ReadLine()) != null) {
                        String[] arr = line.Split(' ');
                        char com = arr[0].Trim()[0];
                        int steps;
                        int.TryParse(arr[1], out steps);
                        ReadCommanD(com, steps);
                    }

                }
                PrintSummary(writer);
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
                    PrintInf(GetDist());
                    break;
                default:
                    break;
            }
        }
        private void PrintInf(int dist) {
            string catPos = Cat.state == State.NotInGame ? "??" : Cat.location.ToString();

            string mousePos = Mouse.state == State.NotInGame ? "??" : Mouse.location.ToString();

            string distStr = dist == -1 ? "" : dist.ToString();

            using (StreamWriter writer = new StreamWriter(outputFile, append: true)) {
                writer.WriteLine($"{catPos,5}{mousePos,7}{distStr,10}");
            }
        }


        public void PrintHeader(TextWriter writer) {
            writer.WriteLine("Cat and Mouse\n");
            writer.WriteLine("  Cat  Mouse   Distance");
            writer.WriteLine("-----------------------");
        }

        public void PrintSummary(TextWriter writer) {
            writer.WriteLine("-----------------------");
            writer.WriteLine();
            writer.WriteLine("Distance traveled:   Mouse    Cat");
            writer.WriteLine($"{Mouse.DistanceTraveled,22}{Cat.DistanceTraveled,7}");
            writer.WriteLine();
            if(GetDist() == 0) {
                writer.WriteLine($"Cat catch Mouse at: {Cat.location}");
            }
            else
                writer.WriteLine($"Mouse evaded Cat");


        }
        private int GetDist() {
            if (Mouse.state == State.NotInGame || Cat.state == State.NotInGame) {
                return -1;
            }
            return Math.Abs(Cat.location - Mouse.location);

        }

    }
}
