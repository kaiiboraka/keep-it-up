using Elythia;
using Godot;    
using static HorizontalDirection;
using static VerticalDirection;

public static class VectorExtensions
    {
        /// <summary>
        /// Returns which HorizontalDirection the vector is facing based on the sign of x.
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static HorizontalDirection Horizontal(this Vector2 vector2)
        {
            HorizontalDirection direction = vector2.X switch
            {
                > Mathf.Epsilon => Right,
                < -Mathf.Epsilon => Left,
                _ => HorizontalDirection.None
            };

            return direction;
        }

        /// <summary>
        /// Returns which VerticalDirection the vector is facing based on the sign of y.
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static VerticalDirection Vertical(this Vector2 vector2)
        {
            VerticalDirection direction = vector2.X switch
            {
                > .001f => Up,
                < -.001f => Down,
                _ => VerticalDirection.None
            };

            return direction;
        }
        
        public static Vector3 XY(this Vector3 v)
        {
            return new Vector3(v.X, v.Y, 0);
        }

        public static Vector3 YZ(this Vector3 v)
        {
            return new Vector3(0, v.Y, v.Z);
        }

        public static Vector3 XZ(this Vector3 v)
        {
            return new Vector3(v.X, 0, v.Z);
        }
        
        #region Vector Rounders

        public static Vector2 Round(this Vector2 vector, int precision = 0)
        {
            vector.X = vector.X.RoundToPrecision(precision);
            vector.Y = vector.Y.RoundToPrecision(precision);
            return vector;
        }

        public static Vector3 Round(this Vector3 vector, int precision = 0)
        {
            vector.X = vector.X.RoundToPrecision(precision);
            vector.Y = vector.Y.RoundToPrecision(precision);
            vector.Z = vector.Z.RoundToPrecision(precision);
            return vector;
        }

        public static Vector4 Round(this Vector4 vector, int precision = 0)
        {
            vector.X = vector.X.RoundToPrecision(precision);
            vector.Y = vector.Y.RoundToPrecision(precision);
            vector.Z = vector.Z.RoundToPrecision(precision);
            vector.W = vector.W.RoundToPrecision(precision);
            return vector;
        }

        #endregion

        #region Vector Flippers

        public static Vector2 FlipX(this Vector2 vector)
        {
            vector.X *= -1;
            return vector;
        }

        public static Vector2 FlipY(this Vector2 vector)
        {
            vector.Y *= -1;
            return vector;
        }

        public static Vector2 FaceLeft(this Vector2 vector)
        {
            return vector.X > 0 ? vector.FlipX() : vector;
        }

        public static Vector2 FaceRight(this Vector2 vector)
        {
            return vector.X < 0 ? vector.FlipX() : vector;
        }

        public static Vector2 FaceDown(this Vector2 vector)
        {
            return vector.Y.IsNeg() ? vector.FlipY() : vector;
        }

        public static Vector2 FaceUp(this Vector2 vector)
        {
            return vector.Y.IsPos() ? vector.FlipY() : vector;
        }

        public static Vector2I FlipX(this Vector2I vector)
        {
            vector.X *= -1;
            return vector;
        }

        public static Vector2I FlipY(this Vector2I vector)
        {
            vector.Y *= -1;
            return vector;
        }

        public static Vector2I FaceLeft(this Vector2I vector)
        {
            return vector.X > 0 ? vector.FlipX() : vector;
        }

        public static Vector2I FaceRight(this Vector2I vector)
        {
            return vector.X < 0 ? vector.FlipX() : vector;
        }

        public static Vector2I FaceDown(this Vector2I vector)
        {
            return vector.Y > 0 ? vector.FlipY() : vector;
        }

        public static Vector2I FaceUp(this Vector2I vector)
        {
            return vector.Y < 0 ? vector.FlipY() : vector;
        }


        public static Vector3 FlipX(this Vector3 vector)
        {
            vector.X *= -1;
            return vector;
        }

        public static Vector3 FlipY(this Vector3 vector)
        {
            vector.Y *= -1;
            return vector;
        }

        public static Vector3 FlipZ(this Vector3 vector)
        {
            vector.Z *= -1;
            return vector;
        }

        public static Vector3 FaceLeft(this Vector3 vector)
        {
            return vector.X > 0 ? vector.FlipX() : vector;
        }

        public static Vector3 FaceRight(this Vector3 vector)
        {
            return vector.X < 0 ? vector.FlipX() : vector;
        }

        public static Vector3 FaceDown(this Vector3 vector)
        {
            return vector.Y > 0 ? vector.FlipY() : vector;
        }

        public static Vector3 FaceUp(this Vector3 vector)
        {
            return vector.Y < 0 ? vector.FlipY() : vector;
        }

        public static Vector3 FaceZNeg(this Vector3 vector)
        {
            return vector.Z > 0 ? vector.FlipZ() : vector;
        }

        public static Vector3 FaceZPos(this Vector3 vector)
        {
            return vector.Z < 0 ? vector.FlipZ() : vector;
        }


        public static Vector3I FlipX(this Vector3I vector)
        {
            vector.X *= -1;
            return vector;
        }

        public static Vector3I FlipY(this Vector3I vector)
        {
            vector.Y *= -1;
            return vector;
        }

        public static Vector3I FlipZ(this Vector3I vector)
        {
            vector.Z *= -1;
            return vector;
        }

        public static Vector3I FaceLeft(this Vector3I vector)
        {
            return vector.X > 0 ? vector.FlipX() : vector;
        }

        public static Vector3I FaceRight(this Vector3I vector)
        {
            return vector.X < 0 ? vector.FlipX() : vector;
        }

        public static Vector3I FaceDown(this Vector3I vector)
        {
            return vector.Y > 0 ? vector.FlipY() : vector;
        }

        
        public static Vector3I FaceUp(this Vector3I vector)
        {
            return vector.Y < 0 ? vector.FlipY() : vector;
        }

        public static Vector3I FaceZNeg(this Vector3I vector)
        {
            return vector.Z > 0 ? vector.FlipZ() : vector;
        }

        public static Vector3I FaceZPos(this Vector3I vector)
        {
            return vector.Z < 0 ? vector.FlipZ() : vector;
        }


        public static Vector4 FlipX(this Vector4 vector)
        {
            vector.X *= -1;
            return vector;
        }

        public static Vector4 FlipY(this Vector4 vector)
        {
            vector.Y *= -1;
            return vector;
        }

        public static Vector4 FlipZ(this Vector4 vector)
        {
            vector.Z *= -1;
            return vector;
        }

        public static Vector4 FlipW(this Vector4 vector)
        {
            vector.W *= -1;
            return vector;
        }

        public static Vector4 FaceLeft(this Vector4 vector)
        {
            return vector.X > 0 ? vector.FlipX() : vector;
        }

        public static Vector4 FaceRight(this Vector4 vector)
        {
            return vector.X < 0 ? vector.FlipX() : vector;
        }

        public static Vector4 FaceDown(this Vector4 vector)
        {
            return vector.Y > 0 ? vector.FlipY() : vector;
        }

        public static Vector4 FaceUp(this Vector4 vector)
        {
            return vector.Y < 0 ? vector.FlipY() : vector;
        }

        public static Vector4 FaceZNeg(this Vector4 vector)
        {
            return vector.Z > 0 ? vector.FlipZ() : vector;
        }

        public static Vector4 FaceZPos(this Vector4 vector)
        {
            return vector.Z < 0 ? vector.FlipZ() : vector;
        }

        public static Vector4 FaceWNeg(this Vector4 vector)
        {
            return vector.W > 0 ? vector.FlipW() : vector;
        }

        public static Vector4 FaceWPos(this Vector4 vector)
        {
            return vector.W < 0 ? vector.FlipW() : vector;
        }

        #endregion

        public static bool CloseTo(this Vector2 vec, Vector2 other, float threshold)
        {
            return vec.DistanceTo(other) < threshold;
        }

        public static bool CloseTo(this Vector3 vec, Vector3 other, float threshold)
        {
            return vec.DistanceTo(other) < threshold;
        }
    }