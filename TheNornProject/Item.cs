using System;
using static TheNornProject.Resources;

namespace TheNornProject
{
    class Item
    {
        private Random god;
        private string name;
        private Sprite sprite;
        private int x, y;

        public Item(string name, int x, int y)
        {
            God = new Random();
            Name = name;
            Sprite = 0;
            X = x;
            Y = y;
        }

        public Item(string name, Sprite sprite, int x, int y)
        {
            God = new Random();
            Name = name;
            Sprite = sprite;
            X = x;
            Y = y;
        }

        public Random God { get => god; set => god = value; }
        public string Name { get => name; set => name = value; }
        public Sprite Sprite { get => sprite; set => sprite = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
