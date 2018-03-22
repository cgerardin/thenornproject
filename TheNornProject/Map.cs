using System.Collections.Generic;
using static TheNornProject.Resources;

namespace TheNornProject
{
    class Map
    {
        private string name;
        private int size;
        private Sprite[,] data;
        private Item[,] items;
        private List<Norn> norns;

        public Map(string name, int size)
        {
            Name = name;
            Size = size;
            Sprites = new Sprite[size, size];
            Items = new Item[size, size];
            Norns = new List<Norn>();
        }

        public string Name { get => name; set => name = value; }
        public int Size { get => size; set => size = value; }
        public Sprite[,] Sprites { get => data; set => data = value; }
        internal Item[,] Items { get => items; set => items = value; }
        internal List<Norn> Norns { get => norns; set => norns = value; }
    }
}
