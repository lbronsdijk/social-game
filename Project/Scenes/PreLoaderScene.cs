using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Project {

	public class PreLoaderScene : BaseScene {

		SpriteBatch spriteBatch;

		public PreLoaderScene(Game game) : base(game) {

			this.UpdateOrder = 0;
			this.DrawOrder = 0;

			base.Construct(game);
		}

		public override void Initialize() {

			base.gameManager.windowTitle = "Preloading Game Components...";

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch(base.game.GraphicsDevice);

			base.LoadFonts();
			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {

			base.gameManager.LoadScene("menu");
			base.Update(gameTime);
		}
	}
}

