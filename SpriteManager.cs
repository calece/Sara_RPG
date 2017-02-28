using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sara_RPG
{
    public class SpriteManager
    {
        public string name = "";
        public Texture2D texture;
        public Vector2 position = Vector2.Zero;   
        public Vector2 Origin;
        private string animation;
        public int height, width, health, damage;
        public float speed = 1.0f;
        public bool alive = true;
        public bool sprint = false;        
        public int curFrame = 0;
        public Dictionary<string, AnimationClass> Animations= new Dictionary<string, AnimationClass>();
        public string Animation
        {
            get { return animation; }
            set
            {
                animation = value;
                curFrame = 0;
            }
        }
        public SpriteManager(string Name, Texture2D texture, int frames, int rows)
        {
            this.name = Name;
            this.texture = texture;
            this.width = texture.Width / frames;
            this.height = texture.Height / rows;
            this.Origin = new Vector2(width / 2, height / 2);
        }

        public void AddAnimation(string name, int frames, int row, AnimationClass animation)
        {
            Rectangle[] artSource = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                artSource[i] = new Rectangle(i * width, row * height, width, height);
            }
            animation.Frames = frames;
            animation.artSource = artSource;
            Animations.Add(name, animation);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Animations[Animation].artSource[curFrame],
                Animations[Animation].color, Animations[Animation].rotation, Animations[Animation].origin,
                Animations[Animation].scale, Animations[Animation].spriteEffect, 0f);
        }


        public bool HandleCollision(SpriteManager spriteOne, SpriteManager spriteTwo)
        {
            Rectangle heroBox = new Rectangle((int)spriteOne.position.X, (int)spriteOne.position.Y, spriteOne.width, spriteOne.height);
            Rectangle enemyBox = new Rectangle((int)spriteTwo.position.X, (int)spriteTwo.position.Y, spriteTwo.width, spriteTwo.height);
            if (spriteTwo.name == "Demon")
            {
                enemyBox = new Rectangle((int)spriteTwo.position.X + 22, (int)spriteTwo.position.Y + 15, spriteTwo.width - 40, spriteTwo.height - 28);
            }
            if (spriteTwo.name.Contains("Fireball"))
            {
                enemyBox = new Rectangle((int)spriteTwo.position.X, (int)spriteTwo.position.Y + 10, spriteTwo.width, 30);
            }
            if (heroBox.Intersects(enemyBox) && spriteTwo.alive == true)
            {
                spriteOne.health -= spriteTwo.damage;
                spriteTwo.health -= spriteOne.damage;
                if (spriteOne.health <= 0)
                {
                    spriteOne.alive = false;
                }
                if (spriteTwo.health <= 0)
                {
                    spriteTwo.alive = false;
                }
                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool Check_Barriers(AnimatedSprite Sprite, List<Rectangle> Barriers)
        {
            foreach (Rectangle barrier in Barriers)
            {
                if (Sprite.Animation == "Left")
                {
                    Rectangle heroBox = new Rectangle((int)Sprite.position.X - 3, (int)Sprite.position.Y + 10, 2, Sprite.height - 10);
                    if (heroBox.Intersects(barrier))
                        return false;
                }
                else if (Sprite.Animation == "Right")
                {
                    Rectangle heroBox = new Rectangle((int)Sprite.position.X + Sprite.width + 1, (int)Sprite.position.Y + 10, 2, Sprite.height - 10);
                    if (heroBox.Intersects(barrier))
                        return false;
                }
                else if (Sprite.Animation == "Up")
                {
                    Rectangle heroBox = new Rectangle((int)Sprite.position.X + 10, (int)Sprite.position.Y - 3, Sprite.width - 10, 2);
                    if (heroBox.Intersects(barrier))
                        return false;
                }
                else if (Sprite.Animation == "Down")
                {
                    Rectangle heroBox = new Rectangle((int)Sprite.position.X + 10, (int)Sprite.position.Y + Sprite.height + 1, Sprite.width - 10, 2);
                    if (heroBox.Intersects(barrier))
                        return false;
                }

            }
            return true;


        }
    }
}
