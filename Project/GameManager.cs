using System;
using Microsoft.Xna.Framework;

namespace Project {

	public class GameManager {

		private Game game;
		public GraphicsDeviceManager graphics;
		private string _contentDirectory;
		private string _windowTitle;
		private bool _isMouseVisible;
		private bool _isFullScreen;
		private int _screenWidth;
		private int _screenHeight;

		public string contentDirectory {
			get { 
				return _contentDirectory;
			}
			set { 
				_contentDirectory = value;
				game.Content.RootDirectory = _contentDirectory;
			}
		}

		public string windowTitle {
			get { 
				return _windowTitle;
			}
			set {
				_windowTitle = value;
				game.Window.Title = _windowTitle;
			}
		}

		public bool isMouseVisible {
			get { 
				return _isMouseVisible;
			}
			set {
				_isMouseVisible = value;
				game.IsMouseVisible = _isMouseVisible;
			}
		}

		public bool isFullScreen {
			get { 
				return _isFullScreen;
			}
			set {
				_isFullScreen = value;
				graphics.IsFullScreen = _isFullScreen;
			}
		}

		public int screenWidth {
			get { 
				return _screenWidth;
			}
			set { 
				_screenWidth = value;
				graphics.PreferredBackBufferWidth = _screenWidth;
			}
		}

		public int screenHeight {
			get { 
				return _screenHeight;
			}
			set { 
				_screenHeight = value;
				graphics.PreferredBackBufferHeight = _screenHeight;
			}
		}

		public GameManager(Game game) {

			Settings settings = new Settings();

			this.game = game;
			this.graphics = new GraphicsDeviceManager(this.game);

			this.contentDirectory = "Content";
			this.windowTitle = "Project 04 - Game Design";
			this.isMouseVisible = true;

			this.screenWidth = 800;
			this.screenHeight = 600;
			this.isFullScreen = false;
		}

		public void StartGame() {

			LoadScene("preloader");
		}

		public void ExitGame() {

			this.game.Exit();
		}

		public void LoadScene(string sceneName){

			game.Components.Clear();

			switch (sceneName) {

			default:
			case "preloader":
				PreLoaderScene preloaderScene = new PreLoaderScene(game);
				game.Components.Add(preloaderScene);
				break;

			case "menu":
				MenuScene menuScene = new MenuScene(game);
				game.Components.Add(menuScene);
				break;

			case "level1":
				Level01Scene level01Scene = new Level01Scene(game);
				game.Components.Add(level01Scene);
				break;

			case "level2":
				Level02Scene level02Scene = new Level02Scene(game);
				game.Components.Add(level02Scene);
				break;

			case "level3":
				Level03Scene level03Scene = new Level03Scene(game);
				game.Components.Add(level03Scene);
				break;
			}
		}
	}
}