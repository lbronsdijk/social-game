using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project {

	public class Obstacle : DrawableGameComponent {

		private Game game;
		private SpriteBatch spriteBatch;
		public Rectangle rect;
		public Texture2D texture;

		public Obstacle (Game game, Vector2 position) : base (game) {

			this.UpdateOrder = 1;
			this.DrawOrder = 1;

			this.game = game;
			this.rect = new Rectangle ((int)position.X, (int)position.Y, 300, 50);

			this.game.Components.Add(this);
		}

		public override void Initialize() {

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch(this.game.GraphicsDevice);

			texture = this.game.Content.Load<Texture2D>("Images/obstacle");

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