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

	public class GameScene : BaseScene {

		SpriteBatch spriteBatch;
		Texture2D logoTexture;

		UITextBox textBox1;
		Texture2D pixel;
		Rectangle plr, npc, npc2, obstacle;

		PathManager pathManager = new PathManager();

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

			//UIButton btn = new UIButton(game);

			textBox1 = new UITextBox(base.game, base.fonts["Comic_Sans_24px"]);
			textBox1.position = new Vector2(25, 25);
			textBox1.width = 300;

			pixel = new Texture2D(this.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });

			plr = new Rectangle(
				350, 
				480, 
				50, 
				50
			);

			npc = new Rectangle(
				350, 
				100, 
				50, 
				50
			);

			npc2 = new Rectangle(
				350, 
				540, 
				50, 
				50
			);

			obstacle = new Rectangle(
				225, 
				300, 
				300, 
				50
			);

			List<Vector2> wayPoints = new List<Vector2>();
			wayPoints.Add(new Vector2(350, 360));
			wayPoints.Add(new Vector2(165, 360));
			wayPoints.Add(new Vector2(165, 240));
			wayPoints.Add(new Vector2(350, 240));
			wayPoints.Add(new Vector2(350, 160));

			List<Vector2> wayPoints2 = new List<Vector2>();
			wayPoints2.Add(new Vector2(350, 160));
			wayPoints2.Add(new Vector2(350, 240));
			wayPoints2.Add(new Vector2(535, 240));
			wayPoints2.Add(new Vector2(535, 360));
			wayPoints2.Add(new Vector2(350, 360));
			wayPoints2.Add(new Vector2(350, 480));

			pathManager.createPath("move_to_npc", wayPoints);
			pathManager.createPath("move_to_npc_2", wayPoints2);

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
		
			KeyboardState keyState = Keyboard.GetState();

			if(keyState.IsKeyDown(Keys.Escape))
				base.gameManager.LoadScene("menu");

			if (MouseEventsHandler.LeftClick(npc)) {

				plr = pathManager.MoveRectAlongPath(plr, "move_to_npc", 3.0f, true);
			}

			if (MouseEventsHandler.LeftClick(npc2)) {

				plr = pathManager.MoveRectAlongPath(plr, "move_to_npc_2", 3.0f, true);
			}

			plr = pathManager.MoveRectAlongPath(plr, "move_to_npc", 3.0f, false);
			plr = pathManager.MoveRectAlongPath(plr, "move_to_npc_2", 3.0f, false);

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {

			base.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.Draw(logoTexture, new Vector2 (700, 500), Color.White);

			spriteBatch.Draw(pixel, plr, Color.White);
			spriteBatch.Draw(pixel, npc, Color.ForestGreen);
			spriteBatch.Draw(pixel, npc2, Color.ForestGreen);
			spriteBatch.Draw(pixel, obstacle, Color.Gray);

			base.fonts["Arial_24px"].DrawText(spriteBatch, new Vector2(335, 25), "Game Scene", Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}