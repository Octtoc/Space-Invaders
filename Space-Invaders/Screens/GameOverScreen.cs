#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
#endregion

namespace Space_Invaders_Gsm
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class GameOverScreen : MenuScreen
    {
        #region Initialization
        private string tmpScore;
        private string name;

        private KeyboardState keyboardstate;
        private KeyboardState oldkeyboardstate;

        public static MouseState mousestate;
        public static MouseState oldmousestate;

        private static MenuEntry playGameMenuEntry;

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public GameOverScreen(string score)
            : base("Main Menu")
        {
            // Create our menu entries.
            playGameMenuEntry = new MenuEntry("User: ");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            
            tmpScore = score;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
        }


        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //if (SQLCommand.Connect == false)
                //SQLCommand.connect("127.0.0.1", "spaceinvaders", "root", "");
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
           
            Microsoft.Xna.Framework.Input.Keys[] pressedKeys;

            keyboardstate = Keyboard.GetState();

            pressedKeys = keyboardstate.GetPressedKeys();

            if (pressedKeys.Length > 0)
                Debug.WriteLine(pressedKeys[0].ToString());            

            if ((pressedKeys != null) && (pressedKeys.Length == 1))
            {
                if (keyboardstate.IsKeyDown(pressedKeys[0])
                     && oldkeyboardstate.IsKeyUp(pressedKeys[0]) &&
                    (keyboardstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) == false) &&
                    (keyboardstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Back) == false))
                {
                    name += pressedKeys[0].ToString();
                    playGameMenuEntry.Text = "User: " + name;
                }
                else if (keyboardstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter)
                      && oldkeyboardstate.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    LoadingScreen.Load(ScreenManager, true, null,
                               new HighScoreScreen(tmpScore, name));
                }
                else if (keyboardstate.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Back)
                      && oldkeyboardstate.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Back))
                {
                    if (name.Length > 0)
                    {
                        name = name.Substring(0, name.Length - 1);
                        playGameMenuEntry.Text = "User: " + name;
                    }
                }
            }
            oldkeyboardstate = keyboardstate;
        }

        #endregion
    }
}
