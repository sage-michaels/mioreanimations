using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Visi.Engine
{
    public class Flock
    {
        public List<Bird> Birds;
        public int Toggle;

        public Flock(Texture2D birdSkin ,Vector2 bounds, int flockSize, float flockRadius, int toggle)
        {
            Toggle = toggle;

            Birds = new List<Bird>();
            int i = 0;
            Random rand1 = new Random();
            Random rand2 = new Random();
            while (i<flockSize){
                Vector2 position = new Vector2(rand1.Next((int)bounds.X), rand2.Next((int)bounds.Y));
                Bird bird = new Bird(birdSkin, position, Vector2.One*.1f, Toggle, flockRadius, bounds, i);
                Birds.Add(bird);
                //Console.WriteLine("Bird {0} created at {1}", bird.ID, bird.Position);
                i += 1;
            }

        }


        public List<Bird> InFront(Bird bird){
            List<Bird> neighbors = new List<Bird>();
            //Console.Write("Visable birds: {");
            foreach (Bird otherBird in Birds){
                if (otherBird!=bird){
                    float distance = Vector2.Distance(otherBird.Position,bird.Position);
                    if (distance < bird.Radius)
                    {
                        Vector2 dirToOther = Vector2.Normalize(otherBird.Position - bird.Position);
                        if (bird.InView(dirToOther,45f)){
                            neighbors.Add(otherBird);
                            if (bird.ID == 0){
                                otherBird.ObjectColor = Color.HotPink;
                            }
                            //Console.Write(" {0},", otherBird.ID);

                        }else if (bird.ID == 0){
                            otherBird.ObjectColor = Color.White;
                        }
                        
                    }else if (bird.ID == 0){
                        otherBird.ObjectColor = Color.White;
                    }
                }
            }
            //Console.Write("}\n");
            return neighbors;
        }

        public void Update(int toggle){
            if (toggle == Toggle)
            {
                foreach (Bird bird in Birds)
                {
                    bird.Neighbors = InFront(bird);
                    bird.Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch){
            foreach (Bird bird in Birds){
                bird.Draw(spriteBatch);
            }
        }
    }
}
