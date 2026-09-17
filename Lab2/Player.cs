using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace Lab2 {
    class Player {
        public String Name { get; private set; }
        public int location { get; private set; }
        public State state { get; set; }
        public int DistanceTraveled = 0;

        public Player(String name) {
            Name = name;
            state = State.NotInGame;
            location = 0;
        }

        // логика внутри изза инкапсуляции
        public void Move(int steps, int sizeGame) {
            state = State.Playing;
            location = Math.Abs((steps + location) % sizeGame);
            DistanceTraveled += Math.Abs(steps);
        }
    }

    enum State {
        Winner,
        Looser,
        Playing,
        NotInGame
    }
}
