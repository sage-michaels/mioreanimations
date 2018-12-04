using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Visi.Engine
{
    public class Sky : World
    {
        public Sky(int width, int height) : base(width, height)
        {
        }

        //set topology of world to be wrapped around
        public Vector2 Move(Vector2 position){
            if (position.X>Width){
                position.X = position.X - Width;
                Console.WriteLine("Wrapping bird around world");
            }
            if (position.Y>Height){
                position.Y = position.Y - Height;
                Console.WriteLine("Wrapping bird around world");
            }
            if (position.X < 0){
                position.X = position.X + Width;
            }
            if (position.Y < 0){
                position.Y = position.Y + Height;
            }
            Console.WriteLine("Bird position: {0}", position);
            return position;

        }



    }
}
