using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Space_Invaders
{
    //Soundsammlung
    //Klasse um für alle im Spiel enthaltenen Sounds
    public class Sounds
    {
        public static SoundEffect invader_killed;
        public static SoundEffect photon_shot;
        public static SoundEffect move1;
        public static SoundEffect move2;
        public static SoundEffect move3;
        public static SoundEffect move4;

        public static void Setup(ContentManager content)
        {
            photon_shot = content.Load<SoundEffect>(@"Sound\shoot");
            //invader_shoot = content.Load<SoundEffect>(@"Sounds\shoot");
            invader_killed = content.Load<SoundEffect>(@"Sound\invaderkilled");

            move1 = content.Load<SoundEffect>(@"Sound\Move\move1");
            move2 = content.Load<SoundEffect>(@"Sound\Move\move2");
            move3 = content.Load<SoundEffect>(@"Sound\Move\move3");
            move4 = content.Load<SoundEffect>(@"Sound\Move\move4");
        }
    }
}
