using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project {

	public class MenuScene : BaseScene {
	
		SpriteBatch spriteBatch;
		Texture2D logoTexture;
		UIButton gameButton, exitButton;

		public MenuScene(Game game) : base(game) {

			this.UpdateOrder = 0;
			this.DrawOrder = 0;

			base.Construct(game);
		}

		public override void Initialize() {

			base.gameManager.windowTitle = "Menu Scene";

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch(base.game.GraphicsDevice);

			logoTexture = base.game.Content.Load<Texture2D>("Images/logo");

			base.LoadFonts();

			gameButton = new UIButton(
				game, 
				new Rectangle(325, 10, 150, 50), 
				base.fonts["Arial_24px"], 
				"Spelen"
			);

			exitButton = new UIButton(
				game, 
				new Rectangle(325, 100, 150, 50), 
				base.fonts["Arial_24px"], 
				"Afsluiten"
			);

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
		
			if(gameButton.isClicked()) base.gameManager.LoadScene("game");
			if(exitButton.isClicked()) base.gameManager.ExitGame();

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {

			base.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.Draw(logoTexture, new Vector2 (25, 200), Color.White);

			base.fonts["Arial_32px"].DrawText(spriteBatch, new Vector2(25, 25), "Menu Scene", Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}