using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing.Imaging;
using System;

namespace OpenTKTut
{
    partial class Viewport
    {
        private GameWindow _window;
        public BitmapData texData1, texData2, texData3, texData4, texData5, texData6,
                         texData7, texData8, texData9, texData10, texData11, texData12, texData13;

        private Random R1 = new Random(4);
        private Random R2 = new Random(19);
        private Random R3 = new Random(25);
        public Vector3[] starposition = new Vector3[1000];

        public Viewport(BitmapData TextureData1, BitmapData TextureData2, BitmapData TextureData3,
               BitmapData TextureData4, BitmapData TextureData5, BitmapData TextureData6,
               BitmapData TextureData7, BitmapData TextureData8, BitmapData TextureData9,
               BitmapData TextureData10, BitmapData TextureData11, BitmapData TextureData12,
               BitmapData TextureData13)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(1720, 800),
                Title = "Solar System Simulation",
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
                API = ContextAPI.OpenGL,
                APIVersion = new Version(3, 3)
            };

            _window = new GameWindow(GameWindowSettings.Default, nativeWindowSettings);
            InitializeObjects();
            SetEvents();

            texData1 = TextureData1;
            texData2 = TextureData2;
            texData3 = TextureData3;
            texData4 = TextureData4;
            texData5 = TextureData5;
            texData6 = TextureData6;
            texData7 = TextureData7;
            texData8 = TextureData8;
            texData9 = TextureData9;
            texData10 = TextureData10;
            texData11 = TextureData11;
            texData12 = TextureData12;
            texData13 = TextureData13;

            // Calculate star positions
            for (int i = 0; i < 1000; i++)
            {
                starposition[i] = new Vector3(
                    R1.Next(-70, 70),
                    R2.Next(-80, 80),
                    R3.Next(-80, 50)
                );
            }
        }

        public void Start()
        {
            _window.Run();
        }

        public void AddShape(Shapes.OGLShape oGLShape)
        {
            _drawList.Add(oGLShape);
        }
    }
}