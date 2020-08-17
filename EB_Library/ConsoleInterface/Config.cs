using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ConsoleGamePlayer.ConsoleInterface
{
    [Serializable]
    public class Config
    {
        //------------------------------------------------------------------------------------------------------------
        // CONSTANTS
        //____________________________________________________________________________________________________________
        //[DataMember]
        //public const string JsonSaveData = "{\"Max\":1,\"Position\":0}";
        public const int NumberMenu = 4;

        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public int Position { get; private set; }
        public InterfaceEnum Menu { get; private set; }
        public int Max { get; private set; }
        public string Cursor{ get; }
        public string Version{ get; set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        private Config(int p_pos)
        {
            this.Position = p_pos;
            this.Menu = InterfaceEnum.CombatMenu;
            this.Max = 10;
            this.Cursor = "> ";
            this.Version = "";
        }
        public Config() : this(0) { }
        public Config(InterfaceEnum interf)
        {
            this.Position = 0;
            this.Menu = interf;
            this.Max = 10;
            this.Cursor = "> ";
            this.Version = "";
        }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        // POSITION NAVIGATION
        //------------------------------------------------------------------------------------------------------------
        public void Up() 
        {
            if (this.Position < this.Max) this.Position++;
        }
        public void Up(int p_inc)
        {
            if (this.Position + p_inc <= this.Max) this.Position++;
        }
        public void Down()
        {
            if (this.Position > 0) this.Position--;
        }
        public void Down(int p_dec)
        {
            if (this.Position - p_dec >= 0) this.Position--;
        }
        public void ChangingMax(int p_max)
        {
            if (this.Position > p_max) this.Position = p_max;
            this.Max = p_max;
        }
        public void ResetPos() => this.Position = 0;
        public void ResetMax() => ChangingMax(NumberMenu);

        // MENU NAVIGATION
        //------------------------------------------------------------------------------------------------------------
        public void MenuChanging(InterfaceEnum p_interf) => this.Menu = p_interf;
    }
}
