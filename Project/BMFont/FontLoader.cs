using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BMFont
{
	public class FontLoader
	{
		public static FontFile InitFileStream(Stream stream)
		{
			XmlSerializer deserializer = new XmlSerializer(typeof(FontFile));
			FontFile file = (FontFile) deserializer.Deserialize(stream);
			return file;
		}

		public static Dictionary<string, FontRenderer> LoadFiles(Game game, string[] fonts){

			Dictionary<string, FontRenderer> dictionary = new Dictionary<string, FontRenderer>();

			foreach (string font in fonts) {

				string fontFileName = font+".fnt";
				string fontTextureName = font+"_0.png";

				string contentPath = game.Content.RootDirectory;
				string fontPath = "Fonts/";

				string fontFilePath = Path.Combine(contentPath, fontPath, fontFileName);
				string fontTexturePath = Path.Combine(fontPath, fontTextureName);

				using(Stream stream = TitleContainer.OpenStream(fontFilePath)) {

					FontFile fontFile = FontLoader.InitFileStream(stream);
					Texture2D fontTexture = game.Content.Load<Texture2D>(fontTexturePath);

					dictionary.Add (font, new FontRenderer(fontFile, fontTexture));

					stream.Close();
				}
			}

			return dictionary;
		}
	}
}