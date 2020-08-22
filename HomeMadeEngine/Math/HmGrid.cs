using HomeMadeEngine.Templates;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace HomeMadeEngine.Math
{
    public class HmGrid
    {
        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public int Dimension { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public List<List<List<SpaceTakersTemplate>>> Space { get; private set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public HmGrid(int p_x, int p_y, int p_z, SpaceTakersTemplate p_spot)
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
            this.Space = new List<List<List<SpaceTakersTemplate>>>();
            for(int x = p_x; x > 0; x--)
            {
                List<List<SpaceTakersTemplate>> Yrow = new List<List<SpaceTakersTemplate>>();
                for (int y = p_y; y > 0; y--) 
                {
                    List<SpaceTakersTemplate> Zrow = new List<SpaceTakersTemplate>();
                    for (int z = p_z; z > 0; z--)
                    {
                        Zrow.Add(p_spot);
                    }
                    if (p_z <= 0)
                        Zrow.Add(p_spot);
                    Yrow.Add(Zrow);
                }
                this.Space.Add(Yrow);
            }
            this.Dimension = 1 + ((this.Y > 0) ? 1 : 0) + ((this.Z > 0) ? 1 : 0);
        }
        public HmGrid(List<List<List<SpaceTakersTemplate>>>p_space)
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
        public HmGrid(int p_x, int p_y, SpaceTakersTemplate p_spot) : this(p_x, p_y, 0, p_spot) { }
        public HmGrid(int p_x, SpaceTakersTemplate p_spot) : this(p_x, 0, p_spot) { }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        public bool IsColliding(HmGrid p_space)
        {
            HmGrid comparative = this.CopySizeTo(p_space);
            for (int x = this.X; x > 0; x--)
            {
                for (int y = this.Y; y > 0; y--)
                {
                    for (int z = this.Z; z > 0; z--)
                    {
                        if ((int)this.Space[x][y][z].Type>0 && (int)comparative.Space[x][y][z].Type > 0) return true;
                    }
                }
            }
            return false;
        }
        public HmGrid CopySizeTo(HmGrid p_space)
        {
            HmGrid newSpace = new HmGrid(this.X, this.Y, this.Z, new SpaceTakersTemplate(SpaceTaker.Nothing));
            if (p_space.X != this.X || p_space.Y != this.Y || p_space.Z != this.Z)
            {
                for (int x = this.X; x > 0; x--)
                {
                    for (int y = this.Y; y > 0; y--)
                    {
                        for (int z = this.Z; z > 0; z--)
                        {
                            if (x > p_space.X || y > p_space.Y || z > p_space.Z)
                                newSpace.Space[x][y][z] = new SpaceTakersTemplate(SpaceTaker.Nothing);
                            else
                                newSpace.Space[x][y][z] = p_space.Space[x][y][z];
                        }
                    }
                }
                return newSpace;
            }
            else
                return p_space;
        }
        public void ResetGrid()
        {
            foreach (List<List<SpaceTakersTemplate>> towd in this.Space)
                foreach (List<SpaceTakersTemplate> oned in towd)
                    foreach (SpaceTakersTemplate sq in oned)
                       sq.ChangeType(SpaceTaker.Nothing);
        }
        public void ChangeSpot(int p_x, int p_y, int p_z, SpaceTaker p_type) => this.Space[p_x][p_y][p_z].ChangeType(p_type);
        public void ChangeSpot(int p_x, int p_y, SpaceTaker p_type) => this.ChangeSpot(p_x, p_y, 0, p_type);
        public void ChangeSpot(int p_x, SpaceTaker p_type) => this.ChangeSpot(p_x, 0, p_type);
        public void ChangeSpot(HmVector p_vector, SpaceTaker p_type) => this.ChangeSpot((int)p_vector.X, (int)p_vector.Y, (int)p_vector.Z, p_type);
    }
}
