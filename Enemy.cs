using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Sara_RPG
{
    public class Enemy : AnimatedSprite
    {
        public int maxX = 0;
        public int maxY = 0;
        public int minX = 0;
        public int minY = 0;
        
        




        public Enemy(string Name, Texture2D texture, int frames, int rows)
            : base(Name, texture, frames, rows)
        {
        }



        public void SetEnemyMovement(int lowX, int lowY, int highX, int highY, float Speed)
        {
            minX = lowX;
            maxX = highX;
            minY = lowY;
            maxY = highY;
            speed = Speed;
        }

        public void MoveEnemy()
        {
            if (this.Animation == "Up")
            {
                if (this.position.Y < minY)
                {
                    this.Animation = "Down";
                    this.speed = speed * -1.0f;
                }
                else
                {
                    this.position.Y += (this.sprint == true) ? (this.speed * 2.0f) : this.speed;
                }
 
            }
            else if (this.Animation == "Down")
            {
                if ((this.position.Y + this.height) > this.maxY)
                {
                    this.Animation = "Up";
                    this.speed = speed * -1.0f;
                }
                else
                {
                    this.position.Y += (this.sprint == true) ? (this.speed * 2.0f) : this.speed;
                }
            }

            else if (this.Animation == "Right")
            {
                if ((this.position.X + this.width) > this.maxX)
                {
                    this.Animation = "Left";
                    this.speed = this.speed * -1.0f;                    
                }
                else
                {
                    this.position.X += (this.sprint == true) ? (this.speed * 2.0f) : this.speed;
                }
 
            }
            else if (this.Animation == "Left")
            {
                if ((this.position.X) < minX)
                {
                    this.Animation = "Right";
                    this.speed = this.speed * -1.0f;
                }
                else
                {
                    this.position.X += (this.sprint == true) ? (this.speed * 2.0f) : this.speed;
                }

            }
        }



        public void CheckSprint(AnimatedSprite hero)
        {
            Rectangle heroBox = new Rectangle((int)hero.position.X, (int)hero.position.Y, hero.width, hero.height);
            if (Animation == "Down")
            {
                Rectangle downSprint = new Rectangle((int)position.X, (int)position.Y + height, width, 300);
                if (downSprint.Intersects(heroBox))
                {
                    this.sprint = true;
                }
                else
                {
                    this.sprint = false; 
                }
                
            }
            if (Animation == "Up")
            {
                Rectangle upSprint = new Rectangle((int)position.X, (int)position.Y-300, width, 300);
                if (upSprint.Intersects(heroBox))
                {
                    this.sprint = true;
                }
                else
                {
                    this.sprint = false;
                }

            }
        }

    }
}
