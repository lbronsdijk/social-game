using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BMFont;

namespace Project {

	public class Question : DrawableGameComponent {

		private Game game;
		private SpriteBatch spriteBatch;
		private UITextBox textBox;
		private UIButton button;
		private string answer, question;
		private FontRenderer font;
		private Action correctHandler, incorrectHandler;
		private Texture2D pixel;

		public Question(Game game, FontRenderer font, string question, string answer, Action correctHandler, Action incorrectHandler) : base(game) {

			this.UpdateOrder = 1;
			this.DrawOrder = 1;

			this.game = game;
			this.font = font;
			this.question = question;
			this.answer = answer;
			this.correctHandler = correctHandler;
			this.incorrectHandler = incorrectHandler;

			this.textBox = new UITextBox(game, font);
			this.textBox.position = new Vector2(100, 300);
			this.textBox.width = 475;
			this.textBox.height = 33;

			this.button = new UIButton (game, new Rectangle(600, 298, 100, 37), font, "OK!");

			this.game.Components.Add(this);
		}

		public override void Initialize() {

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch(this.game.GraphicsDevice);

			pixel = new Texture2D(this.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });

			base.LoadContent();
		}

		public override void Draw(GameTime gameTime) {

			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

			spriteBatch.Draw (pixel, new Rectangle(0, 0, 800, 600), Color.Black * 0.75f);
			spriteBatch.Draw (pixel, new Rectangle(90, 240, 620, 153), Color.CornflowerBlue);

			font.DrawText(spriteBatch, new Vector2(100, 250), question, Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}