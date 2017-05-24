using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders
{
    class NormalTexture : Entity
    {
        private Texture2D texture;

        public NormalTexture(Rectangle _position, Texture2D _texture)
            : base(_position, 0)
        {
            this.texture = _texture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    
    }
}
