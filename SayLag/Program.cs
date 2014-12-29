using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace SayLag
{
    class Program
    {
        private static Obj_AI_Hero Player;
        private static int DeathCount;
        private static float tickLastDeath;
        private static bool lagSaid;
        private static Menu Menu;
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += GameLoad;
        }

        static void GameLoad(EventArgs args)
        {
            Player = ObjectManager.Player;

            lagSaid = true;
            DeathCount = Player.Deaths;

            //Initialize Menu

            Menu = new Menu("SayLag", "SayLag", true);
            Menu.AddItem(new MenuItem("alwaysLag", "Always Lag").SetValue(true));
            Menu.AddItem(new MenuItem("allChat", "In All chat").SetValue(false));
            Menu.AddToMainMenu();

            LeagueSharp.Game.OnGameUpdate += GameUpdateCheck;


            Game.PrintChat("<font color = \"#D6B600\">Say Lag by ItsNotMeDos</font>"); // D6B600
        }

        static void GameUpdateCheck(EventArgs args)
        {

            if (Player.IsDead)
            {
                
                if (Player.Deaths > DeathCount)
                {
                    DeathCount = Player.Deaths;
                    tickLastDeath = Game.Time;
                    lagSaid = false;
                }
            }

            if ((tickLastDeath + 1f) <= Game.Time && lagSaid == false)
            {
                lagSaid = true;
                Random Random = new Random();
                int randomNumber;
                if (Menu.Item("alwaysLag").GetValue<bool>())
                {
                    randomNumber = Random.Next(4);
                }
                else
                {
                    randomNumber = Random.Next(8);
                }

                string WhatToSay;

                if (randomNumber == 0)
                {
                    WhatToSay = "lag";
                }
                else if (randomNumber == 1)
                {
                    WhatToSay = "omg lag";
                }
                else if (randomNumber == 2)
                {
                    WhatToSay = "omg Rito fix lag";
                }
                else if (randomNumber == 3)
                {
                    WhatToSay = "this lag";
                }
                else
                {
                    WhatToSay = "";
                }

                if (randomNumber < 4)
                {
                    if (Menu.Item("allChat").GetValue<bool>())
                    {
                        Game.Say("/all " + WhatToSay);
                    }
                    else
                    {
                        Game.Say(WhatToSay);
                    }
                }
                else
                {

                }
            }

        }
    }
}
