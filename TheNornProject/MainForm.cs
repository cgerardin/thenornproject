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
        Font gamefont18;
        Font gamefont10;
        DateTime startCompute;
        DateTime startRender;

        public MainForm()
        {

            InitializeComponent();
            Application.Idle += HandleApplicationIdle;

            rnd = new Random();

            gamefont10 = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold);
            gamefont18 = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);

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
            if ((DateTime.Now - startCompute).TotalMilliseconds >= 250) // Vitesse de simulation
            {
                startCompute = DateTime.Now;

                for (int y = 0; y < Program.Map.Size; y++)
                {
                    for (int x = 0; x < Program.Map.Size; x++)
                    {
                        // Items
                    }
                }

                // Norns
                foreach (Norn n in Program.Map.Norns)
                {
                    if (n.IsAlive())
                    {
                        // Environment
                        Map environment = new Map(null, 5);

                        int map_x = 0, map_y = 0;
                        int view_y = -2;
                        for (int env_y = 0; env_y < 4; env_y++)
                        {
                            int view_x = -2;
                            for (int env_x = 0; env_x < 4; env_x++)
                            {

                                map_x = n.X + view_x;
                                map_y = n.Y + view_y;

                                if (map_x < 0 || map_x >= Program.Map.Size || map_y < 0 || map_y >= Program.Map.Size)
                                {
                                    environment.Sprites[env_x, env_y] = 0;
                                    environment.Items[env_x, env_y] = null;
                                }
                                else
                                {
                                    environment.Sprites[env_x, env_y] = Program.Map.Sprites[map_x, map_y];
                                    environment.Items[env_x, env_y] = Program.Map.Items[map_x, map_y];
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
        }

        void Render()
        {
            if ((DateTime.Now - startRender).TotalMilliseconds >= 16) // Environ 60 FPS
            {
                startRender = DateTime.Now;
                Invalidate();
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Map
            int physical_mapsize = Program.Map.Size * 32;
            int my = 0;

            for (int y = 0; y < physical_mapsize; y += 32)
            {
                int mx = 0;
                for (int x = 0; x < physical_mapsize; x += 32)
                {
                    // Sprites
                    e.Graphics.DrawImage(Program.Bitmaps[(int)Program.Map.Sprites[mx, my]], x, y);

                    // Items
                    if (Program.Map.Items[mx, my] != null)
                    {
                        e.Graphics.DrawImage(Program.Bitmaps[(int)Program.Map.Items[mx, my].Sprite], x, y);
                    }

                    mx++;
                }
                my++;
            }

            // Norns
            foreach (Norn n in Program.Map.Norns)
            {
                // Sprite
                e.Graphics.DrawImage(Program.Bitmaps[(int)n.Sprite], n.X * 32, n.Y * 32);
                // Nom
                e.Graphics.DrawString(n.Name + " [" + n.Age / 12 + "]", gamefont10, Brushes.White, (n.X * 32 - (n.Name.Length + 8)), n.Y * 32 - 24);
                if (n.IsAlive())
                {
                    // Vie
                    //e.Graphics.DrawRectangle(Pens.White, n.X * 32 - 1, (n.Y * 32 - 7) - 1, (1000 / 32) + 2, 5);
                    e.Graphics.DrawRectangle(Pens.Red, n.X * 32, (n.Y * 32 - 7), 32, 2);
                    e.Graphics.DrawRectangle(Pens.SpringGreen, n.X * 32, (n.Y * 32 - 7), (n.Life * 10 / 32), 1);
                    e.Graphics.DrawRectangle(Pens.Blue, n.X * 32, (n.Y * 32 - 5), (n.Hunger * 10 / 32), 1);

                    // Humeur
                    if (n.IsHungry() && !n.IsChasing())
                    {
                        e.Graphics.DrawString("?", gamefont18, Brushes.Orange, (n.X * 32 + 32), n.Y * 32);
                    }
                    else if (n.IsChasing())
                    {
                        e.Graphics.DrawString("!", gamefont18, Brushes.Red, (n.X * 32 + 32), n.Y * 32);
                    }
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int x, y;

            switch (e.KeyCode)
            {
                case Keys.N:

                    bool sex = rnd.Next(0, 2) != 0;
                    Norn n = new Norn(Norn.GenerateNornName(sex), sex, 0, 0);
                    do
                    {
                        x = rnd.Next(0, Program.Map.Size);
                        y = rnd.Next(0, Program.Map.Size);
                    } while (!Program.PopNorn(n, x, y));
                    break;

                case Keys.M:
                    Item i = new Item("Meat", Sprite.object_meat, 0, 0);
                    do
                    {
                        x = rnd.Next(0, Program.Map.Size);
                        y = rnd.Next(0, Program.Map.Size);
                    } while (!Program.PopItem(i, x, y));
                    break;

            }
        }

        bool IsApplicationIdle()
        {
            return PeekMessage(out NativeMessage result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
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
