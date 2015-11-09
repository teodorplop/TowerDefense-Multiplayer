using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Custom geometry namespace. This implements some needed geometric algorithms.
/// </summary>

namespace Geometry {

	public enum Signature { Negative = -1, Positive}
    public enum Order { Trigonometric = 1, Clockwise = 2}

	public class Algorithm {

		/// <summary>
		/// Return the value of cross product for vectors {point1, point2} and {point1, point3}
		/// only for Z component.
		/// Usability: we can check that second vector is on the right side or the left side,
		/// checking the signature of the cross product for Z component.
		/// </summary>

		public static float CrossProductZ (Vector2 point1, Vector2 point2, Vector2 point3) {
			return (point2.x-point1.x)*(point3.y-point1.y)-(point3.x-point1.x)*(point2.y-point1.y);
		}

		/// <summary>
		/// Determines if the specified point is in the polygon[].
		/// Algorithm: Create a line from -10000 to 10000 on X-axis
		/// and check if intersects an odd number of segments that
		/// correspond to polygon. To find if two segments are intersected
		/// I use Cross Product (above) to check if the end points of this line
		/// are on opposite sides of segment and viceversa.
		/// 
		/// Complexity: O(N) with quite big constant, but is better than
		/// signed Area Algorithm which is O(N) with smaller constant, but with
		/// worse precision.
		/// </summary>

		public static bool IsInPolygon (Vector2 point, Vector2[] polygon) {

			int leftCrossings = 0;
			int rightCrossings = 0;

			Vector2 leftInfinity = new Vector2 (-Mathf.Infinity, point.y);
			Vector2 rightInfinity = new Vector2 (Mathf.Infinity, point.y);

			for (int i=0; i<polygon.Length; i++) {
                Vector2 startPoint = polygon[i];
                Vector2 endPoint = polygon[(i + 1) % polygon.Length];
                if (IntersectionCheck(point, leftInfinity, startPoint, endPoint))
					++ leftCrossings;
                if(IntersectionCheck(point, rightInfinity, startPoint, endPoint))
					++ rightCrossings;
			}

			return (leftCrossings&1) == 1 && (rightCrossings&1) == 1;

/*			Signature areaSignature = GetSignature (SignedDoubledTriangleArea (point, polygon [polygon.Length - 1], polygon [0]));

			for (int i=0; i<polygon.Length - 1; i++) {
				if (areaSignature != GetSignature (SignedDoubledTriangleArea (point, polygon [i], polygon [i+1]))) {
					return false;
				}
			}*/
		}

		public static Signature GetSignature (float x) {
			if (x <= 0.1f) {
				return Signature.Negative;
			} else {
				return Signature.Positive;
			}
		}

		/// <summary>
		/// Calculate the signed area of the triangle given by its points.
		/// 
		/// Usability: Triangle area formula IN 2D is:	1   | x1 y1 1 |
		/// 											- * | x2 y2 1 |
		/// 											2   | x3 y3 1 |
		/// 
		/// Sometimes we don't need the absolute value of determinant,
		/// but only the value of the determinant.
		/// </summary>

		public static float SignedDoubledTriangleArea (Vector2 point1, Vector2 point2, Vector2 point3) {
			return point1.x * point2.y + point1.y * point3.x + point2.x * point3.y 
					- point1.y * point2.x - point3.x * point2.y - point1.x * point3.y;

		}

		/// <summary>
		/// Calculate the area of a triangle given by its points.
		/// </summary>

		public static float TriangleArea (Vector2 point1, Vector2 point2, Vector2 point3) {
			return 0.5f * Mathf.Abs (SignedDoubledTriangleArea (point1, point2, point3));
		}

		/// <summary>
		/// Calculate and return the set of points who are
		/// inside of the of the polygon[].
		/// </summary>

		public static Vector2[] IntersectedPointsWithPolygon(Vector2[] polygon, Vector2[] points) {
			List<Vector2> intersectedPoints = new List<Vector2> ();

			foreach (Vector2 point in points) {
				if (IsInPolygon (point, polygon)) {
					intersectedPoints.Add (point);
				}
			}

			return intersectedPoints.ToArray ();
		}

        /// <summary>
        /// Linear interpolation between two 2D vectors by a factor.
        /// </summary>

        public static Vector2 Lerp(Vector2 from, Vector2 to, float lerpFactor) {
            return from * (1f - lerpFactor) + to * lerpFactor; 
        }

        /// <summary>
        /// Calculate the Order (Trigonometric or Clockwise) of a set of three points.
        /// </summary>

        public static Order GetOrder(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3) {
            if (GetSignature(CrossProductZ(vertex1, vertex2, vertex3)) == Signature.Positive)
                return Order.Clockwise;
            return Order.Trigonometric;
        }

        /// <summary>
        /// Collinear Check => | x1 y1 1 |
        ///                    | x2 y2 1 | = 0
        ///                    | x3 y3 1 |
        /// </summary>

        public static bool CollinearityCheck(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3)
        {
            return Mathf.Abs(vertex1.x * vertex2.y + vertex2.x * vertex3.y + vertex3.x * vertex1.y -
                vertex3.y * vertex2.y - vertex2.x * vertex1.y - vertex1.x * vertex3.y) < Mathf.Epsilon;
        }

