using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Visi.Engine
{
    public class GridObject
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Scale;
        public Vector2 Scale1;
        public float Angle;
        public Color ObjectColor;
        public float RotationSpeed;
        public int Toggle;


        public GridObject(Texture2D texture, Vector2 position, Vector2 scale,int toggle)
        {
            Texture = texture;
            Scale = scale*.5f;
            Scale1 = scale;
            Position = position;
            Angle = 0.0f;
            RotationSpeed = .0f;
            ObjectColor = Color.White;
            Toggle = toggle;
        }

        public void Update(int toggled)
        {
            Angle += RotationSpeed;
            if (toggled == Toggle)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.L))
                {
                RotationSpeed += 0.0005f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.J)){
                    RotationSpeed -= 0.0005f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.K)){
                    if (Scale1.X >= Scale.X)
                    {
                        Scale1 -= new Vector2(.01f, .01f);
                    }
                }if (Keyboard.GetState().IsKeyDown(Keys.I))
                {
                    Scale1 += new Vector2(.01f, .01f);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                Random rnd = new Random();
                ObjectColor = new Color(255, rnd.Next(0,255), rnd.Next(0, 255), rnd.Next(0, 255));
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Texture, 
                             position: Position, 
                             color: ObjectColor, 
                             rotation: Angle, 
                             origin: new Vector2(Texture.Width / 2, Texture.Height / 2), 
                             scale: Scale1);
        }
    }
}
