using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Space_Invaders
{
    //Texturensammlung
    //Klasse um für alle im Spiel enthaltenen Texturen
    public class Textures
    {
        public static Texture2D blueship;
        public static Texture2D redship;
        public static Texture2D blueshoot;
        public static Texture2D redshoot;

        public static Texture2D Invader_Black;
        public static Texture2D Invader_Blue;
        public static Texture2D Invader_Blue_02;
        public static Texture2D Invader_explode;

        //Items
        public static Texture2D banana;
        public static Texture2D blackout;
        public static Texture2D faster;
        public static Texture2D freeze;
        public static Texture2D invasion;
        public static Texture2D noitem;
        public static Texture2D shield;
        public static Texture2D speedup;
        public static Texture2D triplespeed;

        public static Texture2D Lives;

        //In Basic.LoadContent aufrufen
        public static void Setup(ContentManager content)
        {
            blueship = content.Load<Texture2D>(@"Textures\ship\ship");
            redship = content.Load<Texture2D>(@"Textures\ship\redShip");
            redshoot = content.Load<Texture2D>(@"Textures\ship\shoot");
            blueshoot = content.Load<Texture2D>(@"Textures\ship\blueshoot");

            Invader_Blue = content.Load<Texture2D>(@"Textures\Invaders\invader01_1");
            Invader_Blue_02 = content.Load<Texture2D>(@"Textures\Invaders\invader01_2");
            Invader_explode = content.Load<Texture2D>(@"Textures\Invaders\explosion");

            Lives = content.Load<Texture2D>(@"Textures\Lives");

            //Items

            banana = content.Load<Texture2D>(@"Textures\Items\banana");
            blackout = content.Load<Texture2D>(@"Textures\Items\blackout");
            faster = content.Load<Texture2D>(@"Textures\Items\faster");
            freeze = content.Load<Texture2D>(@"Textures\Items\freeze");
            invasion = content.Load<Texture2D>(@"Textures\Items\invasion");
            noitem = content.Load<Texture2D>(@"Textures\Items\noitem");
            shield = content.Load<Texture2D>(@"Textures\Items\shield");
            speedup = content.Load<Texture2D>(@"Textures\Items\speedup");
            triplespeed = content.Load<Texture2D>(@"Textures\Items\triplespeed");
        }
    }
}
