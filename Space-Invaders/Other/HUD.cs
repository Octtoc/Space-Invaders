using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    struct HudData
    {
        public int value;
        public Vector2 position;
        public Color color;
    }

    public class HUD
    {
        HudData ScoreData;
        HudData LifeData;
        ItemManager.ItemID currentItem;

        NormalTexture Live;
        NormalTexture Item;
               
        /// <summary>
        /// Erstellen einer HUD
        /// </summary>
        /// <param name="_positionscore">Position der Score Anzeige</param>
        /// <param name="_positionlifes">Position der Lebensanzeige</param>
        /// <param name="lifes"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public HUD(Vector2 positionscore, Vector2 positionlifes, int lifes, int posX, int posY)
        {
            ScoreData.position = positionscore;
            ScoreData.color = Color.Azure;
            ScoreData.value = 0;

            LifeData.position = positionlifes;
            LifeData.color = Color.Azure;
            LifeData.value = lifes;

            Live = new NormalTexture(new Rectangle(int.Parse(LifeData.position.X.ToString()), int.Parse(LifeData.position.Y.ToString()), 32, 32), Textures.Lives);
            Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.Lives);
        }

        #region Eigentschafftsfunktionen

        public int Score
        {
            get
            {
                return ScoreData.value;
            }
            set
            {
                ScoreData.value = value;
            }
        }

        public int Lifes
        {
            get
            {
                return LifeData.value;
            }
            set
            {
                LifeData.value = value;
            }
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Fonts.standard, "Score: " + ScoreData.value.ToString(), ScoreData.position, ScoreData.color);

            DrawLifes(spriteBatch);
            DrawItem(spriteBatch);
        }

        public void DrawLifes(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < LifeData.value; i++)
            {
                Live.Draw(spriteBatch);
                Live.position.X += 34;
            }
            Live.position.X = 10;
        }

        public void DrawItem(SpriteBatch spriteBatch)
        {
            Item.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime, Player player)
        {
            LifeData.value = player.Lifes;
            ScoreData.value = player.Score;

            if (this.currentItem != player.CurrentItem)
            {
                this.currentItem = player.CurrentItem;

                Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.redship);

                switch (player.CurrentItem)
                {
                    case ItemManager.ItemID.Banana:
                        Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.banana);
                        break;
                    case ItemManager.ItemID.Blackout:
                        Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.blackout);
                        break;
                    case ItemManager.ItemID.Faster:
                        Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.faster);
                        break;
                    case ItemManager.ItemID.Freeze:
                        Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.freeze);
                        break;
                    case ItemManager.ItemID.Invasion:
                        Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.invasion);
                        break;
                    case ItemManager.ItemID.NoItem:
                        Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.noitem);
                        break;
                    case ItemManager.ItemID.Shield:
                        Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.shield);
                        break;
                    case ItemManager.ItemID.SpeedUp:
                        Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.speedup);
                        break;
                    case ItemManager.ItemID.TripleSpeed:
                        Item = new NormalTexture(new Rectangle(700, 550, 32, 32), Textures.triplespeed);
                        break;
                }
            }
        }
    }
}
