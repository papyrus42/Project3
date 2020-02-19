using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteSheet sheet;
        Player player;
        List<Platform> platforms;
        AxisList gameWorld;
        List<GameObject> gameObjects;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            platforms = new List<Platform>();
            gameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var t = Content.Load<Texture2D>("Sprite Sheet");
            sheet = new SpriteSheet(t, 31, 35, 3, 2);

            var playerFrames = from index in Enumerable.Range(0, 5) select sheet[index];
            player = new Player(playerFrames, this);
            player.LoadContent(Content, "");
            

            platforms.Add(new Platform(new BoundaryRectangle(80, 350, 105, 21), sheet[7]));
            platforms.Add(new Platform(new BoundaryRectangle(180,290,105,21), sheet[6]));
            platforms.Add(new Platform(new BoundaryRectangle(230,220,105,21), sheet[7]));
            platforms.Add(new Platform(new BoundaryRectangle(390, 300, 105, 21), sheet[6]));
            platforms.Add(new Platform(new BoundaryRectangle(490, 230, 105, 21), sheet[7]));
            platforms.Add(new Platform(new BoundaryRectangle(600, 160, 105, 21), sheet[6]));

            gameWorld = new AxisList();
            foreach (Platform platform in platforms)
            {
                gameWorld.AddGameObject(platform);
                gameObjects.Add(platform);
            }
            gameObjects.Add(player);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach(GameObject gm in gameObjects)
            {
                gm.Update(gameTime);
            }


            var platformQuery = gameWorld.QueryRange(player.bounds.X, player.bounds.X + player.bounds.Width);
            player.CheckForPlatformCollision(platformQuery);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            gameObjects.ForEach(obj =>
            {
                obj.Draw(spriteBatch);
            });


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
