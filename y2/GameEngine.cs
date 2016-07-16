using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using ECS;
using y2.Systems;
using y2.Components;
using y2.Data;

namespace y2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameEngine : Game
    {
        TimeSpan updateInterval = TimeSpan.FromSeconds(new Click(1));
        TimeSpan nextUpdate = TimeSpan.Zero;

        GraphicsDeviceManager graphics;
        public GraphicsDeviceManager Graphics => graphics;

        EntityWorld world = new EntityWorld();
        public EntityWorld World => world;

        float progress;
        public float UpdateProgress => progress;

        Click time = new Click();
        public Click Time => time;

        float timeScale = 1f;
        public float TimeScale
        {
            get { return timeScale; }
            set { updateInterval = TimeSpan.FromSeconds(new Click(1) * (timeScale = value)); }
        }

        public GameEngine()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferMultiSampling = true;
            //graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            world.AddSystem(new VoxelSystem());
            world.AddSystem(new InputSystem(this));
            world.AddSystem(new MovementSystem(this));
            world.AddSystem(new CollisionSystem());
            world.AddSystem(new AnimationSystem(this));
            world.AddSystem(new RenderSystem(this));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            world.Load();

            Voxel.model = new ModelComponent(Content.Load<Model>("voxel"));

            world.GetSystem<InputSystem>().Player = world.CreateEntity(
                new ModelComponent(Content.Load<Model>("player")),
                new TransformComponent(),
                new MovementComponent(),
                new CollisionComponent(),
                new AnimationComponent(),
                new GridComponent()
                );

            world.GetSystem<RenderSystem>().Camera = world.CreateEntity(
                new TransformComponent(-5f, 2f, -1f),
                new CameraComponent()
                );

            var voxel = world.GetSystem<VoxelSystem>();
            VoxelType type = new VoxelType();
            foreach (var pos in new HashSet<Int3>
            {
                new Int3(0, -1, 0),
                new Int3(1, -1, 0),
                new Int3(2, -1, 0),
                new Int3(2, -1, 1),
                new Int3(3, -1, 0),
                new Int3(1, -2, -1),
                new Int3(4, 0, 0)
            })
            {
                voxel[pos] = new Voxel(world, type);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            world.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            progress = (float)(1 - (nextUpdate - gameTime.TotalGameTime).TotalSeconds / updateInterval.TotalSeconds);

            world.Update();

            if (gameTime.TotalGameTime > nextUpdate)
            {
                nextUpdate = gameTime.TotalGameTime + updateInterval;
                ++time;

                world.Interval();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            world.Render();

            base.Draw(gameTime);
        }
    }
}
