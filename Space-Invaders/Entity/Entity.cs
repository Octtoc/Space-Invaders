using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    public abstract class Entity
    {
        #region Variablen
        //List of Entity
        public static List<Entity> entities = new List<Entity>();

        public static List<Entity> EntityPlayer = new List<Entity>();
        public static List<Entity> EntityInvader = new List<Entity>();
        public static List<Entity> EntityShoot = new List<Entity>();

        //Texture Standard is Tileset

        //Position and Color
        public Rectangle position;
        protected Vector2 vPosition;
        protected Vector2 vDirection;
        protected Vector2 vSpeed;
        protected Color color;

        //Type for collide
        protected int type;
        protected bool collide;

        #endregion

        #region Konstruktor

        //Konstruktor
        public Entity(Rectangle position, int type)
        {
            this.Position = position;

            this.type = type;
        }

        #endregion

        #region EigentschafftsMethoden

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion

        #region Functions

        public virtual void Update(GameTime gameTime)
        {
            //Put some Update here
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Put some Draw here
        }

        public virtual void Move(int x_Offset, int y_Offset, GameTime gameTime)
        {
            this.position.X += x_Offset;
            this.position.Y += y_Offset;
        }

        public virtual void Remove()
        {

        }

        #endregion
    }
}
