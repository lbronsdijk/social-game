using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using BMFont;

namespace Project {

	public class TextBox : DrawableGameComponent {

		private Game game;
		private SpriteBatch spriteBatch;
		private MouseEventsHandler mouseEventHandler;
		private bool selected;
		private int delay;
		private Keys[] oldKeys;

		private string _text;

		public string text{
			get{ 
				return _text;
			} 
			set{
				_text = value;
			}
		}

		private FontRenderer _font;

		public FontRenderer font{
			get{
				return _font;
			}
			set{ 
				_font = value;
			}
		}

		private Vector2 _position;

		public Vector2 position{
			get{
				return _position;
			}
			set{ 
				_position = value;

				if (mouseEventHandler != null) {
					mouseEventHandler.rect.X = (int)_position.X;
					mouseEventHandler.rect.Y = (int)_position.Y;
				}
			}
		}

		private int _width, _height, _borderSize;

		public int width{
			get{
				return _width;
			}
			set{
				_width = value;

				if (mouseEventHandler != null) {
					mouseEventHandler.rect.Width = _width;
				}
			}
		}

		public int height{
			get{
				return _height;
			}
			set{
				_height = value;

				if (mouseEventHandler != null) {
					mouseEventHandler.rect.Height = _height;
				}
			}
		}

		public int borderSize{
			get{
				return _borderSize;
			}
			set{
				_borderSize = value;
			}
		}

		private Color _backgroundColor, _borderColor;

		public Color backgroundColor{
			get{ 
				return _backgroundColor;
			}
			set{
				_backgroundColor = value;
			}
		}

		public Color borderColor{
			get{ 
				return _borderColor;
			}
			set{
				_borderColor = value;
			}
		}

		public TextBox(Game game, FontRenderer font) : base(game) {

			this.UpdateOrder = 1;
			this.DrawOrder = 1;

			this.game = game;
			this.font = font;
			this.position = Vector2.Zero;
			this.text = "";

			this.width = 200;
			this.height = 50;
			this.borderSize = 1;
			this.backgroundColor = Color.White;
			this.borderColor = Color.Black;

			this.game.Components.Add(this);
		}

		public override void Initialize() {

			mouseEventHandler = new MouseEventsHandler(
				new Rectangle(
					(int)_position.X, 
					(int)_position.X, 
					_width, 
					_height
				)
			);

			selected = false;
			delay = 500;
			oldKeys = new Keys[0];

			base.Initialize();
		}

		protected override void LoadContent() {

			spriteBatch = new SpriteBatch (this.game.GraphicsDevice);

			base.LoadContent();
		}

		public override void Update(GameTime gameTime) {

			//mouse
			if (mouseEventHandler.LeftClick()) {

				Debug.WriteLine ("TextBox select event");

				selected = true;
			}

			if (mouseEventHandler.GlobalLeftClick() && !mouseEventHandler.InsideRect()) {

				Debug.WriteLine("TextBox deselect event");

				selected = false;
			}

			if(mouseEventHandler.Enter()) {

				Debug.WriteLine("TextBox mouseEnter event");
			}

			if(mouseEventHandler.Leave()) {

				Debug.WriteLine("TextBox mouseLeave event");
			}

			if (!selected) {

				return;
			}

			//keyboard
			KeyboardState keyState = Keyboard.GetState();

			if (keyState.IsKeyDown(Keys.Back)) {

				if (delay <= 0 && _text.Length > 0) {

					_text = _text.Remove (_text.Length - 1);

				} else {

					delay -= gameTime.ElapsedGameTime.Milliseconds;
				}

			} else {

				delay = 500;
			}

			Keys[] pressedKeys = keyState.GetPressedKeys();

			foreach (Keys newKey in pressedKeys) {

				bool foundIt = false;

				foreach (Keys oldKey in oldKeys) {

					if (newKey == oldKey) {

						foundIt = true;
						break;
					}
				}

				if (!foundIt) {

					string keyString = "";

					switch (newKey) {

					case Keys.A:
						keyString = "A";
						break;
					case Keys.B:
						keyString = "B";
						break;
					case Keys.C:
						keyString = "C";
						break;
					case Keys.D:
						keyString = "D";
						break;
					case Keys.E:
						keyString = "E";
						break;
					case Keys.F:
						keyString = "F";
						break;
					case Keys.G:
						keyString = "G";
						break;
					case Keys.H:
						keyString = "H";
						break;
					case Keys.I:
						keyString = "I";
						break;
					case Keys.J:
						keyString = "J";
						break;
					case Keys.K:
						keyString = "K";
						break;
					case Keys.L:
						keyString = "L";
						break;
					case Keys.M:
						keyString = "M";
						break;
					case Keys.N:
						keyString = "N";
						break;
					case Keys.O:
						keyString = "O";
						break;
					case Keys.P:
						keyString = "P";
						break;
					case Keys.Q:
						keyString = "Q";
						break;
					case Keys.R:
						keyString = "R";
						break;
					case Keys.S:
						keyString = "S";
						break;
					case Keys.T:
						keyString = "T";
						break;
					case Keys.U:
						keyString = "U";
						break;
					case Keys.V:
						keyString = "V";
						break;
					case Keys.W:
						keyString = "W";
						break;
					case Keys.X:
						keyString = "X";
						break;
					case Keys.Y:
						keyString = "Y";
						break;
					case Keys.Z:
						keyString = "Z";
						break;

					case Keys.D0:
						keyString = "0";
						break;
					case Keys.D1:
						keyString = "1";
						break;
					case Keys.D2:
						keyString = "2";
						break;
					case Keys.D3:
						keyString = "3";
						break;
					case Keys.D4:
						keyString = "4";
						break;
					case Keys.D5:
						keyString = "5";
						break;
					case Keys.D6:
						keyString = "6";
						break;
					case Keys.D7:
						keyString = "7";
						break;
					case Keys.D8:
						keyString = "8";
						break;
					case Keys.D9:
						keyString = "9";
						break;

					case Keys.Space:
						keyString = " ";
						break;
					case Keys.OemPeriod:
						keyString = ".";
						break;
					case Keys.Back:
						if (_text.Length > 0) {
							_text = _text.Remove(_text.Length - 1);
						}
						break;
					}

					if (keyState.IsKeyUp (Keys.LeftShift) && keyState.IsKeyUp (Keys.RightShift)) {

						keyString = keyString.ToLower();
					}

					_text += keyString;
				}
			}

			oldKeys = pressedKeys;

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
		
			spriteBatch.Begin();

			_font.DrawText(spriteBatch, (int)_position.X, (int)_position.Y, _text);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}