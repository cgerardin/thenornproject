using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static TheNornProject.Resources;

namespace TheNornProject
{
    public partial class MainForm : Form
    {
        Random rnd;
        DateTime startCompute;
        DateTime startRender;

        Map map;
        Bitmap[] bitmaps;
        List<Item> items;

        public MainForm()
        {

            InitializeComponent();
            Application.Idle += HandleApplicationIdle;

            rnd = new Random();

            map = new Map("demo_map", 19);
            map.Data = new Sprite[,] {
                {Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall},
                {Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_empty, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall}
            };

            bitmaps = new Bitmap[SpritesCount];
            for (int i = 0; i < SpritesCount; i++)
            {
                if (File.Exists("resources\\" + i + ".png"))
                {
                    bitmaps[i] = new Bitmap(new Bitmap("resources\\" + i + ".png"), new Size(32, 32));
                }
            }

            items = new List<Item>();
            items.Add(new Item("", Sprite.object_meat, 512, 128));

            startCompute = DateTime.Now;
            startRender = DateTime.Now;
        }

        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                Compute();
                Application.DoEvents();
                Render();
            }
        }

        void Compute()
        {
            if ((DateTime.Now - startCompute).TotalMilliseconds >= 250)
            {
                startCompute = DateTime.Now;

                foreach (Item i in items)
                {
                    if (i is Norn)
                    {
                        Norn n = (Norn)i;
                        if (n.isAlive())
                        {
                            Map environment = new Map("", 5);

                            int map_x = 0, map_y = 0;
                            int view_y = -2;
                            for (int env_y = 0; env_y < 4; env_y++)
                            {
                                int view_x = -2;
                                for (int env_x = 0; env_x < 4; env_x++)
                                {

                                    map_x = n.X + view_x;
                                    map_y = n.Y + view_y;

                                    if (map_x < 0 || map_x >= map.Size || map_y < 0 || map_y >= map.Size)
                                    {
                                        environment.Data[env_x, env_y] = 0;
                                    }
                                    else
                                    {
                                        environment.Data[env_x, env_y] = map.Data[map_x, map_y];
                                    }

                                    view_x++;
                                }

                                view_y++;
                            }

                            n.Live(environment);
                        }
                        else
                        {
                            //p.Remove(n);
                        }
                    }
                }

                // Log
                textBoxLog.Text = "";
                textBoxLog.Text = "Norn (" + items.Count + ")\t\tAge\tLife\tHunger\r\n";
                foreach (Item i in items)
                {
                    if (i is Norn)
                    {
                        Norn n = (Norn)i;
                        textBoxLog.Text += i.Name + "\t\t\t" + n.Age + "\t" + n.Life + "\t" + n.Hunger + "\r\n";
                    }
                }
            }
        }

        void Render()
        {
            if ((DateTime.Now - startRender).TotalMilliseconds >= 16)
            {
                startRender = DateTime.Now;

                display.Invalidate();
            }
        }

        private void display_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(display.BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int mx = 0, my = 0;
            int physical_mapsize = map.Size * 32;

            // Map
            for (int y = 0; y < physical_mapsize; y += 32)
            {
                for (int x = 0; x < physical_mapsize; x += 32)
                {
                    e.Graphics.DrawImage(bitmaps[(int)map.Data[mx, my]], x, y);
                    mx++;
                }
                mx = 0;
                my++;
            }

            // Objects
            foreach (Item i in items)
            {
                e.Graphics.DrawImage(bitmaps[(int)i.Sprite], i.X * 32, i.Y * 32);
                if (i is Norn)
                {
                    Norn n = (Norn)i;
                    e.Graphics.DrawString(n.Name, Font, Brushes.White, (n.X * 32 - (n.Name.Length)), n.Y * 32 - Font.Size * 2);
                    e.Graphics.DrawRectangle(Pens.Red, n.X * 32, (n.Y * 32 - 5), (1000 / 32), 1);
                    e.Graphics.DrawRectangle(Pens.Green, n.X * 32, (n.Y * 32 - 5), (n.Life * 10 / 32), 1);
                }

            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.N)
            {
                int x, y;
                do
                {
                    x = rnd.Next(0, map.Size);
                    y = rnd.Next(0, map.Size);
                } while (map.Data[x, y] == Sprite.terrain_empty || map.Data[x, y] == Sprite.terrain_wall);

                bool sex = rnd.Next(0, 2) != 0;
                Norn n = new Norn(Norn.GenerateNornName(sex), sex, x, y);

                items.Add(n);
            }

        }

        bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }

        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

    }
}
