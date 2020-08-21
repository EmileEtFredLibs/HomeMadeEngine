using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Math
{
    public class HmAreaOfEffect
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public int Dimension { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public List<bool> Xspace { get; private set; }
        public List<bool> Yspace { get; private set; }
        public List<bool> Zspace { get; private set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public HmAreaOfEffect(int p_x, int p_y, int p_z)
        {
            if (p_x <= 0)
                throw new ArgumentException("X MUST BE HIGHER THAN 0");
            if (p_y < 0)
                throw new ArgumentException("Y MUST BE POSITIVE");
            if (p_z < 0)
                throw new ArgumentException("Z MUST BE POSITIVE");
            this.X = p_x;
            this.Y = p_y;
            this.Z = p_z;
            this.Xspace = new List<bool>();
            this.Yspace = new List<bool>();
            this.Zspace = new List<bool>();
            for(int i = p_x; i > 0; i--)
            {
                this.Xspace.Add(true);
            }
            for (int i = p_y; i > 0; i--)
            {
                this.Yspace.Add(true);
            }
            for (int i = p_z; i > 0; i--)
            {
                this.Zspace.Add(true);
            }
            this.Dimension = 1 + ((this.Y > 0) ? 1 : 0) + ((this.Z > 0) ? 1 : 0);
        }
        public HmAreaOfEffect(List<bool> p_x, List<bool> p_y, List<bool> p_z)
        {
            if (p_x.Count <= 0)
                throw new ArgumentException("X MUST BE HIGHER THAN 0");
            if (p_y.Count < 0)
                throw new ArgumentException("Y MUST BE POSITIVE");
            if (p_z.Count < 0)
                throw new ArgumentException("Z MUST BE POSITIVE");
            this.X = p_x.Count;
            this.Y = p_y.Count;
            this.Z = p_z.Count;
            this.Xspace = p_x;
            this.Yspace = p_y;
            this.Zspace = p_z;
            this.Dimension = 1 + ((this.Y > 0) ? 1 : 0) + ((this.Z > 0) ? 1 : 0);
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public HmAreaOfEffect(int p_x, int p_y) : this(p_x, p_y, 0) { }
        public HmAreaOfEffect(int p_x,) : this(p_x, 0) { }
        public HmAreaOfEffect(List<bool> p_x, List<bool> p_y) : this(p_x, p_y, new List<bool>()) { }
        public HmAreaOfEffect(List<bool> p_x):this(p_x, new List<bool>()) { }


        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
