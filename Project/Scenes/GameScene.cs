using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project {

	public class GameScene : BaseScene {

		SpriteBatch spriteBatch;
		Texture2D logoTexture;

		string messageString = "";

		public GameScene(Game game) : base(game) {

			base.Construct(game);
		}

		public override void Initialize() {

			base.gameManager.windowTitle = "Game Scene";

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch(base.game.GraphicsDevice);

			logoTexture = base.game.Content.Load<Texture2D>("Images/logo");

			base.LoadFonts();
			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
		
			KeyboardState keyState = Keyboard.GetState();

			if(keyState.IsKeyDown(Keys.Escape))
				base.LoadScene("menu");

			//messageString = textBoxInput(messageString);

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {

			base.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.Draw(logoTexture, new Vector2 (365, 200), Color.White);

			base.fonts["Arial_24px"].DrawText(spriteBatch, 335, 25, "Game Scene");

			base.fonts["Arial_16px"].DrawText(spriteBatch, 25, 50, messageString);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}