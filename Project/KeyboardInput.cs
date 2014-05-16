using System;
using Microsoft.Xna.Framework.Input;

namespace Project {

	public class KeyboardInput {

		private Keys[] oldKeys = new Keys[0];

		protected string textBoxInput(string text){

			KeyboardState keyState = Keyboard.GetState();
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

						case Keys.Space:
							keyString = " ";
							break;
						case Keys.OemPeriod:
							keyString = ".";
							break;
						case Keys.Back:
							if (text.Length > 0) {
								text = text.Remove(text.Length - 1);
							}
							break;
					}

					if (keyState.IsKeyUp (Keys.LeftShift) && keyState.IsKeyUp (Keys.RightShift)) {

						keyString = keyString.ToLower();
					}

					text += keyString;
				}
			}

			oldKeys = pressedKeys;

			return text;
		}
	}
}