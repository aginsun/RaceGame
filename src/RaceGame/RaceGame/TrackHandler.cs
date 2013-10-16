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
        Texture2D texture;

        private static TrackHandler getInstance()
        {
            return new TrackHandler();
        }

        public TrackHandler(){}

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
