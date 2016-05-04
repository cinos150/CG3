using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lab3CG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            comboBox1.Items.Add("DDA");
            comboBox1.Items.Add("Midpoint circle");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;



            comboBox2.Items.Add("DDA");
            comboBox2.Items.Add("Midpoint line");
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndex = 0;

        }

        private Color CustomColor;
        private Bitmap _bt;
        private Graphics _g;

        private int _screenWidth ;
        private int _screenHeight ;

     

  


        private void Form1_Load(object sender, EventArgs e)
        {
            CustomColor = Color.DarkGreen;
              _g = this.CreateGraphics();
            _screenWidth = Screen.PrimaryScreen.Bounds.Width;
            _screenHeight = Screen.PrimaryScreen.Bounds.Height;
            _bt = new Bitmap(_screenWidth,_screenHeight,_g);

        }

        private void btnDDA_Click(object sender, EventArgs e)
        {

            if (txtXa.Text == "" || txtYa.Text == "" || txtXb.Text == "" || txtYb.Text == "")
            {
                MessageBox.Show("Left mouse click beginning \nRight mouse click end",
                    "Choose points first",
                    MessageBoxButtons.OK);
                return;
            }

            _bt = new Bitmap(_screenWidth, _screenHeight, _g);

            var xa = int.Parse(txtXa.Text);

            var xb = int.Parse(txtXb.Text);

            var ya = int.Parse(txtYa.Text);
            var yb = int.Parse(txtYb.Text);


            this.BackgroundImage = this.BackgroundImage = comboBox2.SelectedIndex == 0
                ? Lines.DdaLine(xa, ya, xb, yb, _bt, this.Width, this.Height,(int)numericUpDown1.Value, CustomColor)
                : Lines.MidpointLine(xa, ya, xb, yb, _bt,this.Width, this.Height ,(int)numericUpDown1.Value, CustomColor);

        }


       


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
            switch (e.Button)
            {
                case MouseButtons.Left:
                    label1.Text = "X: " + e.X;
                    label2.Text = "Y: " + e.Y;
                    label10.Text ="X: " + e.X;
                    label11.Text ="Y: " + e.Y;
                    txtXa.Text = e.X.ToString();
                    txtYa.Text = e.Y.ToString();
                    txtCircleX.Text = e.X.ToString();
                    txtCircleY.Text = e.Y.ToString();
                    
                    _bt.SetPixel(e.X,e.Y,CustomColor);

                    break;
                case MouseButtons.Right:
                    txtXb.Text = e.X.ToString();
                    txtYb.Text = e.Y.ToString();

                    if (txtCircleX.Text != "" && txtCircleY.Text != "")
                    {
                        var Xa = int.Parse(txtCircleX.Text);
                        var Ya = int.Parse(txtCircleY.Text);

                        var Xb = e.X;
                        var Yb = e.Y;

                        txtRadius.Text = EuclideanDistance(Xa, Xb, Ya, Yb).ToString(CultureInfo.InvariantCulture);

                    }

                    break;
                default:
                    break;

            }
        }


        private static int EuclideanDistance(int Xa, int Xb, int Ya, int Yb)
        {

            var distX = Xb - Xa;
            var distY = Yb - Ya;
           ;

            return Convert.ToInt32(Math.Sqrt(Math.Pow(Math.Abs(distX), 2) + Math.Pow(Math.Abs(distY), 2)));

        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (txtCircleX.Text == "" || txtCircleY.Text == "")
            {
                label10.Text = "X: " + e.X;
                label11.Text = "Y: " + e.Y;
            }


            if (txtXa.Text == "" || txtYa.Text == "")
            {
                label1.Text = "X: " + e.X;
                label2.Text = "Y: " + e.Y;
            }
            else
            {
                label6.Text = "X: " + e.X;
                label5.Text = "Y: " + e.Y;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            txtRadius.Text = "";
            txtXa.Text = "";
            txtYa.Text = "";
            txtXb.Text = "";
            txtYb.Text = "";
            txtCircleX.Text = "";
            txtCircleY.Text = "";

            _bt = new Bitmap(_screenWidth, _screenHeight, _g);
            this.BackgroundImage = _bt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int radius = Convert.ToInt32(txtRadius.Text);
            int x0  = Convert.ToInt32(txtCircleX.Text);
            int y0 = Convert.ToInt32(txtCircleY.Text);
            _bt = new Bitmap(_screenWidth, _screenHeight, _g);

            this.BackgroundImage = comboBox1.SelectedIndex == 0 ? 
                Circle.DDACircle(x0, y0, radius,_bt,this.Width,this.Height,(int)numericUpDown1.Value, CustomColor) :
                Circle.MidpointCircle(x0, y0, radius,_bt,this.Width,this.Height,(int)numericUpDown1.Value, CustomColor);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (txtXa.Text == "" || txtYa.Text == "" || txtXb.Text == "" || txtYb.Text == "")
            {
                MessageBox.Show("Left mouse click beginning \nRight mouse click end",
                    "Choose points first",
                    MessageBoxButtons.OK);
                return;
            }

            _bt = new Bitmap(_screenWidth, _screenHeight, _g);

            var xa = int.Parse(txtXa.Text);

            var xb = int.Parse(txtXb.Text);

            var ya = int.Parse(txtYa.Text);
            var yb = int.Parse(txtYb.Text);

          this.BackgroundImage =  Antialiasing.drawLine(_bt,xa, ya, xb, yb,(int)numericUpDown1.Value, CustomColor, this.BackColor);
         // this.BackgroundImage =  Antialiasing.XiaoWuu(xa, ya, xb, yb, _bt,(int)numericUpDown1.Value, CustomColor);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult result = colorDialog1.ShowDialog();



            if (result == DialogResult.OK)
            {


                button3.BackColor = colorDialog1.Color;

                CustomColor = colorDialog1.Color;



            };

            }
    }
}
