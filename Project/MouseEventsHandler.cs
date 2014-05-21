using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project {

	public class MouseEventsHandler {

		public Rectangle rect;

		private bool leftClick = false;
		private bool rightClick = false;
		private bool globalLeftClick = false;
		private bool globalRightClick = false;
		private bool hover = false;

		public MouseEventsHandler(Rectangle rect) {

			this.rect = rect;
		}

		public bool LeftClick() {

			MouseState mouseState = Mouse.GetState();

			if (!leftClick && mouseState.LeftButton == ButtonState.Pressed && InsideRect()) {

				leftClick = true;
				return true;

			} else if (leftClick && mouseState.LeftButton != ButtonState.Pressed) {

				leftClick = false;
				return false;

			} else {

				return false;
			}
		}

		public bool RightClick() {

			MouseState mouseState = Mouse.GetState();

			if (!rightClick && mouseState.RightButton == ButtonState.Pressed && InsideRect()) {

				rightClick = true;
				return true;

			} else if (rightClick && mouseState.RightButton != ButtonState.Pressed) {

				rightClick = false;
				return false;

			} else {

				return false;
			}
		}

		public bool GlobalLeftClick() {

			MouseState mouseState = Mouse.GetState();

			if (!globalLeftClick && mouseState.LeftButton == ButtonState.Pressed) {

				globalLeftClick = true;
				return true;

			} else if (globalLeftClick && mouseState.LeftButton != ButtonState.Pressed) {

				globalLeftClick = false;
				return false;

			} else {

				return false;
			}
		}

		public bool GlobalRightClick() {

			MouseState mouseState = Mouse.GetState();

			if (!globalRightClick && mouseState.RightButton == ButtonState.Pressed) {

				globalRightClick = true;
				return true;

			} else if (globalRightClick && mouseState.RightButton != ButtonState.Pressed) {

				globalRightClick = false;
				return false;

			} else {

				return false;
			}
		}

		public bool Enter() {

			Point mousePos = Mouse.GetState().Position;

			if (!hover && InsideRect()) {

				hover = true;
				return true;

			} else {

				return false;
			}
		}

		public bool Leave() {

			Point mousePos = Mouse.GetState().Position;

			if (hover && !InsideRect()) {

				hover = false;
				return true;

			} else {

				return false;
			}
		}

		public bool InsideRect(){
		
			Point mousePos = Mouse.GetState().Position;
			return ((mousePos.X >= rect.X && mousePos.X <= (rect.X + rect.Width)) && (mousePos.Y >= rect.Y && mousePos.Y <= (rect.Y + rect.Height)));
		}
	}
}