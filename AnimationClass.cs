using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sara_RPG
{
    public class AnimationClass
    {

        public Rectangle[] artSource;
        public Color color = Color.White;
        public Vector2 origin;
        public float rotation = 0f;
        public float scale = 1f;
        public SpriteEffects spriteEffect;
        public int Frames;
        public bool loopAnimation = false;


        public AnimationClass Copy()
        {
            AnimationClass copy = new AnimationClass();
            copy.artSource = artSource;
            copy.color = color;
            copy.origin = origin;
            copy.rotation = rotation;
            copy.scale = scale;
            copy.spriteEffect = spriteEffect;
            copy.loopAnimation = loopAnimation;
            copy.Frames = Frames;
            return copy;
        }

    }
}
