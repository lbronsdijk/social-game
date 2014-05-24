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
		TextBox textBox1, textBox2;

		public GameScene(Game game) : base(game) {

			this.UpdateOrder = 0;
			this.DrawOrder = 0;

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

			textBox1 = new TextBox(base.game, base.fonts["Arial_24px"]);
			textBox1.position = new Vector2(25, 25);
			textBox1.width = 300;

			textBox2 = new TextBox(base.game, base.fonts["Arial_16px"]);
			textBox2.position = new Vector2(25, 75);
			textBox2.height = 17;
			textBox2.borderSize = 5;

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
		
			KeyboardState keyState = Keyboard.GetState();

			if(keyState.IsKeyDown(Keys.Escape))
				base.gameManager.LoadScene("menu");

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {

			base.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.Draw(logoTexture, new Vector2 (365, 200), Color.White);

			base.fonts["Arial_24px"].DrawText(spriteBatch, 335, 25, base.gameManager.screenWidth, "Game Scene", Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}