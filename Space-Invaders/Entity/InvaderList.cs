using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    public class InvaderList
    {
        #region Variablen

        //List of Entity
        public  List<Entity> EntityInvaders = new List<Entity>();
        public  List<Invader> invaderList = new List<Invader>();

        public  List<Shoot> shoots;

        private  List<Invader> sublist = new List<Invader>();

        private  float elapsedTime = 0;
        private  float elapsedTimeMove = 0;
        private float elapsedTimeActive = 0;

        private  float shootSpeed = 2f;
        private  float moveSpeed = 0.52f;

        private  int mvCounter = 0;

        private  System.Random rnd = new System.Random();

        private bool isActive = true;
        private  bool moveDirection = true;
        private  bool move = true;
        private  bool goDown = false;

        //Variablen für die Animation Invader getroffen
        private  Rectangle destroyInvaderPosition;
        private  bool destroyAnInvader = false;
        private  float destroytime = 0;

        //Animation for Invaders
        private  Animation explode;

        private  readonly int x_set = 6;
        private  readonly int y_set = 50;

        #endregion

        #region Eigentschafftsmethoden
        public float ShootSpeed
        {
            get
            {
                return shootSpeed;
            }
            set
            {
                shootSpeed = value;
            }
        }

        public float MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
            set
            {
                if (moveSpeed >= 0.02)
                    moveSpeed = value;
            }
        }
        #endregion

        #region Konstruktor

        public InvaderList(int column, int row)
        {
            //Shoot List
            shoots = new List<Shoot>();
            sublist = new List<Invader>();

            int x_Offset = x_set;
            int y_Offset = y_set;

            for (int i = 0; i < column; i++)
            {
                for (int b = 0; b < row; b++)
                {
                    this.invaderList.Add(new Invader(new Rectangle(x_Offset, y_Offset, 32, 32), 0.6f, Textures.Invader_Blue, Textures.Invader_Blue_02));
                    
                    x_Offset += 54;
                }
                y_Offset += 39;
                x_Offset = 6;
            }
            explode = new Animation(Textures.Invader_explode, 3, 1, 32, 32, 0.8f, 1, 1);
        }

        #endregion

        #region Update_Draw

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
                elapsedTimeMove += (float)gameTime.ElapsedGameTime.TotalSeconds;
                destroytime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Update all Invaders
                foreach (Invader inv in this.invaderList)
                {
                    inv.Update(gameTime);
                }

                //Update all Bullets
                foreach (Shoot sh in this.shoots)
                {
                    sh.Update(gameTime);
                }

                ShootBullet(gameTime);
                MoveInvaders(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw all Invaders
            foreach (Invader inv in this.invaderList)
            {
                inv.Draw(spriteBatch);
            }

            //Draw all Bullets
            foreach (Shoot sh in this.shoots)
            {
                sh.Draw(spriteBatch);
            }

            InvaderAnimationExplode();
        }

        #endregion

        #region Move_LookForMove

        public void MoveInvaders(GameTime gameTime)
        {
            if (elapsedTimeMove >= moveSpeed)
            {
                foreach (Invader inv in this.invaderList)
                {
                    if (inv.Position.X <= 0)
                    {
                        moveDirection = true;

                        goDown = true;
                    }
                    if (inv.position.X >= 800 - 32)
                    {
                        moveDirection = false;

                        goDown = true;
                    }
                }

                if (goDown)
                {
                    foreach (Invader inv in this.invaderList)
                        inv.Move(0, 18, gameTime);
                    goDown = false;
                }

                if (move == true)
                {
                    if (moveDirection == true)
                    {
                        foreach (Invader inv in this.invaderList)
                        {
                            inv.Update(gameTime);
                            inv.switchAnim();
                            inv.Move(8, 0, gameTime);                            
                        }

                        switch (mvCounter)
                        {
                            case 0:
                                Sounds.move1.Play();
                                break;
                            case 1:
                                Sounds.move2.Play();
                                break;
                            case 2:
                                Sounds.move3.Play();
                                break;
                            case 3:
                                Sounds.move4.Play();
                                break;
                        }

                        mvCounter++;
                        if (mvCounter > 3)
                            mvCounter = 0;
                    }
                    else if (moveDirection == false)
                    {
                        foreach (Invader inv in this.invaderList)
                        {
                            inv.Update(gameTime);
                            inv.switchAnim();
                            inv.Move(-8, 0, gameTime);
                        }
                        switch (mvCounter)
                        {
                            case 0:
                                Sounds.move1.Play();
                                break;
                            case 1:
                                Sounds.move2.Play();
                                break;
                            case 2:
                                Sounds.move3.Play();
                                break;
                            case 3:
                                Sounds.move4.Play();
                                break;
                        }

                        mvCounter++;
                        if (mvCounter > 3)
                            mvCounter = 0;
                    }
                }
                elapsedTimeMove = 0;
            }
        }

        #endregion

        #region ShootBullet

        private void ShootBullet(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            int s = 0;

            s = rnd.Next(0, 260);

            if ((invaderList.Count != 0) && (s == 2 || s==24))
            {
                Rectangle Reference = this.invaderList[rnd.Next(0, invaderList.Count)].position;

                //Add Shoot to Shoot List
                Shoot _shoot = new Shoot(new Rectangle(Reference.X + 7, Reference.Y - 10, 4, 16), 8);
                shoots.Add(_shoot);
                Shoot.EntityShoot.Add(_shoot);
            }
        }

        #endregion

        #region ExplodeAnimation_InvadersLeft
        public  void destroyAnimInvader(Invader inv)
        {
            destroyAnInvader = true;
            destroyInvaderPosition = inv.Position;
            destroytime = 0;
            elapsedTimeMove = elapsedTimeMove - 0.04f;
        }

        public void InvaderAnimationExplode()
        {
            if (destroyAnInvader == true)
            {
                if (destroyAnInvader == true && destroytime < 0.2f && InvadersLeft() == true)
                {
                    //explode.Draw(destroyInvaderPosition);
                }
                else
                {
                    destroytime = 0;
                    destroyAnInvader = false;
                }
            }
        }

        public bool InvadersLeft()
        {
            if (invaderList.Count == 0)
                return false;
            else
                return true;
        }

        #endregion

        #region Funktionen

        public void Setup(int column, int row)
        {
            shoots.Clear();
            sublist.Clear();
            invaderList.Clear();

            int x_Offset = x_set;
            int y_Offset = y_set;

            for (int i = 0; i < column; i++)
            {
                for (int b = 0; b < row; b++)
                {
                    this.invaderList.Add(new Invader(new Rectangle(x_Offset, y_Offset, 32, 32), 0.6f, Textures.Invader_Blue, Textures.Invader_Blue_02));

                    x_Offset += 54;
                }
                y_Offset += 39;
                x_Offset = 6;
            }
        }

        public void ClearInvadersList()
        {
            invaderList.Clear();
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

        public void StopMove(float sec)
        {
            
        }

        #endregion
    }
}