using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter
{
    public class Platform : IBoundable, GameObject
    {
        BoundaryRectangle bounds;

        Sprite sprite;

        int tileCount;

        public BoundaryRectangle Bounds => bounds;

        public Platform(BoundaryRectangle br, Sprite s)
        {
            bounds = br;
            sprite = s;
            tileCount = (int)bounds.Width / sprite.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tileCount; i++)
            {
                sprite.Draw(spriteBatch, new Vector2(bounds.X + i * sprite.Width, bounds.Y), Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
