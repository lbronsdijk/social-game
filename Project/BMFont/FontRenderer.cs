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

		public void DrawText(SpriteBatch spriteBatch, int x, int y, int maxWidth, string text, Color fontColor)
		{
			if (spriteBatch == null || text == null) {
				return;
			}

			List<Character> characters = new List<Character>();

			int dx = x;
			int dy = y;

			int totalWidth = 0;

			foreach(char c in text)
			{
				FontChar fc;

				if(_characterMap.TryGetValue(c, out fc))
				{
					Character character = new Character (
						_texture,
						new Rectangle (fc.X, fc.Y, fc.Width, fc.Height),
						new Vector2 (dx + fc.XOffset, dy + fc.YOffset),
						fc.Width + fc.XOffset
					);

					characters.Add(character);

					totalWidth += character.width;
					dx += fc.XAdvance;
				}
			}

			if (totalWidth < maxWidth)
				goto draw;
				
			totalWidth += characters[0].width;

			while (totalWidth >= maxWidth) {

				int width = characters[0].width;

				totalWidth -= width;
				characters.RemoveAt(0);

				foreach (Character character in characters) {
					character.pos.X -= width;
				}

				Debug.WriteLine("totalWidth: " + totalWidth);
			}

			draw:

			foreach (Character character in characters) {
			
				spriteBatch.Draw(character.texture, character.pos, character.rect, fontColor);
			}
		}
	}
}