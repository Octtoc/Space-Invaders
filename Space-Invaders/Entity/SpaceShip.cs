using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Space_Invaders_Gsm;

namespace Space_Invaders
{
    public class SpaceShip : Entity
    {
        #region Variablen

        //List of Entity and all shoots
        private List<Shoot> shoots;

        //Animation
        private Animation idle, currentAnimation;

        private Microsoft.Xna.Framework.Input.Keys right;
        private Microsoft.Xna.Framework.Input.Keys left;
        private Microsoft.Xna.Framework.Input.Keys shootKey;

        //Sound Effect
        private SoundEffect photon_shot;

        //Speed Player
        private readonly static int movingSpeed = 2;
        private readonly static int DeleteOverWindowY = 5;

        //can shoot? because of time
        private bool canShoot;
        private int lives;
        private int score;

        private bool canMove;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Erstellen eines Spielers ohne HUD
        /// </summary>
        /// <param name="position">Anfangsposition des Objekts</param>
        /// <param name="animSpeed">Animationsgeschwindigkeit</param>
        /// <param name="moveSpeed"></param>
        /// <param name="type"></param>
        /// <param name="shipTexture">Textur des Spielers</param>
        /// <param name="_right">Taste um nach rechts zu steuern</param>
        /// <param name="_left">Taste um nach rechts zu steuern</param>
        /// <param name="shootKey">Taste um nach links zu steuern</param>
        public SpaceShip(Rectangle position, float animSpeed, float moveSpeed, int type,
                      Texture2D shipTexture,
                      Microsoft.Xna.Framework.Input.Keys rightKey,
                      Microsoft.Xna.Framework.Input.Keys leftKey,
                      Microsoft.Xna.Framework.Input.Keys shootKey)
            : base(position, type)
        {
            //List of Shoots
            this.shoots = new List<Shoot>();
            this.canShoot = false;
            this.canMove = true;

            //Load Animation
            this.idle = new Animation(shipTexture, 1, 1, this.position.Width, this.position.Height, animSpeed, 1, 1);

            //Load Sounds
            this.photon_shot = Sounds.photon_shot;

            //Set currentAnimation
            this.currentAnimation = this.idle;
            this.lives = 3;

            this.vSpeed = new Vector2(120, 0);

            this.right = rightKey;
            this.left = leftKey;
            this.shootKey = shootKey;

            //Add Entity to List
            entities.Add(this);
        }

        #endregion

        #region Eigentschafftsmethoden

        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }

        public int Life
        {
            get
            {
                return lives;
            }
            set
            {
                lives = value;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        public List<Shoot> Shoots
        {
            get
            {
                return this.shoots;
            }
            set
            {
                this.shoots = value;
            }
        }

        #endregion

        #region Virtual_DRAW_UPDATE_MOVE

        public override void Update(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);

            Move(2, 0, gameTime);
            ShootBullet(gameTime);

            foreach (Shoot sh in this.shoots)
            {
                sh.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(this.position, spriteBatch);

            foreach (Shoot sh in this.shoots)
            {
                sh.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Bewegen des Spielers
        /// </summary>
        /// <param name="gameTime">Spielzeit für die Zeit</param>
        public override void Move(int x_Offset, int y_Offset, GameTime gameTime)
        {
            //elapsedTimeMove += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Tastatur Abfrage

            if (canMove)
            {
                if (GameplayScreen.keyboardstate.IsKeyDown(right) && this.position.X <= 800 - 18/* && elapsedTimeMove > movingSpeed*/)
                {
                    position.X += movingSpeed;
                }
                if ((GameplayScreen.keyboardstate.IsKeyDown(left) && this.position.X >= 0)/* && elapsedTimeMove > movingSpeed*/)
                {
                    position.X -= movingSpeed;
                }
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Überprüft ob geschossen werden beim Druck der Schuss Taste
        /// </summary>
        /// <param name="gameTime"></param>
        private void ShootBullet(GameTime gameTime)
        {

            if (shoots.Count == 0)
            {
                canShoot = true;
            }

            if (GameplayScreen.keyboardstate.IsKeyDown(shootKey)
                     && GameplayScreen.oldkeyboardstate.IsKeyUp(shootKey)
                     && (canShoot == true))
            {
                //Add Shoot to Shoot List
                canShoot = false;
                Shoot _shoot = new Shoot(new Rectangle(((this.position.Right - this.position.Left) / 2) + this.position.Left, this.position.Y - 10, 4, 16), -10);
                shoots.Add(_shoot);

                this.photon_shot.Play(0.2f, 0.0f, 0.0f);
            }

            if (this.shoots.Count != 0)
            {
                if (this.shoots[0].Position.Y < DeleteOverWindowY)
                    this.shoots.Clear();
            }
        }

        /// <summary>
        /// Leeren der Schussliste
        /// </summary>
        public void ClearShootList()
        {
            this.shoots.Clear();
        }

        #endregion
    }
}