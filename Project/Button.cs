using System;
using Microsoft.Xna.Framework;
using BMFont;

namespace Project {

	public class Button : DrawableGameComponent{

		private Game game;
		public FontRenderer font;
		public string text;

		private int _width, _height;

		public int width {
			get;
			set;
		}

		public int height {
			get;
			set;
		}

		public Button (Game game, FontRenderer font, string text, int width, int height) : base(game) {

			this.game = game;
			this.font = font;
			this.text = text;
			this.width = width;
			this.height = height;
		}
	}
}