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
            using (StreamWriter writer = new StreamWriter(outputFile, append: false)) {
                PrintHeader(writer);
                int steps = 0;
                using (StreamReader sr = new StreamReader(inputFile)) {
                    String? line;
                    int.TryParse(sr.ReadLine(), out size);
                    while (gameStatus && (line = sr.ReadLine()) != null) {
                        string[] arr = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        char com = arr[0].Trim()[0];
                      
                        switch (com) {
                            case 'C':
                                int.TryParse(arr[1], out steps);
                                Cat.Move(steps, size);
                                break;
                            case 'M':
                                int.TryParse(arr[1], out steps);
                                Mouse.Move(steps, size);
                                break;
                            case 'P':
                                PrintInf(GetDist(), writer);
                                break;
                            default:
                                break;
                        }
                        if (Cat.state != State.NotInGame && Mouse.state != State.NotInGame && GetDist() == 0) {
                            gameStatus = false;
                        }

                    }
                }
                PrintSummary(writer);
            }
        }


        private void PrintInf(int dist, StreamWriter writer) {
            string catPos = Cat.state == State.NotInGame ? "??" : Cat.location.ToString();

            string mousePos = Mouse.state == State.NotInGame ? "??" : Mouse.location.ToString();

            string distStr = dist == -1 ? "" : dist.ToString();

            writer.WriteLine($"{catPos,5}{mousePos,7}{distStr,10}");
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
            if (GetDist() == 0) {
                writer.WriteLine($"Cat catch Mouse at: {Cat.location}");
                Cat.state = State.Winner;
                Mouse.state = State.Looser;
            }
            else {
                writer.WriteLine($"Mouse evaded Cat");
                Cat.state = State.Looser;
                Mouse.state = State.Winner;
            }


        }
        private int GetDist() {
            if (Mouse.state == State.NotInGame || Cat.state == State.NotInGame) {
                return -1;
            }
            return Math.Abs(Cat.location - Mouse.location);

        }

    }
}
