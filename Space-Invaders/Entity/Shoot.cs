using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    enum Type : int { Player, Invader, Shoot };

    public class Shoot : Entity
    {
        #region Variablen
        //Liste der Entity con Shoot

        //Animation der Shoot
        private Animation shoot;
        private int shootSpeed;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Erstellen eines Schusses
        /// </summary>
        /// <param name="position">Startposition des Objekts</param>
        /// <param name="shootSpeed">Schussgescwindigkeit in pixel</param>
        public Shoot(Rectangle position, int shootSpeed)
            : base(position, (int)Type.Shoot)
        {
            Shoot.EntityShoot.Add(this);
            this.shootSpeed = shootSpeed;

            shoot = new Animation(Textures.redshoot, 1, 1, 4, 16, 1, 1, 1);
        }

        #endregion

        #region Draw_Update_Move_Delete
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw Animation (Standard)
            this.shoot.Draw(this.position, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            //Update Animation
            shoot.Update(gameTime);

            //Move Automatic to the top with shootspeed
            Move(0, shootSpeed, gameTime);
        }

        public override void Move(int x_Offset, int y_Offset, GameTime gameTime)
        {
            base.Move(x_Offset, y_Offset, gameTime);
        }

        /// <summary>
        /// Entfernen des Schuss
        /// </summary>
        /// <param name="list"></param>
        public void Delete(ref List<Shoot> list)
        {
            list.Remove(this);
        }
        #endregion
    }
}
