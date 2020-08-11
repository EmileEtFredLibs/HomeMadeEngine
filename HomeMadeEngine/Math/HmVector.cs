using System;
using System.Numerics;
using static System.Math;

namespace HomeMadeEngine.Math
{
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
        public HmVector(double p_x, double p_y, double p_z)
        {
            this.X = p_x;
            this.Y = p_y;
            this.Z = p_z;
        }
        public HmVector(double p_x, double p_y) : this(p_x, p_y, 0) { }
        public HmVector(double p_x) : this(p_x, 0) { }
        public HmVector() : this(0) { }

        // public HmVector(float p_angle, double p_size) => new HmVector()

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        // MUILIPLY
        //------------------------------------------------------------------------------------------------------------
        public HmVector Multiply(HmVector vector) => new HmVector(this.X * vector.X, this.Y * vector.Y, this.Z * vector.Z);
        public HmVector Multiply(double x) => new HmVector(this.X * x, this.Y, this.Z);
        public HmVector Multiply(double x, double y) => new HmVector(this.X * x, this.Y * y, this.Z);
        public HmVector Multiply(double x, double y, double z) => new HmVector(this.X * x, this.Y * y, this.Z * z);
        public void MultiplyOnSelf(HmVector vector)
        {
            this.X *= vector.X;
            this.Y *= vector.Y;
            this.Z *= vector.Z;
        }
        public void MultiplyOnSelf(double x) => this.X *= x;
        public void MultiplyOnSelf(double x, double y)
        {
            this.X *= x;
            this.Y *= y;
        }
        public void MultiplyOnSelf(double x, double y, double z)
        {
            this.X *= x;
            this.Y *= y;
            this.Z *= z;
        }

        // DIVIDE
        //------------------------------------------------------------------------------------------------------------
        public HmVector Divide(HmVector vector) => new HmVector(this.X / vector.X, this.Y / vector.Y, this.Z / vector.Z);
        public HmVector Divide(double x) => new HmVector(this.X / x, this.Y, this.Z);
        public HmVector Divide(double x, double y) => new HmVector(this.X / x, this.Y / y, this.Z);
        public HmVector Divide(double x, double y, double z) => new HmVector(this.X / x, this.Y / y, this.Z / z);
        public void DivideOnSelf(HmVector vector)
        {
            this.X /= vector.X;
            this.Y /= vector.Y;
            this.Z /= vector.Z;
        }
        public void DivideOnSelf(double x) => this.X /= x;
        public void DivideOnSelf(double x, double y)
        {
            this.X /= x;
            this.Y /= y;
        }
        public void DivideOnSelf(double x, double y, double z)
        {
            this.X /= x;
            this.Y /= y;
            this.Z /= z;
        }

        // ADD
        //------------------------------------------------------------------------------------------------------------
        public HmVector Add(HmVector vector) => new HmVector(this.X + vector.X, this.Y + vector.Y, this.Z + vector.Z);
        public HmVector Add(double x) => new HmVector(this.X + x, this.Y, this.Z);
        public HmVector Add(double x, double y) => new HmVector(this.X + x, this.Y + y, this.Z);
        public HmVector Add(double x, double y, double z) => new HmVector(this.X + x, this.Y + y, this.Z + z);
        public void AddOnSelf(HmVector vector)
        {
            this.X += vector.X;
            this.Y += vector.Y;
            this.Z += vector.Z;
        }
        public void AddOnSelf(double x) => this.X += x;
        public void AddOnSelf(double x, double y)
        {
            this.X += x;
            this.Y += y;
        }
        public void AddOnSelf(double x, double y, double z)
        {
            this.X += x;
            this.Y += y;
            this.Z += z;
        }

        // SUBSTRACT
        //------------------------------------------------------------------------------------------------------------
        public HmVector Substract(HmVector vector) => new HmVector(this.X - vector.X, this.Y - vector.Y, this.Z - vector.Z);
        public HmVector Substract(double x) => new HmVector(this.X - x, this.Y, this.Z);
        public HmVector Substract(double x, double y) => new HmVector(this.X - x, this.Y - y, this.Z);
        public HmVector Substract(double x, double y, double z) => new HmVector(this.X - x, this.Y - y, this.Z - z);
        public void SubstractOnSelf(HmVector vector)
        {
            this.X -= vector.X;
            this.Y -= vector.Y;
            this.Z -= vector.Z;
        }
        public void SubstractOnSelf(double x) => this.X -= x;
        public void SubstractOnSelf(double x, double y)
        {
            this.X -= x;
            this.Y -= y;
        }
        public void SubstractOnSelf(double x, double y, double z)
        {
            this.X -= x;
            this.Y -= y;
            this.Z -= z;
        }
    }
}