        /// <summary>
        /// Check intersection between two segments.
        /// 
        /// Implements box checking. Uses Z-component from the Cross Product.
        /// </summary>

        public static bool IntersectionCheck(Vector2 v1s1, Vector2 v2s1, Vector2 v1s2, Vector2 v2s2)
        {
            if (GetSignature(CrossProductZ(v1s1, v2s1, v1s2)) == GetSignature(CrossProductZ(v1s1, v2s1, v2s2)))
                return false;
            if(GetSignature(CrossProductZ(v1s2, v2s2, v1s1)) == GetSignature(CrossProductZ(v1s2, v2s2, v2s1)))
                return false;

            return true;
        }

        /// <summary>
        /// Calculate the order (Trigonometric or Clockwise) of a polygon.
        /// 
        /// Implements this: http: //stackoverflow.com/questions/1165647/how-to-determine-if-a-list-of-polygon-points-are-in-clockwise-order.
        /// </summary>

        public static Order GetPolygonOrder(Vector2[] polygon)
        {
            float sum = 0f;

            for(int i=0;i<polygon.Length;i++)
            {
                Vector2 startPoint = polygon[i];
                Vector2 endPoint = polygon[(i + 1) % polygon.Length];

                sum += (endPoint.x - startPoint.x) * (endPoint.y + startPoint.y);
            }

            // We ignore degenerate cases.
            if (sum < 0)
                return Order.Clockwise;
            return Order.Trigonometric;
        }

        /// <summary>
        /// Return an array with indices on the vertices list. Every three 
        /// indices in a row determine a triangle.
        /// 
        /// Implements Ear Clipping Polygon Triangulation Algorithm.
        /// </summary>

        public static int[] CalculateTriangles(Vector2[] vertices)
        {
            List<int> result = new List<int>();
            List<int> availablePoints = new List<int>();
            
            for(int i=0;i<vertices.Length;i++)
                availablePoints.Add(i);

            while (availablePoints.Count > 3)
            {
                int[] ear = GetEar(availablePoints, vertices);
                if (ear.Length != 3)
                {
//                    Debug.LogError("Could not calculate a viable triangulation. Please check this problem!");
                    return new int[0];
                }

                availablePoints.Remove(ear[1]);

                result.AddRange(ear);
            }

            result.AddRange(availablePoints);

            return result.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>

        private static int[] GetEar(List<int> availablePoints, Vector2[] vertices)
        {
            for (int i = 0; i < availablePoints.Count; i++)
            {
                int firstVertex = availablePoints[i];
                int secondVertex = availablePoints[(i + 1) % availablePoints.Count];
                int thirdVertex = availablePoints[(i + 2) % availablePoints.Count];

                if (CollinearityCheck(vertices[firstVertex], vertices[secondVertex], vertices[thirdVertex]))
                    continue;
                if (Geometry.Algorithm.GetOrder(vertices[firstVertex], vertices[secondVertex], vertices[thirdVertex]) == Order.Clockwise)
                    continue;
                bool isEar = true;
                for (int j = 0; j < availablePoints.Count; j++)
                {
                    int startPoint = availablePoints[j];
                    int endPoint = availablePoints[(j + 1) % availablePoints.Count];
                    if (startPoint == firstVertex || endPoint == firstVertex)
                        continue;
                    if (startPoint == secondVertex || endPoint == secondVertex)
                        continue;
                    if (startPoint == thirdVertex || endPoint == thirdVertex)
                        continue;
                    if (IntersectionCheck(vertices[startPoint], vertices[endPoint], vertices[firstVertex], vertices[thirdVertex]))
                    {
                        isEar = false;
                        break;
                    }
                }
                if (isEar)
                    return new int[3] { firstVertex, secondVertex, thirdVertex };
            }

            return new int[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstVector"></param>
        /// <param name="secondVector"></param>
        /// <returns></returns>

        public static float Angle(Vector2 firstVector, Vector2 secondVector)
        {
            float dot = Vector2.Dot(firstVector, secondVector);
            float length = firstVector.magnitude * secondVector.magnitude;
            float cos = dot / length;
            return Mathf.Acos(cos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="secondPoint"></param>
        /// <param name="thirdPoint"></param>
        /// <returns></returns>

        public static float Angle(Vector2 firstPoint, Vector2 secondPoint, Vector2 thirdPoint)
        {
            return Angle(secondPoint - firstPoint, secondPoint - thirdPoint);
        }
	}

	public class Math {

		/// <summary>
		/// Calculate the counter clockwise angle of a 2D vector.
		/// </summary>
		
		public static float CounterClockwiseAngle(Vector2 vector) {
			float angle = Mathf.Atan2(vector.y, vector.x);
			return angle;
		}
		
		/// <summary>
		/// Calculate the clockwises angle of a 2D vector.
		/// </summary>
		
		public static float ClockwiseAngle(Vector2 vector) {
			float angle = Mathf.Atan2(vector.y, vector.x);
			return 2f * Mathf.PI - angle;
		}
	}
}
