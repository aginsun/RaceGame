using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RaceGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RaceGame : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        SpriteFont font;
        static RaceGame game = new RaceGame();

        public static RaceGame getInstance()
        {
            return game;
        }

        public RaceGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 700;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
           TrackHandler.getInstance().initializeCars();
           base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("myFont");

            TrackHandler.getInstance().initGame();
            TrackHandler.getInstance().InitializeTextures(this.Content);
        }

        private void DrawText()
        {
            spriteBatch.DrawString(font, "Player 1", new Vector2(400, 250), Color.Blue);
            spriteBatch.DrawString(font, "Health: " + TrackHandler.getInstance().car1.Health, new Vector2(400, 270), Color.Blue);
            spriteBatch.DrawString(font, "Remaining energy: " + (int)TrackHandler.getInstance().car1.Fuel, new Vector2(400, 290), Color.Blue);
            spriteBatch.DrawString(font, "Completed laps: " + TrackHandler.getInstance().car1.amountLaps, new Vector2(400, 310), Color.Blue);
            spriteBatch.DrawString(font, "Current speed: " + (int)TrackHandler.getInstance().car1.Speed, new Vector2(400, 330), Color.Blue);
            //spriteBatch.DrawString(font, "Projection: ", new Vector2(400, 350), Color.Blue);
            //spriteBatch.DrawString(font, "Pitstops made: ", new Vector2(400, 370), Color.Blue);

            spriteBatch.DrawString(font, "Player 2", new Vector2(650, 250), Color.Blue);
            spriteBatch.DrawString(font, "Health: " + TrackHandler.getInstance().car2.Health, new Vector2(650, 270), Color.Blue);
            spriteBatch.DrawString(font, "Remaining energy: " + (int)TrackHandler.getInstance().car2.Fuel, new Vector2(650, 290), Color.Blue);
            spriteBatch.DrawString(font, "Completed laps: " + TrackHandler.getInstance().car2.amountLaps, new Vector2(650, 310), Color.Blue);
            spriteBatch.DrawString(font, "Current speed: " + (int)TrackHandler.getInstance().car2.Speed, new Vector2(650, 330), Color.Blue);
            //spriteBatch.DrawString(font, "Projection: ", new Vector2(650, 350), Color.Blue);
            //spriteBatch.DrawString(font, "Pitstops made: ", new Vector2(650, 370), Color.Blue);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            TrackHandler.getInstance().update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            TrackHandler.getInstance().DrawTextures(spriteBatch);

            DrawText();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
