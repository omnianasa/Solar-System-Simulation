using OpenTK.Mathematics;  // Changed from using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace OpenTKTut.Shapes
{
    class CircleDraw : OGLShape
    {
        public CircleDraw(Vector3 center, double radius, float rotatingSpeed,
                         float RotatingRadius, float orbitingSpeed,
                         float moonorbit, float moonSpeed)
        {
            _Center = center;
            Radius = radius;
            RotatingSpeed = rotatingSpeed;
            Rotatingradius = RotatingRadius;
            OrbitingSpeed = orbitingSpeed;
            MoonOrbit = moonorbit;
            MoonSpeed = moonSpeed;

            // Calculate positions 
            for (int phi = 0; phi < 361; phi++)
            {
                position[phi].X = (float)(Radius * Math.Cos(phi * Math.PI / 180));
                position[phi].Z = (float)(Radius * Math.Sin(phi * Math.PI / 180));
                position[phi].Y = 0;
            }
        }

        public double Radius { get; set; }
        public Vector3[] position = new Vector3[361];

        protected override void ShapeDrawing()
        {
            GL.LineWidth(2);
            GL.Color3(0.329412f, 0.3294128f, 0.329412f);
            GL.Begin(PrimitiveType.LineStrip);

            for (int phi = 0; phi < 361; phi++)
            {
                GL.Vertex3(position[phi]);
            }

            GL.End();
        }
    }
}