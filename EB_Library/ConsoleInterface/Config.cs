using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ConsoleGamePlayer.ConsoleInterface
{
    [DataContract]
    public class Config
    {
        //------------------------------------------------------------------------------------------------------------
        // CONSTANTS
        //____________________________________________________________________________________________________________
        public const string JsonSaveData = "{\"Max\":1,\"Position\":0}";
        public const int NumberMenu = 4;

        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        [DataMember]
        public int Position { get; private set; }
        [DataMember]
        public InterfaceEnum Menu { get; private set; }
        [DataMember]
        public int Max { get; private set; }
        public string Cursor{ get; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        private Config(int p_pos)
        {
            this.Position = p_pos;
            this.Menu = InterfaceEnum.CombatMenu;
            this.Max = 10;
            this.Cursor = "> ";
        }
        public Config() : this(0) { }
        public Config(InterfaceEnum interf)
        {
            this.Position = 0;
            this.Menu = interf;
            this.Max = 10;
            this.Cursor = "> ";
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
