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

        public Game(int size, Player First, Player Second, String inp, String outp) {
            Cat = First;
            Mouse = Second;
            gameStatus = true;
            this.size = size;
            inputFile = inp;
            outputFile = outp;
        }

        public void Run() {
            while (gameStatus) {
                //парс из файла 
                char com = 'O';
                int step = 0;
                MoveCommanD(com, step);
                int dist = GetDist();
                PrintInf();
                if(dist == 0) gameStatus = false;
            }
        }

        private void MoveCommanD(char command, int steps) {
            switch (command) {
                case 'C':
                    Cat.Move(steps, size);
                    break;
                case 'M':
                    Mouse.Move(steps, size);
                    break;
                default:
                    break;
            }
        }
        private void PrintInf() {
              
        }
        private int GetDist() {
            if (Mouse.state == State.NotInGame || Cat.state == State.NotInGame) {
                return -1;
            }
            return Math.Abs(Cat.location - Mouse.location);
            
        }
        
    }
}
