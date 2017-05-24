using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders.ItemManager
{
    public class Item : Entity
    {
        #region Variablen

        private ItemID currentItem;

        //Animation
        private Animation idle, currentAnimation;

        #endregion

        #region Konstruktor

        public Item(Rectangle position, int type, ItemID itemId)
            : base(position, type)
        {
            switch (itemId)
            {
                case ItemID.Banana:
                    this.idle = new Animation(Textures.Lives, 1, 1, this.position.Width, this.position.Height, 0.1f, 1, 1);
                    break;
                case ItemID.Blackout:
                    this.idle = new Animation(Textures.Lives, 1, 1, this.position.Width, this.position.Height, 0.1f, 1, 1);
                    break;
                case ItemID.Faster:
                    this.idle = new Animation(Textures.Lives, 1, 1, this.position.Width, this.position.Height, 0.1f, 1, 1);
                    break;
                case ItemID.Freeze:
                    this.idle = new Animation(Textures.Lives, 1, 1, this.position.Width, this.position.Height, 0.1f, 1, 1);
                    break;
                case ItemID.Invasion:
                    this.idle = new Animation(Textures.Lives, 1, 1, this.position.Width, this.position.Height, 0.1f, 1, 1);
                    break;
                case ItemID.Shield:
                    this.idle = new Animation(Textures.Lives, 1, 1, this.position.Width, this.position.Height, 0.1f, 1, 1);
                    break;
                case ItemID.SpeedUp:
                    this.idle = new Animation(Textures.Lives, 1, 1, this.position.Width, this.position.Height, 0.1f, 1, 1);
                    break;
                case ItemID.TripleSpeed:
                    this.idle = new Animation(Textures.Lives, 1, 1, this.position.Width, this.position.Height, 0.1f, 1, 1);
                    break;
            }

            this.currentAnimation = this.idle;
        }

        #endregion

        #region Eigentschafftsmethoden

        public ItemID CurrentItem
        {
            get
            {
                return currentItem;
            }
            set
            {
                currentItem = value;
            }
        }

        #endregion

        #region Virtual_DRAW_UPDATE_MOVE
        
        public override void Update(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);

            Move(0, 2, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(this.position, spriteBatch);
        }

        public override void  Move(int x_Offset, int y_Offset, GameTime gameTime)
        {
            position.Y += 1;
        }

        #endregion
    }
}
