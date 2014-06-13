using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project {

	public static class MouseEventsHandler {
	
		private static Dictionary<Rectangle, Dictionary<string, bool>> events = new Dictionary<Rectangle, Dictionary<string, bool>>();

		private static void checkIfEventsExistForClickArea(Rectangle clickArea){

			if (!events.ContainsKey(clickArea)) {

				Dictionary<string, bool> e = new Dictionary<string, bool>();
				e.Add ("leftClick", false);
				e.Add ("rightClick", false);
				e.Add ("globalLeftClick", false);
				e.Add ("globalRightClick", false);
				e.Add ("hover", false);

				events.Add(clickArea, e);
			}
		}

		public static bool LeftClick(Rectangle clickArea) {

			checkIfEventsExistForClickArea(clickArea);

			MouseState mouseState = Mouse.GetState();

			if (!events[clickArea]["leftClick"] && mouseState.LeftButton == ButtonState.Pressed && InsideClickArea(clickArea)) {

				events[clickArea]["leftClick"] = true;
				return true;

			} else if (events[clickArea]["leftClick"] && mouseState.LeftButton != ButtonState.Pressed) {

				events[clickArea]["leftClick"] = false;
				return false;

			} else {

				return false;
			}
		}

		public static bool RightClick(Rectangle clickArea) {

			checkIfEventsExistForClickArea(clickArea);

			MouseState mouseState = Mouse.GetState();

			if (!events[clickArea]["rightClick"] && mouseState.RightButton == ButtonState.Pressed && InsideClickArea(clickArea)) {

				events[clickArea]["rightClick"] = true;
				return true;

			} else if (events[clickArea]["rightClick"] && mouseState.RightButton != ButtonState.Pressed) {

				events[clickArea]["rightClick"] = false;
				return false;

			} else {

				return false;
			}
		}

		public static bool GlobalLeftClick(Rectangle clickArea) {

			checkIfEventsExistForClickArea(clickArea);

			MouseState mouseState = Mouse.GetState();

			if (!events[clickArea]["globalLeftClick"] && mouseState.LeftButton == ButtonState.Pressed) {

				events[clickArea]["globalLeftClick"] = true;
				return true;

			} else if (events[clickArea]["globalLeftClick"] && mouseState.LeftButton != ButtonState.Pressed) {

				events[clickArea]["globalLeftClick"] = false;
				return false;

			} else {

				return false;
			}
		}

		public static bool GlobalRightClick(Rectangle clickArea) {

			checkIfEventsExistForClickArea(clickArea);

			MouseState mouseState = Mouse.GetState();

			if (!events[clickArea]["globalRightClick"] && mouseState.RightButton == ButtonState.Pressed) {

				events[clickArea]["globalRightClick"] = true;
				return true;

			} else if (events[clickArea]["globalRightClick"] && mouseState.RightButton != ButtonState.Pressed) {

				events[clickArea]["globalRightClick"] = false;
				return false;

			} else {

				return false;
			}
		}

		public static bool Enter(Rectangle clickArea) {

			checkIfEventsExistForClickArea(clickArea);

			Point mousePos = Mouse.GetState().Position;

			if (!events[clickArea]["hover"] && InsideClickArea(clickArea)) {

				events[clickArea]["hover"] = true;
				return true;

			} else {

				return false;
			}
		}

		public static bool Leave(Rectangle clickArea) {

			checkIfEventsExistForClickArea(clickArea);

			Point mousePos = Mouse.GetState().Position;

			if (events[clickArea]["hover"] && !InsideClickArea(clickArea)) {

				events[clickArea]["hover"] = false;
				return true;

			} else {

				return false;
			}
		}

		public static bool InsideClickArea(Rectangle clickArea){

			checkIfEventsExistForClickArea(clickArea);

			Point mousePos = Mouse.GetState().Position;
			return ((mousePos.X >= clickArea.X && mousePos.X <= (clickArea.X + clickArea.Width)) && (mousePos.Y >= clickArea.Y && mousePos.Y <= (clickArea.Y + clickArea.Height)));
		}
	}
}