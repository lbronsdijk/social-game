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

		private List<Character> GetListOfCharacters(string text){

			List<Character> characters = new List<Character>();

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

					characters.Add(character);
				}
			}

			return characters;
		}

		private int GetTotalWidthOfCharacters(List<Character> characters){

			int totalWidth = 0; 

			foreach (Character character in characters) {

				totalWidth += character.rect.Width + character.xOffset;
			}

			return totalWidth;
		}

		public void DrawText(SpriteBatch spriteBatch, Vector2 pos, string text, Color fontColor){

			if (spriteBatch == null || text == null) {
				return;
			}

			List<Character> characters = GetListOfCharacters(text);

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
		}

		public void DrawTextForUITextBox(SpriteBatch spriteBatch, Vector2 pos, int maxWidth, string text, Color fontColor){

			if (spriteBatch == null || text == null) {
				return;
			}

			List<Character> characters = GetListOfCharacters(text);
			int totalWidth = GetTotalWidthOfCharacters(characters);

			//remove characters until text fits in textbox

			while (totalWidth >= maxWidth) {

				int width = characters [0].rect.Width + characters [0].xOffset;
				totalWidth -= width;

				characters.RemoveAt (0);
			}

			//draw text in textbox

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

		}

		public void DrawTextForUIDialog(SpriteBatch spriteBatch, Vector2 pos, int maxWidth, string text, Color fontColor){

			if (spriteBatch == null || text == null) {
				return;
			}

			List<Character> characters = GetListOfCharacters(text);

			Dictionary<int, List<Character>> lines = new Dictionary<int, List<Character>>();

			List<Character> line = new List<Character>();
			int lineNumber = 1;
			int curWidth = 0;

			foreach (Character character in characters) {

				curWidth += (character.rect.Width + character.xOffset);

				if (curWidth >= maxWidth) {
				
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

			int x = (int)pos.X;

			foreach (KeyValuePair<int, List<Character>> l in lines) {

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

		public void DrawTextForUIButton(SpriteBatch spriteBatch, Rectangle buttonRect, string text, Color fontColor){

			if (spriteBatch == null || text == null) {
				return;
			}

			List<Character> characters = GetListOfCharacters(text);
			int totalWidth = GetTotalWidthOfCharacters(characters);

			Vector2 textPos = Vector2.Zero;
			textPos.X = buttonRect.X + ((buttonRect.Width - totalWidth) / 2);

			int height = 0;

			foreach(Character character in characters){

				int charHeight = character.rect.Height + character.yOffset;

				if (charHeight > height) {

					height = charHeight;
				}
			}

			textPos.Y = buttonRect.Y + ((buttonRect.Height - height) / 2);

			foreach (Character character in characters) {

				spriteBatch.Draw (
					_texture, 
					new Vector2 (
						textPos.X + character.xOffset, 
						textPos.Y + character.yOffset
					), 
					character.rect, 
					fontColor
				);

				textPos.X += character.rect.Width + character.xOffset;
			}
		}
	}
}