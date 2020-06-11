// Implementation of Fast Poisson Disk Sampling in Arbitrary Dimensions by Robert Bridson
// https://www.cs.ubc.ca/~rbridson/docs/bridson-siggraph07-poissondisk.pdf
using GeometryUtils;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FastPoissonDiskSampling {

	public static class CreateSamples {
		private static float squareWidth;
		private static int gridCols;
		private static int gridRows;
		private static Dictionary<(int, int), Vector2> grid;

		/// <summary> Rectangle </summary>
		public static List<Vector2> SamplePoints(float height, float width, float exclusionRadius, int numberOfCandidates = 30) {
			//Pick random start position
			var startPos = new Vector2(Random.Range(0, width), Random.Range(0, height));
			InitGrid(height, width, exclusionRadius);
			return CreatePoints(startPos, exclusionRadius, numberOfCandidates, height, width);
		}

		/// <summary> Triangle </summary>
		public static List<Vector2> SamplePoints(Vector2 v0, Vector2 v1, Vector2 v2, float exclusionRadius, int numberOfCandidates = 30) {
			//Pick random start position
			var startPos = TriangleUtils.GetRandomPosInTriangle(v0, v1, v2) + v0;
			InitGrid(v0, v1, v2, exclusionRadius);
			return CreatePoints(startPos, exclusionRadius, numberOfCandidates, v0, v1, v2);
		}

		/// <summary> Quadrilateral </summary>
		public static List<Vector2> SamplePoints(Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3, float exclusionRadius, int numberOfCandidates = 30) {
			//Pick random start position
			var startPos = QuadrilateralUtils.GetRandomPosInQuadrilateral(v0, v1, v2, v3) + v0;
			InitGrid(v0, v1, v2, v3, exclusionRadius);
			return CreatePoints(startPos, exclusionRadius, numberOfCandidates, v0, v1, v2, v3);
		}

		/// <summary> Circle (Around starting point) </summary>
		public static List<Vector2> SamplePoints(float radius, float exclusionRadius, int numberOfCandidates = 30) {
			InitGrid(radius, exclusionRadius);
			return CreatePoints( exclusionRadius, numberOfCandidates, radius);
		}

		#region Rectangle

		/// <summary> Rectangle Init</summary>
		private static void InitGrid(float height, float width, float exclusionRadius) {
			squareWidth = exclusionRadius / Mathf.Sqrt(2f); //Makes square's diagonal smaller than search radius
			gridCols = (int)Mathf.Ceil(width / squareWidth);
			gridRows = (int)Mathf.Ceil(height / squareWidth);
			grid = new Dictionary<(int, int), Vector2>(gridCols * gridRows);
		}

		private static List<Vector2> CreatePoints(Vector2 startPos, float exclusionRadius, int numberOfCandidates, float height, float width) {
			var activePoints = new List<Vector2>();
			var candidatePoints = new List<Vector2>();
			var finalPoints = new List<Vector2>();

			int gridX = (int)Mathf.Ceil(startPos.x / squareWidth);
			int gridY = (int)Mathf.Ceil(startPos.y / squareWidth);
			//set first grid element picked at random
			grid.Add((gridX, gridY), startPos);
			activePoints.Add(startPos);

			Vector2 randomActive;
			while (activePoints.Count > 0) {
				randomActive = activePoints[Random.Range(0, activePoints.Count)];
				for (int i = 0; i < numberOfCandidates; i++) {
					Vector2 candidatePoint = randomActive + Random.insideUnitCircle.normalized * Random.Range(exclusionRadius, exclusionRadius * 2f);
					//check if within bounds of the selected shape
					if (candidatePoint.x.IsWithinRange(0, width) && candidatePoint.y.IsWithinRange(0, height)) {
						gridX = (int)Mathf.Ceil(candidatePoint.x / squareWidth);
						gridY = (int)Mathf.Ceil(candidatePoint.y / squareWidth);

						//check if this grid point is not used
						if (!grid.ContainsKey((gridX, gridY))) {
							candidatePoints.Add(candidatePoint);

							//check if far enough from neighbors
							bool isFarEnough = true;
							for (int y = -1; y <= 1; y++) {
								for (int x = -1; x <= 1; x++) {
									if (grid.TryGetValue((gridX + x, gridY + y), out Vector2 gridPoint)) {
										if (Vector2.Distance(candidatePoint, gridPoint) < exclusionRadius) isFarEnough = false;
									}
								}
							}
							if (isFarEnough) {
								activePoints.Add(candidatePoint);
								grid.Add((gridX, gridY), candidatePoint);
								candidatePoints.Clear();
								break;
							}
						}
					}
					//if none of the candidates were a match
					if (i == numberOfCandidates - 1) {
						//if no appropriate candidates were found, move point to finalPoints and assign to grid
						activePoints.Remove(randomActive);
						finalPoints.Add(randomActive);
						candidatePoints.Clear();
					}
				}
			}
			return finalPoints;
		}

		#endregion Rectangle
		#region Triangle

		/// <summary> Triangle Init</summary>
		private static void InitGrid(Vector2 v0, Vector2 v1, Vector2 v2, float exclusionRadius) {
			squareWidth = exclusionRadius / Mathf.Sqrt(2f); //Makes square's diagonal smaller than search radius
			float lowX = Mathf.Infinity;
			float lowY = Mathf.Infinity;
			float highX = Mathf.NegativeInfinity;
			float highY = Mathf.NegativeInfinity;
			SetLowHigh(v0);
			SetLowHigh(v1);
			SetLowHigh(v2);
			gridCols = (int)Mathf.Ceil((highX - lowX) / squareWidth);
			gridRows = (int)Mathf.Ceil((highY - lowY) / squareWidth);
			grid = new Dictionary<(int, int), Vector2>(gridCols * gridRows);
			void SetLowHigh(Vector2 vector2) {
				if (vector2.x < lowX) lowX = vector2.x;
				if (vector2.y < lowY) lowY = vector2.y;
				if (vector2.x > highX) highX = vector2.x;
				if (vector2.y > highY) highY = vector2.y;
			}
		}

		private static List<Vector2> CreatePoints(Vector2 startPos, float exclusionRadius, int numberOfCandidates, Vector2 v0, Vector2 v1, Vector2 v2) {
			var activePoints = new List<Vector2>();
			var candidatePoints = new List<Vector2>();
			var finalPoints = new List<Vector2>();

			int gridX = (int)Mathf.Ceil(startPos.x / squareWidth);
			int gridY = (int)Mathf.Ceil(startPos.y / squareWidth);
			//set first grid element picked at random
			grid.Add((gridX, gridY), startPos);
			activePoints.Add(startPos);

			Vector2 randomActive;
			while (activePoints.Count > 0) {
				randomActive = activePoints[Random.Range(0, activePoints.Count)];
				for (int i = 0; i < numberOfCandidates; i++) {
					Vector2 candidatePoint = randomActive + Random.insideUnitCircle.normalized * Random.Range(exclusionRadius, exclusionRadius * 2f);
					//check if within bounds of the selected shape
					if (TriangleUtils.IsPointInsideTriangle(candidatePoint, v0, v1, v2)) {
						gridX = (int)Mathf.Ceil(candidatePoint.x / squareWidth);
						gridY = (int)Mathf.Ceil(candidatePoint.y / squareWidth);

						//check if this grid point is not used
						if (!grid.ContainsKey((gridX, gridY))) {
							candidatePoints.Add(candidatePoint);

							//check if far enough from neighbors
							bool isFarEnough = true;
							for (int y = -1; y <= 1; y++) {
								for (int x = -1; x <= 1; x++) {
									if (grid.TryGetValue((gridX + x, gridY + y), out Vector2 gridPoint)) {
										if (Vector2.Distance(candidatePoint, gridPoint) < exclusionRadius) isFarEnough = false;
									}
								}
							}
							if (isFarEnough) {
								activePoints.Add(candidatePoint);
								grid.Add((gridX, gridY), candidatePoint);
								candidatePoints.Clear();
								break;
							}
						}
					}
					//if none of the candidates were a match
					if (i == numberOfCandidates - 1) {
						//if no appropriate candidates were found, move point to finalPoints and assign to grid
						activePoints.Remove(randomActive);
						finalPoints.Add(randomActive);
						candidatePoints.Clear();
					}
				}
			}
			return finalPoints;
		}

		#endregion Triangle
		#region Quadrilateral

		/// <summary> Quadrilateral Init Grid </summary>
		private static void InitGrid(Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3, float exclusionRadius) {
			squareWidth = exclusionRadius / Mathf.Sqrt(2f); //Makes square's diagonal smaller than search radius
			float lowX = Mathf.Infinity;
			float lowY = Mathf.Infinity;
			float highX = Mathf.NegativeInfinity;
			float highY = Mathf.NegativeInfinity;
			SetLowHigh(v0);
			SetLowHigh(v1);
			SetLowHigh(v2);
			SetLowHigh(v3);
			gridCols = (int)Mathf.Ceil((highX - lowX) / squareWidth);
			gridRows = (int)Mathf.Ceil((highY - lowY) / squareWidth);
			grid = new Dictionary<(int, int), Vector2>(gridCols * gridRows);
			void SetLowHigh(Vector2 vector2) {
				if (vector2.x < lowX) lowX = vector2.x;
				if (vector2.y < lowY) lowY = vector2.y;
				if (vector2.x > highX) highX = vector2.x;
				if (vector2.y > highY) highY = vector2.y;
			}
		}

		private static List<Vector2> CreatePoints(Vector2 startPos, float exclusionRadius, int numberOfCandidates, Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3) {
			var activePoints = new List<Vector2>();
			var candidatePoints = new List<Vector2>();
			var finalPoints = new List<Vector2>();

			int gridX = (int)Mathf.Ceil(startPos.x / squareWidth);
			int gridY = (int)Mathf.Ceil(startPos.y / squareWidth);
			//set first grid element picked at random
			grid.Add((gridX, gridY), startPos);
			activePoints.Add(startPos);

			Vector2 randomActive;
			while (activePoints.Count > 0) {
				randomActive = activePoints[Random.Range(0, activePoints.Count)];
				for (int i = 0; i < numberOfCandidates; i++) {
					Vector2 candidatePoint = randomActive + Random.insideUnitCircle.normalized * Random.Range(exclusionRadius, exclusionRadius * 2f);
					//check if within bounds of the selected shape
					if (QuadrilateralUtils.IsPointInsideQuadrilateral(candidatePoint, v0, v1, v2, v3)) {
						gridX = (int)Mathf.Ceil(candidatePoint.x / squareWidth);
						gridY = (int)Mathf.Ceil(candidatePoint.y / squareWidth);

						//check if this grid point is not used
						if (!grid.ContainsKey((gridX, gridY))) {
							candidatePoints.Add(candidatePoint);

							//check if far enough from neighbors
							bool isFarEnough = true;
							for (int y = -1; y <= 1; y++) {
								for (int x = -1; x <= 1; x++) {
									if (grid.TryGetValue((gridX + x, gridY + y), out Vector2 gridPoint)) {
										if (Vector2.Distance(candidatePoint, gridPoint) < exclusionRadius) isFarEnough = false;
									}
								}
							}
							if (isFarEnough) {
								activePoints.Add(candidatePoint);
								grid.Add((gridX, gridY), candidatePoint);
								candidatePoints.Clear();
								break;
							}
						}
					}
					//if none of the candidates were a match
					if (i == numberOfCandidates - 1) {
						//if no appropriate candidates were found, move point to finalPoints and assign to grid
						activePoints.Remove(randomActive);
						finalPoints.Add(randomActive);
						candidatePoints.Clear();
					}
				}
			}
			return finalPoints;
		}

		#endregion Quadrilateral
		#region Circle

		/// <summary> Circle Init</summary>
		private static void InitGrid(float radius, float exclusionRadius) {
			squareWidth = exclusionRadius / Mathf.Sqrt(2f); //Makes square's diagonal smaller than search radius
			gridCols = gridRows = (int)Mathf.Ceil(2 * radius / squareWidth);
			grid = new Dictionary<(int, int), Vector2>(gridCols * gridRows);
		}

		private static List<Vector2> CreatePoints(float exclusionRadius, int numberOfCandidates, float radius) {
			var activePoints = new List<Vector2>();
			var candidatePoints = new List<Vector2>();
			var finalPoints = new List<Vector2>();
			Vector2 center = new Vector2(radius, radius);

			//Pick random start position
			Vector2 startPos = Random.insideUnitCircle * radius + center;

			int gridX = (int)Mathf.Ceil(startPos.x / squareWidth);
			int gridY = (int)Mathf.Ceil(startPos.y / squareWidth);
			//set first grid element picked at random
			grid.Add((gridX, gridY), startPos);
			activePoints.Add(startPos);

			Vector2 randomActive;
			while (activePoints.Count > 0) {
				randomActive = activePoints[Random.Range(0, activePoints.Count)];
				for (int i = 0; i < numberOfCandidates; i++) {
					Vector2 candidatePoint = randomActive + Random.insideUnitCircle.normalized * Random.Range(exclusionRadius, exclusionRadius * 2f);
					//check if within bounds of the selected shape
					if (Vector2.Distance(center, candidatePoint) < radius) {
						gridX = (int)Mathf.Ceil(candidatePoint.x / squareWidth);
						gridY = (int)Mathf.Ceil(candidatePoint.y / squareWidth);

						//check if this grid point is not used
						if (!grid.ContainsKey((gridX, gridY))) {
							candidatePoints.Add(candidatePoint);

							//check if far enough from neighbors
							bool isFarEnough = true;
							for (int y = -1; y <= 1; y++) {
								for (int x = -1; x <= 1; x++) {
									if (grid.TryGetValue((gridX + x, gridY + y), out Vector2 gridPoint)) {
										if (Vector2.Distance(candidatePoint, gridPoint) < exclusionRadius) isFarEnough = false;
									}
								}
							}
							if (isFarEnough) {
								activePoints.Add(candidatePoint);
								grid.Add((gridX, gridY), candidatePoint);
								candidatePoints.Clear();
								break;
							}
						}
					}
					//if none of the candidates were a match
					if (i == numberOfCandidates - 1) {
						//if no appropriate candidates were found, move point to finalPoints and assign to grid
						activePoints.Remove(randomActive);
						finalPoints.Add(randomActive - center);
						candidatePoints.Clear();
					}
				}
			}
			return finalPoints;
		}

		#endregion Circle
	}
}