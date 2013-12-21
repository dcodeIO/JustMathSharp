/*
Copyright 2013 Daniel Wirtz <dcode@dcode.io>

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;

/// <summary>
/// A straight port of JustMath.js to C#.
/// </summary>
namespace JustMath {

	/// <summary>
	/// Just math.
	/// </summary>
	public static class JustMath {

		/// <summary>
		/// Respresents the square root of 2.
		/// </summary>
		public static double SQRT2 = Math.Sqrt(2);

		/// <summary>
		/// Respresents the square root of 1/2.
		/// </summary>
		public static double SQRT1_2 = Math.Sqrt(0.5);
	}

	/// <summary>
	/// Represents a two dimensional vector.
	/// </summary>
	/// <description>Vector operations always affect the initial Vec2 instance and return the instance itself
	/// for chaining. So clone where necessary. This is done to reduce the allocation footprint slightly.
	/// </description>
	public class Vec2 : IComparable {
		/// <summary>
		/// X component.
		/// </summary>
		public double X;
		/// <summary>
		/// Y component.
		/// </summary>
		public double Y;

		/// <summary>
		/// Constructs a new Vec2 at (0|0).
		/// </summary>
		public Vec2() {
			this.X = 0;
			this.Y = 0;
		}

		/// <summary>
		/// Constructs a new Vec2 with the specified X and Y components.
		/// </summary>
		public Vec2(double x, double y) {
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Constructs a new Vec2 by copying another Vec2.
		/// </summary>
		public Vec2(Vec2 v) {
			this.X = v.X;
			this.Y = v.Y;
		}

		/// <summary>
		/// Clones this Vec2.
		/// </summary>
		public Vec2 Clone() {
			return new Vec2(this);
		}

		/// <summary>
		/// Sets the X and Y components of this Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Set(double x, double y) {
			this.X = x;
			this.Y = y;
			return this;
		}

		/// <summary>
		/// Sets the components of this Vec2 by copying another Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Set(Vec2 v) {
			this.X = v.X;
			this.Y = v.Y;
			return this;
		}

		/// <summary>
		/// Adds a X and Y component to this Vec2.
		/// </summary>
		public Vec2 Add(double x, double y) {
			this.X += x;
			this.Y += y;
			return this;
		}

		/// <summary>
		/// Adds a Vec2 to this Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Add(Vec2 v) {
			this.X += v.X;
			this.Y += v.Y;
			return this;
		}

		/// <summary>
		/// Substracts a X and Y component from this Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Sub(double x, double y) {
			this.X -= x;
			this.Y -= y;
			return this;
		}

		/// <summary>
		/// Substracts a Vec2 from this Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Sub(Vec2 v) {
			this.X -= v.X;
			this.Y -= v.Y;
			return this;
		}

		/// <summary>
		/// Inverts this Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Inv() {
			this.X = -this.X;
			this.Y = -this.Y;
			return this;
		}

		/// <summary>
		/// Makes this Vec2 an orthogonal of itself by setting X=-Y and Y=X.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Ort() {
			double x = this.X;
			this.X = -this.Y;
			this.Y = x;
			return this;
		}

		/// <summary>
		/// Scales this Vec2 by a factor.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Scale(double f) {
			this.X *= f;
			this.Y *= f;
			return this;
		}

		/// <summary>
		/// Calculates the dot product of this and another Vec2.
		/// </summary>
		public double Dot(Vec2 v) {
			return this.X * v.X + this.Y * v.Y;
		}

		/// <summary>
		/// Normalizes this Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Norm() {
			double l = Math.Sqrt(this.Dot(this));
			if (l != 0) {
				this.X /= l;
				this.Y /= l;
			}
			return this;
		}

		/// <summary>
		/// Calculates the squared distance between this and another Vec2.
		/// </summary>
		public double DistSq(Vec2 v) {
			double dx = this.X - v.X;
			double dy = this.Y - v.Y;
			return dx * dx + dy * dy;
		}

		/// <summary>
		/// Calculates the distance between this and another Vec2.
		/// </summary>
		public double Dist(Vec2 v) {
			return Math.Sqrt(this.DistSq(v));
		}

		/// <summary>
		/// Calculates the direction of this Vec2.
		/// </summary>
		public double Dir() {
			return Math.Atan2(this.Y, this.X);
		}

		/// <summary>
		/// Calculates the squared magnitude of this Vec2.
		/// </summary>
		public double MagSq() {
			return this.Dot(this);
		}

		/// <summary>
		/// Calculates the magnitude of this Vec2.
		/// </summary>
		public double Mag() {
			return Math.Sqrt(this.MagSq());
		}

		/// <summary>
		/// Rotates this Vec2 by the given angle.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Rotate(double theta) {
			double sin = Math.Sin(theta),
			cos = Math.Cos(theta),
			x = this.X * cos - this.Y * sin;
			this.Y = this.X * sin + this.Y * cos;
			this.X = x;
			return this;
		}

		/// <summary>
		/// Projects this Vec2 on another Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Project(Vec2 v) {
			return this.Set(v.Clone().Scale(this.Dot(v) / v.Dot(v)));
		}

		/// <summary>
		/// Rejects this Vec2 from another Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Reject(Vec2 v) {
			return this.Sub(this.Clone().Project(v));
		}

		/// <summary>
		/// Reflects this Vec2 from another Vec2.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Reflect(Vec2 v) {
			v = v.Clone().Norm();
			return this.Set(v.Scale(2 * this.Dot(v)).Sub(this));
		}

		/// <summary>
		/// Reflects this Vec2 from another Vec2 and scales the projected and reflected component by the given factors.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 ReflectAndScale(Vec2 v, double projectFactor, double rejectFactor) {
			Vec2 p = v.Clone().Norm(),
			r = v.Clone().Ort().Norm();
			return this.Set(p.Scale(this.Dot(p) * projectFactor).Add(r.Scale(-this.Dot(r) * rejectFactor)));
		}

		/// <summary>
		/// Interpolates the point between this and another point (in that direction) at the given percentage.
		/// </summary>
		/// <returns><c>this</c></returns>
		public Vec2 Lerp(Vec2 v, double percent) {
			return this.Add(v.Clone().Sub(this).Scale(percent));
		}

		/// <summary>
		/// Tests if this Vec2 is contained in the rectangle created between p1 and p2.
		/// </summary>
		/// <returns><c>true</c>, if contained, <c>false</c> otherwise.</returns>
		public bool InRect(Vec2 p1, Vec2 p2) {
			return ((p1.X <= this.X && this.X <= p2.X) || (p1.X >= this.X && this.X >= p2.X)) &&
			((p1.Y <= this.Y && this.Y <= p2.Y) || (p1.Y >= this.Y && this.Y >= p2.Y));
		}

		/// <summary>
		/// Calculates the determinant of the matrix [v1,v2].
		/// </summary>
		public static double Det(Vec2 v1, Vec2 v2) {
			return v1.X * v2.Y - v2.X * v1.Y;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="JustMath.Vec2.Vec2"/>.
		/// </summary>
		/// <param name="o">The <see cref="System.Object"/> to compare with the current <see cref="JustMath.Vec2.Vec2"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="JustMath.Vec2.Vec2"/>;
		/// otherwise, <c>false</c>.</returns>
		public override bool Equals(object o) {
			if (o == null || !(o is Vec2))
				return false;
			Vec2 v = o as Vec2;
			return this.X == v.X && this.Y == v.Y;
		}

		/// <summary>
		/// Compares the current instance with another object of the same type.
		/// </summary>
		public int CompareTo(Object o) {
			if (o == null || !(o is Vec2)) {
				return 0;
			}
			Vec2 v = o as Vec2;
			double m = this.Mag(),
			mv = v.Mag();
			return m < mv ? -1 : (m > mv ? 1 : 0);
		}

		/// <summary>
		/// Provides a Hash function for the value
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode() {
			return this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="JustMath.Vec2"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="JustMath.Vec2"/>.</returns>
		public override String ToString() {
			return "Vec2(" + this.X + "/" + this.Y + ")";
		}
	}
}

