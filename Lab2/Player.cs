using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace Lab2 {
    class Player {
        public String Name { get; private set; }
        //поприкалываться
        public int mmr {  get; private set; }
        public int location { get; private set; }
        public State state { get; private set; }
        public int DistanceTraveled = 0;

        public Player(String name) {
            Name = name;
            state = State.NotInGame;
            location = -1;
        }

        // логика внутри или вне?? location = newPos 
        // наверное внутри изза инкапсуляции
        public void Move(int steps, int sizeGame) {
            location = Math.Abs((steps + location) % sizeGame);
        }
    }

    enum State {
        Winner,
        Looser,
        Playing,
        NotInGame
    }
}
