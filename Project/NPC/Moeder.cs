using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project {

	public class Moeder : DrawableGameComponent {

		private Game game;
		private SpriteBatch spriteBatch;
		public Rectangle rect;
		public Texture2D texture;

		public Moeder (Game game, Vector2 position) : base (game) {

			this.UpdateOrder = 1;
			this.DrawOrder = 1;

			this.game = game;
			this.rect = new Rectangle ((int)position.X, (int)position.Y, 50, 100);

			this.game.Components.Add(this);
		}

		public override void Initialize() {

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch(this.game.GraphicsDevice);

			texture = this.game.Content.Load<Texture2D>("Images/moeder");

			base.LoadContent();
		}

		public override void Draw(GameTime gameTime) {

			spriteBatch.Begin();

			spriteBatch.Draw(texture, rect, Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}

