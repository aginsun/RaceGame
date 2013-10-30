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
        //instance van trackhandler, ook private!
        static TrackHandler instance = new TrackHandler();

        //textures
        public Texture2D texture1;
        public Texture2D car1Texture;
        public Texture2D car2Texture;
        public Texture2D powerupTexture;

        //de 2 autos
        public Car car1;
        public Car car2;

        //List met powerups
        public List<Powerup> ListPowerups = new List<Powerup>();

        //Vierkanten van de pitstop, checkpoint en finish!
        Rectangle Pitstop = new Rectangle(467, 489, (467 - 176), (557 - 489));
        Rectangle Checkpoint = new Rectangle(278, 20, (285 - 278), (115 - 20));
        Rectangle Finish = new Rectangle(928, 583, 5, (676 - 583));

        //Private constructor zodat niemand per ongeluk een 2e instantie van trackhandler kan maken
        private TrackHandler() { }

        //een instance van trackhandler zodat er niet opeens 2 objecten kunnen zijn van TrackHandler
        public static TrackHandler getInstance()
        {
            return instance;
        }


        public void initGame()
        {
            initializeCars();
            initializePowerups();
        }

        //initializeert de 2 autos voor het begin van de game!
        public void initializeCars()
        {
            car1 = new Car();
            car2 = new Car();
        }

        //initializeert alle powerups voor de baan en hun positie
        public void initializePowerups()
        {
            addPowerup(new Powerup(0), 5, 10);
            addPowerup(new Powerup(1), 20, 50);
        }


        //een methode die alle verschillende dingen die geupdate moeten worden bij elkaar brengt zodat het in een keer kan aangeroepen worden in de RaceGame class
        public void update(GameTime gameTime)
        {  
            updateCarPosition(gameTime);
            checkCollisions(gameTime);
        }

        //Zorgt ervoor dat de auto's verplaatsen elke update cycle, doormiddel van de update functie in car!
        public void updateCarPosition(GameTime gameTime)
        {
            car1.Update(gameTime);
            car2.UpdateCar2(gameTime);
        }


        //Kijkt of een van de cars overlapt met checkpoints, pitstop of de finish!!!
        public void checkCollisions(GameTime gameTime)
        {
            //vierkantjes van de car!
            Rectangle car1Rec = new Rectangle((int)car1.CarPosition.X, (int)car1.CarPosition.Y, car1.Width, car1.Height);
            Rectangle car2Rec = new Rectangle((int)car2.CarPosition.X, (int)car2.CarPosition.Y, car2.Width, car2.Height);

            //doormiddel van te kijken of 2 rectangles met elkaar overlappen kijken we of ie inderdaad overlapt met een van de dingen
            if (car1Rec.Intersects(Checkpoint))
                car1.hasCheckPoint = true;
            if (car2Rec.Intersects(Checkpoint))
                car2.hasCheckPoint = true;
            if (car1Rec.Intersects(Pitstop))
                car1.Fuel += 0.015;
            if (car2Rec.Intersects(Pitstop))
                car2.Fuel += 0.015;
            if (car1Rec.Intersects(Finish) && car1.hasCheckPoint)
            {
                car1.hasCheckPoint = false;
                car1.amountLaps++;
            }
            if (car2Rec.Intersects(Finish) && car2.hasCheckPoint)
            {
                car2.hasCheckPoint = false;
                car2.amountLaps++;
            }
        }

        //een methode om powerups toe te voegen op een bepaalde locatie op de baan!
        private void addPowerup(Powerup power, int x, int y)
        {
            power.setPosition(x, y);
            //Voegt het aan een lijst toe om het later weer van de baan te kunnen verwijderen
            ListPowerups.Insert(power.number, power);
        }

        //inits alle textures die op de baan moeten komen te liggen
        public void InitializeTextures(ContentManager Content)
        {
            this.texture1 = Content.Load<Texture2D>("baanv6");
            this.car1Texture = Content.Load<Texture2D>("bumper");
            this.car2Texture = Content.Load<Texture2D>("bumper");
            this.powerupTexture = Content.Load<Texture2D>("mushroom");
        }

        //Elke update worden alle textures geupdate met hun positie en of ze er moeten liggen
        public void DrawTextures(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture1, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(this.car1Texture, car1.CarPosition, null, Color.White, car1.Direction, new Vector2(car1Texture.Bounds.Center.X, car1Texture.Bounds.Center.Y), 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(this.car2Texture, car2.CarPosition, null, Color.White, car2.Direction, new Vector2(car2Texture.Bounds.Center.X, car2Texture.Bounds.Center.Y), 1.0f, SpriteEffects.None, 0f);
            foreach (Powerup p in ListPowerups)
            {
                spriteBatch.Draw(this.powerupTexture, new Vector2(p.posx, p.posy), Color.White);
            }
        }
    }
}
