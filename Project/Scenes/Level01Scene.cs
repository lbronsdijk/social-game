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

	public class Level01Scene : BaseScene {

		SpriteBatch spriteBatch;
		Bobby bobby;
		Moeder moeder;

		PathManager pathManager = new PathManager();

		public Level01Scene(Game game) : base(game) {

			this.UpdateOrder = 0;
			this.DrawOrder = 0;

			base.Construct(game);
		}

		public override void Initialize() {

			base.gameManager.windowTitle = "Level 1";

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch(base.game.GraphicsDevice);

			base.LoadFonts();

			new UIDialog (
				game, 
				fonts ["Arial_24px"], 
				"Kom op Bobby een beetje opschieten! Ik heb niet heel de dag.",
				gameManager.screenWidth,
				gameManager.screenHeight,
				null
			);

			bobby = new Bobby(game, new Vector2(-110, 250));
			moeder = new Moeder(game, new Vector2(-50, 250));

			List<Vector2> waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(375, 250));

			pathManager.createPath("bobby walk behind mom" , waypoints, () => {

				new UIDialog (
					game, 
					fonts ["Arial_24px"], 
					"Mama! niet zo snel.. Ik kan je niet bijhouden.",
					gameManager.screenWidth,
					gameManager.screenHeight,
					() => {
						bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby panic 1", 3.0f, true);
					}
				);
			});

			waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(850, 250));

			pathManager.createPath("mom walks out the screen" , waypoints, () => {
				//ignore
			});

			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby walk behind mom", 2.0f, true);
			moeder.rect = pathManager.MoveRectAlongPath(moeder.rect, "mom walks out the screen", 3.0f, true);

			waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(275, 250));

			pathManager.createPath("bobby panic 1" , waypoints, () => {

				new UIDialog (
					game, 
					fonts ["Arial_24px"], 
					"Mam!?.",
					gameManager.screenWidth,
					gameManager.screenHeight,
					() => {
						bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby panic 2", 5.0f, true);
					}
				);
			});

			waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(475, 250));

			pathManager.createPath("bobby panic 2" , waypoints, () => {

				new UIDialog (
					game, 
					fonts ["Arial_24px"], 
					"HALLO MAM!?.",
					gameManager.screenWidth,
					gameManager.screenHeight,
					() => {
						new UIDialog (
							game, 
							fonts ["Arial_24px"], 
							"AAAAAAAAAAAAAA!!! :(",
							gameManager.screenWidth,
							gameManager.screenHeight,
							() => {
								base.gameManager.LoadScene("level2");
							}
						);

						bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby panic 3", 5.0f, true);
					}
				);
			});

			waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(850, 250));

			pathManager.createPath("bobby panic 3" , waypoints, () => {});

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {
		
			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby walk behind mom", 2.0f, false);
			moeder.rect = pathManager.MoveRectAlongPath(moeder.rect, "mom walks out the screen", 3.0f, false);
			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby panic 1", 3.0f, false);
			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby panic 2", 5.0f, false);
			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby panic 3", 5.0f, false);

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {

			base.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}