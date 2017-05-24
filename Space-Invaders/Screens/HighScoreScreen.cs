using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space_Invaders_Gsm
{
    class HighScoreScreen : MenuScreen
    {
        #region Initialization
        private string[] allPeople;
        private string[] allScore;

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public HighScoreScreen()
            : base("HighScore")
        {
            /*if (SQLCommand.Connect == false)
            {
                SQLCommand.connect("127.0.0.1", "spaceinvaders", "root", "");
            }

            if (SQLCommand.Connect)
            {
                allPeople = SQLCommand.SelectAllCommand("SELECT Name FROM spaceinvadershighscore ORDER BY score DESC");
                allScore = SQLCommand.SelectAllCommand("SELECT Score FROM spaceinvadershighscore ORDER BY score DESC");
            }

            MenuEntry[] entry = new MenuEntry[10];

            if (allPeople != null)
            {
                for (int i = 0; (i < 10) && (i < allPeople.Length); i++)
                {
                    entry[i] = new MenuEntry(i + "    " + allPeople[i] + "    " + allScore[i]);
                    MenuEntries.Add(entry[i]);
                }
            }

            // Create our menu entries.
            MenuEntry exitMenuEntry = new MenuEntry("Back");
            // Hook up menu event handlers.
            exitMenuEntry.Selected += BackToMainMenu;

            // Add entries to the menu.
            MenuEntries.Add(exitMenuEntry);*/
        }

        public HighScoreScreen(string score, string name)
            : base("HighScore")
        {
            /*if (SQLCommand.Connect == false)
            {
                SQLCommand.connect("127.0.0.1", "spaceinvaders", "root", "");
            }

            if (SQLCommand.Connect)
            {
                SQLInsertData insert = new SQLInsertData();

                insert.Add("Name", name);
                insert.Add("Score", score);
                insert.Add("Level", "1");

                SQLCommand.InsertData("Spaceinvadershighscore", insert);
                SQLCommand.ExecuteNonQuery();
            }

            if (SQLCommand.Connect)
            {
                allPeople = SQLCommand.SelectAllCommand("SELECT Name FROM spaceinvadershighscore ORDER BY score DESC");
                allScore = SQLCommand.SelectAllCommand("SELECT Score FROM spaceinvadershighscore ORDER BY score DESC");
            }

            MenuEntry[] entry = new MenuEntry[10];
            
            if (allPeople != null)
            {
                for (int i = 0; (i < 10) && (i < allPeople.Length); i++)
                {
                    entry[i] = new MenuEntry(i + "    " + allPeople[i] + "    " + allScore[i]);
                    MenuEntries.Add(entry[i]);
                }
            }

            // Create our menu entries.
            MenuEntry exitMenuEntry = new MenuEntry("Back");
            // Hook up menu event handlers.
            exitMenuEntry.Selected += BackToMainMenu;

            // Add entries to the menu.
            MenuEntries.Add(exitMenuEntry);   */         
        }


        #endregion

        #region Handle Input

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        void BackToMainMenu(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new MainMenuScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
        #endregion

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;
        }
    }
}
