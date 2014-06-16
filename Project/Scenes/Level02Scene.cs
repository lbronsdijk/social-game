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

	public class Level02Scene : BaseScene {

		SpriteBatch spriteBatch;

		Texture2D pixel;
		Rectangle npc, npc2;
		Bobby bobby;
		bool answer1 = false;
		bool answer2 = false;

		PathManager pathManager = new PathManager();

		public Level02Scene(Game game) : base(game) {

			this.UpdateOrder = 0;
			this.DrawOrder = 0;

			base.Construct(game);
		}

		public override void Initialize() {

			base.gameManager.windowTitle = "Level 2";

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch(base.game.GraphicsDevice);

			base.LoadFonts();

			pixel = new Texture2D(this.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });

			bobby = new Bobby(game, new Vector2(-50, 430));

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

			Obstacle obstacle = new Obstacle (game, new Vector2 (225, 300));

			List<Vector2> waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(350, 430));

			pathManager.createPath ("bobby move in screen", waypoints, () => {});

			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby move in screen", 3.0f, true);

			waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(850, 430));

			pathManager.createPath ("bobby move out screen", waypoints, () => {
				gameManager.LoadScene("level3");
			});

			waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(350, 360));
			waypoints.Add(new Vector2(165, 360));
			waypoints.Add(new Vector2(165, 190));
			waypoints.Add(new Vector2(350, 190));
			waypoints.Add(new Vector2(350, 160));

			pathManager.createPath("move_to_npc", waypoints, () => {

				new UIDialog(
					game, 
					base.fonts["Arial_24px"], 
					"Hallo meneer heeft u misschien mijn moeder gezien?",
					base.gameManager.screenWidth, 
					base.gameManager.screenHeight,
					() => {

						new UIDialog(
							game, 
							base.fonts["Arial_24px"], 
							"Is het een vrouw met haast? Misschien heb ik die wel zien lopen maar als je een hint wilt. Zul je mij eerst moeten helpen. Weet jij het antwoord op deze vraag?",
							base.gameManager.screenWidth, 
							base.gameManager.screenHeight,
							() => {

								new Question(game, base.fonts["Arial_32px"], "Hoeveel is 7 x 8?", "56", () => {


									new UIDialog(
										game, 
										base.fonts["Arial_24px"], 
										"Wow dat is helemaal goed! Jij bent echt slim! Een afspraak is een afspraak de markt verkoopman hier verder op heeft je moeder zien lopen. Succes!",
										base.gameManager.screenWidth, 
										base.gameManager.screenHeight,
										() => {
											answer1 = true;
										}
									);

								}, () => {

									new UIDialog(
										game, 
										base.fonts["Arial_24px"], 
										"Aww helaas dat is niet goed... Volgende keer beter!",
										base.gameManager.screenWidth, 
										base.gameManager.screenHeight,
										() => {
											answer1 = false;
										}
									);
								});
							}
						);
					}
				);
			});

			waypoints = new List<Vector2>();
			waypoints.Add(new Vector2(350, 160));
			waypoints.Add(new Vector2(350, 190));
			waypoints.Add(new Vector2(535, 190));
			waypoints.Add(new Vector2(535, 360));
			waypoints.Add(new Vector2(350, 360));
			waypoints.Add(new Vector2(350, 430));

			pathManager.createPath("move_to_npc_2", waypoints, () => {

				new UIDialog(
					game, 
					base.fonts["Arial_24px"], 
					"Is er iets jongen?",
					base.gameManager.screenWidth, 
					base.gameManager.screenHeight,
					() => {

						if(!answer1){

							new UIDialog(
								game, 
								base.fonts["Arial_24px"], 
								"Nee hoor dank u wel meneer...",
								base.gameManager.screenWidth, 
								base.gameManager.screenHeight,
								() => {}
							);
						} else {

							new UIDialog(
								game, 
								base.fonts["Arial_24px"], 
								"Ja de meneer verder op zei dat u weet waar mijn moeder heen is gegaan?",
								base.gameManager.screenWidth, 
								base.gameManager.screenHeight,
								() => {

									new UIDialog(
										game, 
										base.fonts["Arial_24px"], 
										"Dat klopt ik denk dat ik wel weet wie je bedoelt. Maar als je wilt dat ik je help net zoals de andere man. Zul je een vraag moeten beantwoorden!",
										base.gameManager.screenWidth, 
										base.gameManager.screenHeight,
										() => {

											new Question(game, base.fonts["Arial_32px"], "Hoeveel is 12 x 12?", "144", () => {
											
												new UIDialog(
													game, 
													base.fonts["Arial_24px"], 
													"Wow dat is helemaal goed! Een afspraak is een afspraak, zij liep zo net hier naar links. Succes!",
													base.gameManager.screenWidth, 
													base.gameManager.screenHeight,
													() => {
														answer2 = true;
														bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby move out screen", 5.0f, true);
													}
												);

											}, () => {

												new UIDialog(
													game, 
													base.fonts["Arial_24px"], 
													"Aww helaas dat is niet goed... Volgende keer beter!",
													base.gameManager.screenWidth, 
													base.gameManager.screenHeight,
													() => {
														answer2 = false;
													}
												);
											});
										}
									);
								}
							);
						}
					}
				);
			});

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {

			if (MouseEventsHandler.LeftClick(npc)) {

				bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "move_to_npc", 3.0f, true);
			}

			if (MouseEventsHandler.LeftClick(npc2)) {

				bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "move_to_npc_2", 3.0f, true);
			}

			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby move in screen", 3.0f, false);
			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "move_to_npc", 3.0f, false);
			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "move_to_npc_2", 3.0f, false);
			bobby.rect = pathManager.MoveRectAlongPath(bobby.rect, "bobby move out screen", 5.0f, false);

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {

			base.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.Draw(pixel, npc, Color.ForestGreen);
			spriteBatch.Draw(pixel, npc2, Color.ForestGreen);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}