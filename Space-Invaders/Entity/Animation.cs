using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space_Invaders
{
    public class Animation
    {
        #region Variablen

        private Texture2D texture;

        //Variablen um herauszufinden wie
        //groß das Tileset ist
        private int rows;
        private int columns;
        private int width;
        private int height;

        //Variablen für den speed der Animation
        private float elapsedTime;
        private float animationSpeed;

        //Ab welcher Koordinate beginnt die Reihe
        private int rowBeginX;
        private int rowBeginY;

        //In welcher Reihe und in welcher Spalte sind
        //die Animationen im Moment
        private int currentRow;
        private int currentColumn;

        #endregion

        //Eigeentschafftsmethoden um auf die Werte
        //Werte zuzugreifen
        #region Eigentschafftsmethoden
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int CurrentRow
        {
            get { return currentRow; }
            set { currentRow = value; }
        }

        public int CurrentColumn
        {
            get { return currentColumn; }
            set { currentColumn = value; }
        }
        #endregion

        #region Konstruktor

        public Animation(Texture2D texture, int rows, int columns, int width, int height, float animSpeed, int rowBeginX, int rowBeginY)
        {
            this.texture = texture;

            this.rowBeginX = rowBeginX;
            this.rowBeginY = rowBeginY;

            this.rows = rows;
            this.columns = columns;
            this.width = width;
            this.height = height;
            this.animationSpeed = animSpeed;

            this.currentRow = 0;
            this.currentColumn = 0;
            this.elapsedTime = 0;
        }

        #endregion

        #region Update_Draw

        //Update Zeit wird hochgezählt wenn die Zeit abgelaufen ist
        //beginnt die nächste Animation
        public void Update(GameTime gametime)
        {
            elapsedTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > animationSpeed)
            {
                currentColumn++;
                elapsedTime -= animationSpeed;

                if (currentColumn >= columns)
                {
                    currentRow++;
                    currentColumn = 0;

                    if (currentRow >= rows)
                    {
                        currentColumn = 0;
                        currentRow = 0;
                    }
                }

            }
        }

        //Zeichne die animationen auf den Bildschirm
        public void Draw(Rectangle position, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, position,
            new Rectangle(
              (rowBeginX + (currentColumn-1)) * width,
              (rowBeginY + (currentRow-1)) * height,
              width, height),
              Color.White
            );
        }

        #endregion
    }
}
