using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MohawkGame2D
{
    public class Eyeball
    {
        public float radius = 50.0f;
        public Color pupilColor = Color.Black;
        public Vector2 position = new Vector2(0, 0);

        public bool closed = false;

        public void Setup()
        {
            closed = false;
            radius = Random.Float(20.0f, 50.0f);
            position = Random.Vector2(50, 550, 50, 550);
            pupilColor = Random.Color();
        }

        public void Update()
        {
            ProcessInputs();
            DrawEyeball();
        }

        public bool IsMouseInside()
        {
            Vector2 eyeballPosToMousePos = Input.GetMousePosition() - position;
            bool insideCircle = eyeballPosToMousePos.Length() < radius;
            return insideCircle;
        }

        void ProcessInputs()
        {
            if (IsMouseInside() && Input.IsMouseButtonPressed(MouseInput.Left))
            {
                closed = true;
            }
        }

        void DrawEyeball()
        {
            Draw.LineSize = 1;
            Draw.LineColor = Color.Black;
            Draw.FillColor = Color.White;

            Draw.Circle(position, radius);

            Vector2 mouseDirection = Input.GetMousePosition() - position;

            float pupilRadius = radius / 2.0f;

            Vector2 pupilPosition = new Vector2(0, 0);
            if (mouseDirection.Length() > radius - pupilRadius)
            {
                // outside, the pupil only follows to a certain distance
                pupilPosition = (radius / 2.0f) * Vector2.Normalize(mouseDirection);
                pupilPosition += position;
            }
            else
            {
                // when the mouse is inside the eyeball, just make the pupil follow the mouse
                pupilPosition = Input.GetMousePosition();
            }
            
            // eye closing logic
            if (closed)
            {
                Draw.FillColor = Color.LightGray;
                Draw.Circle(position, radius);

                Draw.Line(position - new Vector2(radius, 0), position + new Vector2(radius, 0));
            }
            else
            {
                // draw iris
                Draw.FillColor = pupilColor;
                Draw.Circle(pupilPosition, pupilRadius);

                // draw pupil
                Draw.FillColor = Color.Black;
                Draw.Circle(pupilPosition, 0.60f * pupilRadius);
            }

        }
    }
}
