using System.Drawing;
using System.Reflection;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace OpenTKTut
{
    class Program
    {
        private static List<Bitmap> _keepAliveBitmaps = new List<Bitmap>();

        static BitmapData LoadTexture(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new System.IO.FileNotFoundException($"Texture not found: {path}");

            Bitmap bitmap = new Bitmap(path);
            _keepAliveBitmaps.Add(bitmap);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            return data;
        }
        static void Main(string[] args)
        {
            string texturesPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Textures\texture\";

            // Load all textures
            BitmapData rocketdata = LoadTexture(System.IO.Path.Combine(texturesPath, "rocket.jpg"));
            BitmapData earthdata = LoadTexture(System.IO.Path.Combine(texturesPath, "2k_earth_daymap.jpg"));
            BitmapData sundata = LoadTexture(System.IO.Path.Combine(texturesPath, "Map_of_the_full_sun.jpg"));
            BitmapData moondata = LoadTexture(System.IO.Path.Combine(texturesPath, "moon.bmp"));
            BitmapData mercurydata = LoadTexture(System.IO.Path.Combine(texturesPath, "mercury.jpg"));
            BitmapData venusdata = LoadTexture(System.IO.Path.Combine(texturesPath, "venus1.bmp"));
            BitmapData marsdata = LoadTexture(System.IO.Path.Combine(texturesPath, "mars.jpg"));
            BitmapData jupiterdata = LoadTexture(System.IO.Path.Combine(texturesPath, "jupiter.jpg"));
            BitmapData saturndata = LoadTexture(System.IO.Path.Combine(texturesPath, "saturn.jpg"));
            BitmapData uranusdata = LoadTexture(System.IO.Path.Combine(texturesPath, "uranus.jpg"));
            BitmapData neptunedata = LoadTexture(System.IO.Path.Combine(texturesPath, "neptune.jpg"));
            BitmapData plutodata = LoadTexture(System.IO.Path.Combine(texturesPath, "pluto.jpeg"));
            BitmapData saturnrocketdata = LoadTexture(System.IO.Path.Combine(texturesPath, "saturnrocket.jpeg"));

            Viewport View = new Viewport(sundata, venusdata, marsdata, earthdata, moondata,
                                         mercurydata, jupiterdata, saturndata, uranusdata,
                                         neptunedata, rocketdata, plutodata, saturnrocketdata);

            // Add orbits
            View.AddShape(new Shapes.CircleDraw(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), 6, 1, 15, 1, 2, 2));
            View.AddShape(new Shapes.CircleDraw(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), 11, 1, 15, 1, 2, 2));
            View.AddShape(new Shapes.CircleDraw(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), 15, 1, 15, 1, 2, 2));
            View.AddShape(new Shapes.CircleDraw(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), 23, 1, 20, 1, 2, 2));
            View.AddShape(new Shapes.CircleDraw(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), 30, 1, 25, 1, 2, 2));
            View.AddShape(new Shapes.CircleDraw(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), 35, 1, 30, 1, 2, 2));
            View.AddShape(new Shapes.CircleDraw(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), 40, 1, 30, 1, 2, 2));
            View.AddShape(new Shapes.CircleDraw(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), 45, 1, 30, 1, 2, 2));
            View.AddShape(new Shapes.CircleDraw(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), 50, 1, 30, 1, 2, 2));

            // Add saturn rockets
            for (int i = 0; i < 100; i++)
            {
                View.AddShape(new Shapes.Sphere(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 43.0f), .3f, true, true, true,
                    1, 35, .5f, 2.3f, 2 + (i * .1f), 13));
            }

            View.Start();
        }


    }
}