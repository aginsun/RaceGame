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
        public Vector2 CarPosition;
        public Vector2 CarPosition2;
        public double Fuel;
        public float Acceleration;
        public double Speed;
        public double Health;
        public float Direction;
        public int Width;
        public int Height;
        public bool hasCheckPoint;
        public int amountLaps;
        public int lapsleft;
        public int Pitstopcount;

        public Car()
        {
            Fuel = 30;
            Acceleration = 0;
            Speed = 0;
            Health = 100;
            Direction = 0;
            Width = 33;
            Height = 24;
            CarPosition = new Vector2(910, 610);
            CarPosition2 = new Vector2(910, 650);
            hasCheckPoint = false;
            amountLaps = 0;
            lapsleft = 5;
            Pitstopcount = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (Fuel > 0 && Health > 0)
            {
                if (Speed > 0)
                    Fuel -= 0.005;
                KeyboardState state = Keyboard.GetState();
                float carMove = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Speed += Acceleration * carMove;
                Vector2 currentDirection = new Vector2((float)(Speed * carMove * Math.Cos(Direction)), (float)(Speed * carMove * Math.Sin(Direction)));

                Background collidesWith = CollisionHandler.CollidesWith((int)carMove * 200, CarPosition, this);
                
                if ((state.IsKeyDown(Keys.A) && Speed!=0))
                    Direction -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                else if ((state.IsKeyDown(Keys.D) && Speed !=0))
                    Direction += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);

                if ((state.IsKeyDown(Keys.W) && (collidesWith != Background.Wall)))
                {
                    Acceleration = 200.0f;
                    CarPosition = Vector2.Add(CarPosition, currentDirection);

                    if (collidesWith == Background.Grass)
                    {
                        if (Speed >= 200.0f * 0.3f)
                        {
                            Acceleration = -200.0f * 0.3f;
                            Health -= 0.2;
                        }
                    }
                    else
                    {
                        if (Speed >= 200.0f)
                        {
                            Speed = 200.0f;
                            Acceleration = 0.0f;
                        }
                    }
                }

                else if ((state.IsKeyUp(Keys.W)) && (collidesWith != Background.Wall))
                {
                    CarPosition = Vector2.Add(CarPosition, currentDirection);
                    if (Speed <= 0.0f)
                        Acceleration = 0.0f;
                    else Speed -= 5.0f;
                }

                if (state.IsKeyDown(Keys.S) && (collidesWith != Background.Wall))
                    CarPosition = Vector2.Subtract(CarPosition, new Vector2((float)(carMove * 100.0f * Math.Cos(Direction)), (float)(carMove * 100.0f * Math.Sin(Direction))));

                if (Speed < 0)
                    Speed = 0;

                if (Health < 0)
                Health = 0;
            }
            else
                Speed = 0;
        }
    
        public void UpdateCar2(GameTime gameTime) //Sneaky code
        {
            if (Fuel > 0 && Health > 0)
            {
                Fuel -= 0.0005;
                KeyboardState state = Keyboard.GetState();
                float carMove = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Speed += Acceleration * carMove;
                Vector2 currentDirection = new Vector2((float)(Speed * carMove * Math.Cos(Direction)), (float)(Speed * carMove * Math.Sin(Direction)));

                Background collidesWith = CollisionHandler.CollidesWith((int)carMove * 200, CarPosition, this);
                if ((state.IsKeyDown(Keys.Left)))
                    Direction -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                else if ((state.IsKeyDown(Keys.Right)))
                    Direction += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);

                if ((state.IsKeyDown(Keys.Up) && (collidesWith != Background.Wall)))
                {
                    Acceleration = 200.0f;
                    CarPosition2 = Vector2.Add(CarPosition2, currentDirection);

                    if (collidesWith == Background.Wall)
                    {
                        Speed -= -1 * (Speed / 4);
                    }
                    if (collidesWith == Background.Grass)
                    {
                        if (Speed >= 200.0f * 0.3f)
                        {
                            Health -= 0.2;
                            Acceleration = -200.0f * 0.3f;
                        }
                    }
                    else
                    {
                        if (Speed >= 200.0f)
                        {
                            Speed = 200.0f;
                            Acceleration = 0.0f;
                        }
                    }
                }

                else if ((state.IsKeyUp(Keys.Up)) && (collidesWith != Background.Wall))
                {
                    CarPosition2 = Vector2.Add(CarPosition2, currentDirection);
                    if (Speed <= 0.0f)
                        Acceleration = 0.0f;
                    else Speed -= 5.0f;
                }

                else if (state.IsKeyDown(Keys.Down) && (collidesWith != Background.Wall))
                     {
                         CarPosition2 = Vector2.Subtract(CarPosition2, new Vector2((float)(carMove * 100.0f * Math.Cos(Direction)), (float)(carMove * 100.0f * Math.Sin(Direction))));
                     }
                if (Speed < 0)
                    Speed = 0;

                if (Health < 0)
                    Health = 0;
            }
        }
        /*public void Update(GameTime gameTime)
        {
            Fuel = Fuel - (0.05 * Speed) * (0.05 * Speed);
            if(Fuel != 0)
            {
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
                        Speed += 0.02;
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
            }

            if ( Speed!=0 && Keyboard.GetState().IsKeyDown(Keys.A))
            {

                Direction -= (float)(50 / Speed);

            }
           
            if (Speed != 0 && Keyboard.GetState().IsKeyDown(Keys.D))
            {

                Direction += (float)(50 / Speed);

            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Direction >= 271 && Direction <= 314)
                {
                    this.PosX -= (int)(Speed * 0.7);
                    this.PosY += (int)(Speed * 0.3);
                }
                else if (Direction >= 315 && Direction <= 359)
                {
                    this.PosX -= (int)(Speed * 0.3);
                    this.PosY += (int)(Speed * 0.7);
                }
                else if (Direction == 270)
                {
                    this.PosX -= (int)(Speed);
                }
                else if (Direction == 0)
                {
                    this.PosY -= (int)(Speed);
                }
                else if (Direction == 90)
                {
                    this.PosX += (int)(Speed);
                }
                else if (Direction == 180)
                {
                    this.PosY += (int)(Speed);
                }
                else if (Direction >= 1 && Direction <= 44)
                {
                    this.PosX += (int)(Speed * 0.3);
                    this.PosY += (int)(Speed * 0.7);

                }
                else if (Direction >= 45 && Direction <= 89)
                {
                    this.PosX += (int)(Speed * 0.7);
                    this.PosY += (int)(Speed * 0.3);
                }
                else if (Direction >= 91 && Direction <= 134)
                {
                    this.PosX += (int)(Speed * 0.7);
                    this.PosY -= (int)(Speed * 0.3);
                }
                else if (Direction >= 135 && Direction <= 179)
                {
                    this.PosX += (int)(Speed * 0.3);
                    this.PosY -= (int)(Speed * 0.7);
                }
                else if (Direction >= 181 && Direction <= 224)
                {
                    this.PosX -= (int)(Speed * 0.3);
                    this.PosY += (int)(Speed * 0.7);
                }
                else if (Direction >= 225 && Direction <= 269)
                {
                    this.PosX -= (int)(Speed * 0.7);
                    this.PosY += (int)(Speed * 0.3);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (Direction >= 271 && Direction <= 314)
                {
                    this.PosX += (int)(Speed * 0.7);
                    this.PosY -= (int)(Speed * 0.3);
                }
                else if (Direction >= 315 && Direction <= 359)
                {
                    this.PosX += (int)(Speed * 0.3);
                    this.PosY -= (int)(Speed * 0.7);
                }
                else if (Direction == 270)
                {
                    this.PosX += (int)(Speed);

                }
                else if (Direction == 0)
                {

                    this.PosY -= (int)(Speed);
                }
                else if (Direction == 90)
                {
                    this.PosX -= (int)(Speed);

                }
                else if (Direction == 180)
                {

                    this.PosY += (int)(Speed);
                }
                else if (Direction >= 1 && Direction <= 44)
                {
                    this.PosX -= (int)(Speed * 0.3);
                    this.PosY -= (int)(Speed * 0.7);

                }
                else if (Direction >= 45 && Direction <= 89)
                {
                    this.PosX -= (int)(Speed * 0.7);
                    this.PosY -= (int)(Speed * 0.3);
                }
                else if (Direction >= 91 && Direction <= 134)
                {
                    this.PosX -= (int)(Speed * 0.7);
                    this.PosY += (int)(Speed * 0.3);
                }
                else if (Direction >= 135 && Direction <= 179)
                {
                    this.PosX -= (int)(Speed * 0.3);
                    this.PosY += (int)(Speed * 0.7);
                }
                else if (Direction >= 181 && Direction <= 224)
                {
                    this.PosX += (int)(Speed * 0.3);
                    this.PosY -= (int)(Speed * 0.7);
                }
                else if (Direction >= 225 && Direction <= 269)
                {
                    this.PosX += (int)(Speed * 0.7);
                    this.PosY -= (int)(Speed * 0.3);
                }
            }
        }*/
    }
}
