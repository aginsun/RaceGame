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
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.PosX -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.PosX += 1;
            }
        }

        /*public void Update(GameTime gameTime)
        {
	        Fuel = Fuel - (0.05 * Speed); 
	        KeyboardState kbstate = Keyboard.GetState();
            if (kbstate.IsKeyDown(Keys.Up)) 
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
            else if (kbstate.IsKeyDown(Keys.Down))
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


        }*/
    }
}
