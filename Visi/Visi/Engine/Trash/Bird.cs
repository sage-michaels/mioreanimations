using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Visi.Engine
{
    public class Bird : GridObject
    {

        public Vector2 Velocity { get; set; }
        public float Radius;
        private Vector2 Alignment;
        private Vector2 Seporation;
        private Vector2 Cohesion;
        private float Acceleration;
        private float Speed;
        public List<Bird> Neighbors { get; set; }

        public Bird(Texture2D texture, Vector2 position, Vector2 scale, int toggle, float radius) : base(texture, position, scale, toggle)
        {
            Velocity = new Vector2(1, 0);
            Radius = radius;
            //Console.WriteLine(" initial Position: {0}---- initial Velocity: {1}-- initial Angle: {2}", Position, Velocity, Angle);

            Alignment = Vector2.Zero;
            Seporation = Vector2.Zero;
            Cohesion = Vector2.Zero;
            Acceleration = .2f;
            Neighbors = new List<Bird>();

        }

        public Bird(Texture2D texture, Vector2 position, Vector2 scale, int toggle) : base(texture, position, scale, toggle)
        {
        }

        public void SetNeighbors(List<Bird> neighbors){
            Neighbors = neighbors;
        }

        public bool Equal(Bird bird){
            bool t1 = (Position == bird.Position);
            bool t2 = (Velocity == bird.Velocity);
            return (t1 && t2);
        }



        public void SetVelocity(Vector2 dir){
            if (dir != Vector2.Zero)
            {
                Velocity = Velocity*Speed + (Vector2.Normalize(dir)*Acceleration*.01f);
            }
        }

        public void SetSpeed(float accel){
            Speed += accel;
        }

        public Vector2 IsHeading()
        {

            return Vector2.Normalize(Velocity);
        }

        public Vector2 Average(Vector2 v1, Vector2 v2){
            Vector2 av = v1 + v2;
            av = Vector2.Divide(av, 2f);
            return av;
        }

        public void Flocking(){

            Vector2 cohesion = Vector2.Zero;
            Vector2 seporation = Vector2.Zero;
            Vector2 alignment = IsHeading();


            if (Neighbors.Count != 0){
                foreach (Bird bird in Neighbors)
                {
                    float distance = Math.Abs(Vector2.Distance(bird.Position, Position));

                    Console.WriteLine("Distance : {0}", distance);

                    //Cohesion
                    Vector2 dir = Vector2.Normalize(bird.Position - Position);
                    cohesion += dir*(distance/Radius);

                    //Seporation
                    dir = Vector2.Normalize(bird.Position- Position);
                    double repel = Math.Abs((double)50 / (Math.Pow(distance,2) + .001d));
                    dir = dir* (float)repel;
                    seporation += dir;

                    //Alignment
                    alignment += bird.IsHeading()*(float)Math.Sqrt(distance);
                }
                alignment = alignment/Neighbors.Count;
                cohesion = cohesion / (Neighbors.Count+1);
                Acceleration = (Vector2.Distance(alignment, Vector2.Zero) + Vector2.Distance(seporation, Vector2.Zero) + Vector2.Distance(cohesion, Vector2.Zero));

            }
            Alignment = Vector2.Normalize(alignment);
            Cohesion = Vector2.Normalize(cohesion);
            Seporation = Vector2.Normalize(seporation);

            SetVelocity(Alignment + Cohesion - Seporation);

            Console.WriteLine("Cohesion:{0} \n Alignment: {1} \n Seporation: {2}", cohesion, alignment, seporation);
            Console.WriteLine("Velocity: {0} \n IsHeading: {1}", Velocity, IsHeading());



        }

        public float AngleBetween(Vector2 v1, Vector2 v2)
        {
            float dotProduct = Vector2.Dot(v1, v2);
            float norms = v1.Length() * v2.Length();
            float angleBetween = MathHelper.ToDegrees((float)Math.Acos(dotProduct)/norms);
            Console.WriteLine("Angle Between: {0}", angleBetween);
            return angleBetween;
        }


        public void Update(Sky sky){

            Vector2 wasHeading = new Vector2(1,0);
            Flocking();
            //Angle = AngleBetween(IsHeading(),wasHeading);
            Position = sky.Move(Position+Velocity);
            //Console.WriteLine("Angle:{0}",Angle);



        }

    }
}
