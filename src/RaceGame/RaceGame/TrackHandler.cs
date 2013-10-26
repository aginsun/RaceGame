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

        private TrackHandler() { }

        public static TrackHandler getInstance()
        {
            return instance;
        }

        public void initializeCars()
        {
            //TODO: car 1 init
            //TODO: car 2 init
        }

        public void update(GameTime gameTime)
        {  
            updateCarPosition(gameTime);
            checkCollisions(gameTime);
        }

        public void updateCarPosition(GameTime gameTime/*, Car car //(Maybe?)*/)
        {
            //TODO: update logic (example: car.update(Gametime?); )
        }

        public void checkCollisions(GameTime gameTime)
        {
            //TODO: Collision update logic
        }

        public void addPowerup(/*Powerup power*/)
        {
            //power.setPosition(int x, int z); TODO
        }

        public void addPitstopLayer()
        {
        }

        public void InitializeTextures(ContentManager Content)
        {
            this.texture = Content.Load<Texture2D>("baan");   
        }

        public void DrawTextures(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, new Vector2(0, 0), Color.White);
        }
    }
}
