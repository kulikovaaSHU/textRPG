using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textRPG
{
    class Program
    {
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
            aquarex.name = "Infernosaur";
            aquarex.type = "Fire";
            aquarex.health = 90;
            aquarex.attack1_1 = 18;
            aquarex.attack1_2 = 23;
            aquarex.attack_1_dec = "Spark: 18-23 damage";
            aquarex.attack2_1 = 15;
            aquarex.attack2_2 = 33;
            aquarex.attack_2_dec = "Ignite: 18-30 damage";
            aquarex.attack3_1 = 6;
            aquarex.attack3_2 = 10;
            aquarex.attack_3_dec = "Rekindle: 6-10 heal";
            aquarex.init_health = 90;
            aquarex.fainted = false;

            Monster pterowind = new Monster();
            aquarex.name = "Pterowind";
            aquarex.type = "Wind";
            aquarex.health = 100;
            aquarex.attack1_1 = 18;
            aquarex.attack1_2 = 20;
            aquarex.attack_1_dec = "Ruffle: 18-20 damage";
            aquarex.attack2_1 = 15;
            aquarex.attack2_2 = 30;
            aquarex.attack_2_dec = "Sky Dive: 15-30 damage";
            aquarex.attack3_1 = 6;
            aquarex.attack3_2 = 12;
            aquarex.attack_3_dec = "Nest: 6-12 heal";
            aquarex.init_health = 100;
            aquarex.fainted = false;
        }
    }
}
