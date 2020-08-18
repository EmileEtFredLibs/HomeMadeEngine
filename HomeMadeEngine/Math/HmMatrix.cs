using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Math
{
    public class HmMatrix
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public int Dimension { get; private set; }
        public object[]? X { get; private set; }
        public object[]? Y { get; private set; }
        public object[]? Z { get; private set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public HmMatrix (int p_cols, int p_rows, int p_dept)
        {
            this.Dimension = 3;
            this.X = new object[p_cols];
            this.Y = new object[p_rows];
            this.Z = new object[p_dept];
        }
        public HmMatrix (int p_cols, int p_rows)
        {
            this.Dimension = 2;
            this.X = new object[p_cols];
            this.Y = new object[p_rows];
            this.Z = null;
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
    }
}
