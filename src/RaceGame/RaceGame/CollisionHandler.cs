using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RaceGame
{
    /// <summary>
    /// A bitwise (power of two)  enumeration which holds all the values for
    /// possible collisions. 1 means there is a collision with the road, 2 for grass,
    /// 4 for dirt, 8 for ice, 16 for walls, 32 for checkpoints, 64 for the finishline, 128
    /// for a full lap, and 239 for everything except the wall...
    /// </summary>
    public enum Background
    {
        Road = 1,
        Grass = 2,
        Dirt = 4,
        Ice = 8,
        Wall = 16,
        CheckPoint = 32,
        FinishLine = 64,
        FullLap = 128,
        AllButWall = (Road | Grass | Dirt | Ice |
                      CheckPoint | FinishLine | FullLap),
    }

    public static class CollisionHandler
    {
        private const int RECTANGLE_OFFSET = 80; //200;

        private static RenderTarget2D trackRender = new RenderTarget2D(RaceGame.graphics.GraphicsDevice, TrackHandler.getInstance().car1Texture.Width + RECTANGLE_OFFSET, TrackHandler.getInstance().car1Texture.Height + RECTANGLE_OFFSET, false, SurfaceFormat.Color, DepthFormat.Depth24);
        private static RenderTarget2D trackRenderRotated = new RenderTarget2D(RaceGame.graphics.GraphicsDevice, TrackHandler.getInstance().car1Texture.Width + RECTANGLE_OFFSET, TrackHandler.getInstance().car1Texture.Height + RECTANGLE_OFFSET, false, SurfaceFormat.Color, DepthFormat.Depth24);

        private static RenderTarget2D TrackRender
        {
            get { return trackRender; }
        }

        internal static RenderTarget2D TrackRenderRotated
        {
            get { return trackRenderRotated; }
        }

        public static Background CollidesWith(int moveMent, Vector2 currentPosition, Car car)
        {
            float theXPosition = (float)(-car.Width / 2 + car.CarPosition.X + moveMent * Math.Cos(car.Direction));
            float theYPosition = (float)(-car.Height / 2 + car.CarPosition.Y + moveMent * Math.Sin(car.Direction));

            Texture2D collisionCheck = CreateCollisionTexture(theXPosition, theYPosition, car);

            int nrOfPixels = car.Width * car.Height;
            Color[] foundColors = new Color[nrOfPixels];

            int x = (int)(collisionCheck.Width / 2 + car.Width / 2);
            int y = (int)(collisionCheck.Height / 2 + car.Height / 2);
            int width = car.Width;
            int height = car.Height;
            Rectangle rec = new Rectangle(x, y, width, height);
            collisionCheck.GetData<Color>(0, rec, foundColors, 0, nrOfPixels);

            Background collidedWith = Background.Road;
            foreach (Color foundColor in foundColors)
            {
                if (foundColor.Equals(Color.Black))
                {
                    collidedWith = Background.Road;
                    break;
                }
                else if (foundColor.Equals(Color.Red))
                {
                    collidedWith = Background.Wall;
                    break;
                }
                else if (foundColor.Equals(Color.White))
                {
                    collidedWith = Background.Grass;
                    break;
                }
                else if (foundColor.Equals(Color.Blue))
                {
                    collidedWith = Background.FullLap;
                    break;
                }
                else if (foundColor.Equals(Color.Green))
                {
                    collidedWith = Background.CheckPoint;
                    break;
                }
                else if (foundColor.Equals(Color.Orange))
                {
                    collidedWith = Background.Dirt;
                    break;
                }
            }
            return collidedWith;
        }

        private static Texture2D CreateCollisionTexture(float theXPosition, float theYPosition, Car car)
        {
            RaceGame.graphics.GraphicsDevice.SetRenderTarget(TrackRender);
            RaceGame.graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);
                       
            RaceGame.spriteBatch.Begin();
            RaceGame.spriteBatch.Draw(TrackHandler.getInstance().texture1, new Rectangle(0, 0, car.Width + RECTANGLE_OFFSET, car.Height + RECTANGLE_OFFSET),
                new Rectangle((int)(theXPosition - 50),
                (int)(theYPosition - 50), car.Width + RECTANGLE_OFFSET, car.Height + RECTANGLE_OFFSET), 
                Color.White);
            RaceGame.spriteBatch.End();

            RaceGame.graphics.GraphicsDevice.SetRenderTarget(null);

            Texture2D aPicture = TrackRender;

            RaceGame.graphics.GraphicsDevice.SetRenderTarget(TrackRenderRotated);
            RaceGame.graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            RaceGame.spriteBatch.Begin();
            RaceGame.spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),
                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),
                Color.White, -car.Direction, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),
                SpriteEffects.None, 0);
            RaceGame.spriteBatch.End();

            RaceGame.graphics.GraphicsDevice.SetRenderTarget(null);
            return TrackRenderRotated;
        }


        public static void Render(Car car)
        {
            Texture2D collisionTexture = CreateCollisionTexture(car.CarPosition.X, car.CarPosition.Y, car);
            RaceGame.spriteBatch.Begin();
            RaceGame.spriteBatch.Draw(collisionTexture, new Rectangle((int)car.CarPosition.X - car.Width, (int)car.CarPosition.Y - car.Height, collisionTexture.Width / 2, collisionTexture.Height / 2), Color.White);
            RaceGame.spriteBatch.End();
        }
    }
}
