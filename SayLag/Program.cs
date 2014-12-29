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
        private static bool incomplete;
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += GameLoad;
        }

        static void GameLoad(EventArgs args)
        {
            Player = ObjectManager.Player;

            lagSaid = true;
            DeathCount = Player.Deaths;
            incomplete = false;

            //Initialize Menu

            Menu = new Menu("SayLag", "SayLag", true);
            Menu.AddItem(new MenuItem("alwaysLag", "Always Lag").SetValue(true));
            Menu.AddItem(new MenuItem("allChat", "In All chat").SetValue(false));
            Menu.AddItem(new MenuItem("randomG", "Add random Number of gs").SetValue(false));
            Menu.AddToMainMenu();

            LeagueSharp.Game.OnGameUpdate += GameUpdateCheck;


            Game.PrintChat("<font color = \"#D6B600\">Say Lag by ItsNotMeDos</font>"); // D6B600
        }

        static void CustomSayAll(string WhatToSay)
        {
            if (Menu.Item("allChat").GetValue<bool>()) // all chat or no all chat
            {
                Game.Say("/all " + WhatToSay);
            }
            else
            {
                Game.Say(WhatToSay);
            }
        }

        static string AddGs(string WhatToSay)
        {
            Random Random = new Random();
            int randomNumber = Random.Next(4);
            for (int i = 0; i != randomNumber; i++)
            {
                int randomNumber3 = Random.Next(2);
                if (randomNumber3 == 0)
                {
                    WhatToSay = WhatToSay + "g";
                }
                else
                {
                    WhatToSay = WhatToSay + "G";
                }
            }
            return WhatToSay;

        }

        static void GameUpdateCheck(EventArgs args)
        {

            string WhatToSay;

            if (Player.IsDead)
            {
                
                if (Player.Deaths > DeathCount)
                {
                    DeathCount = Player.Deaths;
                    tickLastDeath = Game.Time;
                    lagSaid = false;
                }
            }

            if (incomplete == true && (tickLastDeath + 3.5f) <= Game.Time)
            {
                WhatToSay = "lag ";
                incomplete = false;
                if (Menu.Item("randomG").GetValue<bool>()) // write gg
                {
                    WhatToSay = AddGs(WhatToSay);
                }
                 CustomSayAll(WhatToSay);
            }

            if ((tickLastDeath + 1.5f) <= Game.Time && lagSaid == false && incomplete == false)
            {
                lagSaid = true;
                Random Random = new Random();
                int randomNumber;
                if (Menu.Item("alwaysLag").GetValue<bool>())
                {
                    randomNumber = Random.Next(7);
                }
                else
                {
                    randomNumber = Random.Next(14);
                }
                

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
                else if (randomNumber == 4)
                {
                    WhatToSay = "-.- lag";
                }
                else if (randomNumber == 5)
                {
                    WhatToSay = "god dis lags!!!!11";
                }
                else if (randomNumber == 6)
                {
                    WhatToSay = "la";
                    incomplete = true;
                }
                else
                {
                    WhatToSay = "";
                }

                if (WhatToSay != "" && Menu.Item("randomG").GetValue<bool>() && incomplete == false) // write gg
                {
                    int randomNumber2 = Random.Next(4);
                    WhatToSay = WhatToSay + " ";
                    WhatToSay = AddGs(WhatToSay);
                }

                

                if (randomNumber < 7)
                {
                    CustomSayAll(WhatToSay);
                }

            }

        }
    }
}
