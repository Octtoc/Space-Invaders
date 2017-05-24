using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Space_Invaders_Gsm;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders.ItemManager
{
    public enum ItemID { Freeze, Invasion, Faster, Banana, Blackout, SpeedUp, TripleSpeed, Shield, NoItem };

    class ItemManager
    {
        private Player thisPlayer;
        private Item fallingItem;
        private ItemID itemExecute;

        private float elapsedTime;

        public ItemManager(Player player)
        {
            this.thisPlayer = player;
            thisPlayer.CurrentItem = ItemID.NoItem;
            itemExecute = ItemID.NoItem;

            fallingItem = new Item(new Rectangle(32,32,32,32), 1, ItemID.Invasion);
        }

        public void Update(GameTime gameTime, InvaderList invaderlist)
        {
            if (fallingItem != null)
            {
                fallingItem.Update(gameTime);

                if(Collission.CollideItemWithPlayer(thisPlayer, fallingItem))
                {
                    thisPlayer.CurrentItem = ItemID.Freeze;

                    fallingItem = null;
                }
            }

            if (GameplayScreen.keyboardstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.I)
                     && GameplayScreen.oldkeyboardstate.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.I))
            {
                itemExecute = thisPlayer.CurrentItem;
                thisPlayer.CurrentItem = ItemID.NoItem;
            }

            UpdateItem(gameTime, invaderlist);
        }

        public void UpdateItem(GameTime gameTime, InvaderList invaderList)
        {
            switch (itemExecute)
            {
                case ItemID.Freeze:
                    elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (elapsedTime > 5.0f)
                    {
                        thisPlayer.Ship.CanMove = true;
                        itemExecute = ItemID.NoItem;
                        elapsedTime = 0;
                    }
                    else
                    {
                        thisPlayer.Ship.CanMove = false;
                    }
                    break;
                case ItemID.Invasion:
                    invaderList.invaderList.Add(new Invader(new Rectangle(400, 400, 32, 32), 0.6f, Textures.Invader_Blue, Textures.Invader_Blue_02));
                    itemExecute = ItemID.NoItem;
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(fallingItem != null)
            {
                fallingItem.Draw(spriteBatch);
            }
        }
    }
}
