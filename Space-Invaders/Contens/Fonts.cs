using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Space_Invaders_Gsm;

namespace Space_Invaders
{
    public enum FontStyle
    {
        Standard, Score
    }

    //Fontsammlung
    //Klasse um für alle im Spiel enthaltenen Fonts
    public static class Fonts
    {
        public static SpriteFont standard;
        public static SpriteFont score;

        private static ContentManager content;

        public static void Setup(ContentManager content)
        {
            standard = content.Load<SpriteFont>("Fonts\\Standard");
            score = content.Load<SpriteFont>("Fonts\\Standard");
        }

        public static SpriteFont getFont(FontStyle style)
        {
            switch (style)
            {
                case FontStyle.Standard:
                    return standard;
                case FontStyle.Score:
                    return score;
                default:
                    return standard;
            }
        }
    }
}
