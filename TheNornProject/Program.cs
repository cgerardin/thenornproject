using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static TheNornProject.Resources;

namespace TheNornProject
{
    static class Program
    {

        private static Bitmap[] bitmaps;
        private static Map map;

        public static Bitmap[] Bitmaps { get => bitmaps; set => bitmaps = value; }
        internal static Map Map { get => map; set => map = value; }

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Chargement des bitmaps
            Bitmaps = new Bitmap[SpritesCount];
            for (int i = 0; i < SpritesCount; i++)
            {
                if (File.Exists("resources\\" + i + ".png"))
                {
                    Bitmaps[i] = new Bitmap(new Bitmap("resources\\" + i + ".png"), new Size(32, 32));
                }
            }

            // Chargement de la carte
            Map = new Map("demo_map", 19)
            {
                Sprites = new Sprite[,] {
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
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall, Sprite.terrain_empty, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_grass, Sprite.terrain_wall},
                {Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall, Sprite.terrain_wall}
            }
            };

            Random rnd = new Random();

            for (int k = 0; k < 20; k++)
            {
                int o = rnd.Next(0, Map.Size);
                int p = rnd.Next(0, Map.Size);
                PopItem(new Item("Meat", Sprite.object_meat, o, p), o, p);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static bool PopItem(Item item, int x, int y)
        {
            if (Map.Sprites[x, y] == Sprite.terrain_empty || Map.Sprites[x, y] == Sprite.terrain_wall || Map.Items[x, y] != null)
            {
                return false;
            }
            else
            {
                item.X = x;
                item.Y = y;
                Map.Items[x, y] = item;
                return true;
            }
        }

        public static void DeleteItem(int x, int y)
        {
            Map.Items[x, y] = null;
        }

        public static bool PopNorn(Norn norn, int x, int y)
        {
            if (Map.Sprites[x, y] == Sprite.terrain_empty || Map.Sprites[x, y] == Sprite.terrain_wall || Map.Items[x, y] != null)
            {
                return false;
            }
            else
            {
                norn.X = x;
                norn.Y = y;
                Map.Norns.Add(norn);
                return true;
            }
        }

    }
}
