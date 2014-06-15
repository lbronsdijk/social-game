using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BMFont;

namespace Project {

	public class UIButton : UIElement {

		public Rectangle rect;
		public string text;
		public FontRenderer font;
		private Color backgroundColor, fontColor;
		public Color backgroundColorON, backgroundColorOFF, fontColorON, fontColorOFF;
		private Texture2D pixel;

		public UIButton(Game game, Rectangle rect, FontRenderer font, string text) : base(game) {

			this.rect = rect;
			this.text = text;
			this.font = font;

			this.backgroundColorON = Color.PaleGreen;
			this.fontColorON = Color.White;

			this.backgroundColorOFF = Color.DarkSlateGray;
			this.fontColorOFF = Color.LightSlateGray;

			this.game.Components.Add(this);
		}

		public bool isClicked(){

			return (MouseEventsHandler.LeftClick(rect)) ? true : false;
		}

		public bool isHover(){

			return (MouseEventsHandler.InsideClickArea(rect)) ? true : false;
		}

		public override void Initialize() {
		
			base.Initialize();
		}

		protected override void LoadContent() {

			base.LoadContent();

			pixel = new Texture2D(this.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
		}

		public override void Update(GameTime gameTime) {

			if (isHover ()) {

				backgroundColor = backgroundColorON;
				fontColor = fontColorON;

			} else {

				backgroundColor = backgroundColorOFF;
				fontColor = fontColorOFF;
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
		
			spriteBatch.Begin();

			spriteBatch.Draw(pixel, rect, backgroundColor);
			font.DrawTextForUIButton(spriteBatch, rect, text, fontColor);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}