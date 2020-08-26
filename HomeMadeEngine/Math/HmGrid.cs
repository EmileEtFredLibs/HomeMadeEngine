using HomeMadeEngine.Templates;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
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
            for(int x = 0; p_x > x; x++)
            {
                List<List<SpaceTakersTemplate>> Yrow = new List<List<SpaceTakersTemplate>>();
                for (int y = 0; p_y> y ; y++) 
                {
                    List<SpaceTakersTemplate> Zrow = new List<SpaceTakersTemplate>();
                    for (int z = 0; p_z> z; z++)
                    {
                        Zrow.Add(new SpaceTakersTemplate(p_spot));
                    }
                    if (p_z <= 0)
                        Zrow.Add(new SpaceTakersTemplate(p_spot));
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
            this.Dimension = 1 + ((this.Y > 1) ? 1 : 0) + ((this.Z > 1) ? 1 : 0);
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public HmGrid(int p_x, int p_y, SpaceTakersTemplate p_spot) : this(p_x, p_y, 1, p_spot) { }
        public HmGrid(int p_x, SpaceTakersTemplate p_spot) : this(p_x, 1, p_spot) { }

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
        public void ResetCharacter()
        {
            foreach (List<List<SpaceTakersTemplate>> towd in this.Space)
                foreach (List<SpaceTakersTemplate> oned in towd)
                    foreach (SpaceTakersTemplate sq in oned)
                        if ((int)sq.Type<2)
                            sq.ChangeType(SpaceTaker.Nothing);
        }
        public void ChangeSpot(int p_x, int p_y, int p_z, SpaceTaker p_type) => this.Space[p_x-1][p_y-1][p_z-1].ChangeType(p_type);
        public void ChangeSpot(int p_x, int p_y, SpaceTaker p_type) => this.ChangeSpot(p_x, p_y, 1, p_type);
        public void ChangeSpot(int p_x, SpaceTaker p_type) => this.ChangeSpot(p_x, 1, p_type);
        public void ChangeSpot(HmVector p_vector, SpaceTaker p_type) => this.ChangeSpot((int)p_vector.X, (int)p_vector.Y, (int)p_vector.Z, p_type);
        public List<HmVector> Pathfinder(HmVector p_begin, HmVector p_end)
        {
            if (p_begin.X > this.X || p_begin.Y > this.Y || p_begin.Z > this.Z ||
                p_begin.X < 0 || p_begin.Y < 0 || p_begin.Z < 0)
                throw new ArgumentException("BEGINNING POINT MUST BE IN THE GRID");
            else if (p_end.X > this.X || p_end.Y > this.Y || p_end.Z > this.Z ||
                p_end.X < 0 || p_end.Y < 0 || p_end.Z < 0)
                throw new ArgumentException("BEGINNING POINT MUST BE IN THE GRID");
            else
            {
                var path = __Pathfinder__(p_begin, p_end, new List<HmVector>(), new List<HmVector>(), __ResetChoice__());
                return path;
            }
        }
        private List<HmVector> __Pathfinder__(HmVector p_actual, HmVector p_end, List<HmVector> p_path, List<HmVector> p_wrong, List<HmVector> p_choices)
        {
            List<HmVector> result = new List<HmVector>(p_path);
            List<HmVector> possibleRoute = p_choices;//__ObstacleFinder__(p_actual, p_choices, p_wrong); 
            result.Add(p_actual);
            if (!p_actual.Compare(p_end))
            {
                List<HmVector> pathX = new List<HmVector>(result);
                List<HmVector> pathY = new List<HmVector>(result);
                HmVector diffAbs = p_actual.SubstractAbs(p_end);
                HmVector diff = p_actual.Substract(p_end);
                HmVector signs = HmVector.ReturnSigns(diff);
                bool xIsPossible = false;
                bool yIsPossible = false;
                foreach (var choice in possibleRoute)
                {
                    if (choice.X == signs.X)
                        xIsPossible = true;
                    if (choice.Y == signs.Y)
                        yIsPossible = true;
                }
                if (xIsPossible && yIsPossible)
                {
                    var shorter = result; __Shortcut__(p_actual, result, p_wrong, possibleRoute);
                    if (diffAbs.Y <= diffAbs.X)
                    {
                        if (xIsPossible)
                        {
                            possibleRoute = __ResetChoice__(new HmVector(0 - signs.X, 0, 0));
                            pathX = __Pathfinder__(new HmVector(p_actual.X - signs.X, p_actual.Y, p_actual.Z), p_end, shorter, p_wrong, possibleRoute);
                        }
                    }
                    if (diffAbs.Y >= diffAbs.X)
                    {
                        if (yIsPossible)
                        {
                            possibleRoute = __ResetChoice__(new HmVector(0, 0 - signs.Y, 0));
                            pathY = __Pathfinder__(new HmVector(p_actual.X, p_actual.Y - signs.Y, p_actual.Z), p_end, shorter, p_wrong, possibleRoute);
                        }
                    }
                    if (diffAbs.Y == diffAbs.X)
                        result = (pathX.Count > pathY.Count) ? pathY : pathX;
                    else
                        result = (pathX.Count < pathY.Count) ? pathY : pathX;
                }
                else
                {
                    if (possibleRoute.Count <= 0)
                    {
                        p_wrong.Add(p_actual);
                        result.RemoveAt(result.Count - 1);
                    }
                }

            }
            else if (result.FindIndex(r => r.Compare(p_end)) == -1)
                result.Add(p_end);
            return result;
        }
        private List<HmVector> __Shortcut__(HmVector p_actual, List<HmVector> p_path, List<HmVector> p_wrong, List<HmVector> p_choice)
        {
            int nbInbetween;
            for(nbInbetween = 3; nbInbetween <= p_path.Count; nbInbetween++)
            {
                if (p_path[^nbInbetween].Compare(p_actual)) break;
            }
            if (nbInbetween >= p_path.Count) return p_path;
            for(int i=2; i<nbInbetween-1;i++)
            {
                p_path.RemoveAt(p_path.Count - i); 
            }
            return __Pathfinder__(p_path[^1], p_actual, p_path, p_wrong, p_choice);
        }
        private List<HmVector> __ResetChoice__() => new List<HmVector>
        {
            new HmVector(-1,0,0),
            new HmVector(0,-1,0),
            new HmVector(1,0,0),
            new HmVector(0,1,0)
        };
        private List<HmVector> __ResetChoice__(HmVector p_backward)
        {
            var choices = new List<HmVector>
            {
                new HmVector(-1,0,0),
                new HmVector(0,-1,0),
                new HmVector(1,0,0),
                new HmVector(0,1,0)
            };
            choices.RemoveAll((c) => c.X == p_backward.X && c.Y == p_backward.Y);
            return choices;
        }
        private List<HmVector> __ObstacleFinder__(HmVector p_actual, List<HmVector> p_choices, List<HmVector> p_wrong)
        {
            for (int i=0;i<p_choices.Count;i++)
            {
                int x = (int)(p_actual.X - p_choices[i].X);
                int y = (int)(p_actual.Y - p_choices[i].Y);
                int z = (int)p_actual.Z-1;
                bool wrong = false;
                foreach (var wr in p_wrong)
                {
                    if (wr.Compare(p_actual.X - p_choices[i].X, p_actual.Y - p_choices[i].Y))
                    {
                        wrong = true;
                    }
                }
                if (this.Space.Count <= x || this.Space[x].Count <= y ||
                    this.Space[x][y][z].Type != SpaceTaker.Nothing || wrong)
                {
                    p_choices.RemoveAll((c) => c == p_choices[i]);
                    i++;
                }

            }
            return p_choices;
        }
    }
}
