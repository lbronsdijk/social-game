using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BMFont
{
	public class FontRenderer
	{
		public FontRenderer (FontFile fontFile, Texture2D fontTexture)
		{
			_fontFile = fontFile;
			_texture = fontTexture;
			_characterMap = new Dictionary<char, FontChar>();

			foreach(var fontCharacter in _fontFile.Chars)
			{
				char c = (char)fontCharacter.ID;
				_characterMap.Add(c, fontCharacter);
			}
		}

		private Dictionary<char, FontChar> _characterMap;
		private FontFile _fontFile;
		private Texture2D _texture;

		public void DrawText(SpriteBatch spriteBatch, int x, int y, int maxWidth, string text, Color fontColor, bool multiLines)
		{
			if (spriteBatch == null || text == null) {
				return;
			}

			List<Character> characters = new List<Character>();

			int totalWidth = 0;

			foreach(char c in text)
			{
				FontChar fc;

				if(_characterMap.TryGetValue(c, out fc))
				{
					int fixWidth = 0;

					if (c.ToString() == " ") {

						fixWidth = 3;
					} 

					Character character = new Character (
						c,
						new Rectangle (fc.X, fc.Y, fc.Width + fixWidth, fc.Height),
						fc.XOffset,
						fc.YOffset
					);

					totalWidth += character.rect.Width + character.xOffset;

					characters.Add(character);
				}
			}

			if (!multiLines) {

				while (totalWidth >= maxWidth) {

					int width = characters [0].rect.Width + characters [0].xOffset;
					totalWidth -= width;

					characters.RemoveAt (0);
				}

				Vector2 pos = new Vector2 (x, y);

				foreach (Character character in characters) {

					spriteBatch.Draw (
						_texture, 
						new Vector2 (
							pos.X + character.xOffset, 
							pos.Y + character.yOffset
						), 
						character.rect, 
						fontColor
					);

					pos.X += character.rect.Width + character.xOffset;
				}

			} else {

				//Debug.WriteLine("multi lines enabled");

				Dictionary<int, List<Character>> lines = new Dictionary<int, List<Character>>();

				List<Character> line = new List<Character>();
				int lineNumber = 1;
				int curWidth = 0;

				foreach (Character character in characters) {

					curWidth += (character.rect.Width + character.xOffset);

					//Debug.WriteLine("curWidth: " + curWidth + " maxWidth: " + maxWidth);

					if (curWidth >= maxWidth) {

						//Debug.WriteLine("new line");

						//add
						lines.Add(lineNumber, line);
						//reset
						line = new List<Character>();
						lineNumber++;
						curWidth = 0;

					}

					line.Add(character);
				}

				lines.Add(lineNumber, line);

				Vector2 pos = new Vector2 (x, y);

				foreach (KeyValuePair<int, List<Character>> l in lines) {

					//Debug.WriteLine("line number: " + l.Key);

					int height = 0;

					foreach (Character character in l.Value as List<Character>) {

						spriteBatch.Draw (
							_texture, 
							new Vector2 (
								pos.X + character.xOffset, 
								pos.Y + character.yOffset
							), 
							character.rect, 
							fontColor
						);

						pos.X += character.rect.Width + character.xOffset;

						int curHeight = character.rect.Height + character.yOffset;

						if (curHeight > height)
							height = curHeight;
					}

					pos.X = x;
					pos.Y += height;
				}
			}
		}
	}
}