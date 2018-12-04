using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Visi.Engine;

namespace Visi.MacOS
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D grid;
        Texture2D ovals;
        Texture2D birdSkin;
        GridObject gridObject;
        GridObject ovalsObject;

        Flock BirdLand;

        int Toggle;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {

            base.Initialize();
            Toggle = 3;

        }

       
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grid = Content.Load<Texture2D>("grid");
            ovals = Content.Load<Texture2D>("ovals");
            birdSkin = Content.Load<Texture2D>("triangle");


            int x = GraphicsDevice.Viewport.Bounds.Width;
            int y = GraphicsDevice.Viewport.Bounds.Height;
            Vector2 size = new Vector2(.75f, .75f);
            Vector2 position = new Vector2((float)x/2, (float)y/2); // Center grid in window




            gridObject = new GridObject(grid, position, size,1);
            ovalsObject = new GridObject(ovals, position, size,2);
            BirdLand = new Flock(birdSkin, new Vector2(x,y), 20, 500, 3);


        }


        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.ToggleFullScreen();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1)){
                Toggle = 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                Toggle = 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3)){
                Toggle = 3;
            }



            //gridObject.Update(Toggle);
            //ovalsObject.Update(Toggle);
            BirdLand.Update(Toggle);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            //gridObject.Draw(spriteBatch);
            //ovalsObject.Draw(spriteBatch);
            BirdLand.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
