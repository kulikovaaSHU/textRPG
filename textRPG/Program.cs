using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textRPG
{
    class Program
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        static int attack(int damage1, int damage2, int enemy_health)
        {
            int damage = roll_dice(damage1, damage2);
            enemy_health -= damage;
            Console.WriteLine("\nDealt " + Convert.ToString(damage) + " damage.");
            return enemy_health;
        }

        static int monster_attack(int damage1, int damage2, int monster_health)
        {
            int damage = roll_dice(damage1, damage2);
            monster_health -= damage;
            Console.WriteLine("\nTook " + Convert.ToString(damage) + " damage.");
            return monster_health;
        }

        static string random_name()
        {
            string[] name_prefix = {"Fire","Frost","Wind","Aqua","Wild","Dark","Shadow","Devil","Evil","Spark","Ptero","Terano","Electro","Stego","Mega","Super","Free","Flex","Stone","Ground","Brave","Blood","Carve","Bone","Beak","Fast","Fright","Moon","Mad","Keen","Claw"};
            string[] name_suffix = {"rex","osaur","es","saur","husk","devil","aur","bor","bird","zard","snake","raptor","sire","sear","fly","dive","crave","dial","dex","lex","ier","tear","cry"};
            int pick1 = roll_dice(0, name_prefix.Length-1);
            int pick2 = roll_dice(0, name_suffix.Length-1);
            string name = name_prefix[pick1] + name_suffix[pick2];
            return name;
        }

        static string random_type()
        {
            string[] types = {"Fire","Frost","Ice","Shadow","Evil","Dark","Grass","Water","Bug","Amphibian","Lava","Space","Dinosaur","Magic","Flying","Wind","Fighting"};
            int pick = roll_dice(0, types.Length-1);
            string monster_type = types[pick];
            return monster_type;
        }

        static int roll_dice(int min, int max)
        {
            lock (syncLock)
            {
                int new_roll = random.Next(min, max + 1);
                return new_roll;
            }
        }

        static Monster random_monster()
        {
            Monster monster = new Monster();
            monster.name = random_name();
            monster.type = random_type();
            monster.health = roll_dice(80, 150);
            monster.init_health = monster.health;
            monster.attack1_1 = roll_dice(10, 13);
            monster.attack1_2 = roll_dice(14, 25);
            monster.attack2_1 = roll_dice(13, 20);
            monster.attack2_2 = roll_dice(24, 35);
            monster.attack3_1 = roll_dice(5, 8);
            monster.attack3_2 = roll_dice(10, 12);
            return monster;
        }

        static void output_monster(Monster monster)
        {
            Console.WriteLine("\nName: " + monster.name + "\nType: " + monster.type + "\nHealth: " + monster.health + "\nMoves: \n" + monster.attack_1_dec + "\n" + monster.attack_2_dec + "\n" + monster.attack_3_dec);
        }

        static void Main(string[] args)
        {
            Monster aquarex = new Monster();
            aquarex.name = "Aquarex";
            aquarex.type = "Water";
            aquarex.health = 110;
            aquarex.attack1_1 = 15;
            aquarex.attack1_2 = 20;
            aquarex.attack_1_dec = "Splash: 15-20 damage";
            aquarex.attack2_1 = 10;
            aquarex.attack2_2 = 28;
            aquarex.attack_2_dec = "Wave: 10-28 damage";
            aquarex.attack3_1 = 11;
            aquarex.attack3_2 = 15;
            aquarex.attack_3_dec = "Refresh: 11-15 heal";
            aquarex.init_health = 110;
            aquarex.fainted = false;

            Monster infernosaur = new Monster();
            infernosaur.name = "Infernosaur";
            infernosaur.type = "Fire";
            infernosaur.health = 90;
            infernosaur.attack1_1 = 18;
            infernosaur.attack1_2 = 23;
            infernosaur.attack_1_dec = "Spark: 18-23 damage";
            infernosaur.attack2_1 = 15;
            infernosaur.attack2_2 = 33;
            infernosaur.attack_2_dec = "Ignite: 18-30 damage";
            infernosaur.attack3_1 = 6;
            infernosaur.attack3_2 = 10;
            infernosaur.attack_3_dec = "Rekindle: 6-10 heal";
            infernosaur.init_health = 90;
            infernosaur.fainted = false;

            Monster pterowind = new Monster();
            pterowind.name = "Pterowind";
            pterowind.type = "Wind";
            pterowind.health = 100;
            pterowind.attack1_1 = 18;
            pterowind.attack1_2 = 20;
            pterowind.attack_1_dec = "Ruffle: 18-20 damage";
            pterowind.attack2_1 = 15;
            pterowind.attack2_2 = 30;
            pterowind.attack_2_dec = "Sky Dive: 15-30 damage";
            pterowind.attack3_1 = 6;
            pterowind.attack3_2 = 12;
            pterowind.attack_3_dec = "Nest: 6-12 heal";
            pterowind.init_health = 100;
            pterowind.fainted = false;

            
            Console.WriteLine("\n <3 <3 <3 Loading <3 <3 <3 \n");
            Console.WriteLine("       Little Monsters\n\n");

            output_monster(aquarex);
            output_monster(infernosaur);
            output_monster(pterowind);


            Console.ReadLine();
        }

        
    }
}
