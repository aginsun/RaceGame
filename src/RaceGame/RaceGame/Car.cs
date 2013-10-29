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
		public double Direction;

        public Car()
        {
		    PosX = 0;
		    PosY = 0;
		    Fuel = 30;
		    Acceleration = 0;
		    Speed = 0;
		    Health = 100;
		    Direction = 90;
        }

        
        public void Update(GameTime gameTime)
        {
	        Fuel = Fuel - (0.05 * Speed); 
	        KeyboardState kbstate = Keyboard.GetState();
            if (kbstate.IsKeyDown(Keys.W)) 
	        {
                if(collision == collision.road)
	            {
                    Speed += 0.05;
                    if(Speed > 300)
                    {
                        Speed=300;
                    }
	            }
                else if (collision == collision.grass)
                {
                    if (Speed > 200)
                    {
                        Speed += -0.025;
                    }
                }
                else
                {
                    Speed += 0.025;
                }
	        }
            else if(collision == collision.wall)
	        {
		        Speed=-1*(Speed/4);
		        Health += -20;
	        }
		    else if(collision == collision.backcar)
		    {
		        Speed+= -40;
		        Health += 15;
                if(Speed<0)
                {
		            Speed=0;
                }
            }
		    else if(collision == collision.frontcar)
			{
			    Speed+= 20;
			    Health+= -10 ;
			}
            else if (kbstate.IsKeyDown(Keys.S))
	        {
                if(collision == collision.road)
	            {
	                if(Speed>0)
	                {	
		                Speed+=-35;
	                }
	                else if(Speed<-200)
	                {
		                Speed=-200;
	                }
                    else
	                {
		                Speed+=-0.25;
	                }
                }
	        }
            else if(collision == collision.grass)
	        {
		        if(Speed>-100)
		        {
			        Speed+= +0.025;
		        }
                else
                {
	                Speed+=0.025;
                }
	        }
            else if(collision == collision.wall)
	        {
		        Speed=1*(Speed/4);
		        Health+= -20;
	        }
		    else if(collision == collision.backcar)
	        {
			    Speed+= +40;
				Health += 15;
                if(Speed>0)
		        {
	                Speed=0;
			    }
            }
			else if(collision == collision.frontcar)
            {
	           Speed+= -20;
		       Health+= -10;
	        }

            if ((Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A)))
            {
               
                    Direction-=1/Speed;
            
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
               
                    Direction+=1/Speed;
            
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D)))
            {

                Direction+=1/Speed;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D))
            {

                Direction-=1/Speed;

            }
         
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.PosY -= (int)(Speed + 1);
                if (Speed < 10)
                    Speed += 0.05;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.PosY += 1;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && Speed !=0 )
            {
                if (Direction >= 0 && Direction <= 44)
                {
                    this.PosX += 0;
                    this.PosY += 0;
                }
                else if (Direction >= 45 && Direction <= 89)
                {
                    this.PosX -= 1;
                    this.PosY += 2;
                }
                else if (Direction >= 90 && Direction <= 134)
                {
                    this.PosX -= 1;
                    this.PosY += 0;
                } 
                else if (Direction >= 135 && Direction <= 179)
                {
                    this.PosX += 0;
                    this.PosY += 0;
                } 
                else if (Direction >= 180 && Direction <= 224)
                {
                    this.PosX += 0;
                    this.PosY += 0;
                }
                else if (Direction >= 225 && Direction <= 269)
                {
                    this.PosX += 0;
                    this.PosY += 0;
                }
                else if (Direction >= 270 && Direction <= 314)
                {
                    this.PosX += 0;
                    this.PosY += 0;
                }
                else if (Direction >= 315 && Direction <= 359)
                {
                    this.PosX += 0;
                    this.PosY += 0;
                }
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.D) && Speed != 0)
            {
                    if (Direction >= 0 && Direction <= 44)
                    {
                        this.PosX += 0;
                        this.PosY += 0;
                    }
                    else if (Direction >= 45 && Direction <= 89)
                    {
                        this.PosX -= 1;
                        this.PosY += 2;
                    }
                    else if (Direction >= 90 && Direction <= 134)
                    {
                        this.PosX -= 1;
                        this.PosY += 0;
                    }
                    else if (Direction >= 135 && Direction <= 179)
                    {
                        this.PosX += 0;
                        this.PosY += 0;
                    }
                    else if (Direction >= 180 && Direction <= 224)
                    {
                        this.PosX += 0;
                        this.PosY += 0;
                    }
                    else if (Direction >= 225 && Direction <= 269)
                    {
                        this.PosX += 0;
                        this.PosY += 0;
                    }
                    else if (Direction >= 270 && Direction <= 314)
                    {
                        this.PosX += 0;
                        this.PosY += 0;
                    }
                    else if (Direction >= 315 && Direction <= 359)
                    {
                        this.PosX += 0;
                        this.PosY += 0;
                    }
             }

        }
    }
}
