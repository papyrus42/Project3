using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameWindowsStarter
{
    enum PlayerAnimState
    {
        Idle,
        JumpingLeft,
        JumpingRight,
        WalkingLeft,
        WalkingRight,
        FallingLeft,
        FallingRight
    }

    public class Player
    {
        const int FRAME = 300;

        const int JUMP_HEIGHT = 400;

        Sprite[] frames;

        int currentFrame = 0;

        PlayerAnimState animationState = PlayerAnimState.Idle;

        int playerSpeed = 3;

        bool jumping = false;

        bool falling = false;

        TimeSpan jumpTime;

        TimeSpan animateTime;

        SpriteEffects spriteEffects = SpriteEffects.None;

        Color color = Color.White;

        //might change depending on how big the frames are
        Vector2 origin = new Vector2(10, 21);

        public Vector2 Position;

        public int groundLevel;

        public BoundaryRectangle bounds;

        Game1 game;

        SoundEffect jumpSFX;

        bool soundHasPlayed;

        public bool isOnPlatform;

        public Player(IEnumerable<Sprite> frames)
        {
            this.frames = frames.ToArray();
            
        }
    }
}
