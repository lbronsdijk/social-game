using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project {

	public class UIElement : DrawableGameComponent {

		protected Game game;
		protected SpriteBatch spriteBatch;

		public UIElement (Game game) : base(game) {

			this.UpdateOrder = 2;
			this.DrawOrder = 2;

			this.game = game;
		}

		public override void Initialize() {

			base.Initialize();
		}

		protected override void LoadContent() {

			this.spriteBatch = new SpriteBatch (this.game.GraphicsDevice);

			base.LoadContent();
		}
	}
}