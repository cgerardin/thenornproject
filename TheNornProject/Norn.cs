using System;
using System.Diagnostics;
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

        private int target_x, target_y;
        private int old_x, old_y;

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
            Hunger = 80;

            last_direction = 0;
            steps_count = 0;
        }

        public void Live(Map environment)
        {

            if (!IsAlive()) return;

            Environment = environment;

            // Age
            real_age += 0.05;
            Age = Convert.ToInt32(Math.Truncate(real_age));
            if (Age > 30) // Durée de vie en mois : un peu moins de 3 ans.
            {
                Life--;
            }

            // Look
            Look();

            // Hunger
            Hunger--;
            if (Hunger < 0)
            {
                Hunger = 0;
                Life--;
            }
            else if (!IsHungry() && IsHurt())
            {
                Life++;
            }

            // Life
            if (Life <= 0)
            {
                Die();
            }

        }

        private void Look()
        {
            target_x = -1;
            target_y = -1;
            int envy = -2;
            for (int y = 0; y < 5; y++)
            {
                int envx = -2;
                for (int x = 0; x < 5; x++)
                {
                    if (Environment.Items[x, y] != null)
                    {
                        if (Environment.Items[x, y].Sprite == Sprite.object_meat && IsHungry())
                        {
                            Debug.WriteLine(Name + " : Look at this steak ! =)");
                            target_x = X + envx;
                            target_y = Y + envy;
                        }
                    }
                    envx++;
                }
                envy++;
            }

            if (IsChasing() || IsHungry())
            {
                Move();
            }
            else if (!IsChasing())
            {
                //
            }

        }

        private void Move()
        {
            old_x = X;
            old_y = Y;

            if (IsChasing())
            {
                if (target_x == X && target_y == Y && IsHungry())
                {
                    Eat();
                    last_direction = 0;
                    return; // Dodo !
                }
                else
                {
                    if (target_x > X && IsDirectionSafe(1)) last_direction = 1;
                    else if (target_x < X && IsDirectionSafe(2)) last_direction = 2;
                    else if (target_y > Y && IsDirectionSafe(3)) last_direction = 3;
                    else if (target_y < Y && IsDirectionSafe(4)) last_direction = 4;
                }
            }
            else
            {
                if (last_direction == 0 || steps_count > 5)
                {
                    steps_count = 0;
                    last_direction = God.Next(1, 5);
                }
            }

            while (!IsDirectionSafe(last_direction))
            {
                last_direction = God.Next(1, 5);
            }

            switch (last_direction)
            {
                case 1:
                    X += 1;
                    break;

                case 2:
                    X -= 1;
                    break;

                case 3:
                    Y += 1;
                    break;

                case 4:
                    Y -= 1;
                    break;
            }

            steps_count++;
        }

        private void Eat()
        {
            Program.DeleteItem(target_x, target_y);
            target_x = -1;
            target_y = -1;
            Hunger = 100;
            if (Sex)
            {
                Sprite = Sprite.norn_male;
            }
            else
            {
                Sprite = Sprite.norn_female;
            }
            Debug.WriteLine(Name + " eats !");
        }

        private void Die()
        {
            Life = -1;
            Sprite = Sprite.norn_dead;
            Debug.WriteLine(Name + " dies !");
        }

        public bool IsAlive()
        {
            return Life > -1;
        }

        public bool IsHungry()
        {
            return Hunger < 50;
        }

        public bool IsHurt()
        {
            return Life < 100;
        }

        public bool IsChasing()
        {
            return target_x != -1;
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

        private bool IsDirectionSafe(int direction)
        {
            switch (last_direction)
            {
                case 1:
                    if (Environment.Sprites[3, 2] != Sprite.terrain_wall && Environment.Sprites[3, 2] != Sprite.terrain_empty)
                    {
                        return true;
                    }
                    break;

                case 2:
                    if (Environment.Sprites[1, 2] != Sprite.terrain_wall && Environment.Sprites[3, 2] != Sprite.terrain_empty)
                    {
                        return true;
                    }
                    break;

                case 3:
                    if (Environment.Sprites[2, 3] != Sprite.terrain_wall && Environment.Sprites[3, 2] != Sprite.terrain_empty)
                    {
                        return true;
                    }
                    break;

                case 4:
                    if (Environment.Sprites[2, 1] != Sprite.terrain_wall && Environment.Sprites[3, 2] != Sprite.terrain_empty)
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

    }
}
