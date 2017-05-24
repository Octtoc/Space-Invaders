#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Invaders;
using Space_Invaders.ItemManager;
#endregion

namespace Space_Invaders_Gsm
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {  
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 enemyPosition = new Vector2(100, 100);

        Random random = new Random();

        float pauseAlpha;

        #region PlayLogicVariables

        private static Player player;
        private static InvaderList invaders;
        private ItemManager manager;

        public static bool active = true;
        private static float elapsedTime;
        private static float invadersMoveFasterTime = 20.0f;
        private static float startValueInvadersMoveSpeed = 0.52f;
        private static float startValueInvadersShootSpeed = 2.0f;

        private static HUD hud;

        public static KeyboardState keyboardstate;
        public static KeyboardState oldkeyboardstate;

        public static MouseState mousestate;
        public static MouseState oldmousestate;

        #endregion

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");
            Textures.Setup(content);
            Sounds.Setup(content);
            Fonts.Setup(content);

            player = new Player();

            invaders = new InvaderList(5, 11);
            hud = new HUD(new Vector2(800 - 200, 5), new Vector2(10, 10), 3, 10, 38);
            manager = new ItemManager(player);

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(100);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            keyboardstate = Keyboard.GetState();
            mousestate = Mouse.GetState();

            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive && active)
            {
                invaders.Update(gameTime);

                player.Update(gameTime);
                Collission.checkCollissionPlayer(gameTime, invaders, player, hud);
                hud.Update(gameTime, player);
                manager.Update(gameTime, invaders);

                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (elapsedTime >= invadersMoveFasterTime)
                {
                    if (invaders.MoveSpeed > 0.1f)
                    {
                        invaders.MoveSpeed -= 0.05f;
                        elapsedTime = 0;
                    }
                    else
                    {
                        invaders.MoveSpeed = 0.1f;
                    }
                }
                
                if (invaders.InvadersLeft() == false)
                {
                    if (invadersMoveFasterTime > 10.0f)
                    {
                        invadersMoveFasterTime -= 1.5f;
                    }

                    if (startValueInvadersMoveSpeed > 0.3f)
                    {
                        startValueInvadersMoveSpeed -= 0.05f;
                    }

                    if (startValueInvadersShootSpeed > 0.3f)
                    {
                        startValueInvadersShootSpeed -= 0.1f;
                    }

                    invaders.ShootSpeed = startValueInvadersShootSpeed;
                    invaders.MoveSpeed = startValueInvadersMoveSpeed;

                    invaders.ClearInvadersList();
                    invaders.Setup(5, 11);
                }
            }

            if (player.Lifes == 0 || invaders.invaderList[0].Position.Y > 400)
            {
                LoadingScreen.Load(ScreenManager, true, null,
                               new GameOverScreen(player.Score.ToString()));
            }

            oldkeyboardstate = keyboardstate;
            oldmousestate = mousestate;
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                    movement.X--;

                if (keyboardState.IsKeyDown(Keys.Right))
                    movement.X++;

                if (keyboardState.IsKeyDown(Keys.Up))
                    movement.Y--;

                if (keyboardState.IsKeyDown(Keys.Down))
                    movement.Y++;

                Vector2 thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                if (movement.Length() > 1)
                    movement.Normalize();

                playerPosition += movement * 2;
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            invaders.Draw(spriteBatch);
            player.Draw(spriteBatch);
            hud.Draw(spriteBatch);
            manager.Draw(spriteBatch);
            
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }

        }


        #endregion
    }
}
