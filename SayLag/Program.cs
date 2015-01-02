using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace SayLag
{
    class Program
    {
        private static Obj_AI_Hero player;
        private static int deathCount;
        private static float tickLastDeath;
        private static bool lagSaid;
        private static Menu menu;
        private static bool incomplete;
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += GameLoad;
        }

        static void GameLoad(EventArgs args)
        {
            player = ObjectManager.Player;

            lagSaid = true;
            deathCount = player.Deaths;
            incomplete = false;

            //Initialize Menu

            menu = new Menu("SayLag", "SayLag", true);
            menu.AddItem(new MenuItem("alwaysLag", "Always Lag").SetValue(true));
            menu.AddItem(new MenuItem("allChat", "In All chat").SetValue(false));
            menu.AddItem(new MenuItem("randomG", "Add random Number of gs").SetValue(false));
            menu.AddToMainMenu();

            Game.OnGameUpdate += GameUpdateCheck;


            Game.PrintChat("<font color = \"#D6B600\">Say Lag by ItsNotMeDos</font>"); // D6B600
        }

        static void CustomSayAll(string whatToSay)
        {
            if (menu.Item("allChat").GetValue<bool>()) // all chat or no all chat
            {
                Game.Say("/all " + whatToSay);
            }
            else
            {
                Game.Say(whatToSay);
            }
        }

        static string AddGs(string whatToSay)
        {
            var random = new Random();
            var randomNumber = random.Next(4);
            for (var i = 0; i != randomNumber; i++)
            {
                var randomNumber3 = random.Next(2);
                if (randomNumber3 == 0)
                {
                    whatToSay = whatToSay + "g";
                }
                else
                {
                    whatToSay = whatToSay + "G";
                }
            }
            return whatToSay;

        }

        static void GameUpdateCheck(EventArgs args)
        {

            string whatToSay;

            if (player.IsDead)
            {
                
                if (player.Deaths > deathCount)
                {
                    deathCount = player.Deaths;
                    tickLastDeath = Game.Time;
                    lagSaid = false;
                }
            }

            if (incomplete && (tickLastDeath + 3.5f) <= Game.Time)
            {
                whatToSay = "lag ";
                incomplete = false;
                if (menu.Item("randomG").GetValue<bool>()) // write gg
                {
                    whatToSay = AddGs(whatToSay);
                }
                 CustomSayAll(whatToSay);
            }

            if ((tickLastDeath + 1.5f) <= Game.Time && !lagSaid && !incomplete)
            {
                lagSaid = true;
                var random = new Random();
                var randomNumber = random.Next(menu.Item("alwaysLag").GetValue<bool>() ? 7 : 14);


                switch (randomNumber)
                {
                    case 0:
                        whatToSay = "lag";
                        break;
                    case 1:
                        whatToSay = "omg lag";
                        break;
                    case 2:
                        whatToSay = "omg Rito fix lag";
                        break;
                    case 3:
                        whatToSay = "this lag";
                        break;
                    case 4:
                        whatToSay = "-.- lag";
                        break;
                    case 5:
                        whatToSay = "god dis lags!!!!11";
                        break;
                    case 6:
                        whatToSay = "la";
                        incomplete = true;
                        break;
                    default:
                        whatToSay = "";
                        break;
                }

                if (whatToSay != "" && menu.Item("randomG").GetValue<bool>() && incomplete == false) // write gg
                {
                    whatToSay = whatToSay + " ";
                    whatToSay = AddGs(whatToSay);
                }

                

                if (randomNumber < 7)
                {
                    CustomSayAll(whatToSay);
                }

            }

        }
    }
}
