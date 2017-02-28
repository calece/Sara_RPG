using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Sara_RPG
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Song music;
        SpriteBatch spriteBatch;
        SpriteFont totalTime;
        Vector2 timePosition;
        SpriteManager background;
        AnimatedSprite health_bar;
        AnimatedSprite beach;
        AnimatedSprite dragonFireball, sorcererFireball, saraFireball;
        Hero Sara;
        List<Enemy> Enemies;
        List<Rectangle> Barriers;
        List<AnimatedSprite> Fireballs;
        Enemy Demon, Demon1, Demon2, Sorcerer;
        Dragon Dragon;
        KeyboardState input;
        float health_interval = 1f;
        float health_timer = 1f;
        Rectangle outL, outT, outB, outR, mapLeft, mapRight, mapTop, mapBottom, cageLeftTop, cageLeftBottom, cageBottom, cageRightTop, cageRightBottom, platformLeft, platformBottomLeft, platformBottomRight, boulderTop, boulderBottom,
            middleWallTop, middleWallLeft, middleWallRight, cornerTreeLeft, cornerTreeRight, bottomWallBottom, bottomWallLeft, bottomWallRight, beachBarrier, beachWall;
        int theTime;
        string TotalTime;
        int difficulty = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {


            /*                   CHARACTER AND ENEMY CONTENT                  */


            
            Enemies = new List<Enemy>();
            Fireballs = new List<AnimatedSprite>();
            AnimationClass animated = new AnimationClass();

            //Constructor (Name, Texture, Frames, Rows)

            Sara = new Hero("Sara", Content.Load<Texture2D>("sara_move_rows"), 5, 6);
            Demon = new Enemy("Demon", Content.Load<Texture2D>("Demon_Walk"), 4, 2);
            Demon1 = new Enemy("Demon", Content.Load<Texture2D>("Demon_Walk"), 4, 2);
            Demon2 = new Enemy("Demon", Content.Load<Texture2D>("Demon_Walk"), 4, 2);            
            Sorcerer = new Enemy("Sorcerer", Content.Load<Texture2D>("Sorcerer_Cast"), 6, 1);
            Dragon = new Dragon("Dragon", Content.Load<Texture2D>("Dragon_Move_S"), 8, 4);
            sorcererFireball = new AnimatedSprite("Sorc_Fireball", Content.Load<Texture2D>("Dragon_Fireball"), 4, 4);
            dragonFireball = new AnimatedSprite("Dragon_Fireball", Content.Load<Texture2D>("Dragon_Fireball"), 4, 4);
            saraFireball = new AnimatedSprite("Sara_Fireball", Content.Load<Texture2D>("Dragon_Fireball"), 4, 4);
            saraFireball.alive = false;


            Enemies.Add(Demon);
            Enemies.Add(Demon1);
            Enemies.Add(Demon2);
            Enemies.Add(Sorcerer);
            Enemies.Add(Dragon);

            Fireballs.Add(sorcererFireball);
            Fireballs.Add(dragonFireball);
            Fireballs.Add(saraFireball);

            //Add Animations (Dictionary Sting Key, Frames, Row of Animation, AnimationClass)
            Sara.AddAnimation("Left", 5, 0, animated.Copy());
            Sara.AddAnimation("Right", 3, 1, animated.Copy());
            Sara.AddAnimation("Down", 5, 2, animated.Copy());
            Sara.AddAnimation("Up", 5, 3, animated.Copy());
            Sara.AddAnimation("Scared", 5, 4, animated.Copy());
            Sara.AddAnimation("Sad", 5, 5, animated.Copy());
            Demon.AddAnimation("Down", 4, 0, animated.Copy());
            Demon.AddAnimation("Up", 4, 1, animated.Copy());
            Demon1.AddAnimation("Down", 4, 0, animated.Copy());
            Demon1.AddAnimation("Up", 4, 1, animated.Copy());
            Demon2.AddAnimation("Down", 4, 0, animated.Copy());
            Demon2.AddAnimation("Up", 4, 1, animated.Copy());
            Sorcerer.AddAnimation("Cast", 6, 0, animated.Copy());
            Dragon.AddAnimation("Up", 8, 0, animated.Copy());
            Dragon.AddAnimation("Down", 8, 1, animated.Copy());
            Dragon.AddAnimation("Left", 8, 2, animated.Copy());
            Dragon.AddAnimation("Right", 8, 3, animated.Copy());
            sorcererFireball.AddAnimation("Right", 4, 0, animated.Copy());
            dragonFireball.AddAnimation("Left", 4, 1, animated.Copy());
            saraFireball.AddAnimation("Right", 4, 0, animated.Copy());
            saraFireball.AddAnimation("Left", 4, 1, animated.Copy());
            saraFireball.AddAnimation("Up", 4, 2, animated.Copy());
            saraFireball.AddAnimation("Down", 4, 3, animated.Copy());
            //Set Animation
            Sara.Animation = "Right";
            Sorcerer.Animation = "Cast";
            Demon.Animation = "Down";
            Demon1.Animation = "Down";
            Demon2.Animation = "Down";
            Dragon.Animation = "Down";
            sorcererFireball.Animation = "Right";
            dragonFireball.Animation = "Left";
            saraFireball.Animation = "Right";
            
            
            //Set Movement (Minimum X, Minimum Y, Maximum X, Maximum Y, Speed)
            Sara.speed = 3.0f;
            sorcererFireball.speed = 5.0f;
            dragonFireball.speed = -5.0f;
            Demon.SetEnemyMovement(0, 0, 0, 347, 1.5f);
            Demon1.SetEnemyMovement(0, 0, 0, 347, 1.25f);
            Demon2.SetEnemyMovement(0, 0, 0, 347, 1.5f);
            Sorcerer.SetEnemyMovement(0, 0, 500, 0, 0);
            Dragon.SetEnemyMovement(0, 0, 0, 320, 2.5f);

            Sara.position = new Vector2(30, 690);
            Demon.position = new Vector2(158,2);
            Demon1.position = new Vector2(159 + Demon.width, 2);
            Demon2.position = new Vector2(160 + (Demon.width * 2), 2);
            Sorcerer.position = new Vector2(25, 510);
            Dragon.position = new Vector2(924, 5);
            sorcererFireball.position = new Vector2(Sorcerer.position.X + Sorcerer.width, 530);
            dragonFireball.position = new Vector2(Dragon.position.X - (dragonFireball.width/2), Dragon.position.Y + 10);


            //Set Health and Damage
            Sara.Set_Health_Damage(300, 1);
            Demon.Set_Health_Damage(10000, 150);
            Demon1.Set_Health_Damage(10000, 150);
            Demon2.Set_Health_Damage(10000, 150);
            Sorcerer.Set_Health_Damage(3, 100);
            Dragon.Set_Health_Damage(15, 300);
            sorcererFireball.Set_Health_Damage(0, 50);
            dragonFireball.Set_Health_Damage(10000, 100);
            saraFireball.Set_Health_Damage(0, 1);

                       

            //Set Frames Per Second (1 Second / Value = Interval)
            Sara.FramesPerSecond = 10;
            Demon.FramesPerSecond = 8;
            Demon1.FramesPerSecond = 8;
            Demon2.FramesPerSecond = 8;
            Sorcerer.FramesPerSecond = 2;
            Dragon.FramesPerSecond = 8;
            sorcererFireball.FramesPerSecond = 12;
            dragonFireball.FramesPerSecond = 12;
            saraFireball.FramesPerSecond = 12;

            Sara.loopAnimation = true;
            Demon.loopAnimation = true;
            Demon1.loopAnimation = true;
            Demon2.loopAnimation = true;
            Sorcerer.loopAnimation = true;
            Dragon.loopAnimation = true;
            sorcererFireball.loopAnimation = true;
            dragonFireball.loopAnimation = true;
            saraFireball.loopAnimation = true;



            /*                   WORLD AND BARRIER CONTENT                  */


            totalTime = Content.Load<SpriteFont>("TotalTime");
            timePosition = new Vector2(0, 745);
            music = Content.Load<Song>("bg_music");
            MediaPlayer.Play(music);             //<=== UN-COMMENT FOR MUSIC TO PLAY ====
            Barriers = new List<Rectangle>();

            //Constructor (Name, Texture, Frames, Rows)
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            background = new SpriteManager("background", Content.Load<Texture2D>("Background"), 1, 1);
            health_bar = new AnimatedSprite("health_bar", Content.Load<Texture2D>("Health_Bar"), 1, 1);
            beach = new AnimatedSprite("beach", Content.Load<Texture2D>("Beach"), 6, 1);

            outL = new Rectangle(-100, 0, 100, 768);
            outR = new Rectangle(1024, 0, 100, 768);
            outT = new Rectangle(0, -100, 1024, 100);
            outB = new Rectangle(0, 768, 1024, 100);

            mapLeft = new Rectangle(0, 0, 25, 768);
            mapRight = new Rectangle(1014, 0, 10, 768);
            mapTop = new Rectangle(0, 0, 1024, 5);
            mapBottom = new Rectangle(0, 743, 1024, 25);

            beachBarrier = new Rectangle(832, 544, 192, 224);
            beachWall = new Rectangle(644, 525, 188, 4);

            platformLeft = new Rectangle(658, 0, 15, 290);
            platformBottomLeft = new Rectangle(673, 288, 175, 10);
            platformBottomRight = new Rectangle(949, 288, 80, 10);

            boulderTop = new Rectangle(752, 68, 163, 43);
            boulderBottom = new Rectangle(752, 183, 163, 48);

            middleWallTop = new Rectangle(132, 448, 528, 2);
            middleWallLeft = new Rectangle(132, 448, 345, 54);
            middleWallRight = new Rectangle(555, 448, 105, 54);

            bottomWallBottom = new Rectangle(0, 640, 740, 4);
            bottomWallLeft = new Rectangle(0, 589, 270, 55);
            bottomWallRight = new Rectangle(341, 589, 239, 55);

            cornerTreeLeft = new Rectangle(660, 453, 49, 70);
            cornerTreeRight = new Rectangle(680, 490, 58, 36);

            cageLeftTop = new Rectangle(132, 0, 47, 105);
            cageLeftBottom = new Rectangle(132, 238, 47, 210);
            cageBottom = new Rectangle(155, 355, 370, 5);
            cageRightTop = new Rectangle(506, 262, 47, 190);
            cageRightBottom = new Rectangle(506, 0, 47, 105);

            Barriers.Add(outT);
            Barriers.Add(outB);
            Barriers.Add(outR);
            Barriers.Add(outL);
            Barriers.Add(mapLeft);
            Barriers.Add(mapRight);
            Barriers.Add(mapTop);
            Barriers.Add(mapBottom);
            Barriers.Add(beachBarrier);
            Barriers.Add(beachWall);
            Barriers.Add(platformLeft);
            Barriers.Add(platformBottomLeft);
            Barriers.Add(platformBottomRight);
            Barriers.Add(boulderTop);
            Barriers.Add(boulderBottom);
            Barriers.Add(middleWallTop);
            Barriers.Add(middleWallLeft);
            Barriers.Add(middleWallRight);
            Barriers.Add(bottomWallBottom);
            Barriers.Add(bottomWallLeft);
            Barriers.Add(bottomWallRight);
            Barriers.Add(cornerTreeLeft);
            Barriers.Add(cornerTreeRight);
            Barriers.Add(cageLeftTop);
            Barriers.Add(cageLeftBottom);
            Barriers.Add(cageBottom);
            Barriers.Add(cageRightTop);
            Barriers.Add(cageRightBottom);
            //Add Animations (Dictionary String Key, Number of Frames, Row of Animation, Animation Class)
            background.AddAnimation("Background", 1, 0, animated.Copy());
            beach.AddAnimation("Beach", 6, 0, animated.Copy());
            health_bar.AddAnimation("Health", 1, 0, animated.Copy());

            //Set Animation
            background.Animation = "Background";
            beach.Animation = "Beach";
            health_bar.Animation = "Health";

            //Set Locations
            beach.position = new Vector2(832, 544);
            health_bar.position = new Vector2(5, 5);
            
            //Enable Animation Loop
            beach.loopAnimation = true;

            //Set Frames Per Second (1 Second / Value = Interval)
            beach.FramesPerSecond = 4;
            
        }
        
        protected override void UnloadContent()
        {
            
          
        }

        protected override void Update(GameTime gameTime)
        {
            theTime = gameTime.TotalGameTime.Seconds;
            TotalTime = "Total Time: " + theTime.ToString() + " Difficulty: " + difficulty.ToString();
            base.Update(gameTime);
            input = Keyboard.GetState();
            if (input.IsKeyDown(Keys.Space))
            {
                Sara.Handle_Sara_Fireball(Sara, saraFireball);
            }
            Sara.HandleMovement(input, gameTime, Sara, Barriers);
            Dragon.Handle_Dragon(gameTime, dragonFireball);
            
            health_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (AnimatedSprite fireball in Fireballs)
            {
                fireball.Update(gameTime);
                if (fireball.alive == true)
                {
                    fireball.alive = fireball.Check_Barriers(fireball, Barriers);
                    if (fireball.name == "Sara_Fireball")
                    {
                        foreach (Enemy enemy in Enemies)
                        {
                            fireball.HandleCollision(fireball, enemy);
                        }
                        foreach (AnimatedSprite Fireball in Fireballs)
                        {
                            if(Fireball.name != "Sara_Fireball")
                            fireball.HandleCollision(fireball, Fireball);
                        }
                    }
                }
                if (fireball.alive == false && fireball.name == "Sorc_Fireball")
                {
                    if(Sorcerer.alive)
                    fireball.alive = true;
                    fireball.position = new Vector2(Sorcerer.position.X + Sorcerer.width, 530);
                }
                else if (fireball.alive == false && fireball.name == "Dragon_Fireball")
                {
                    fireball.position = new Vector2(Dragon.position.X - (dragonFireball.width), Dragon.position.Y + 10);
                }
                if (fireball.name != "Sara_Fireball")
                {
                    fireball.position.X += fireball.speed;
                }
                else
                {
                    if (fireball.Animation == "Up" || fireball.Animation == "Down")
                    {
                        fireball.position.Y += fireball.speed;
                    }
                    else
                    {
                        fireball.position.X += fireball.speed;
                    }
                }
                if (health_timer > health_interval)
                {
                    if (Sara.HandleCollision(Sara, fireball))
                    {
                        fireball.alive = false;
                        {
                            health_bar.Update_Health(health_bar, Sara);
                            health_timer = 0;
                        }
                    }
                }
               
            }
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.alive == true)
                {
                    enemy.Update(gameTime);
                    if (enemy.name == "Demon")
                    enemy.CheckSprint(Sara);
                    enemy.MoveEnemy();
                    if (health_timer > health_interval)
                    {
                        if (Sara.HandleCollision(Sara, enemy))
                        {                            
                            health_bar.Update_Health(health_bar, Sara);
                            health_timer = 0;                            
                        }
                    }
                    
                }
            }

           

            beach.Update(gameTime);
            if (Sara.alive == false || Dragon.alive == false)
            {
                Sara.Set_Health_Damage(300, 1);
                Demon.Set_Health_Damage(10000, 150);
                Demon1.Set_Health_Damage(10000, 150);
                Demon2.Set_Health_Damage(10000, 150);
                Sorcerer.Set_Health_Damage(3, 100);
                Dragon.Set_Health_Damage(15, 300);
                health_bar.Update_Health(health_bar, Sara);
                if (Dragon.alive == false)
                {
                    foreach (Enemy enemy in Enemies)
                    {
                        enemy.speed += 0.1f;
                    }
                    Sara.speed += 0.1f;
                    difficulty += 1;
                }
                foreach (Enemy enemy in Enemies)
                {
                    enemy.alive = true;                    
                }
                
                Sara.position = new Vector2(30, 690);
                Sara.alive = true;
            }

           


        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            beach.Draw(spriteBatch);
            health_bar.Draw(spriteBatch);            
            if (Sara.alive == true)
            {
                Sara.Draw(spriteBatch);
            }
            foreach (Enemy enemy in Enemies)
            {
                if(enemy.alive == true)
                enemy.Draw(spriteBatch);
            }
            foreach (AnimatedSprite fireball in Fireballs)
            {
                if (fireball.alive == true)
                    fireball.Draw(spriteBatch);
            }
            spriteBatch.DrawString(totalTime, TotalTime, timePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
