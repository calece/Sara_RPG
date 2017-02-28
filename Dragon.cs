using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sara_RPG
{
    class Dragon : Enemy
    {

        bool fire = false;
        float fireballDelay = 2.0f;
        float turnDelay = 1.0f;
        float shootTimer = 0;
        float turnTimer = 0;
        string lastDirection;
        bool checkDirection = false;
        public Dragon(string Name, Texture2D texture, int frames, int rows)
            : base(Name, texture, frames, rows)
        {
        }

        public void Handle_Dragon(GameTime gameTime, AnimatedSprite fireball)
        {
            if (!alive)
                return;
            shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shootTimer > fireballDelay && !fire)
            {
                if (0 <= this.position.Y && this.position.Y <= 5 || 95 <= this.position.Y && this.position.Y <= 95 || 315 <= this.position.Y + this.height && this.position.Y + this.height <= 320)
                {
                    fire = true;
                    
                }
            }
            if (fire)
            {
                turnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (!checkDirection)
                {
                    checkDirection = true;
                    lastDirection = this.Animation;
                }
                if (this.Animation != "Left")
                {
                    this.Animation = "Left";
                    fireball.alive = true;
                    
                }
                this.speed = 0;
                if (turnTimer > turnDelay)
                {
                    turnTimer = 0;
                    shootTimer = 0;
                    this.Animation = lastDirection;
                    this.speed = (this.Animation == "Up") ? -2.5f : 2.5f;
                    lastDirection = null;
                    checkDirection = false;
                    fire = false;
                }
            }








        }










    }
}
