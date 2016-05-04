using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3CG
{
    class Lines
    {
        public static Bitmap MidpointLine(int x1, int y1, int x2, int y2, Bitmap bt, int maxWidth,int maxHeight,int brushSize, Color CustomColor)
        {

            brushSize--;

            bool steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);
            if (steep)
            {
                Swap<int>(ref x1, ref y1);
                Swap<int>(ref x2, ref y2);
            }
            if (x1 > x2)
            {
                Swap<int>(ref x1, ref x2);
                Swap<int>(ref y1, ref y2);
            }
            int dX = (x2 - x1), dY = Math.Abs(y2 - y1), err = (dX/2), ystep = (y1 < y2 ? 1 : -1), y = y1;

            for (int x = x1; x <= x2; ++x)
            {

              
                        if (steep && clump(y,x, maxWidth,maxHeight))
                             DrawBrush(y, x, CustomColor, brushSize, bt);

              
                        else
                             DrawBrush(x, y, CustomColor, brushSize, bt);


                err = err - dY;
                if (err < 0)
                {
                    y += ystep;
                    err += dX;
                }
            }


            return bt;
        }


        private static bool clump(int x, int y, int maxWidth, int maxHeight)
        {
            return x < maxWidth && x >= 0 && y < maxHeight && y >= 0;
        }


        private static
            void Swap<T>(ref T lhs, ref T rhs) { T temp; temp = lhs; lhs = rhs; rhs = temp; }


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

        public static Bitmap DdaLine(int xa, int ya, int xb, int yb, Bitmap bt, int maxWidth, int maxHeight, int brushSize, Color CustomColor)
        {
            var dx = xb - xa;
            var dy = yb - ya;
            brushSize --;

            var steps = Math.Abs(Math.Abs(dx) > Math.Abs(dy) ? dx : dy);

            var inc_x = (float)dx / (float)steps;
            var inc_y = (float)dy / (float)steps;


            float x = xa;
            float y = ya;

            
                   if(clump((int)x,(int)y,maxWidth,maxHeight))
                   DrawBrush((int)x, (int)y, CustomColor, brushSize, bt);

            

            for (var xd = 0; xd < steps; xd++)
            {
                x += inc_x;
                y += inc_y;

       
                        if (clump((int)x, (int)y, maxWidth, maxHeight))
                             DrawBrush((int)x, (int)y, CustomColor, brushSize, bt);


            }



            return bt;
        }


    }
}
