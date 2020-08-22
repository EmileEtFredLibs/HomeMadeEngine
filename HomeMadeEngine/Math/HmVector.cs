using System;
using System.Numerics;
using static System.Math;

namespace HomeMadeEngine.Math
{
    [Serializable]
    public class HmVector
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        /// <summary>
        /// Creation of a vector 3D
        /// </summary>
        /// <param name="p_x">X axe</param>
        /// <param name="p_y">Y axe</param>
        /// <param name="p_z">Z axe</param>
        public HmVector(double p_x, double p_y, double p_z)
        {
            this.X = p_x;
            this.Y = p_y;
            this.Z = p_z;
        }
        /// <summary>
        /// Creation of a vector 2D
        /// </summary>
        /// <param name="p_x">X axe</param>
        /// <param name="p_y">Y axe</param>
        public HmVector(double p_x, double p_y) : this(p_x, p_y, 0) { }
        /// <summary>
        /// Creation of a vector 1D
        /// </summary>
        /// <param name="p_x">X axe</param>
        public HmVector(double p_x) : this(p_x, 0) { }
        /// <summary>
        /// Creation of a vector 0,0,0
        /// </summary>
        public HmVector() : this(0) { }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        // MUILIPLY
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Multiply this vector with another one without changing either
        /// </summary>
        /// <param name="vector">Vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Multiply(HmVector vector) => new HmVector(this.X * vector.X, this.Y * vector.Y, this.Z * vector.Z);
        /// <summary>
        /// Multiply this vector with another one without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Multiply(double x) => new HmVector(this.X * x, this.Y, this.Z);
        /// <summary>
        /// Multiply this vector with another one without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Multiply(double x, double y) => new HmVector(this.X * x, this.Y * y, this.Z);
        /// <summary>
        /// Multiply this vector with another one without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <param name="z">Z axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Multiply(double x, double y, double z) => new HmVector(this.X * x, this.Y * y, this.Z * z);
        /// <summary>
        /// Multiply a vector on this one
        /// </summary>
        /// <param name="vector"></param>
        public void MultiplyOnSelf(HmVector vector)
        {
            this.X *= vector.X;
            this.Y *= vector.Y;
            this.Z *= vector.Z;
        }
        /// <summary>
        /// Multiply a vector on this one
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        public void MultiplyOnSelf(double x) => this.X *= x;
        /// <summary>
        /// Multiply a vector on this one
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        public void MultiplyOnSelf(double x, double y)
        {
            this.X *= x;
            this.Y *= y;
        }
        /// <summary>
        /// Multiply a vector on this one
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <param name="z">Z axe of the vector used for the operation on this</param>
        public void MultiplyOnSelf(double x, double y, double z)
        {
            this.X *= x;
            this.Y *= y;
            this.Z *= z;
        }

        // DIVIDE
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Divide this vector with another without changing either
        /// </summary>
        /// <param name="vector">Vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Divide(HmVector vector) => new HmVector(this.X / vector.X, this.Y / vector.Y, this.Z / vector.Z);
        /// <summary>
        /// Divide this vector with another without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Divide(double x) => new HmVector(this.X / x, this.Y, this.Z);
        /// <summary>
        /// Divide this vector with another without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Divide(double x, double y) => new HmVector(this.X / x, this.Y / y, this.Z);
        /// <summary>
        /// Divide this vector with another without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <param name="z">Z axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Divide(double x, double y, double z) => new HmVector(this.X / x, this.Y / y, this.Z / z);
        /// <summary>
        /// Divide this vector with another without changing either
        /// </summary>
        /// <param name="vector"></param>
        public void DivideOnSelf(HmVector vector)
        {
            this.X /= vector.X;
            this.Y /= vector.Y;
            this.Z /= vector.Z;
        }
        /// <summary>
        /// Divide this vector with another
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        public void DivideOnSelf(double x) => this.X /= x;
        /// <summary>
        /// Divide this vector with another
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        public void DivideOnSelf(double x, double y)
        {
            this.X /= x;
            this.Y /= y;
        }
        /// <summary>
        /// Divide this vector with another
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <param name="z">Z axe of the vector used for the operation on this</param>
        public void DivideOnSelf(double x, double y, double z)
        {
            this.X /= x;
            this.Y /= y;
            this.Z /= z;
        }

