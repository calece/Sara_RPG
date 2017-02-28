using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sara_RPG
{
     public class AnimatedSprite : SpriteManager
    {
        public float timer;
        public bool loopAnimation = false;
        public float interval = 0.05f;
        public int FramesPerSecond
        {
            set { interval = (1f / value); }
        }



        public AnimatedSprite(string Name, Texture2D texture, int frames, int rows)
            : base(Name, texture, frames, rows)
        {
        }



        public void Set_Health_Damage(int Health, int Damage)
        {
            this.health = Health;
            this.damage = Damage;
        }


        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > interval)
            {
                timer -= interval;
                if (curFrame < Animations[Animation].Frames - 1)
                {
                    curFrame++;
                }
                else if(loopAnimation)
                {
                    curFrame = 1;
                }
            }
        }

        public void Update_Health(AnimatedSprite health, Hero Sara)
        {
            health.Animations["Health"].artSource[0].Width = Sara.health;
        }




        public void Handle_Sara_Fireball(Hero Sara, AnimatedSprite fireball)
        {

            if (fireball.alive)
                return;

            fireball.Animation = Sara.Animation;
            
            if (fireball.Animation == "Up")
            {              
                fireball.speed = -7.0f;
                fireball.position.X = Sara.position.X;
                fireball.position.Y = Sara.position.Y -(fireball.height);

            }
            else if (fireball.Animation == "Left")
            {
                fireball.speed = -7.0f;
                fireball.position.X = Sara.position.X - (fireball.width);
                fireball.position.Y = Sara.position.Y;
            }
            else if (fireball.Animation == "Right")
            {
                fireball.speed = 7.0f;
                fireball.position.X = Sara.position.X + Sara.width;
                fireball.position.Y = Sara.position.Y;
            }
            else if (fireball.Animation == "Down")
            {
                fireball.speed = 7.0f;
                fireball.position.X = Sara.position.X;
                fireball.position.Y = Sara.position.Y + (Sara.height);
            }
            else
            {
                fireball.speed = 7.0f;
            }
            fireball.alive = true;    
            




        }



        
    }
}
