using System;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace OpenTKTut.Shapes
{
    class MeshElement
    {
        private static double _converter_factor = Math.PI / 180;

        public MeshElement(Vector3[] vertices, Vector2[] texcoord)
        {
            Vertices = vertices;
            Texcoord = texcoord;
        }

        public Vector3[] Vertices { get; private set; }
        public Vector2[] Texcoord { get; private set; }

        public static MeshElement[] Sphere(double radius)
        {
            List<MeshElement> res = new List<MeshElement>();

            int dlta = 10;
            float s = 36;
            float t = 0;
            float s_factor = (dlta / 10) / 36f;
            float t_factor = (dlta / 10) / 18f;

            for (int phi = 0; phi <= 180 - dlta; phi += dlta)
            {
                for (int theta = 0; theta <= 360 - dlta; theta += dlta)
                {
                    // Vertex layout:
                    // 1 ===== 4
                    // |       |
                    // 2 ===== 3

                    Vector3 _1 = GetCartezianOf(radius, phi, theta);
                    Vector2 _1tex = new Vector2(s * s_factor, t * t_factor);

                    Vector3 _2 = GetCartezianOf(radius, phi + dlta, theta);
                    Vector2 _2tex = new Vector2(s * s_factor, (t + 1) * t_factor);

                    Vector3 _3 = GetCartezianOf(radius, phi + dlta, theta + dlta);
                    Vector2 _3tex = new Vector2((s - 1) * s_factor, (t + 1) * t_factor);

                    Vector3 _4 = GetCartezianOf(radius, phi, theta + dlta);
                    Vector2 _4tex = new Vector2((s - 1) * s_factor, t * t_factor);

                    Vector3[] vertices = { _1, _2, _3, _4 };
                    Vector2[] texcoord = { _1tex, _2tex, _3tex, _4tex };
                    res.Add(new MeshElement(vertices, texcoord));
                    s--;
                }
                t++;
                s = 36; // Reset s for each phi loop
            }

            return res.ToArray();
        }

        private static Vector3 GetNormal(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 a = p2 - p1;
            Vector3 b = p3 - p1;
            Vector3 normal = Vector3.Cross(a, b);
            normal.Normalize();
            return normal;
        }

        private static Vector3 GetCartezianOf(double radius, int theta, int phi)
        {
            // Convert degrees to radians
            double th = theta * _converter_factor;  // theta = polar angle (0 at north pole)
            double ph = phi * _converter_factor;    // phi = azimuthal angle

            // Spherical to Cartesian coordinates
            float x = (float)(radius * Math.Sin(th) * Math.Cos(ph));
            float z = (float)(radius * Math.Sin(th) * Math.Sin(ph));
            float y = (float)(radius * Math.Cos(th));

            return new Vector3(x, y, z);
        }
    }
}