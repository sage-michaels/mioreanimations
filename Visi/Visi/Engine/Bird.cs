using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Visi.Engine
{
    public class Bird:GridObject
    {

        public Vector2 Direction;         //Unit vector indicating direction bird is heading

        private Vector2 newDir;


        private float Speed;
        private float Acceleration;  
        public List<Bird> Neighbors;      //neighbors visable to bird
        public float Radius;             //attraction radius
        private static float Pspace = 100; //personal space radius

        public int ID;

        private static Vector2 Bounds;

        public Bird(Texture2D texture, Vector2 position, Vector2 scale, int toggle, float radius, Vector2 bounds, int id) : base(texture, position, scale, toggle)
        {

            Random rnd = new Random();
            Radius = radius;
            Direction = Vector2.Normalize(new Vector2(rnd.Next(10), rnd.Next(10)));
            Bounds = bounds;
            SetPosition(position);
            newDir = Vector2.Zero;
            Speed = 2f;

            Acceleration = 0f;
            Neighbors = new List<Bird>();
            ID = id;
            if (ID == 0){
                ObjectColor = Color.Aqua;
            }

        }

        public bool Eqauls(Bird bird){
            return (bird.ID == ID);
        }

        public bool InView(Vector2 objectDir, float viewInDegrees){
            float dot = Vector2.Dot(objectDir, Direction);
            float magnitudes = (float)(Math.Sqrt(Vector2.Dot(objectDir, objectDir)) * Math.Sqrt(Vector2.Dot(Direction, Direction)));
            float angleBetween = (float)MathHelper.ToDegrees((float)Math.Acos(dot / magnitudes));
            return (Math.Abs(angleBetween) <= viewInDegrees);
        }


        public void Flocking(){


            Vector2 seporation = Vector2.Zero;
            Vector2 cohesion = Vector2.Zero;
            Vector2 alignment = Direction;

            if (Neighbors.Count > 0)
            {

                //Console.WriteLine("{0} neighbors", Neighbors.Count);
                foreach (Bird bird in Neighbors)
                {

                    Vector2 toOther = bird.Position - Position;
                    float distance = Vector2.Distance(bird.Position, Position);

                    //Seporate
                    float coef = Pspace / (float)Math.Pow((double)distance, 2);
                    seporation += Vector2.Normalize(toOther) * coef;

                    //Align
                    alignment += bird.Direction;
                    //Console.WriteLine("Alignment: {0} \n bird {1} heading: {2} \n self heading {3} ",alignment,bird.ID,bird.Direction,Direction);

                    //Cohere
                    coef = distance / Radius;
                    cohesion += (bird.Position - Position)*coef;
                    //Console.Write("distance: {0} ", distance);
                    if (distance < Radius)
                    {
                        //Console.Write("Seporation: {0} \n", seporation);
                    }else{
                        //Console.WriteLine("");
                    }

                }
                alignment = alignment / Neighbors.Count;
                cohesion = cohesion / Neighbors.Count;
                Vector2 earlyDir = (cohesion*.3f + alignment*.5f - seporation*.2f);
                //Console.WriteLine("Alignment: {0}  Cohesion: {1}", alignment, cohesion);
                newDir = Vector2.Normalize(earlyDir);
                float magnitude = Length(earlyDir);
                float comp = Math.Max(Math.Max(Length(alignment), Length(cohesion)), Length(seporation));
                if ((int)comp == (int)Length(seporation)){
                    Acceleration = comp / magnitude;
                    //ObjectColor = Color.Red;
                }else{
                    Acceleration = comp / magnitude;
                    //ObjectColor = Color.White;
                }
            }
        }


        public float Length(Vector2 vec){
            return Math.Abs(Vector2.Distance(Vector2.Zero, vec));
        }

        private void SetPosition(Vector2 position){
            if (position.X < 0){
                position.X += Bounds.X;
            }
            if (position.X> Bounds.X){
                position.X -= Bounds.X;
            }
            if (position.Y<0){
                position.Y += Bounds.Y;
            }
            if (position.Y>Bounds.Y){
                position.Y -= Bounds.Y;
            }
            Position = position;
        }


        public void SetAngle(){
            Vector2 tFacing = new Vector2(1,0);
            float dot = Vector2.Dot(tFacing, Direction);
            float norms = (float)(Math.Sqrt(Vector2.Dot(tFacing, tFacing)) * Math.Sqrt(Vector2.Dot(Direction, Direction)));
            float almostAngle = MathHelper.WrapAngle((float)Math.Acos(dot / norms));
            if (Direction.Y < 0){
                almostAngle = almostAngle * -1;
            }
            Angle = almostAngle;
        }

        public void SetSpeed(){
            if (Math.Abs(Speed + Acceleration * .05f)<5f){

                Speed *= .8f;

                Speed += Acceleration * .05f;
            }
        }

        public void Update(){

            Flocking();
            Vector2 velocity = (Direction) * Speed + (newDir * Acceleration);
            SetSpeed();
            if (velocity != Vector2.Zero){
                Direction = Vector2.Normalize(velocity);

            }
            SetAngle();
            int check = (int) Math.Acos(Vector2.Dot(Direction, new Vector2(1, 0)));
            SetPosition(Position + velocity);
            //Console.WriteLine("Bird {0}: \n   Direction:{1}\n    Velocity:{2}\n    Speed:{3}\n    Acceleration:{4}\n   Angle:{5}\n    Position: {6}", ID, Direction, velocity, Speed, Acceleration, MathHelper.ToDegrees(Angle), Position);
        }
    }
}

