using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project {

	public class Path {

		public bool isMoving;

		private Rectangle moveableObject;
		private List<Vector2> wayPoints;
		private int numberOfWayPoints, curWayPoint;
		private float speed, lastDistance;

		public Path (Rectangle moveableObject, List<Vector2> wayPoints, float speed) {
		
			this.moveableObject = moveableObject;
			this.wayPoints = wayPoints;
			this.speed = speed;

			curWayPoint = 0;
			numberOfWayPoints = wayPoints.Count;

			isMoving = true;
		}

		public Rectangle Follow(GameTime gameTime){

			isMoving = true;

			if (curWayPoint == numberOfWayPoints) {

				isMoving = false;
				Debug.WriteLine("reached end of path");

			} else {

				Vector2 destination = wayPoints[curWayPoint];
				Vector2 position = new Vector2(moveableObject.X, moveableObject.Y);

				Vector2 direction = destination - position;

				position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

				float distance = Vector2.Distance(position, destination);

				if (distance == lastDistance) {

					curWayPoint++;
					Debug.WriteLine("reached a waypoint");
				}

				lastDistance = distance;

				moveableObject.X = (int)position.X;
				moveableObject.Y = (int)position.Y;
			}

			return moveableObject;
		}
	}
}

