using System;
using Microsoft.Xna.Framework;

namespace Project {

	public class Application : Game {

		public static GameManager gameManager;

		public Application() {

			gameManager = new GameManager(this);
			gameManager.LoadScene("menu");
		}
	}
}