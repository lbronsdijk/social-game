using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BMFont;

namespace Project {

	public class UIDialog : UIElement {

		public string text;
		public FontRenderer font;
		public Color backgroundColor, borderColor, fontColor;
		private Rectangle backgroundRect, borderRect;
		private Texture2D pixel;
		public Action finishHandler;
		private string typedText;
		private double typedTextLength;
		private float delayInMilliseconds, time, removeDelay;
		private bool isDoneDrawing;

		public UIDialog (Game game, FontRenderer font, string text, int screenWidth, int screenHeight, Action finishHandler) : base(game) {

			this.font = font;
			this.text = text;

			this.backgroundRect = new Rectangle(
				100,
				screenHeight - 160,
				screenWidth - 200,
				150
			);

			this.borderRect = new Rectangle(
				98,
				screenHeight - 162,
				screenWidth - 196,
				154
			);

			this.backgroundColor = Color.White;
			this.borderColor = Color.Black;
			this.fontColor = Color.Black;
			this.finishHandler = finishHandler;

			this.game.Components.Add(this);
		}

		public bool isClicked(){

			return (MouseEventsHandler.LeftClick(backgroundRect)) ? true : false;
		}

		public override void Initialize() {

			base.Initialize();

			delayInMilliseconds = 50;
			removeDelay = 3000;
			time = 0;
			isDoneDrawing = false;
		}

		protected override void LoadContent() {

			base.LoadContent();

			pixel = new Texture2D(this.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
		}

		public override void Update(GameTime gameTime) {

			if (isClicked() && !isDoneDrawing) {

				typedText = text;
				isDoneDrawing = true;
			}

			if (isClicked() && isDoneDrawing) {

				if (finishHandler != null) 
					finishHandler();

				this.game.Components.Remove(this);
			}

			if (!isDoneDrawing) {

				if (typedTextLength < text.Length) {

					typedTextLength = typedTextLength + gameTime.ElapsedGameTime.TotalMilliseconds / delayInMilliseconds;
					typedText = text.Substring (0, (int)typedTextLength);

				} else {

					typedTextLength = text.Length;
					isDoneDrawing = true;
				}

			} else {

				if (time > removeDelay) {

					if (finishHandler != null) 
						finishHandler();

					this.game.Components.Remove(this);

				} else {

					time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
				}


			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {

			spriteBatch.Begin();

			spriteBatch.Draw(pixel, borderRect, borderColor);

			spriteBatch.Draw(pixel, backgroundRect, backgroundColor);

			font.DrawTextForUIDialog(spriteBatch, new Vector2(backgroundRect.X, backgroundRect.Y), backgroundRect.Width, 5, typedText, fontColor);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}