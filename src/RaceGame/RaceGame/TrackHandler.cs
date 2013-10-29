using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace RaceGame
{
    class TrackHandler
    {
        static TrackHandler instance = new TrackHandler();
        Texture2D texture;
        public Texture2D texture1;
        public Texture2D car1Texture;
        public Texture2D car2Texture;
        public Car car1;
        public Car car2;

        private TrackHandler() { }

        public static TrackHandler getInstance()
        {
            return instance;
        }

        public void initializeCars()
        {
            car1 = new Car();
            car2 = new Car();
        }

        public void initializePowerups()
        {
            Random rand = new Random();
            if (rand.Next(40) == 15)
            {
                //addPowerup(new Powerup(), 5, 10);
                //addPowerup(new Powerup(), 20, 50);
            }
        }

        public void update(GameTime gameTime)
        {  
            updateCarPosition(gameTime);
            checkCollisions(gameTime);
            Console.WriteLine(gameTime.TotalGameTime.Seconds);
        }

        public void updateCarPosition(GameTime gameTime/*, Car car //(Maybe?)*/)
        {
             car1.Update(gameTime);
        }

        public void checkCollisions(GameTime gameTime)
        {
            //TODO: Collision update logic
        }

        public void addPowerup(/*Powerup power,*/ int x, int y)
        {
            //power.setPosition(x, y);
        }

        public void addPitstopLayer()
        {
        }

        public void InitializeTextures(ContentManager Content)
        {
            this.texture = Content.Load<Texture2D>("baan");
            this.texture1 = Content.Load<Texture2D>("baan1");
            this.car1Texture = Content.Load<Texture2D>("bumper");
            this.car2Texture = Content.Load<Texture2D>("bumper");
        }

        public void DrawTextures(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture1, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(this.texture, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(this.car1Texture, new Vector2(car1.PosX, car1.PosY), Color.White);
            spriteBatch.Draw(this.car2Texture, new Vector2(car2.PosX, car2.PosY), Color.White);
        }
    }
}
