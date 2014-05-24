using System;
using Microsoft.Xna.Framework;

namespace BMFont
{
	public class Character
	{
		public char c;
		public Rectangle rect;
		public int xOffset, yOffset;

		public Character(char c, Rectangle rect, int xOffset, int yOffset)
		{
			this.c = c;
			this.rect = rect;
			this.xOffset = xOffset;
			this.yOffset = yOffset;
		}
	}
}