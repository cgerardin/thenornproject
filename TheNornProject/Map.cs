using static TheNornProject.Resources;

namespace TheNornProject
{
    class Map
    {
        private string name;
        private int size;
        private Sprite[,] data;

        public Map(string name, int size)
        {
            Name = name;
            Size = size;
            Data = new Sprite[size, size];
        }

        public string Name { get => name; set => name = value; }
        public int Size { get => size; set => size = value; }
        public Sprite[,] Data { get => data; set => data = value; }
    }
}