        // ADD
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Add this vector with another without changing either
        /// </summary>
        /// <param name="vector">Vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Add(HmVector vector) => new HmVector(this.X + vector.X, this.Y + vector.Y, this.Z + vector.Z);
        /// <summary>
        /// Add this vector with another without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Add(double x) => new HmVector(this.X + x, this.Y, this.Z);
        /// <summary>
        /// Add this vector with another without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Add(double x, double y) => new HmVector(this.X + x, this.Y + y, this.Z);
        /// <summary>
        /// Add this vector with another without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <param name="z">Z axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Add(double x, double y, double z) => new HmVector(this.X + x, this.Y + y, this.Z + z);
        /// <summary>
        /// Add this vector with another
        /// </summary>
        /// <param name="vector"></param>
        public void AddOnSelf(HmVector vector)
        {
            this.X += vector.X;
            this.Y += vector.Y;
            this.Z += vector.Z;
        }
        /// <summary>
        /// Add this vector with another
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        public void AddOnSelf(double x) => this.X += x;
        /// <summary>
        /// Add this vector with another
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        public void AddOnSelf(double x, double y)
        {
            this.X += x;
            this.Y += y;
        }
        /// <summary>
        /// Add this vector with another
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <param name="z">Z axe of the vector used for the operation on this</param>
        public void AddOnSelf(double x, double y, double z)
        {
            this.X += x;
            this.Y += y;
            this.Z += z;
        }

        // SUBSTRACT
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Substract a vector out of this one without changing either
        /// </summary>
        /// <param name="vector">Vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Substract(HmVector vector) => new HmVector(this.X - vector.X, this.Y - vector.Y, this.Z - vector.Z);
        /// <summary>
        /// Substract a vector out of this one without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Substract(double x) => new HmVector(this.X - x, this.Y, this.Z);
        /// <summary>
        /// Substract a vector out of this one without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Substract(double x, double y) => new HmVector(this.X - x, this.Y - y, this.Z);
        /// <summary>
        /// Substract a vector out of this one without changing either
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <param name="z">Z axe of the vector used for the operation on this</param>
        /// <returns>Vector resulting</returns>
        public HmVector Substract(double x, double y, double z) => new HmVector(this.X - x, this.Y - y, this.Z - z);
        /// <summary>
        /// Substract a vector out of this one
        /// </summary>
        /// <param name="vector"></param>
        public void SubstractOnSelf(HmVector vector)
        {
            this.X -= vector.X;
            this.Y -= vector.Y;
            this.Z -= vector.Z;
        }
        /// <summary>
        /// Substract a vector out of this one
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        public void SubstractOnSelf(double x) => this.X -= x;
        /// <summary>
        /// Substract a vector out of this one
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        public void SubstractOnSelf(double x, double y)
        {
            this.X -= x;
            this.Y -= y;
        }
        /// <summary>
        /// Substract a vector out of this one
        /// </summary>
        /// <param name="x">X axe of the vector used for the operation on this</param>
        /// <param name="y">Y axe of the vector used for the operation on this</param>
        /// <param name="z">Z axe of the vector used for the operation on this</param>
        public void SubstractOnSelf(double x, double y, double z)
        {
            this.X -= x;
            this.Y -= y;
            this.Z -= z;
        }
        // SUBSTRACT ABSOLUTE
        //------------------------------------------------------------------------------------------------------------
        public HmVector SubstractAbs(HmVector p_vector)
        {
            HmVector result = this.Substract(p_vector);
            if (result.X < 0)
                result.X *= -1;
            if (result.Y < 0)
                result.Y *= -1;
            if (result.Z < 0)
                result.Z *= -1;
            return result;
        }
        public HmVector SubstractAbs(double x, double y, double z) => this.SubstractAbs(new HmVector(x, y, z));
        public HmVector SubstractAbs(double x, double y) => this.SubstractAbs(new HmVector(x, y, 0));
        public HmVector SubstractAbs(double x) => this.SubstractAbs(new HmVector(x, 0));

        public void SubstractAbsOnSelf(HmVector p_vector)
        {
            this.SubstractOnSelf(p_vector);
            if (this.X < 0)
                this.X *= -1;
            if (this.Y < 0)
                this.Y *= -1;
            if (this.Z < 0)
                this.Z *= -1;
        }
        public void SubstractAbsOnSelf(double x, double y, double z) => this.SubstractAbs(new HmVector(x, y, z));
        public void SubstractAbsOnSelf(double x, double y) => this.SubstractAbs(new HmVector(x, y, 0));
        public void SubstractAbsOnSelf(double x) => this.SubstractAbs(new HmVector(x, 0));
    }
}
