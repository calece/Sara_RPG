using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;



namespace Sara_RPG
{
    public class Hero : AnimatedSprite
    {




        public Hero(string Name, Texture2D texture, int frames, int rows)
            : base(Name, texture, frames, rows)
        {


        }



        public void HandleMovement(KeyboardState input, GameTime gameTime, Hero Sara, List<Rectangle> Barriers)
        {
            
            
            if (input.IsKeyDown(Keys.A))
            {
                if (Sara.Animation != "Left")
                    Sara.Animation = "Left";
                if (Sara.Check_Barriers(Sara, Barriers))
                    Sara.position.X -= (sprint == true) ? (speed * 1.5f) : speed;                    
                Update(gameTime);
            }
            else if (input.IsKeyDown(Keys.D))
            {
                if (Sara.Animation != "Right")
                    Sara.Animation = "Right";
                if (Sara.Check_Barriers(Sara, Barriers))
                    Sara.position.X += (sprint == true) ? (speed * 1.5f) : speed;                    
                Update(gameTime);
            }
            else if (input.IsKeyDown(Keys.W))
            {
                if (Sara.Animation != "Up")
                    Sara.Animation = "Up";
                if (Sara.Check_Barriers(Sara, Barriers))
                    Sara.position.Y -= (sprint == true) ? (speed * 1.5f) : speed;
                Update(gameTime);
            }
            else if (input.IsKeyDown(Keys.S))
            {
                if (Sara.Animation != "Down")
                    Sara.Animation = "Down";
                if (Sara.Check_Barriers(Sara, Barriers))
                    Sara.position.Y += (sprint == true) ? (speed * 1.5f) : speed;                    
                Update(gameTime);
            }           
            else if (input.IsKeyDown(Keys.E))
            {
                Sara.Animation = "Sad";
                Update(gameTime);
            }
            else
            {
                curFrame = 0;
            }
        }
        



    }
}
