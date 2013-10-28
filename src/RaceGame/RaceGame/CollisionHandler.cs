/*using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RaceGame.Gamelogic
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

    public static class CollisionTester
    {

        #region Constants
        private const int RECTANGLE_OFFSET = 100; //200;
        #endregion

        #region Variables

        /// <summary>
        /// The Rendertargets below, make sure we can handle pixeldata for collision detection of
        /// some kind.
        /// We render small amounts of a texture, but we do that in memory (thus not on screen) afterwards
        /// we can generate an entire new texture (from code) which can be displayed using a spritebatch.
        /// To be extra fast, we do this in GPU time instead of CPU.
        /// </summary>
        private static RenderTarget2D trackRender = new RenderTarget2D(RaceGame.Device,
            RaceCar.Width + RECTANGLE_OFFSET, car.Height + RECTANGLE_OFFSET, false, SurfaceFormat.Color, DepthFormat.Depth24);
        private static RenderTarget2D trackRenderRotated = new RenderTarget2D(RaceGame.Device,
            RaceCar.Width + RECTANGLE_OFFSET, RaceCar.Height + RECTANGLE_OFFSET, false, SurfaceFormat.Color, DepthFormat.Depth24);

        #endregion

        #region
        /// <summary>
        /// Property for retrieving the TrackRender RenderTarget2D
        /// </summary>
        private static RenderTarget2D TrackRender
        {
            get { return trackRender; }
        }

        /// <summary>
        /// Property for retrieving the TrackRenderRotated RenderTarget2D
        /// </summary>
        internal static RenderTarget2D TrackRenderRotated
        {
            get { return trackRenderRotated; }
        }
        #endregion


        /// <summary>
        /// Helper method which returns the information on what kind of collision occurred.
        /// See enum BackGround for more information.
        /// </summary>
        /// <param name="moveMent">The movement of our object (e.g. the car)</param>
        /// <param name="currentPosition">The current position of our object (e.g. the car)</param>
        /// <returns>A bitwise enumeration with the collision values.</returns>
        public static Background CollidesWith(int moveMent,
            Vector2 currentPosition)
        {
            if (RaceCar.CurrentTrack == null)
                throw new ArgumentNullException("RaceCar.CurrentTrack",
                    "Backgroundcollision, Track cannot be null");
            if (currentPosition == null)
                throw new ArgumentNullException("CurrentPosition: No argument specified",
                    "Backgroundcollision, currentPosition cannot be empty or null");

            //Calculate the position of the car and create the collision texture. This texture will contain 
            //all of the pixels that are directly underneath the sprite currently on the track image.
            float theXPosition = (float)(-RaceCar.Width / 2 + RaceCar.CarPosition.X + moveMent * Math.Cos(RaceCar.CarRotation));
            float theYPosition = (float)(-RaceCar.Height / 2 + RaceCar.CarPosition.Y + moveMent * Math.Sin(RaceCar.CarRotation));

            Texture2D collisionCheck = CreateCollisionTexture(theXPosition, theYPosition);

            //Now things get a little more complicated, we need to fill in an array with all of the colors of the pixels in the
            //area of the (required image from above) collision image.

            int nrOfPixels = RaceCar.Width * RaceCar.Height;
            Color[] foundColors = new Color[nrOfPixels];
            collisionCheck.GetData<Color>(0, new Rectangle((int)(collisionCheck.Width / 2 + RaceCar.Width / 2),
                (int)(collisionCheck.Height / 2 + RaceCar.Height / 2), RaceCar.Width, RaceCar.Height), foundColors, 0,
                nrOfPixels);


            //At least we found a few colors, iterate through them and see if we find a color which would
            //mean we 'hit' something. Gray is still the road however!
            Background collidedWith = Background.Road;
            foreach (Color foundColor in foundColors)
            {
                if (foundColor.Equals(Color.Gray))
                {
                    collidedWith = Background.Road;
                    break;
                }
                else if (foundColor.Equals(Color.Blue))
                {
                    collidedWith = Background.Wall;
                    break;
                }
                else if (foundColor.Equals(Color.White))
                {
                    collidedWith = Background.Grass;
                    break;
                }
                else if (foundColor.Equals(Color.Red))
                {
                    collidedWith = Background.FullLap;
                    break;
                }
                else if (foundColor.Equals(Color.Green))
                {
                    collidedWith = Background.CheckPoint;
                    break;
                }
                else if (foundColor.Equals(Color.Brown))
                {
                    collidedWith = Background.Dirt;
                    break;
                }
                //Can be extended with other colors.
            }
            return collidedWith;
        }

        /// <summary>
        /// Helper method which creates a texture to determine whether there has
        /// occurred a collision or not. The returned texture has color data for
        /// more accurate values.
        /// </summary>
        /// <param name="theXPosition">The x position on screen</param>
        /// <param name="theYPosition">The y position on screen</param>
        /// <returns></returns>
        private static Texture2D CreateCollisionTexture(float theXPosition, float theYPosition)
        {
            RaceGame.Device.SetRenderTarget(TrackRender);
            RaceGame.Device.Clear(ClearOptions.Target, Color.Red, 0, 0);

            RaceGame.spriteBatch.Begin();
            RaceGame.spriteBatch.Draw(Track.theTrack, new Rectangle(0, 0, RaceCar.Width + RECTANGLE_OFFSET, RaceCar.Height + RECTANGLE_OFFSET),
                new Rectangle((int)(theXPosition - 50),
                (int)(theYPosition - 50), RaceCar.Width + RECTANGLE_OFFSET, RaceCar.Height + RECTANGLE_OFFSET),
                Color.White);
            RaceGame.spriteBatch.End();

            RaceGame.Device.SetRenderTarget(null);

            Texture2D aPicture = TrackRender;

            RaceGame.Device.SetRenderTarget(TrackRenderRotated);
            RaceGame.Device.Clear(ClearOptions.Target, Color.Red, 0, 0);

            RaceGame.spriteBatch.Begin();
            RaceGame.spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),
                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),
                Color.White, -RaceCar.carRotation, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),
                SpriteEffects.None, 0);
            RaceGame.spriteBatch.End();

            RaceGame.Device.SetRenderTarget(null);
            return TrackRenderRotated;
        }


        public static void Render()
        {
            Texture2D collisionTexture = CreateCollisionTexture(RaceCar.CarPosition.X, RaceCar.CarPosition.Y);
            RaceGame.spriteBatch.Begin();
            RaceGame.spriteBatch.Draw(collisionTexture,
                new Rectangle((int)RaceCar.CarPosition.X - RaceCar.Width, (int)RaceCar.CarPosition.Y - RaceCar.Height,
                    collisionTexture.Width / 2, collisionTexture.Height / 2),
                    Color.White);
            RaceGame.spriteBatch.End();
        }
    }
}*/
