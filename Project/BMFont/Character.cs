using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BMFont
{
	public class Character
	{
		public Texture2D texture;
		public Rectangle rect;
		public Vector2 pos;
		public int width;

		public Character(Texture2D texture, Rectangle rect, Vector2 pos, int width)
		{
			this.texture = texture;
			this.rect = rect;
			this.pos = pos;
			this.width = width;
		}
	}
}