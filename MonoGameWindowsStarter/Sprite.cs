using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter
{
    //Using Bean's example for sprite struct
    public struct Sprite
    {

        private Rectangle rect;

        private Texture2D texture;

        public int Width => rect.Width;

        public int Height => rect.Height;

        public Sprite(Rectangle sourceRect, Texture2D texture)
        {
            this.texture = texture;
            this.rect = sourceRect;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture, destinationRectangle, rect, color, rotation, origin, effects, layerDepth);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            spriteBatch.Draw(texture, destinationRectangle, rect, color);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(texture, position, rect, color);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture, position, rect, color, rotation, origin, scale, effects, layerDepth);
        }
    }

}
