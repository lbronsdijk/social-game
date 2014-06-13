using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project {

	public class PathManager {
	
		private List<Path> paths;
		private List<MoveableRectangle> moveableRects;

		public PathManager() {

			paths = new List<Path>();
			moveableRects = new List<MoveableRectangle>();
		}

		public void createPath(string pathName, List<Vector2> waypoints) {

			if(getPath(pathName) == null)
				paths.Add(new Path(pathName, waypoints));
		}

		public Rectangle MoveRectAlongPath(Rectangle rect, string pathName, float speed, bool start) {

			Path path = getPath(pathName);

			if (path == null) {

				Debug.WriteLine("MoveRectAlongPath ERROR: " + pathName + " path does not exists!");
				return rect;
			}

			MoveableRectangle mRect = getMoveableRectangle(rect);

			if (mRect == null) {

				mRect = new MoveableRectangle(rect);
				moveableRects.Add(mRect);
			}

			if (start && mRect.followingPathName != pathName) {

				mRect.followingPathName = pathName;

				mRect.StartMoving ();

				mRect.curWaypoint = path.indexOfNearestWaypoint (mRect.rect);

			} else if (!start && mRect.followingPathName != pathName) {

				return rect;

			} else if (!start && mRect.isFinished) {

				return rect;
			}

			mRect.isMoving = true;

			if (mRect.curWaypoint == path.numberOfWayPoints) {
			
				mRect.FinishMoving();
				Debug.WriteLine("reached end of path");

			} else {

				Vector2 destination = path.waypoints[mRect.curWaypoint];
				Vector2 position = new Vector2((float)mRect.rect.X, (float)mRect.rect.Y);

				float distance = 0.0f;

				if (destination != position) {

					Vector2 direction = destination - position;
					direction.Normalize();

					position += direction * speed;

					distance = Vector2.Distance(position, destination);
				}

				if (distance < 5.0f) {

					position = destination;
					mRect.curWaypoint++;
					Debug.WriteLine("reached a waypoint");
				}

				mRect.rect.X = (int)position.X;
				mRect.rect.Y = (int)position.Y;
			}

			return mRect.rect;
		}

		private Path getPath(string pathName){

			Path result = null;

			foreach (Path path in paths) {

				if (path.name == pathName)
					result = path;
			}

			return result;
		}

		private MoveableRectangle getMoveableRectangle(Rectangle rect){

			MoveableRectangle result = null;

			foreach (MoveableRectangle mRect in moveableRects) {

				if (mRect.rect == rect) {

					result = mRect;
				}
			}

			return result;
		}
	}

	class Path {

		public string name;
		public List<Vector2> waypoints;
		public int numberOfWayPoints;

		public Path(string pathName, List<Vector2> waypoints) {

			this.name = pathName;
			this.waypoints = waypoints;
			this.numberOfWayPoints = waypoints.Count;
		}

		public int indexOfNearestWaypoint(Rectangle rect){

			int curWaypointIndex = 0;

			int nearestWaypointIndex = 0;
			float nearestDistance = -1.0f;

			foreach (Vector2 waypoint in waypoints) {
			
				Vector2 position = new Vector2((float)rect.X, (float)rect.Y);
				float distance = Vector2.Distance(position, waypoint);

				if (nearestDistance < 0.0f) {
					nearestWaypointIndex = curWaypointIndex;
					nearestDistance = distance;
				}

				if (distance < nearestDistance) {

					nearestWaypointIndex = curWaypointIndex;
					nearestDistance = distance;
				}

				curWaypointIndex++;
			}

			return nearestWaypointIndex;
		}
	}

	class MoveableRectangle {

		public Rectangle rect;

		public int curWaypoint;
		public bool isMoving, isFinished;

		public string followingPathName;

		public MoveableRectangle(Rectangle rect) {

			this.rect = rect;

			curWaypoint = 0;
			isMoving = false;
			isFinished = false;
		}
			
		public void StartMoving(){

			curWaypoint = 0;
			isFinished = false;
			isMoving = true;
		}

		public void FinishMoving(){

			isMoving = false;
			isFinished = true;
		}
	}
}