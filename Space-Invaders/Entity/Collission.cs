using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Space_Invaders_Gsm;

namespace Space_Invaders
{
    public class Collission
    {
        #region Variablen

        private static Shoot deleteShoot = null;
        private static Invader deleteInvader = null;

        #endregion

        #region CollisionDetection

        private static bool BoundingBoxCollide(Rectangle A, Rectangle B)
        {
            if (A.Intersects(B))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Collision with

        public static bool CollideItemWithPlayer(Player player, ItemManager.Item item)
        {
            if(Collission.BoundingBoxCollide(player.Ship.Position, item.position))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void Collide_Shoot_From_Player_With_Invader(HUD hud, Player player, InvaderList invaderList)
        {
            int index = 0;
            int col = 0;

            foreach (Shoot s in player.Ship.Shoots)
            {
                foreach (Invader inv in invaderList.invaderList)
                {
                    if (Collission.BoundingBoxCollide(s.Position, inv.position))
                    {
                        deleteShoot = s;
                        deleteInvader = inv;
                        col = index;
                        Sounds.invader_killed.Play(0.1f, 0.0f, 0.0f);

                        player.Score += 100;
                    }

                    index++;
                }
                index = 0;
            }

            if (deleteShoot != null)
            {
                player.Ship.Shoots.Remove(deleteShoot);
                Shoot.EntityShoot.Remove(deleteShoot);
                deleteShoot = null;
            }

            if (deleteInvader != null)
            {
                invaderList.invaderList.Remove(deleteInvader);
                deleteInvader = null;
            }
        }

        private static void Collide_Shoot_From_Invader_With_Player(Player player, InvaderList invaderList)
        {
            foreach (Shoot s in invaderList.shoots)
            {
                if (Collission.BoundingBoxCollide(s.Position, player.Ship.Position))
                {
                    player.Hit();
                    invaderList.ChangeMove();
                    player.ChangeMove();
                    deleteShoot = s;
                    break;
                }
            }

            if (deleteShoot != null)
            {
                invaderList.shoots.Remove(deleteShoot);
                Shoot.EntityShoot.Remove(deleteShoot);
                deleteShoot = null;
            }
        }

        #endregion

        #region Zusammenfassung_Collision

        public static void checkCollissionPlayer(GameTime gameTime,InvaderList invaderList, Player player, HUD hud)
        {
            Collide_Shoot_From_Player_With_Invader(hud, player, invaderList);
            Collide_Shoot_From_Invader_With_Player(player, invaderList);
        }

        #endregion

    }
}
