using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGame
{
    class Powerup
    {
        public int posx;
        public int posy;
        public int number;

        public Powerup(int number)
        {
            posx = 0;
            posy = 0;
            this.number = number;
        }

        public void onCollision(Car car)
        {
            car.Fuel += 200;
            TrackHandler.getInstance().ListPowerups.RemoveAt(number);
        }

        public void setPosition(int x, int y)
        {
            posx = x;
            posy = y;
        }
    }
}
