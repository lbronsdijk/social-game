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

	public class Level03Scene : BaseScene {

		SpriteBatch spriteBatch;
		Bobby bobby;
		Moeder moeder;
		PathManager pathManager = new PathManager();
		bool gevonden = false;

		public Level03Scene(Game game) : base(game) {

			this.UpdateOrder = 0;
			this.DrawOrder = 0;

			base.Construct(game);
		}

		public override void Initialize() {

			base.gameManager.windowTitle = "Level 3";

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch(base.game.GraphicsDevice);

			base.LoadFonts();

			bobby = new Bobby(game, new Vector2(-50, 250));
			moeder = new Moeder(game, new Vector2(850, 250));

			List<Vector2> waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(350, 250));

			pathManager.createPath ("bobby move in screen", waypoints, () => {

				new UIDialog (
					game, 
					fonts ["Arial_24px"], 
					"Mamaaaa! Ben je hier???.",
					gameManager.screenWidth,
					gameManager.screenHeight,
					() => {
						moeder.rect = pathManager.MoveRectAlongPath(moeder.rect, "moeder move in screen", 5.0f, true);
					}
				);
			});

			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby move in screen", 3.0f, true);

			waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(410, 250));

			pathManager.createPath ("moeder move in screen", waypoints, () => {

				new UIDialog (
					game, 
					fonts ["Arial_24px"], 
					"Bobby ik ben hier! Waar zat je nou toch?",
					gameManager.screenWidth,
					gameManager.screenHeight,
					() => {
						new UIDialog (
							game, 
							fonts ["Arial_24px"], 
							"GEVONDEN!!! JEEEAH!",
							gameManager.screenWidth,
							gameManager.screenHeight,
							() => {gevonden = true;}
						);
					}
				);
			});

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
		
			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby move in screen", 3.0f, false);
			moeder.rect = pathManager.MoveRectAlongPath(moeder.rect, "moeder move in screen", 5.0f, false);

			if (gevonden) {
				if (MouseEventsHandler.GlobalLeftClick(bobby.rect)) {
					base.gameManager.LoadScene("menu");
				}
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {

			base.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			if (gevonden) {
				fonts ["Arial_32px"].DrawText (spriteBatch, new Vector2 (300, 150), "Jij hebt gewonnen!", Color.White);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}