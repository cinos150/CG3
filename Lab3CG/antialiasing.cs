using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Lab3CG
{
     class Antialiasing
    {




         public static Bitmap XiaoWuu(int x1, int y1, int x2, int y2, Bitmap bp,int brushSize, Color customColor, Color backgroundColor)
         {
             brushSize--;
             Color L = customColor; /*Line color*/
             Color B = backgroundColor; /*Background Color*/
             float y = y1;
             int m = 0;
             for (int x = x1; x <= x2; ++x)
             {
                 

               Color c1 =   Color.FromArgb(clump((int) (L.R*(1 - y) + B.R*y)), clump((int) (L.G*(1 - y) + B.G*y)), clump((int) (L.B*(1 - y) + B.B*y)));
               Color c2 =   Color.FromArgb(clump((int) (L.R*(y) + B.R*(1-y))),clump((int) (L.G*(y) + B.G*(1-y))),clump((int) (L.B*(y) + B.B*(1-y))));


                DrawBrush(x, (int)Math.Floor(y) , c1, brushSize, bp);

                DrawBrush(x, (int)Math.Floor(y) + 1, c2, brushSize, bp);

               
                  
                 y += m;


                 
             }

            return bp;
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

        private static int clump(int a)
         {
             return a = (a > 255) ? 255 : a < 0 ? 0 : a;
         }

         static void plot(Bitmap bp, double x, double y, double c, Color CustomColor, int brushSize)
        {
            Color c1 = CustomColor;
           
            Color c2 = Color.FromArgb(c1.A, (int)(c1.R * c), (int)(c1.G * c), (int)(c1.B * c));

         
            DrawBrush((int)x,(int) y, c2, brushSize, bp);


        }

        static int ipart(double x)
        {
            return (int)x;
        }

       static double fpart(double x)
        {
            return x - Math.Floor(x);
        }

        static double rfpart(double x)
        {
            return 1.0 - fpart(x);
        }




        public static Bitmap drawLine(Bitmap g, double x0, double y0, double x1, double y1, int brushSize, Color CustomColor, Color BackgroundColor)
        {

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
                drawLine(g, y0, x0, y1, x1,brushSize,  CustomColor, BackgroundColor);

            if (x0 > x1)
                drawLine(g, x1, y1, x0, y0,brushSize,  CustomColor, BackgroundColor);

            double dx = x1 - x0;
            double dy = y1 - y0;

            double gradient;

             gradient = (dx ==0)?dy : dy / dx;

            // handle first endpoint
            double xend = Math.Round(x0);
            double yend = y0 + gradient * (xend - x0);
            double xgap = rfpart(x0 + 0.5);
            double xpxl1 = xend; // this will be used in the main loop
            double ypxl1 = ipart(yend);

            if (steep)
            {
                plot(g, ypxl1, xpxl1, rfpart(yend) * xgap,CustomColor,brushSize);
                plot(g, ypxl1 + 1, xpxl1, fpart(yend) * xgap, BackgroundColor,brushSize);
            }
            else {
                plot(g, xpxl1, ypxl1, rfpart(yend) * xgap, CustomColor,brushSize);
                plot(g, xpxl1, ypxl1 + 1, fpart(yend) * xgap, BackgroundColor,brushSize);
            }

            // first y-intersection for the main loop
            double intery = yend + gradient;

            // handle second endpoint
            xend = Math.Round(x1);
            yend = y1 + gradient * (xend - x1);
            xgap = fpart(x1 + 0.5);
            double xpxl2 = xend; // this will be used in the main loop
            double ypxl2 = ipart(yend);

            if (steep)
            {
                plot(g, ypxl2, xpxl2, rfpart(yend) * xgap, CustomColor,brushSize);
                plot(g, ypxl2 + 1, xpxl2, fpart(yend) * xgap, BackgroundColor,brushSize);
            }
            else {
                plot(g, xpxl2, ypxl2, rfpart(yend) * xgap, CustomColor,brushSize);
                plot(g, xpxl2, ypxl2 + 1, fpart(yend) * xgap, BackgroundColor,brushSize);
            }

            // main loop
            for (double x = xpxl1 + 1; x <= xpxl2 - 1; x++)
            {
                if (steep)
                {

                 

                            plot(g, ipart(intery), x, rfpart(intery), CustomColor,brushSize);
                            plot(g, ipart(intery) + 1 , x, fpart(intery), BackgroundColor,brushSize);
                        
                }
                else {
                 
                            plot(g, x , ipart(intery) , rfpart(intery), CustomColor,brushSize);
                            plot(g, x , ipart(intery) + 1 , fpart(intery), BackgroundColor,brushSize);
                   
                }
                intery = intery + gradient;
            }


            return g;
        }





    }
   
}
