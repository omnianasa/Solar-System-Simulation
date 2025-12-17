using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenTKTut
{
    partial class Viewport
    {
        // Use OpenTK 4.x Keys enum
        private OpenTK.Windowing.GraphicsLibraryFramework.Keys _keyPressed;  // OpenTK.Windowing.GraphicsLibraryFramework.Keys

        // Your planet objects
        Shapes.Sphere Sun = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 5, true, false, false, 1, 0, 0, 0, 0, 1);
        Shapes.Sphere mercury = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), .488f, true, true, false, 1, 6, 1.5f, 0, 0, 2);
        Shapes.Sphere venus = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 1, true, true, false, 2, 11, 1.3f, 0, 0, 3);
        Shapes.Sphere earth = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 1.3f, true, true, false, 3, 15, 1.1f, 0, 0, 4);
        Shapes.Sphere moon_earth = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 0.7f, true, true, true, 1, 15, 1.1f, 1.5f, 2f, 5);
        Shapes.Sphere mars = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 0.88f, true, true, false, 1, 23, .9f, 0, 0, 6);
        Shapes.Sphere mars_moon = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 0.4f, true, true, true, 1, 23, .9f, 1f, 2f, 5);
        Shapes.Sphere jupiter = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 2.5f, true, true, false, 1, 30, .6f, 0, 0, 7);
        Shapes.Sphere saturn = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 2, true, true, false, 1, 35, .5f, 0, 0, 8);
        Shapes.Sphere uranus = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 1.5f, true, true, false, 1, 40, .3f, 0, 0, 9);
        Shapes.Sphere neptune = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 1.3f, true, true, false, 1, 45, .2f, 0, 0, 10);
        Shapes.Sphere PLUOT = new Shapes.Sphere(new Vector3(0.0f, 0.0f, 43.0f), 2, true, true, false, 1, 50, .4f, 0, 0, 12);
        Shapes.Sphere rocket = new Shapes.Sphere(new Vector3(0, 0, 5), 0.5f, false, false, false, 0, 0, 0, 0, 0, 11);

        private List<Shapes.OGLShape> _drawList;
        private int texture1, texture2, texture3, texture4, texture5, texture6, texture7,
                    texture8, texture9, texture10, texture11, texture12, texture13;

        private void InitializeObjects()
        {
            _drawList = new List<Shapes.OGLShape>();
            _drawList.Add(rocket);
            _drawList.Add(earth);
            _drawList.Add(Sun);
            _drawList.Add(mercury);
            _drawList.Add(venus);
            _drawList.Add(moon_earth);
            _drawList.Add(mars);
            _drawList.Add(mars_moon);
            _drawList.Add(jupiter);
            _drawList.Add(uranus);
            _drawList.Add(neptune);
            _drawList.Add(saturn);
            _drawList.Add(PLUOT);
        }

        private void SetEvents()
        {
            // OpenTK 4.x uses lambda syntax or different method signatures
            _window.RenderFrame += (args) => OnRenderFrame(args);
            _window.Resize += (args) => OnResize(args);
            _window.Load += () => OnLoad();
            _window.KeyDown += (args) => OnKeyDown(args);
            _window.UpdateFrame += (args) => OnUpdateFrame(args);
        }

        private void OnLoad()
        {
            GL.ClearColor(0.0f, 0.0f, 0.095f, 0.0f);
            GL.Enable(EnableCap.DepthTest);

            // Textures
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out texture1);
            GL.GenTextures(1, out texture2);
            GL.GenTextures(1, out texture3);
            GL.GenTextures(1, out texture4);
            GL.GenTextures(1, out texture5);
            GL.GenTextures(1, out texture6);
            GL.GenTextures(1, out texture7);
            GL.GenTextures(1, out texture8);
            GL.GenTextures(1, out texture9);
            GL.GenTextures(1, out texture10);
            GL.GenTextures(1, out texture11);
            GL.GenTextures(1, out texture12);
            GL.GenTextures(1, out texture13);

            // Load textures
            LoadTexture(1, texData1);
            LoadTexture(2, texData2);
            LoadTexture(3, texData3);
            LoadTexture(4, texData4);
            LoadTexture(5, texData5);
            LoadTexture(6, texData6);
            LoadTexture(7, texData7);
            LoadTexture(8, texData8);
            LoadTexture(9, texData9);
            LoadTexture(10, texData10);
            LoadTexture(11, texData11);
            LoadTexture(12, texData12);
            LoadTexture(13, texData13);
        }

        private void LoadTexture(int textureId, BitmapData data)
        {
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb,
                data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgr,
                PixelType.UnsignedByte, data.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        private void OnResize(ResizeEventArgs e)
        {
            if (e.Height != 0)
            {
                GL.Viewport(0, 0, e.Width, e.Height);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                    45f * MathF.PI / 180.0f,
                    e.Width / (float)e.Height,
                    0.10f, 150.0f);
                GL.LoadMatrix(ref perspective);
                GL.MatrixMode(MatrixMode.Modelview);
                MoveModelDown(7);
                MoveTheModeltome(18);
                RotateModelDown(10);
            }
        }

        private void OnRenderFrame(FrameEventArgs e)
        {
            // Collision detection
            if (((earth._Center.Z - 1.3f) <= rocket._Center.Z && rocket._Center.Z <= (earth._Center.Z + 1.3f)) &&
                ((earth._Center.Y - 1.3f) <= rocket._Center.Y && rocket._Center.Y <= (earth._Center.Y + 1.3f)) &&
                ((earth._Center.X - 1.3f) <= rocket._Center.X && rocket._Center.X <= (earth._Center.X + 1.3f)))
            {
                MessageBox.Show("Game Over, you crashed with Earth !", "Message");
                Environment.Exit(0);
            }
            if (((mercury._Center.Z - .488f) <= rocket._Center.Z && rocket._Center.Z <= (mercury._Center.Z + .488f)) &&
                ((mercury._Center.Y - .488f) <= rocket._Center.Y && rocket._Center.Y <= (mercury._Center.Y + .488f)) &&
                ((mercury._Center.X - .488f) <= rocket._Center.X && rocket._Center.X <= (mercury._Center.X + .488f)))
            {
                MessageBox.Show("Game over, you crashed with Mercury !", "Message");
                Environment.Exit(0);
            }
            if (((venus._Center.Z - 1) <= rocket._Center.Z && rocket._Center.Z <= (venus._Center.Z + 1)) &&
                ((venus._Center.Y - 1) <= rocket._Center.Y && rocket._Center.Y <= (venus._Center.Y + 1)) &&
                ((venus._Center.X - 1) <= rocket._Center.X && rocket._Center.X <= (venus._Center.X + 1)))
            {
                MessageBox.Show("Game over, you crashed with Venus !", "Message");
                Environment.Exit(0);
            }
            if (((mars._Center.Z - 0.88) <= rocket._Center.Z && rocket._Center.Z <= (mars._Center.Z + 0.88)) &&
                ((mars._Center.Y - 0.88) <= rocket._Center.Y && rocket._Center.Y <= (mars._Center.Y + 0.88)) &&
                ((mars._Center.X - 0.88) <= rocket._Center.X && rocket._Center.X <= (mars._Center.X + 0.88)))
            {
                MessageBox.Show("Game over, you crashed with Mars !", "Message");
                Environment.Exit(0);
            }
            if (((jupiter._Center.Z - 2.5) <= rocket._Center.Z && rocket._Center.Z <= (jupiter._Center.Z + 2.5)) &&
                ((jupiter._Center.Y - 2.5) <= rocket._Center.Y && rocket._Center.Y <= (jupiter._Center.Y + 2.5)) &&
                ((jupiter._Center.X - 2.5) <= rocket._Center.X && rocket._Center.X <= (jupiter._Center.X + 2.5)))
            {
                MessageBox.Show("Game over, you crashed with Jupiter !", "Message");
                Environment.Exit(0);
            }
            if (((uranus._Center.Z - 1.5) <= rocket._Center.Z && rocket._Center.Z <= (uranus._Center.Z + 1.5)) &&
                ((uranus._Center.Y - 1.5) <= rocket._Center.Y && rocket._Center.Y <= (uranus._Center.Y + 1.5)) &&
                ((uranus._Center.X - 1.5) <= rocket._Center.X && rocket._Center.X <= (uranus._Center.X + 1.5)))
            {
                MessageBox.Show("Game over, you crashed with Uranus !", "Message");
                Environment.Exit(0);
            }
            if (((neptune._Center.Z - 1.3) <= rocket._Center.Z && rocket._Center.Z <= (neptune._Center.Z + 1.3)) &&
                ((neptune._Center.Y - 1.3) <= rocket._Center.Y && rocket._Center.Y <= (neptune._Center.Y + 1.3)) &&
                ((neptune._Center.X - 1.3) <= rocket._Center.X && rocket._Center.X <= (neptune._Center.X + 1.3)))
            {
                MessageBox.Show("Game over, you crashed with Neptune !", "Message");
                Environment.Exit(0);
            }
            if (((saturn._Center.Z - 2) <= rocket._Center.Z && rocket._Center.Z <= (saturn._Center.Z + 2)) &&
                ((saturn._Center.Y - 2) <= rocket._Center.Y && rocket._Center.Y <= (saturn._Center.Y + 2)) &&
                ((saturn._Center.X - 2) <= rocket._Center.X && rocket._Center.X <= (saturn._Center.X + 2)))
            {
                MessageBox.Show("Game over, you crashed with Saturn !", "Message");
                Environment.Exit(0);
            }
            if (((PLUOT._Center.Z - 2) <= rocket._Center.Z && rocket._Center.Z <= (PLUOT._Center.Z + 2)) &&
                ((PLUOT._Center.Y - 2) <= rocket._Center.Y && rocket._Center.Y <= (PLUOT._Center.Y + 2)) &&
                ((PLUOT._Center.X - 2) <= rocket._Center.X && rocket._Center.X <= (PLUOT._Center.X + 2)))
            {
                MessageBox.Show("Game over, you crashed with Plout !", "Message");
                Environment.Exit(0);
            }

            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Draw Stars
            GL.PointSize(3);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(0.87f, .58f, 0.98f);

            foreach (var item in starposition)
            {
                GL.Vertex3(item.X, item.Y, item.Z);
            }
            GL.End();

            for (int i = 0; i < _drawList.Count; i++)
            {
                _drawList[i].Draw();
            }

            _window.SwapBuffers();
        }

        private void OnKeyDown(KeyboardKeyEventArgs e)
        {
            _keyPressed = e.Key;
        }

        private void OnUpdateFrame(FrameEventArgs e)
        {
            switch (_keyPressed)
            {
                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.Right:
                    rocket._Center = rocket._Center + new Vector3(1, 0, 0);
                    MoveModelLeft(1);

                    if ((38 <= rocket._Center.Z && rocket._Center.Z <= 48) &&
                        (-5 <= rocket._Center.X && rocket._Center.X <= 5) &&
                        (-5 <= rocket._Center.Y && rocket._Center.Y <= 5))
                    {
                        rocket._Center = rocket._Center + new Vector3(-1, 0, 0);
                        MoveModelLeft(-1);
                    }
                    break;

                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.Left:
                    rocket._Center = rocket._Center + new Vector3(-1, 0, 0);
                    MoveModelRight(1);

                    if ((38 <= rocket._Center.Z && rocket._Center.Z <= 48) &&
                        (-5 <= rocket._Center.X && rocket._Center.X <= 5) &&
                        (-5 <= rocket._Center.Y && rocket._Center.Y <= 5))
                    {
                        rocket._Center = rocket._Center + new Vector3(1, 0, 0);
                        MoveModelRight(-1);
                    }
                    break;

                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.Down:
                    rocket._Center = rocket._Center + new Vector3(0, 0, -1);
                    MoveTheModeltome(1);

                    if ((38 <= rocket._Center.Z && rocket._Center.Z <= 48) &&
                        (-5 <= rocket._Center.X && rocket._Center.X <= 5) &&
                        (-5 <= rocket._Center.Y && rocket._Center.Y <= 5))
                    {
                        rocket._Center = rocket._Center + new Vector3(0, 0, 1);
                        MoveTheModeltome(-1);
                    }
                    break;

                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.Up:
                    rocket._Center = rocket._Center + new Vector3(0, 0, 1);
                    MoveTheModelAway(1);

                    if ((38 <= rocket._Center.Z && rocket._Center.Z <= 48) &&
                        (-5 <= rocket._Center.X && rocket._Center.X <= 5) &&
                        (-5 <= rocket._Center.Y && rocket._Center.Y <= 5))
                    {
                        rocket._Center = rocket._Center + new Vector3(0, 0, -1);
                        MoveTheModelAway(-1);
                    }
                    break;

                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.W:
                    MoveModelDown(1);
                    rocket._Center = rocket._Center + new Vector3(0, 1, 0);

                    if ((38 <= rocket._Center.Z && rocket._Center.Z <= 48) &&
                        (-5 <= rocket._Center.X && rocket._Center.X <= 5) &&
                        (-5 <= rocket._Center.Y && rocket._Center.Y <= 5))
                    {
                        rocket._Center = rocket._Center + new Vector3(0, -1, 0);
                        MoveModelDown(-1);
                    }
                    break;

                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.S:
                    MoveModelUp(1);
                    rocket._Center = rocket._Center + new Vector3(0, -1, 0);
                    if ((38 <= rocket._Center.Z && rocket._Center.Z <= 48) &&
                        (-5 <= rocket._Center.X && rocket._Center.X <= 5) &&
                        (-5 <= rocket._Center.Y && rocket._Center.Y <= 5))
                    {
                        rocket._Center = rocket._Center + new Vector3(0, 1, 0);
                        MoveModelUp(-1);
                    }
                    break;

                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.A:
                    RotateModelRight(1);
                    break;
                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.D:
                    RotateModelLeft(1);
                    break;
                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftControl:
                    RotateModelDown(1);
                    break;
                case OpenTK.Windowing.GraphicsLibraryFramework.Keys.RightControl:
                    RotateModelUp(1);
                    break;
            }
        }

        public void RotateModelRight(float v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Rotate(v, -Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void RotateModelLeft(float v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Rotate(v, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void RotateModelUp(float v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Rotate(v, -Vector3.UnitX);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void RotateModelDown(float v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Rotate(v, Vector3.UnitX);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void MoveTheModelAway(int v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(Vector3.UnitZ * v);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void MoveTheModeltome(int v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(-Vector3.UnitZ * v);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void MoveModelRight(int v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(Vector3.UnitX * v);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void MoveModelLeft(int v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(-Vector3.UnitX * v);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void MoveModelUp(int v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(Vector3.UnitY * v);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void MoveModelDown(int v)
        {
            _keyPressed = OpenTK.Windowing.GraphicsLibraryFramework.Keys.Unknown;
            GL.MatrixMode(MatrixMode.Projection);
            GL.Translate(-Vector3.UnitY * v);
            GL.MatrixMode(MatrixMode.Modelview);
        }
    }
}