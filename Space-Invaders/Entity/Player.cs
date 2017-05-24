using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Space_Invaders_Gsm;
using System.Threading;

namespace Space_Invaders
{
    public class Player
    {
        HUD hud;
        SpaceShip spaceship;
        ItemManager.ItemID currentItem;

        int score;
        int lifes;

        private bool isActive = true;
        private float elapsedTimeActive = 0;

        public Player()
        {
            this.lifes = 3;

            hud = new HUD(new Vector2(400, 5), new Vector2(10, 10), 3, 10, 38);

            spaceship = new SpaceShip(new Rectangle(400, 560, 50, 20), 0.25f, 0.02f, 0,
                Textures.blueship,
                Microsoft.Xna.Framework.Input.Keys.Right,
                Microsoft.Xna.Framework.Input.Keys.Left,
                Microsoft.Xna.Framework.Input.Keys.Space);
        }

        public ItemManager.ItemID CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value; }
        }

        public SpaceShip Ship
        {
            get { return spaceship; }
        }

        public HUD Hud
        {
            get { return hud; }
        }

        public int Lifes
        {
            get { return lifes; }
            set { lifes = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public void Hit()
        {
            this.lifes -= 1;

            if (lifes == 0)
            {
                
            }
            
            this.Ship.Position = new Rectangle(400, 560, 50, 20);
        }

        public void Update(GameTime gameTime)
        {
            if (!isActive)
            {
                elapsedTimeActive += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (elapsedTimeActive >= 1.5)
                {
                    ChangeMove();
                    elapsedTimeActive = 0;
                }
            }

            if (isActive)
            {
                Ship.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Ship.Draw(spriteBatch);
        }

        public void ChangeMove()
        {
            if (isActive)
            {
                isActive = false;
            }
            else
            {
                isActive = true;
            }
        }
    }
}
