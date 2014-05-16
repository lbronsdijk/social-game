using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using BMFont;

namespace Project {

	public class BaseScene : DrawableGameComponent {
	
		protected Game game;
		protected GameManager gameManager;
		protected GraphicsDeviceManager graphics;
		protected Dictionary<string, FontRenderer> fonts;

		public BaseScene(Game game) : base(game) {}

		protected void Construct(Game game) {

			this.game = game;
			this.gameManager = Application.gameManager;
			this.graphics = gameManager.graphics;
		}

		protected void LoadFonts(){

			this.fonts = FontLoader.LoadFiles(this.game, new string[]{
				"Arial_16px",
				"Arial_24px",
				"Arial_32px"
			});
		}

		protected void LoadScene(string sceneName) {

			this.gameManager.LoadScene(sceneName);
		}
	}
}