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

        static class Globals
        {
            public static int score = 0;
        }

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

        static string random_story()
        {
            string[] stories = { "Suddenly a giant shadow covers you!\n",
                                "You see a bush rustle!\n",
                                "You see a shadow above, you look up...\n",
                                "You feel a presence behind you, you turn around!\n",
                                "You smell something attrocious...\n",
                                "You step on something... It's a tail!\n",
                                "You feel uneasy...\n",
                                "Your monsters start growling.\n",
                                "Shiver runs down your body...\n",
                                "You hear a menacing screach.\n",
                                "Ground shakes under your feet!\n",
                                "Startled birds fly away...\n",
                                "Pleasant aroma surrounds you...\n",
                                "Suddenly it's hard to breathe!\n",
                                "You feel dropplets of water on your skin.\n",
                                "There is a bright light!\n",
                                "The skies darken...\n",
                                "Intricate snowflakes land on your hair...\n",
                                "You feel crushing pressure!\n",
                                "A feeling of total dismay washes over you...\n",
                                "Claws dig in into your shoulder!\n",
                                "Dropplets of sweat form on your forehead...\n"
                                };
            string story_line = stories[roll_dice(0, stories.Length - 1)];
            return story_line;
        }

        static string random_adventure()
        {
            string[] adventures = { "You walk for hours, almost ready to turn back when: ",
                                   "You turn right, facing a stone cliff: ",
                                   "Your monster drags you forward: ",
                                   "You don't even take two steps when: ",
                                   "You feel like you walk in a circle through fog: ",
                                   "You sit down to shut your eyes for a couple minutes, when: ",
                                   "You backtrack through the field: ",
                                   "You enter a dark forest: ",
                                   "You sit down to sketch your previous foe when: ",
                                   "You run and run, finally you stop, but: ",
                                   "You find a hollow tree and hope to hide in it, however: ",
                                   "You climb a thick oak, finally stopping to take a breath on a big branch when: ",
                                   "You aren't careful and in your hurry slip and slide into a ravine: ",
                                   "It's getting dark and your feet hurt, you drag yourself to rest under a fallen tree, but: ",
                                   "You are out of water. You find a small stream. It gleams red, but thirst urges you to drink: ",
                                   "You turn left, sharp drop off is only steps away: "
                                  };
            string new_adv = adventures[roll_dice(0, adventures.Length - 1)];
            return new_adv;
        }

        static void output_monster(Monster monster)
        {
            Console.WriteLine("\nName: " + monster.name + "\nType: " + monster.type + "\nHealth: " + monster.health + "\nMoves: \n" + monster.attack_1_dec + "\n" + monster.attack_2_dec + "\n" + monster.attack_3_dec);
        }

        static bool encounter_main(Monster aquarex, Monster infernosaur, Monster pterowind)
        {
            Monster monster = random_monster();
            Console.WriteLine("\nA wild " + monster.name + " (" + monster.type + ") appears!\n");

            Monster player_monster = monster_picker(aquarex, infernosaur, pterowind);
            if(player_monster.name == "Invalid")
            {
                player_monster = monster_picker(aquarex, infernosaur, pterowind);
                Console.WriteLine("!!! Something went wrong !!!");
            }
            Console.WriteLine("\n" + player_monster.name + " go!");
            attack_main(monster, player_monster);
            if(aquarex.health <= 0 && infernosaur.health <= 0 && pterowind.health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static Monster monster_picker(Monster aquarex, Monster infernosaur, Monster pterowind)
        {
            Console.WriteLine("Which monster do you choose?");
            output_monster(aquarex);
            output_monster(infernosaur);
            output_monster(pterowind);
            Console.WriteLine("\nEnter Aquarex, Infernosaur or Pterowind: ");
            string pick = Console.ReadLine();
            while (pick != "Aquarex" && pick != "Infernosaur" && pick != "Pterowind" && pick != "aquarex" && pick != "infernosaur" && pick != "pterowind" && pick != "a" && pick != "i" && pick != "p")
            {
                Console.WriteLine("Invalid choice. Please enter monster's name to call it out (Aquarex, Infernosaur or Pterowind): ");
                pick = Console.ReadLine();
            }
            

            if (pick == "Aquarex" || pick == "aquarex" || pick == "a")
            {
                if (aquarex.fainted)
                {
                    Console.WriteLine("\n" + aquarex.name + " has fainted and can't fight anymore... Pick a different monster.");
                    return monster_picker(aquarex, infernosaur, pterowind);
                }
                else
                {
                    return aquarex;
                }
            }
            else if (pick == "Infernosaur" || pick == "infernosaur" || pick == "i")
            {
                if (infernosaur.fainted)
                {
                    Console.WriteLine("\n" + infernosaur.name + " has fainted and can't fight anymore... Pick a different monster.");
                    return monster_picker(aquarex, infernosaur, pterowind);
                }
                else
                {
                    return infernosaur;
                }
            }
            else if (pick == "Pterowind" || pick == "pterowind" || pick == "p")
            {
                if (pterowind.fainted)
                {
                    Console.WriteLine("\n" + pterowind.name + " has fainted and can't fight anymore... Pick a different monster.");
                    return monster_picker(aquarex, infernosaur, pterowind);
                }
                else
                {
                    return pterowind;
                }
            }
            else
            {
                
            }
            Console.WriteLine("An error has occured. Pick was: " + pick);
            Monster empty_monster = new Monster();
            empty_monster.name = "Invalid";
            return empty_monster;
            
        }

        static void attack_main(Monster monster, Monster player_monster)
        {
            Console.WriteLine("\nYour turn!");
            Console.WriteLine("\n" + player_monster.name + "'s health: " + player_monster.health);
            string move;
            Console.WriteLine("Which move should " + player_monster.name + " use?");

            Console.WriteLine("\n1. " + player_monster.attack_1_dec + "\n2. " + player_monster.attack_2_dec + "\n3. " + player_monster.attack_3_dec + "\nInput move number (1, 2 or 3): ");
            move = Console.ReadLine();
            while (move != "1" && move != "2" && move != "3") {
                Console.WriteLine("Invalid choice. Which move should " + player_monster.name + " use (1, 2, or 3)?");

                Console.WriteLine("\n1. " + player_monster.attack_1_dec + "\n2. " + player_monster.attack_2_dec + "\n3. " + player_monster.attack_3_dec + "\nInput move number (1, 2 or 3): ");
                move = Console.ReadLine();
            }

            if(move=="1")
            {
                Console.WriteLine("\n" + player_monster.name + " used " + player_monster.attack_1_dec + ".");
                monster.health = attack(player_monster.attack1_1, player_monster.attack1_2, monster.health);
            }
            else if(move=="2")
            {
                Console.WriteLine("\n" + player_monster.name + " used " + player_monster.attack_2_dec + ".");
                monster.health = attack(player_monster.attack2_1, player_monster.attack2_2, monster.health);
            }
            else if(move=="3")
            {
                Console.WriteLine("\n" + player_monster.name + " used " + player_monster.attack_3_dec + ".");
                int health_roll = roll_dice(player_monster.attack3_1, player_monster.attack3_2);
                if(player_monster.health != player_monster.init_health)
                {
                    if(player_monster.health + health_roll < player_monster.init_health)
                    {
                        player_monster.health -= health_roll;
                        Console.WriteLine("\n" + player_monster.name + "'s health: " + player_monster.health);
                    }
                    else if(player_monster.health + health_roll >= player_monster.init_health)
                    {
                        player_monster.health = player_monster.init_health;
                        Console.WriteLine("\n" + player_monster.name + " is back to full health!");
                    }
                }
                else
                {
                    Console.WriteLine("\nUsed a healing move, but already at full health...");
                }
            }

            if(monster.health <= 0)
            {
                Console.WriteLine("\n" + monster.name + " has been defeated!\n You move on.");
                if(monster.init_health < 100)
                {
                    Console.WriteLine("\nScore + 5");
                    Globals.score += 5;
                }
                else if (monster.init_health < 120)
                {
                    Console.WriteLine("\nScore + 10");
                    Globals.score += 10;
                }
                else if (monster.init_health < 135)
                {
                    Console.WriteLine("\nScore + 15");
                    Globals.score += 15;
                }
                else
                {
                    Console.WriteLine("\nScore + 20");
                    Globals.score += 20;
                }
                Console.WriteLine("\nCurrent Score: " + Globals.score);
            }
            else
            {
                Console.WriteLine("\n" + monster.name + "'s turn...");
                int monster_move = roll_dice(1, 3);
                if(monster_move == 1)
                {
                    Console.WriteLine("\n" + monster.name + " used a short range attack!");
                    player_monster.health = monster_attack(monster.attack1_1, monster.attack1_2, player_monster.health);
                    if(player_monster.health < 0)
                    {
                        player_monster.health = 0;
                    }
                }
                else if(monster_move == 2)
                {
                    Console.WriteLine("\n" + monster.name + " used a long range attack!");
                    player_monster.health = monster_attack(monster.attack2_1, monster.attack2_2, player_monster.health);
                    if (player_monster.health < 0)
                    {
                        player_monster.health = 0;
                    }
                }
                else if(monster_move == 3)
                {
                    if(monster.health == monster.init_health)
                    {
                        Console.WriteLine("\n" + monster.name + " used a healing move, but already at full health...");
                    }
                    else if(monster.health != monster.init_health)
                    {
                        int monster_healing_roll = roll_dice(monster.attack3_1, monster.attack3_2);
                        if(monster.health + monster_healing_roll < monster.init_health)
                        {
                            monster.health += monster_healing_roll;
                            Console.WriteLine("\n" + monster.name + "'s health: " + monster.health);
                        }
                        else
                        {
                            monster.health = monster.init_health;
                            Console.WriteLine("\n" + monster.name + " is back to full health...");
                        }
                    }
                }

                if(player_monster.health <= 0)
                {
                    Console.WriteLine("\n" + player_monster.name + " has been defeated...");
                    player_monster.fainted = true;
                    Console.WriteLine("\nScore - 10\nCurrent Score: " + Globals.score);
                    if(Globals.score > 0)
                    {
                        if(Globals.score - 10 > 0)
                        {
                            Globals.score -= 10;
                        }
                        else
                        {
                            Globals.score = 0;
                        }
                    }
                    Console.WriteLine("\nYou ran away!");
                }
                else
                {
                    attack_main(monster, player_monster);
                }
            }
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

            Console.WriteLine("You walk into the field:");
            Console.WriteLine("\n"+ random_adventure() + random_story());
            bool all_down = encounter_main(aquarex, infernosaur, pterowind);
        
            Console.WriteLine("\n----------------------------------------------------\n\nContinue your adventure? (Yes or No)\n");
            string cont = Console.ReadLine();
            
            if(cont == "Yes" || cont == "yes" || cont == "y" || cont == "Y")
            {
                Console.WriteLine("\n----------------------------------------------------\n");
                bool continue_game = true;
                while(continue_game)
                {
                    Console.WriteLine("\n----------------------------------------------------\n");
                    Console.WriteLine("\n" + random_adventure() + random_story());
                    all_down = encounter_main(aquarex, infernosaur, pterowind);
                    if(all_down)
                    {
                        continue_game = false;
                        Console.WriteLine("\nAll your monsters have fainted...");
                        Console.WriteLine("\nYou hurry to find your way home and call it a night.");
                    }
                    else
                    {
                        Console.WriteLine("\n----------------------------------------------------\n\nContinue your adventure? (Yes or No)\n");
                        cont = Console.ReadLine();
                        if (cont == "Yes" || cont == "yes" || cont == "y" || cont == "Y")
                        {
                            Console.WriteLine("\n----------------------------------------------------\n");
                            Console.WriteLine("\n" + random_adventure() + random_story());
                            continue_game = true;
                        }
                        else
                        {
                            continue_game = false;
                            Console.WriteLine("\nYou find your way home and call it a night.");
                        }
                    }
                }
            }

            else
            {
                Console.WriteLine("\nYou find your way home and call it a night.");
            }

            Console.ReadLine();
        }

        
    }
}
