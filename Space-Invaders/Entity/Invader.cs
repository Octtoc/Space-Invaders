using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Space_Invaders
{
    public class Invader : Entity
    {
        #region Variablen

        //Variablen für die Animation Invader getroffen
        private bool testbool = false;

        //Animation for Invaders
        private Animation idle_01, idle_02;
        private static Animation explode;
        private Animation currentAnimation;

        private static readonly int x_Invader = 32;
        private static readonly int y_Invader = 32;

        #endregion

        #region Konstruktor

        public Invader(Rectangle position, float animSpeed, Texture2D texture1, Texture2D texture2)
            : base(position, (int)Type.Invader)
        {
            //Animation for Player
            this.idle_01 = new Animation(texture1, 1, 1, x_Invader, y_Invader, animSpeed, 1, 1);
            this.idle_02 = new Animation(texture2, 1, 1, x_Invader, y_Invader, animSpeed, 1, 1);
            
            this.currentAnimation = this.idle_01;
        }

        #endregion

        #region Update_Draw

        public override void Update(GameTime gameTime)
        {
            //Update Animation for one Bullet or Invader
            currentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw Animation for one Bullet or Invader
            currentAnimation.Draw(this.position, spriteBatch);
        }

        #endregion

        #region Funktionen
        public void switchAnim()
        {
            if (testbool == true)
            {
                currentAnimation = idle_01;
                testbool = false;
            }
            else if (testbool == false)
            {
                currentAnimation = idle_02;
                testbool = true;
            }
        }
        #endregion
    }
}