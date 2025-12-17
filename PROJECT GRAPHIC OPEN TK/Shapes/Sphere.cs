using System;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace OpenTKTut.Shapes
{
    class Sphere : OGLShape
    {
        public Sphere(Vector3 center, double radius, bool AutoRotate, bool orbiting, bool moon,
                     float rotatingSpeed, float RotatingRadius, float orbitingSpeed,
                     float moonorbit, float moonSpeed, int textype)
        {
            _Center = center;
            Radius = radius;
            MeshPolygons = MeshElement.Sphere(Radius);
            EnableAutoRotate = AutoRotate;
            Orbiting = orbiting;
            Moon = moon;
            RotatingSpeed = rotatingSpeed;
            Rotatingradius = RotatingRadius;
            OrbitingSpeed = orbitingSpeed;
            MoonOrbit = moonorbit;
            MoonSpeed = moonSpeed;
            Textype = textype;
        }

        public double Radius { get; set; }
        public int Textype;

        protected override void ShapeDrawing()
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.BindTexture(TextureTarget.Texture2D, Textype);

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0f, 1.0f, 1.0f);

            for (int i = 0; i < MeshPolygons.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    GL.Normal3(MeshPolygons[i].Vertices[j]);
                    GL.TexCoord2(MeshPolygons[i].Texcoord[j]);
                    GL.Vertex3(MeshPolygons[i].Vertices[j]);
                }
            }

            GL.End();
        }
    }
}