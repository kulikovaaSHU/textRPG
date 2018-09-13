using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textRPG
{
    class Program
    {
        // Generate a random from clock to use for entire game.
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        // Simulates global variable score
        static class Globals
        {
            public static int score = 0;
        }

        // Stores ammounts of items in inventory
        static class Inventory
        {
            public static int potion = 0;
            public static int megaPotion = 0;
            public static int healAllPotion = 0;
        }

        // Stores info about Potion item
        static class Potion
        {
            public const int healAmount = 25;
            public const int probability = 50;
        }

        // Stores info about Mega Potion item
        static class MegaPotion
        {
            public const int healAmount = 50;
            public const int probability = 25;
        }

        // Stores info about HealAll Potion item
        static class HealAllPotion
        {
            public const int healAmount = 120;
            public const int probability = 5;
        }

        // Lists current inventory contents
        static void inventory_listing()
        {
            Console.WriteLine("\nInventory:\n1. Potions:          " + Inventory.potion + " (" + Potion.healAmount + " heal)\n2. Mega Potions:     " + Inventory.megaPotion + " (" + MegaPotion.healAmount + " heal)\n3. HealAll Potions:  " + Inventory.healAllPotion + " (" + HealAllPotion.healAmount + " heal)\n");
        }

        // A function to generate a random item dropped upon defeating an enemy
        static void random_item_drop()
        {
            // Potion is more likely to drop than mega potion, which is more likely than healall potion. Sometimes nothing drops.
            int roll = roll_dice(0, 100);
            if (roll <= 65 && roll > 20)
            {
                Inventory.potion += 1;
                Console.WriteLine("A Potion was dropped. You pick it up and put it in your inventory.\n");
            }
            else if (roll > 65 && roll <= 95)
            {
                Inventory.megaPotion += 1;
                Console.WriteLine("A Mega Potion was dropped. You pick it up and put it in your inventory.\n");
            }
            else if (roll > 95)
            {
                Inventory.healAllPotion += 1;
                Console.WriteLine("A HealAll Potion was dropped. You pick it up and put it in your inventory.\n");
            }
            else if (roll <= 20)
            {
                Console.WriteLine("Monster didn't drop anything.\n");
            }
        }

        // Function for player's monster attack
        static int attack(int damage1, int damage2, int enemy_health)
        {
            int damage = roll_dice(damage1, damage2);
            enemy_health -= damage;
            Console.WriteLine("\nDealt " + Convert.ToString(damage) + " damage.");
            return enemy_health;
        }

        // Function for enemy attack
        static int monster_attack(int damage1, int damage2, int monster_health)
        {
            int damage = roll_dice(damage1, damage2);
            monster_health -= damage;
            Console.WriteLine("\nTook " + Convert.ToString(damage) + " damage.");
            return monster_health;
        }

        // Randomized name made of prefix and suffix for monster
        static string random_name()
        {
            string[] name_prefix = {"Fire","Frost","Wind","Aqua","Wild","Dark","Shadow","Devil","Evil","Spark","Ptero","Terano","Electro","Stego","Mega","Super","Free","Flex","Stone","Ground","Brave","Blood","Carve","Bone","Beak","Fast","Fright","Moon","Mad","Keen","Claw"};
            string[] name_suffix = {"rex","osaur","es","saur","husk","devil","aur","bor","bird","zard","snake","raptor","sire","sear","fly","dive","crave","dial","dex","lex","ier","tear","cry"};
            int pick1 = roll_dice(0, name_prefix.Length-1);
            int pick2 = roll_dice(0, name_suffix.Length-1);
            string name = name_prefix[pick1] + name_suffix[pick2];
            return name;
        }

        // Random type assignment for monster
        static string random_type()
        {
            string[] types = {"Fire","Frost","Ice","Shadow","Evil","Dark","Grass","Water","Bug","Amphibian","Lava","Space","Dinosaur","Magic","Flying","Wind","Fighting"};
            int pick = roll_dice(0, types.Length-1);
            string monster_type = types[pick];
            return monster_type;
        }

        // Random number generator
        static int roll_dice(int min, int max)
        {
            lock (syncLock)
            {
                int new_roll = random.Next(min, max + 1);
                return new_roll;
            }
        }

        // Random monster generator
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

        // Random story generator. Second part
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

        // Random adventure move generator. First part
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

        // Outputs all information on passed monster. 
        static void output_monster(Monster monster)
        {
            Console.WriteLine("\nName: " + monster.name + "\nType: " + monster.type + "\nHealth: " + monster.health + "\nMoves: \n" + monster.attack_1_dec + "\n" + monster.attack_2_dec + "\n" + monster.attack_3_dec);
        }

        // Main encounter generation function
        static bool encounter_main(Monster aquarex, Monster infernosaur, Monster pterowind)
        {
            // Calls random_monster() to get a new monster for encounter
            Monster monster = random_monster();
            Console.WriteLine("\nA wild " + monster.name + " (" + monster.type + ") appears!\n");
            // Prompt player to pick monster
            Monster player_monster = monster_picker(aquarex, infernosaur, pterowind);
            // If error somehow occurs try again
            while(player_monster.name == "Invalid")
            {
                Console.WriteLine("!!! Something went wrong !!!");
                player_monster = monster_picker(aquarex, infernosaur, pterowind);
            }
            // Valid pick
            Console.WriteLine("\n" + player_monster.name + " go!");
            // Go to attack
            attack_main(monster, player_monster);
            // If all player monsters have fainted, return true to end game.
            if(aquarex.health <= 0 && infernosaur.health <= 0 && pterowind.health <= 0)
            {
                return true;
            }
            // Else return false to allow game to continue.
            else
            {
                return false;
            }
        }

        // Allows user to pick an unfainted monster
        static Monster monster_picker(Monster aquarex, Monster infernosaur, Monster pterowind)
        {
            // Prompt for player's pick of monster. Output all monsters and their data
            Console.WriteLine("Which monster do you choose?");
            output_monster(aquarex);
            output_monster(infernosaur);
            output_monster(pterowind);
            Console.WriteLine("\nEnter Aquarex, Infernosaur or Pterowind: ");
            string pick = Console.ReadLine();
            // Ensures valid input
            while (pick != "Aquarex" && pick != "Infernosaur" && pick != "Pterowind" && pick != "aquarex" && pick != "infernosaur" && pick != "pterowind" && pick != "a" && pick != "i" && pick != "p")
            {
                Console.WriteLine("Invalid choice. Please enter monster's name to call it out (Aquarex, Infernosaur or Pterowind): ");
                pick = Console.ReadLine();
            }
            
            // Aquarex picked
            if (pick == "Aquarex" || pick == "aquarex" || pick == "a")
            {   
                // Aquarex is fainted
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
            // Infernosaur picked
            else if (pick == "Infernosaur" || pick == "infernosaur" || pick == "i")
            {
                // Infernosaur is fainted
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
            // Pterowind picked
            else if (pick == "Pterowind" || pick == "pterowind" || pick == "p")
            {
                // Pterowind is fainted
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
            
            // Somehow error occured
            Console.WriteLine("An error has occured. Pick was: " + pick);
            Monster empty_monster = new Monster();
            empty_monster.name = "Invalid";
            return empty_monster;
            
        }

        // Function to pick and use an item from inventory
        static Monster use_item(Monster monster)
        {
            //List inventory
            inventory_listing();
            Console.WriteLine("Which item do you want to use? (1,2 or 3)");
            string itemToUse = Console.ReadLine();
            // Player is out of item, wastes a move
            if ((itemToUse == "1" && Inventory.potion <= 0) || (itemToUse == "2" && Inventory.megaPotion <= 0) || (itemToUse == "3" && Inventory.healAllPotion <= 0))
            {
                Console.WriteLine("\nYou are out of that item. You waste time looking for the item and lose your turn.");
            }
            // Player uses item
            else
            {
                // Use potion
                if (itemToUse == "1")
                {
                    // Back to full health
                    if (monster.health + Potion.healAmount >= monster.init_health)
                    {
                        monster.health = monster.init_health;
                        Inventory.potion -= 1;
                        Console.WriteLine("\nUsed a Potion on " + monster.name + ". Back to full health.\n");
                    }
                    // Partial heal
                    else if (monster.health + Potion.healAmount < monster.init_health)
                    {
                        monster.health += Potion.healAmount;
                        Console.WriteLine("\nUsed a Potion on " + monster.name + ". Current health: " + monster.health + "\n");
                    }
                }
                // Use mega potion
                else if (itemToUse == "2")
                {
                    // Back to full health
                    if (monster.health + MegaPotion.healAmount >= monster.init_health)
                    {
                        monster.health = monster.init_health;
                        Inventory.megaPotion -= 1;
                        Console.WriteLine("\nUsed a Mega Potion on " + monster.name + ". Back to full health.\n");
                    }
                    // Partial heal
                    else if (monster.health + MegaPotion.healAmount < monster.init_health)
                    {
                        monster.health += MegaPotion.healAmount;
                        Inventory.healAllPotion -= 1;
                        Console.WriteLine("\nUsed a Mega Potion on " + monster.name + ". Current health: " + monster.health + "\n");
                    }
                }
                // Use healall potion
                else if (itemToUse == "3")
                {
                    // Back to full health
                    if (monster.health + HealAllPotion.healAmount >= monster.init_health)
                    {
                        monster.health = monster.init_health;
                        Console.WriteLine("\nUsed a HealAll Potion on " + monster.name + ". Back to full health.\n");
                    }
                    // Partial heal
                    else if (monster.health + HealAllPotion.healAmount < monster.init_health)
                    {
                        monster.health += HealAllPotion.healAmount;
                        Console.WriteLine("\nUsed a HealAll Potion on " + monster.name + ". Current health: " + monster.health + "\n");
                    }
                }
            }
            // return monster with new or unmodified health
            return monster;
        }

        // Main attack phase function
        static void attack_main(Monster monster, Monster player_monster)
        {
            // Player's turn
            Console.WriteLine("\nYour turn!");
            Console.WriteLine("\n" + player_monster.name + "'s health: " + player_monster.health);
            // See if user wants to use an item or a move
            Console.WriteLine("Use item or a move? Enter i to open inventory or m to pick a move: ");
            string pick = Console.ReadLine();
            // If picked item, call use_item()
            if(pick == "i" || pick == "item" || pick == "inventory")
            {
                player_monster = use_item(player_monster);
            }
            // Otherwise pick a move
            else
            {
                string move;
                Console.WriteLine("Which move should " + player_monster.name + " use?");
                Console.WriteLine("\n1. " + player_monster.attack_1_dec + "\n2. " + player_monster.attack_2_dec + "\n3. " + player_monster.attack_3_dec + "\nInput move number (1, 2 or 3): ");
                // Which move should player monster use? 
                move = Console.ReadLine();
                // Ensure valid input
                while (move != "1" && move != "2" && move != "3")
                {
                    Console.WriteLine("Invalid choice. Which move should " + player_monster.name + " use (1, 2, or 3)?");

                    Console.WriteLine("\n1. " + player_monster.attack_1_dec + "\n2. " + player_monster.attack_2_dec + "\n3. " + player_monster.attack_3_dec + "\nInput move number (1, 2 or 3): ");
                    move = Console.ReadLine();
                }
                //  Picked first move
                if (move == "1")
                {
                    Console.WriteLine("\n" + player_monster.name + " used " + player_monster.attack_1_dec + ".");
                    monster.health = attack(player_monster.attack1_1, player_monster.attack1_2, monster.health);
                }
                // Picked second move
                else if (move == "2")
                {
                    Console.WriteLine("\n" + player_monster.name + " used " + player_monster.attack_2_dec + ".");
                    monster.health = attack(player_monster.attack2_1, player_monster.attack2_2, monster.health);
                }
                // Picked the healing move
                else if (move == "3")
                {
                    Console.WriteLine("\n" + player_monster.name + " used " + player_monster.attack_3_dec + ".");
                    int health_roll = roll_dice(player_monster.attack3_1, player_monster.attack3_2);
                    // Not at full health
                    if (player_monster.health != player_monster.init_health)
                    {
                        // Player monster heals not to full health
                        if (player_monster.health + health_roll < player_monster.init_health)
                        {
                            player_monster.health -= health_roll;
                            Console.WriteLine("\n" + player_monster.name + "'s health: " + player_monster.health);
                        }
                        // Player monster heals to full health
                        else if (player_monster.health + health_roll >= player_monster.init_health)
                        {
                            player_monster.health = player_monster.init_health;
                            Console.WriteLine("\n" + player_monster.name + " is back to full health!");
                        }
                    }
                    // Already at full health
                    else
                    {
                        Console.WriteLine("\nUsed a healing move, but already at full health...");
                    }
                }
            }
            

            if(monster.health <= 0)
            {
                Console.WriteLine("\n" + monster.name + " has been defeated!\n");
                random_item_drop();
                Console.WriteLine("\nYou move on.");
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

        // Main function, established the player monsters and calls for more encounters or exits game.
        static void Main(string[] args)
        {
            // Player monsters
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
            // An extra potion player gets in the beginning
            Inventory.potion += 1;
            // Introduction
            Console.WriteLine("\n <3 <3 <3 Loading <3 <3 <3 \n");
            Console.WriteLine("       Little Monsters\n\n");

            Console.WriteLine("You walk into the field:");
            // Random story generated
            Console.WriteLine("\n"+ random_adventure() + random_story());
            // Bool to track if all monsters have fainted
            bool all_down = encounter_main(aquarex, infernosaur, pterowind);
            // Check if user wants to continue
            Console.WriteLine("\n----------------------------------------------------\n\nContinue your adventure? (Yes or No)\n");
            string cont = Console.ReadLine();
            // Generate a new encounter
            if(cont == "Yes" || cont == "yes" || cont == "y" || cont == "Y")
            {
                bool continue_game = true;
                // While user chooses to continue, game continues, unless all monsters have fainted
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
            // Quit
            else
            {
                Console.WriteLine("\nYou find your way home and call it a night.");
            }

            Console.ReadLine();
        }

        
    }
}
