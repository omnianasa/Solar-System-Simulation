using System;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace OpenTKTut.Shapes
{
    class OGLShape
    {
        public bool EnableAutoRotate { get; set; }
        public bool Orbiting, Moon;
        public float RotatingSpeed;
        public Vector3 _Center;
        public float Rotatingradius;
        public float OrbitingSpeed;
        public float MoonOrbit;
        public float MoonSpeed;

        public MeshElement[] MeshPolygons { get; set; }

        public float _rotateAngle;
        public float _orbitAngle;
        public float _moonAngle;
        public float x1, z1, x2, y2, z2;

        public virtual void Draw()
        {
            GL.PushMatrix();
            GL.Translate(_Center.X, _Center.Y, -_Center.Z);

            if (EnableAutoRotate)
            {
                GL.Rotate(_rotateAngle, Vector3.UnitY);
                _rotateAngle = _rotateAngle < 360 ? _rotateAngle + RotatingSpeed : _rotateAngle - 360;

                if (Orbiting)
                {
                    _Center.X -= x1;
                    _Center.Z -= z1;
                    x1 = Rotatingradius * MathF.Cos(_orbitAngle * MathF.PI / 180);
                    z1 = Rotatingradius * MathF.Sin(_orbitAngle * MathF.PI / 180);
                    _Center.X += x1;
                    _Center.Z += z1;
                    _orbitAngle = _orbitAngle < 360 ? _orbitAngle + OrbitingSpeed : _orbitAngle - 360;

                    if (Moon)
                    {
                        _Center.X -= x2;
                        _Center.Y -= y2;
                        _Center.Z -= z2;
                        x2 = MoonOrbit * MathF.Cos(_moonAngle * MathF.PI / 180);
                        y2 = MoonOrbit * MathF.Cos(_moonAngle * MathF.PI / 180);
                        z2 = MoonOrbit * MathF.Sin(_moonAngle * MathF.PI / 180);
                        _Center.X += x2;
                        _Center.Y += y2;
                        _Center.Z += z2;
                        _moonAngle = _moonAngle < 360 ? _moonAngle + MoonSpeed : _moonAngle - 360;
                    }
                }
            }

            ShapeDrawing();
            GL.PopMatrix();
        }

        protected virtual void ShapeDrawing()
        {
        }
    }
}