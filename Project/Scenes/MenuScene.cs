using System;
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
			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {

			KeyboardState keyState = Keyboard.GetState();

			if(keyState.IsKeyDown(Keys.Space))
				base.gameManager.LoadScene("game");
			
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {

			base.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.Draw(logoTexture, new Vector2 (25, 200), Color.White);

			base.fonts["Arial_32px"].DrawText(spriteBatch, 25, 25, base.gameManager.screenWidth, "Menu Scene", Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}