using System;
using static TheNornProject.Resources;

namespace TheNornProject
{
    class Norn : Item
    {

        private int age;
        private bool sex;
        private double real_age;
        private int life;
        private int hunger;

        private Map environment;
        private int last_direction;
        private int steps_count;

        public Norn(string name, bool sex, int x, int y) : base(name, x, y)
        {
            Age = 0;
            Sex = sex;
            if (Sex)
            {
                Sprite = Sprite.norn_male;
            }
            else
            {
                Sprite = Sprite.norn_female;
            }
            real_age = 0;
            Life = 100;
            Hunger = 60;

            last_direction = 0;
            steps_count = 0;
        }

        public void Live(Map environment)
        {

            if (!isAlive()) return;

            Environment = environment;

            // Age
            real_age += 0.05;
            Age = Convert.ToInt32(Math.Truncate(real_age));
            if (Age > 8)
            {
                Life--;
            }

            // Hunger
            Hunger--;
            if (Hunger < 50)
            {
                if (Hunger < 0)
                {
                    Hunger = 0;
                    Life--;
                }
                Move();
            }

            // Life
            if (Life <= 0)
            {
                Die();
            }

        }

        private void Look()
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (Environment.Data[x, y] == Sprite.object_meat)
                    {

                    }
                }
            }
        }

        private void Move()
        {

            if (last_direction == 0 || steps_count > 5)
            {
                last_direction = God.Next(1, 5);
                steps_count = 0;
            }

            switch (last_direction)
            {
                case 1:
                    if (Environment.Data[3, 2] != Sprite.terrain_wall && Environment.Data[3, 2] != Sprite.terrain_empty)
                    {
                        X += 32;
                    }
                    else
                    {
                        last_direction = 0;
                    }
                    break;

                case 2:
                    if (Environment.Data[1, 2] != Sprite.terrain_wall && Environment.Data[3, 2] != Sprite.terrain_empty)
                    {
                        X -= 32;
                    }
                    else
                    {
                        last_direction = 0;
                    }
                    break;

                case 3:
                    if (Environment.Data[2, 3] != Sprite.terrain_wall && Environment.Data[3, 2] != Sprite.terrain_empty)
                    {
                        Y += 32;
                    }
                    else
                    {
                        last_direction = 0;
                    }
                    break;

                case 4:
                    if (Environment.Data[2, 1] != Sprite.terrain_wall && Environment.Data[3, 2] != Sprite.terrain_empty)
                    {
                        Y -= 32;
                    }
                    else
                    {
                        last_direction = 0;
                    }
                    break;
            }

            steps_count++;
        }

        private void Eat()
        {

        }

        private void Die()
        {
            Life = -1;
            Sprite = Sprite.norn_dead;
        }

        public bool isAlive()
        {

            return Life > -1;

        }

        public int Age { get => age; set => age = value; }
        public bool Sex { get => sex; set => sex = value; }
        public int Life { get => life; set => life = value; }
        public int Hunger { get => hunger; set => hunger = value; }
        internal Map Environment { get => environment; set => environment = value; }

        public static string GenerateNornName(bool sex)
        {
            Random rnd = new Random();
            string name = "";

            string[] ta = { "di", "do", "da", "li", "lo", "la", "ji", "jo", "ja", "pi", "po", "pa", "ci", "co", "ca" };
            string[] tb = { "ri", "ro", "ra", "ti", "to", "ta", "zi", "zo", "za", "ki", "ko", "ka" };
            string[] tc = { "bib", "lib", "rib", "zib", "bid", "lid", "rid", "zid", "bad", "lad", "rad", "zad" };

            int a = rnd.Next(0, ta.Length), b = rnd.Next(0, tb.Length), c = rnd.Next(0, tc.Length);

            name = ta[a];
            if (rnd.Next(0, 2) != 0) name += tb[b];
            name += tc[c];

            if (sex) name += "o";
            else name += "a";

            return name[0].ToString().ToUpper() + name.Substring(1).ToLower();
        }

    }
}
