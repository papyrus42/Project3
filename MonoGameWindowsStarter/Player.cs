﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

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

    enum VerticalMovementState
    {
        OnGround,
        Jumping,
        Falling
    }

    public class Player: GameObject
    {
        const int FRAME = 300;

        const int JUMP_HEIGHT = 400;


        Sprite[] frames;

        int currentFrame = 0;

        PlayerAnimState animationState = PlayerAnimState.Idle;

        VerticalMovementState verticalState = VerticalMovementState.OnGround;

        int playerSpeed = 3;

        bool jumping = false;

        bool falling = false;

        TimeSpan jumpTime;

        TimeSpan animateTime;

        SpriteEffects spriteEffects = SpriteEffects.None;

        Color color = Color.White;

        //might change depending on how big the frames are
        Vector2 origin = new Vector2(19, 10);

        public Vector2 Position = new Vector2(200,400);

        public int groundLevel;

        public BoundaryRectangle bounds;

        Game1 game;

        SoundEffect jumpSFX;

        bool soundHasPlayed = false;

        public bool isOnPlatform = false;

        public Player(IEnumerable<Sprite> frames, Game1 g)
        {
            this.frames = frames.ToArray();
            game = g;
            bounds = new BoundaryRectangle(Position-1.8f * origin, this.frames[0].Width, this.frames[0].Height);
        }

        //public void Initialize(float width, float height, float x, float y)
        //{
        //    bounds.Width = width;
        //    bounds.Height = height;
        //    bounds.X = x;
        //    bounds.Y = y;
        //    groundLevel = 600;
        //    soundHasPlayed = false;
        //    isOnPlatform = false;
        //}

        public void LoadContent(ContentManager cm, string name)
        {
           jumpSFX = cm.Load<SoundEffect>("JumpSound");
        }

        public void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();

            //walking left and right
            if (keyboard.IsKeyDown(Keys.Left))
            {
                if (jumping || falling) animationState = PlayerAnimState.JumpingLeft;
                else animationState = PlayerAnimState.WalkingLeft;
                Position.X -= playerSpeed;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                if (jumping || falling) animationState = PlayerAnimState.JumpingRight;
                else animationState = PlayerAnimState.WalkingRight;
                Position.X += playerSpeed;
            }
            else
            {
                animationState = PlayerAnimState.Idle;
            }

            //jumping and falling
            if (jumping)
            {
                jumpTime += gameTime.ElapsedGameTime;
                // Simple jumping with platformer physics
                Position.Y -= (250 / (float)jumpTime.TotalMilliseconds);
                if (jumpTime.TotalMilliseconds >= JUMP_HEIGHT)
                {
                    jumping = false;
                    falling = true;
                    verticalState = VerticalMovementState.Falling;
                }
            }
            if (falling)
            {
                Position.Y += playerSpeed;
                // TODO: This needs to be replaced with collision logic
                if (Position.Y > 400)
                {
                    Position.Y = 400;
                    falling = false;
                    verticalState = VerticalMovementState.OnGround;
                }
            }

            if (!jumping && !falling && keyboard.IsKeyDown(Keys.Up))
            {
                jumping = true;
                jumpTime = new TimeSpan(0);
                verticalState = VerticalMovementState.Jumping;
                if (!soundHasPlayed)
                {
                    jumpSFX.Play();
                    soundHasPlayed = false;
                }
            }

        


            bounds.X = Position.X;
            bounds.Y = Position.Y;

            //frames may change depending on sprite sheet
            switch (animationState)
            {
                case PlayerAnimState.Idle:
                    
                        currentFrame = 0;
                        animateTime = new TimeSpan(0);
                    
                    break;
                case PlayerAnimState.JumpingLeft:
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    currentFrame = 3;
                    break;
                case PlayerAnimState.JumpingRight:
                    spriteEffects = SpriteEffects.None;
                    currentFrame = 3;
                    break;
                case PlayerAnimState.WalkingLeft:
                    animateTime += gameTime.ElapsedGameTime;
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    
                    if (animateTime.TotalMilliseconds > FRAME * 2)
                    {
                        animateTime = new TimeSpan(0);
                    }
                    currentFrame = (int)animateTime.TotalMilliseconds / FRAME + 2;
                    break;
                case PlayerAnimState.WalkingRight:
                    animateTime += gameTime.ElapsedGameTime;
                    spriteEffects = SpriteEffects.None;
                    
                    if (animateTime.TotalMilliseconds > FRAME * 2)
                    {
                        animateTime = new TimeSpan(0);
                    }
                    currentFrame = (int)animateTime.TotalMilliseconds / FRAME + 2;
                    break;
            }

                    //staying in the screen
                    if (bounds.X < 0)
                    {
                        bounds.X = 0;
                        Position.X = 0;
                    }
                    if (bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width)
                    {
                        bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
                        Position.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
            }
                    if (bounds.Y < 0)
                    {
                        bounds.Y = 0;
                        Position.Y = 0;
                    }
                    //if (bounds.Y > groundLevel)
                    //{
                    //    bounds.Y = groundLevel;
                    //    Position.Y = groundLevel;
                    //}
            
        }

        public void CheckForPlatformCollision(IEnumerable<IBoundable> platforms)
        {

            if (verticalState != VerticalMovementState.Jumping)
            {
                if (verticalState != VerticalMovementState.OnGround)
                {
                    falling = true;
                }
                verticalState = VerticalMovementState.Falling;
               
                foreach (Platform platform in platforms)
                {
                    if (bounds.CollidesWith(platform.Bounds))
                    {
                        Position.Y = platform.Bounds.Y - 1;
                        verticalState = VerticalMovementState.OnGround;
                        jumping = false;
                        falling = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            frames[currentFrame].Draw(spriteBatch, Position, color, 0, origin, (float)1.3, spriteEffects, 1);
        }


    }
}
