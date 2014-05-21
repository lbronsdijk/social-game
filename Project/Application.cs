using System;
using Microsoft.Xna.Framework;

namespace Project {

	public class Application : Game {

		public Application() {

			GameManager gameManager = new GameManager(this);
			Services.AddService(typeof(GameManager), gameManager);

			gameManager.StartGame();
		}
	}
}