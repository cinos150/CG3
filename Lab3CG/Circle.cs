using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3CG
{
    class Circle
    {
        //DDA circle algorithm
        public static Bitmap DDACircle(int xc, int yc, int r, Bitmap bt, int maxWidth, int maxHeight, int brushSize, Color CustomColor)
        {
            int val, i = 0;

            float xc1 = r, yc1 = 0;
            brushSize--;
            var sx = xc1;
            var sy = yc1;


            do
            {
                val = (int) Math.Pow(2, i);
                i++;
            } while (val < r);

            var eps = (float) (1/Math.Pow(2, i - 1));

            do
            {

                var xc2 = xc1 + yc1*eps;
                var yc2 = yc1 - eps*xc2;
                var pixX = (int) (xc + xc2);
                var pixY = (int) (yc - yc2);


          
                        if (clump(pixX, pixY , maxWidth, maxHeight))
                        {
                            DrawBrush(pixX, pixY, CustomColor, brushSize, bt);
                        }
                    
                
                xc1 = xc2;

                yc1 = yc2;

            } while ((yc1 - sy) < eps || (sx - xc1) > eps);



            return bt;
        }


        private static bool clump(int x, int y, int maxWidth, int maxHeight)
        {
            return x < maxWidth && x >= 0 && y < maxHeight && y >= 0;
        }


        //Midpoint Circle algorithm
        public static Bitmap MidpointCircle(int x0, int y0, int radius, Bitmap bt, int maxWidth, int maxHeight,
            int brushSize, Color CustomColor)
        {
            brushSize--;
            int x = 0, y = radius;
            int dp = 1 - radius;
            do
            {
                if (dp < 0)
                    dp = dp + 2 * (++x) + 3;
                else
                    dp = dp + 2 * (++x) - 2 * (--y) + 5;

            

                        if (clump(x0 + x, y0 + y, maxWidth, maxHeight))
                              DrawBrush(x0 + x, y0 + y, CustomColor, brushSize, bt);

                        if (clump(x0 - x, y0 + y, maxWidth, maxHeight))
                             DrawBrush(x0 - x, y0 + y, CustomColor, brushSize, bt);
                        
                        if (clump(x0 + x, y0 - y, maxWidth, maxHeight))
                             DrawBrush(x0 + x, y0 - y, CustomColor, brushSize, bt);

                if (clump(x0 - x, y0 - y, maxWidth, maxHeight))
                    DrawBrush(x0 - x, y0 - y, CustomColor, brushSize, bt);
            
                        if (clump(x0 + y, y0 + x, maxWidth, maxHeight))
                    DrawBrush(x0 + y, y0 + x, CustomColor, brushSize, bt);
              
                        if (clump(x0 - y, y0 + x, maxWidth, maxHeight))
                             DrawBrush(x0 - y, y0 + x, CustomColor, brushSize, bt);

                if (clump(x0 + y, y0 - x, maxWidth, maxHeight))
                    DrawBrush(x0 + y, y0 - x, CustomColor, brushSize, bt);

                if (clump(x0 - y, y0 - x, maxWidth, maxHeight))
                    DrawBrush(x0 - y, y0 -x, CustomColor, brushSize, bt);




            } while (x < y);



            return bt;
        }

        public static void DrawBrush(int px, int py, Color color, int strokeSize, Bitmap image)
        {
            for (int y = -strokeSize; y <= strokeSize; y++)
            {
                for (int x = -strokeSize; x <= strokeSize; x++)
                {
                    if (x * x + y * y <= strokeSize * strokeSize)
                    {
                        if (px + x < image.Width && px + x > 0 && py + y < image.Height && py + y > 0)
                            image.SetPixel(px + x, py + y, color);
                    }
                }
            }

        }

    }


}
