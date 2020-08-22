using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        public List<List<List<bool>>> Space { get; private set; }

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
            this.Space = new List<List<List<bool>>>();
            for(int x = p_x; x > 0; x--)
            {
                List<List<bool>> Yrow = new List<List<bool>>();
                for (int y = p_y; y > 0; y--) 
                {
                    List<bool> Zrow = new List<bool>();
                    for (int z = p_z; z > 0; z--)
                    {
                        Zrow.Add(true);
                    }
                    if (p_z <= 0)
                        Zrow.Add(true);
                    Yrow.Add(Zrow);
                }
                this.Space.Add(Yrow);
            }
            this.Dimension = 1 + ((this.Y > 0) ? 1 : 0) + ((this.Z > 0) ? 1 : 0);
        }
        public HmAreaOfEffect(List<List<List<bool>>>p_space)
        {
            if (p_space.Count <= 0)
                throw new ArgumentException("X MUST BE HIGHER THAN 0");
            if (p_space[0].Count < 0)
                throw new ArgumentException("Y MUST BE POSITIVE");
            if (p_space[0][0].Count < 0)
                throw new ArgumentException("Z MUST BE POSITIVE");
            this.X = p_space.Count;
            this.Y = p_space[0].Count;
            this.Z = p_space[0][0].Count;
            this.Space = p_space;
            this.Dimension = 1 + ((this.Y > 0) ? 1 : 0) + ((this.Z > 0) ? 1 : 0);
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public HmAreaOfEffect(int p_x, int p_y) : this(p_x, p_y, 0) { }
        public HmAreaOfEffect(int p_x) : this(p_x, 0) { }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
