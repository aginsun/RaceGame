using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RaceGame
{
    public class Car
    {
        public int PosX;
        public int PosY;
        public double Fuel;
        public float Acceleration;
        public double Speed;
        public float Health;
        public float Direction;
        public int Width;
        public int Height;

        public Car()
        {
            PosX = 25;
            PosY = 52;
            Fuel = 30;
            Acceleration = 0;
            Speed = 0;
            Health = 100;
            Direction = 90;
            Width = 47;
            Height = 65;
        }

        public void Update(GameTime gameTime)
        {
            Fuel = Fuel - (0.05 * Speed);
            KeyboardState kbstate = Keyboard.GetState();
            if (kbstate.IsKeyDown(Keys.W))
            {
                if (CollisionHandler.CollidesWith((int)Speed, new Vector2(PosX, PosY), this) == Background.Road)
                {
                    Speed += 0.05;
                    if (Speed > 300)
                    {
                        Speed = 300;
                    }
                }
                else if (CollisionHandler.CollidesWith((int)Speed, new Vector2(PosX, PosY), this) == Background.Grass)
                {
                    if (Speed > 200)
                    {
                        Speed += -0.025;
                    }
                }
                else if (CollisionHandler.CollidesWith((int)Speed, new Vector2(PosX, PosY), this) == Background.Wall)
                {
                    Speed = -1 * (Speed / 4);
                    Health += -20;
                }
                else
                {
                    Speed += 0.025;
                }
            }

            else if (kbstate.IsKeyDown(Keys.S))
            {
                if (CollisionHandler.CollidesWith((int)Speed, new Vector2(PosX, PosY), this) == Background.Road)
                {
                    if (Speed > 0)
                    {
                        Speed += -35;
                    }
                    else if (Speed < -200)
                    {
                        Speed = -200;
                    }
                    else if (CollisionHandler.CollidesWith((int)Speed, new Vector2(PosX, PosY), this) == Background.Grass)
                    {
                        if (Speed > -100)
                        {
                            Speed += +0.025;
                        }
                        else
                        {
                            Speed += 0.025;
                        }
                    }
                    else if (CollisionHandler.CollidesWith((int)Speed, new Vector2(PosX, PosY), this) == Background.Wall)
                    {
                        Speed = 1 * (Speed / 4);
                        Health += -20;
                    }
                    else
                    {
                        Speed += -0.25;
                    }
                }
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A)))
            {

                Direction -= (float)(1 / Speed);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A))
            {

                Direction += (float)(1 / Speed);

            }

            if ((Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D)))
            {

                Direction += (float)(1 / Speed);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D))
            {

                Direction -= (float)(1 / Speed);

            }



            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {

                if (Direction >= 1 && Direction <= 44)
                {
                    this.PosX -= (int)(Speed * 0.7);
                    this.PosY += (int)(Speed * 0.3);
                }
                else if (Direction >= 45 && Direction <= 89)
                {
                    this.PosX -= (int)(Speed * 0.3);
                    this.PosY += (int)(Speed * 0.7);
                }
                else if (Direction == 0)
                {
                    this.PosX -= (int)(Speed);

                }
                else if (Direction == 90)
                {

                    this.PosY += (int)(Speed);
                }
                else if (Direction == 180)
                {
                    this.PosX += (int)(Speed);

                }
                else if (Direction == 270)
                {

                    this.PosY -= (int)(Speed);
                }
                else if (Direction >= 91 && Direction <= 134)
                {
                    this.PosX += (int)(Speed * 0.3);
                    this.PosY += (int)(Speed * 0.7);

                }
                else if (Direction >= 135 && Direction <= 179)
                {
                    this.PosX += (int)(Speed * 0.7);
                    this.PosY += (int)(Speed * 0.3);
                }
                else if (Direction >= 181 && Direction <= 224)
                {
                    this.PosX += (int)(Speed * 0.7);
                    this.PosY -= (int)(Speed * 0.3);
                }
                else if (Direction >= 225 && Direction <= 269)
                {
                    this.PosX += (int)(Speed * 0.3);
                    this.PosY -= (int)(Speed * 0.7);
                }
                else if (Direction >= 271 && Direction <= 314)
                {
                    this.PosX -= (int)(Speed * 0.3);
                    this.PosY += (int)(Speed * 0.7);
                }
                else if (Direction >= 315 && Direction <= 359)
                {
                    this.PosX -= (int)(Speed * 0.7);
                    this.PosY += (int)(Speed * 0.3);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (Direction >= 1 && Direction <= 44)
                {
                    this.PosX += (int)(Speed * 0.7);
                    this.PosY -= (int)(Speed * 0.3);
                }
                else if (Direction >= 45 && Direction <= 89)
                {
                    this.PosX += (int)(Speed * 0.3);
                    this.PosY -= (int)(Speed * 0.7);
                }
                else if (Direction == 0)
                {
                    this.PosX += (int)(Speed);

                }
                else if (Direction == 90)
                {

                    this.PosY -= (int)(Speed);
                }
                else if (Direction == 180)
                {
                    this.PosX -= (int)(Speed);

                }
                else if (Direction == 270)
                {

                    this.PosY += (int)(Speed);
                }
                else if (Direction >= 91 && Direction <= 134)
                {
                    this.PosX -= (int)(Speed * 0.3);
                    this.PosY -= (int)(Speed * 0.7);

                }
                else if (Direction >= 135 && Direction <= 179)
                {
                    this.PosX -= (int)(Speed * 0.7);
                    this.PosY -= (int)(Speed * 0.3);
                }
                else if (Direction >= 181 && Direction <= 224)
                {
                    this.PosX -= (int)(Speed * 0.7);
                    this.PosY += (int)(Speed * 0.3);
                }
                else if (Direction >= 225 && Direction <= 269)
                {
                    this.PosX -= (int)(Speed * 0.3);
                    this.PosY += (int)(Speed * 0.7);
                }
                else if (Direction >= 271 && Direction <= 314)
                {
                    this.PosX += (int)(Speed * 0.3);
                    this.PosY -= (int)(Speed * 0.7);
                }
                else if (Direction >= 315 && Direction <= 359)
                {
                    this.PosX += (int)(Speed * 0.7);
                    this.PosY -= (int)(Speed * 0.3);
                }
            }

        }
    }
}
