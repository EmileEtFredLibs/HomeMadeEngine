using System;
using System.Collections.Generic;
using System.Linq;
using HomeMadeEngine.Math;

namespace HomeMadeEngine.Character
{
    [Serializable]
    public class CharacterTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // CONSTANTS
        //____________________________________________________________________________________________________________
        public static HmVector BaseGravity = new HmVector() { X = 0, Y = 0, Z = 0 };
        public static string[] StatNames = new string[] {
            "Unmidigatable Attack", "Unmidigatable Defense", 
            "Physical Attack", "Physical Defense",
            "Magical Attack", "Magical Defense", 
            "Psychological Attack", "Psychological Defense"
        };

        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
        public int Level { get; private set; }
        public decimal Experience { get; private set; }
        public int CurrentHp { get; private set; }
        public int MaxHp { get; private set; }
        public int Shield { get; private set; }
        public int ShieldTimer { get; private set; }
        public SpellCost Spellcost { get; private set; }
        public int CurrentRessource { get; private set; }
        public int MaxRessource { get; private set; }
        public bool IsDead { get; private set; }
        public List<StatsTemplate> Stats { get; private set; }
        public List<EquipementsTemplate> Equipement { get; private set; }
        public List<ActionsTemplate> Actions { get; private set; }
        public List<BuffsTemplate> Buffs { get; private set; }
        public List<DebuffsTemplate> Debuffs { get; private set; }
        public HmVector Position { get; private set; }
        public HmVector Velocity { get; private set; }
        public HmVector Acceleration { get; private set; }
        public HmVector Gravity { get; private set; }

        //------------------------------------------------------------------------------------------------------------
        // CONSTRUCTORS
        //____________________________________________________________________________________________________________
        // MAIN CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor for a character
        /// </summary>
        /// <param name="p_lvl">Level of the character</param>
        /// <param name="p_exp">Experience accumulated for leveling up</param>
        /// <param name="p_cHp">Current health point</param>
        /// <param name="p_maxHp">Maximum health point</param>
        /// <param name="p_shield">Temporary health that take damage before health</param>
        /// <param name="p_shieldTimer">Time until the shield expirer</param>
        /// <param name="p_spellCost">What will take the cost of the cast of an actions if it has a cost</param>
        /// <param name="p_cRessource">Current action ressources</param>
        /// <param name="p_ressource">Maximum action ressources</param>
        /// <param name="p_isDead">Is the character dead?</param>
        /// <param name="p_stat">List of stats of the character</param>
        /// <param name="p_equip">List of equipement</param>
        /// <param name="p_actions">List of actions the character can do</param>
        /// <param name="p_buffs">List of buffs</param>
        /// <param name="p_debuffs">List of debuffs</param>
        /// <param name="p_xPox">X spacial positions</param>
        /// <param name="p_yPos">Y spacial positions</param>
        /// <param name="p_zPos">Z spacial positions</param>
        /// <param name="p_xVect">X velocity</param>
        /// <param name="p_yVect">Y velocity</param>
        /// <param name="p_zVect">Z velocity</param>
        public CharacterTemplate(int p_lvl, decimal p_exp, int p_cHp, int p_maxHp, int p_shield, int p_shieldTimer, SpellCost p_spellCost, 
            int p_cRessource, int p_ressource, bool p_isDead, List<StatsTemplate>p_stat, List<EquipementsTemplate>p_equip, 
            List<ActionsTemplate> p_actions, List<BuffsTemplate> p_buffs, List<DebuffsTemplate> p_debuffs,
            double p_xPox, double p_yPos, double p_zPos, double p_xVect, double p_yVect, double p_zVect)
        {
            if (p_maxHp < 0)
                throw new ArgumentException("HP MUST BE POSITIVE");
            if (p_cHp > p_maxHp)
                throw new ArgumentException("CURRENT HP CAN'T BE HIGHER THAN MAX HP");
            if (p_cHp > p_maxHp)
                throw new ArgumentException("CURRENT RESSOURCE CAN'T BE HIGHER THAN MAX RESSOURCE");
            if (p_ressource < 0)
                throw new ArgumentException("RESSOURCES MUST BE POSITIVE");
            this.Level = p_lvl;
            this.Experience = p_exp;
            this.CurrentHp = p_cHp;
            this.MaxHp = p_maxHp;
            this.Shield = p_shield;
            this.ShieldTimer = p_shieldTimer;
            this.Spellcost = p_spellCost;
            this.CurrentRessource = p_cRessource;
            this.MaxRessource = p_ressource;
            this.IsDead = p_isDead;
            this.Stats = p_stat;
            this.Equipement = p_equip;
            this.Actions = p_actions;
            this.Buffs = p_buffs;
            this.Debuffs = p_debuffs;
            this.Position = new HmVector(p_xPox, p_yPos, p_zPos);
            this.Velocity = new HmVector(p_xVect, p_yVect, p_zVect);
            this.Acceleration = new HmVector();
            this.Gravity = new HmVector(BaseGravity.X, BaseGravity.Y, BaseGravity.Z);
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor for a character
        /// </summary>
        /// <param name="p_cHp">Current health point</param>
        /// <param name="p_maxHp">Maximum health point</param>
        /// <param name="p_shield">Temporary health that take damage before health</param>
        /// <param name="p_shieldTimer">Time until the shield expirer</param>
        /// <param name="p_spellCost">What will take the cost of the cast of an actions if it has a cost</param>
        /// <param name="p_cRessource">Current action ressources</param>
        /// <param name="p_ressource">Maximum action ressources</param>
        /// <param name="p_isDead">Is the character dead?</param>
        /// <param name="p_xPox">X spacial positions</param>
        /// <param name="p_yPos">Y spacial positions</param>
        /// <param name="p_zPos">Z spacial positions</param>
        /// <param name="p_xVect">X velocity</param>
        /// <param name="p_yVect">Y velocity</param>
        /// <param name="p_zVect">Z velocity</param>
        public CharacterTemplate(int p_cHp, int p_maxHp, int p_shield, int p_shieldTimer, SpellCost p_spellCost,
            int p_cRessource, int p_ressource, bool p_isDead, double p_xPox, double p_yPos, double p_zPos,
            double p_xVect, double p_yVect, double p_zVect) :
            this(1, 0, p_cHp, p_maxHp, p_shield, p_shieldTimer, p_spellCost, p_cRessource, p_ressource, p_isDead,
                __StatInitialiser__(), new List<EquipementsTemplate>(), new List<ActionsTemplate>(), 
                new List<BuffsTemplate>(), new List<DebuffsTemplate>(), p_xPox, p_yPos, p_zPos, p_xVect, p_yVect, p_zVect) { }
        /// <summary>
        /// Constructor for a character
        /// </summary>
        /// <param name="p_cHp">Current health point</param>
        /// <param name="p_maxHp">Maximum health point</param>
        /// <param name="p_cRessource">Current action ressources</param>
        /// <param name="p_ressource">Maximum action ressources</param>
        /// <param name="isDead">Is the character dead?</param>
        /// <param name="p_xPox">X spacial positions</param>
        /// <param name="p_yPos">Y spacial positions</param>
        /// <param name="p_zPos">Z spacial positions</param>
        /// <param name="p_xVect">X velocity</param>
        /// <param name="p_yVect">Y velocity</param>
        /// <param name="p_zVect">Z velocity</param>
        public CharacterTemplate(int p_cHp, int p_maxHp, int p_cRessource, int p_ressource, bool isDead, 
            double p_xPox, double p_yPos, double p_zPos, double p_xVect, double p_yVect, double p_zVect) :
            this(p_cHp, p_maxHp, 0, 0, (SpellCost)2, p_cRessource, p_ressource, isDead, 
                p_xPox, p_yPos, p_zPos, p_xVect, p_yVect, p_zVect) { }
        /// <summary>
        /// Constructor for a character
        /// </summary>
        /// <param name="p_cHp">Current health point</param>
        /// <param name="p_maxHp">Maximum health point</param>
        /// <param name="p_cRessource">Current action ressources</param>
        /// <param name="p_ressource">Maximum action ressources</param>
        /// <param name="isDead">Is the character dead?</param>
        /// <param name="p_xPox">X spacial positions</param>
        /// <param name="p_yPos">Y spacial positions</param>
        /// <param name="p_zPos">Z spacial positions</param>
        public CharacterTemplate(int p_cHp, int p_maxHp, int p_cRessource, int p_ressource, bool isDead, 
            double p_xPox, double p_yPos, double p_zPos) :
            this(p_cHp, p_maxHp, p_cRessource, p_ressource, isDead, p_xPox, p_yPos, p_zPos, 0, 0, 0) { }
        /// <summary>
        /// Constructor for a character
        /// </summary>
        /// <param name="p_cHp">Current health point</param>
        /// <param name="p_maxHp">Maximum health point</param>
        /// <param name="p_cRessource">Current action ressources</param>
        /// <param name="p_ressource">Maximum action ressources</param>
        /// <param name="isDead">Is the character dead?</param>
        public CharacterTemplate(int p_cHp, int p_maxHp, int p_cRessource, int p_ressource, bool isDead) :
            this(p_cHp, p_maxHp, p_cRessource, p_ressource, isDead, 4, 5, 0) { }
        /// <summary>
        /// Constructor for a character with 10/10 hp and 0/0 mana
        /// </summary>
        public CharacterTemplate() : this(10, 10, 0, 0, false) { }

        // DEFAULT BUILDERS
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Create a base StatsTemplate if needed
        /// </summary>
        /// <returns>base StatsTemplate</returns>
        private static List<StatsTemplate> __StatInitialiser__()
        {
            List<StatsTemplate> placeHolder = new List<StatsTemplate>();
            for (int i = 0; i < StatNames.Length; i++)
            {
                placeHolder.Add(new StatsTemplate
                {
                    dmg = (DamageType)(System.Math.Floor((decimal)i / 2)),
                    type = (StatsType)(i % 2),
                    name = StatNames[i],
                    flat = (System.Math.Floor((decimal)i / 2) > 0) ? 0 : 1,
                    multi = 1
                }) ;
            }
            return placeHolder;
        }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        // GRAVITY
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Change the force of gravity
        /// </summary>
        /// <param name="p_vect">Vector X,Y,Z of force</param>
        public void ChangeGravity(HmVector p_vect) => this.Gravity = p_vect;
        /// <summary>
        /// Change the force of gravity
        /// </summary>
        /// <param name="p_x">X of the vector of force</param>
        /// <param name="p_y">Y of the vector of force</param>
        /// <param name="p_z">Z of the vector of force</param>
        public void ChangeGravity(double p_x, double p_y, double p_z) => this.Gravity = new HmVector(p_x, p_y, p_z);
        /// <summary>
        /// Change only the X and Y of the force of gravity
        /// </summary>
        /// <param name="p_x">X of the vector of force</param>
        /// <param name="p_y">Y of the vector of force</param>
        public void ChangeGravity(double p_x, double p_y) => this.Gravity = new HmVector(p_x, p_y, this.Gravity.Z);
        /// <summary>
        /// Change only the X and Y of the force of gravity
        /// </summary>
        /// <param name="x">X of the vector of force</param>
        public void ChangeGravity(double x) => this.Gravity = new HmVector(x, this.Gravity.Y);

        // LEVEL AND EXPERIENCE
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Add experience on the character
        /// </summary>
        /// <param name="p_exp">Experience to add</param>
        /// <returns>Amount of level up</returns>
        public int AddExp(decimal p_exp) => AddExp(new decimal[] { p_exp });
        /// <summary>
        /// Add multiple source of experience on the character
        /// </summary>
        /// <param name="p_exp">Array of experience to add</param>
        /// <returns>Amount of level up</returns>
        public int AddExp(decimal[] p_exp)
        {
            foreach(decimal exp in p_exp)
                this.Experience += exp;
            return UpdateLevel();
        }
        /// <summary>
        /// Add an amount of level to the character
        /// </summary>
        /// <param name="p_lvl">Amount of level to add</param>
        public void LevelUp(int p_lvl) => this.Level += p_lvl;
        /// <summary>
        /// Level up the character to a specific level
        /// </summary>
        /// <param name="p_lvl">Level to levelup to</param>
        /// <returns>Amount of level up</returns>
        public int LevelUpTo(int p_lvl)
        {
            if (this.Level < p_lvl)  
                this.Level += p_lvl - this.Level;
            return p_lvl - this.Level;
        }

        // HP AND RESSOURCE CHANGER
        //------------------------------------------------------------------------------------------------------------
        public void Defend()
        {
            if (this.Spellcost == SpellCost.Energy)
            {
                if (this.CurrentRessource + this.CurrentRessource * 0.1 >= this.MaxRessource)
                    this.CurrentRessource = this.MaxRessource;
                else
                    this.CurrentRessource += (int)(this.CurrentRessource * 0.1);
            }
            else if (this.Spellcost == SpellCost.Rage)
            {
                    this.CurrentRessource -= (int)(this.CurrentRessource * 0.1);
            }
        }
        /// <summary>
        /// Damage the character
        /// </summary>
        /// <param name="p_damage">Amount of damage taken</param>
        public void Hurt(int p_damage)
        {
            if (p_damage < 0)
                throw new ArgumentException("DAMAGE MUST BE POSITIVE");
            if (this.CurrentHp < p_damage)
            {
                this.CurrentHp = 0;
                this.IsDead = true;
            }
            else
            {
                if (this.Shield > 0 && this.Shield >= p_damage) 
                {
                    this.Shield -= p_damage; 
                }
                else if (this.Shield > 0 && this.Shield < p_damage)
                {
                    this.CurrentHp -= (p_damage - this.Shield);
                }
                else
                    this.CurrentHp -= p_damage;
            }
        }
        /// <summary>
        /// Restore health to the character
        /// </summary>
        /// <param name="p_heal">Amount of health restored</param>
        public void Heal(int p_heal)
        {
            if (p_heal < 0)
                throw new ArgumentException("HEAL MUST BE POSITIVE");
            if (!IsDead)
            {
                if (this.CurrentHp + p_heal > this.MaxHp)
                    this.CurrentHp = this.MaxHp;
                else
                    this.CurrentHp += p_heal;
            }
        }

        // ACTIONS
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Use an action of the character on one or more target
        /// </summary>
        /// <param name="p_index">Index of the action used</param>
        /// <param name="p_target">Targets of that action</param>
        public void UseAction(int p_index, CharacterTemplate[] p_target)
        {
            if (this.CurrentRessource < this.Actions[p_index].cost && (int)this.Spellcost > 1)
                Console.WriteLine("Not enough {2} (Current {0} / Cost {1})", this.CurrentRessource, this.Actions[p_index].cost, this.Spellcost);
            else if (this.CurrentHp < this.Actions[p_index].cost && (int)this.Spellcost == 1)
                Console.WriteLine("Not enough health (Current {0} / Cost {1})", this.CurrentRessource, this.Actions[p_index].cost);
            else if (!ActionsLib.Action[this.Actions[p_index].index](this, p_target))
            {
                if ((int)this.Spellcost > 1)
                    this.CurrentRessource -= this.Actions[p_index].cost;
                Console.WriteLine("{0} FAILED", this.Actions[p_index].name);
            }
            else
            {
                if ((int)this.Spellcost > 1)
                    this.CurrentRessource -= this.Actions[p_index].cost;
                else if ((int)this.Spellcost == 1)
                {
                    this.CurrentHp -= this.Actions[p_index].cost;
                }
                Console.WriteLine("You casted {0}", this.Actions[p_index].name);
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Add an action to the character
        /// </summary>
        /// <param name="p_name">Name of the action (can be different of the function name)</param>
        /// <param name="p_cost">Cost of the action in ressources</param>
        /// <param name="p_action">Static function uses when the action is used</param>
        public void LearnAction(string p_name, int p_cost, int p_index)
                => this.Actions.Add(new ActionsTemplate { name = p_name, cost = p_cost, index = p_index });
        /// <summary>
        /// Delete an action of the list of actions
        /// </summary>
        /// <param name="p_index">Index of the action</param>
        public void UnlearnAction(int p_index)
                => this.Actions.Remove(this.Actions[p_index]);
        /// <summary>
        /// Delete an action of the list of actions
        /// </summary>
        /// <param name="p_action">ActionsTemplate of the action</param>
        public void UnlearnAction(ActionsTemplate p_action) => this.Actions.RemoveAll(a => a.name == p_action.name);
        /// <summary>
        /// Delete an action of the list of actions
        /// </summary>
        /// <param name="p_action">Name of the action (can be different of the function name)</param>
        public void UnlearnAction(string p_action) => this.Actions.RemoveAll(a => a.name == p_action);

        // BUFFS AND DEBUFFS
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a buff to the character
        /// </summary>
        /// <param name="p_buff">BuffsTemplate of the buff</param>
        public void ApplyBuff(BuffsTemplate p_buff) {
            if (Buffs.Contains(p_buff))
            {
                this.Buffs.Remove(p_buff);
            }
            this.Buffs.Add(p_buff);
        } 
        /// <summary>
        /// Remove a buff from the buffs list
        /// </summary>
        /// <param name="p_index">Index of the buff</param>
        public void RemoveBuff(int p_index) => this.Buffs.Remove(this.Buffs[p_index]);
        /// <summary>
        /// Remove a buff from the buffs list
        /// </summary>
        /// <param name="p_buff">BuffsTemplate of the buff</param>
        public void RemoveBuff(BuffsTemplate p_buff)=>Buffs.RemoveAll((b) => b.name == p_buff.name);
        /// <summary>
        /// Remove a buff from the buffs list
        /// </summary>
        /// <param name="p_buff">Buff to remove</param>
        public void RemoveBuff(Buffs p_buff) => Buffs.RemoveAll((b) => b.name == p_buff);
        /// <summary>
        /// Add a debuff to the character
        /// </summary>
        /// <param name="p_debuff">DebuffsTemplate of the buff</param>
        public void ApplyDebuff(DebuffsTemplate p_debuff)
        {
            if (Debuffs.Contains(p_debuff))
            {
                this.Debuffs.Remove(p_debuff);
            }
            this.Debuffs.Add(p_debuff);
        }
        /// <summary>
        /// Remove a debuff from the buffs list
        /// </summary>
        /// <param name="p_index">Index the debuff</param>
        public void RemoveDebuff(int p_index) => this.Debuffs.Remove(this.Debuffs[p_index]);
        /// <summary>
        /// Remove a debuff from the buffs list
        /// </summary>
        /// <param name="p_debuff">DebuffsTemplate of the debuff</param>
        public void RemoveDebuff(DebuffsTemplate p_debuff) => Debuffs.RemoveAll(d => d.name == p_debuff.name);
        /// <summary>
        /// Remove a debuff from the buffs list
        /// </summary>
        /// <param name="p_debuff">Debuff to remove</param>
        public void RemoveDebuff(Debuffs p_debuff) => Debuffs.RemoveAll(d => d.name == p_debuff);

        // UPDATERS
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Update all timers and velocity in the character
        /// </summary>
        public void UpdateTimers()
        {
            if (this.ShieldTimer > 0)
                this.ShieldTimer -= 1;
            else
                this.Shield = 0;
            for (int i = 0; i < Buffs.Count; i++)
            {
                BuffsTemplate buff = this.Buffs[i];
                if (buff.timer > 0)
                { 
                    buff.timer -= 1;
                    this.Buffs.RemoveAt(i);
                    this.Buffs.Add(buff);
                }
                else
                    this.Buffs.RemoveAt(i);
            }
            for (int i = 0; i < Debuffs.Count; i++)
            {
                DebuffsTemplate debuffs = this.Debuffs[i];
                if (debuffs.timer > 0)
                {
                    debuffs.timer -= 1;
                    this.Debuffs.RemoveAt(i);
                    this.Debuffs.Add(debuffs);
                }
                else
                    this.Debuffs.RemoveAt(i);
            }
            UpdateVelocity();
        }
        /// <summary>
        /// UpdateTimers X time
        /// </summary>
        /// <param name="turn">Amount of time</param>
        public void UpdateTimers(int turn)
        { 
            for(int i = 0; i < turn; i++)
            {
                UpdateTimers();
            }
        }
        /// <summary>
        /// Update position and velocity
        /// </summary>
        private void UpdateVelocity()
        {
            this.Position.AddOnSelf(this.Velocity);
            this.Velocity.AddOnSelf(this.Acceleration);
        }
        /// <summary>
        /// Update the leve of the character
        /// </summary>
        /// <returns></returns>
        public int UpdateLevel()
        {
            int amountOfLevelUp = 0;
            for (; ; )
            {
                decimal expToLevelUp = (decimal)(4 + (7 * System.Math.Pow(1.5, this.Level)));
                if (expToLevelUp > this.Experience) break;
                this.Experience -= expToLevelUp;
                this.Level++;
                amountOfLevelUp++;
            }
            return amountOfLevelUp;
        }
        /// <summary>
        /// Reset the acceleration of the character
        /// </summary>
        public void ResetAcc() => this.Acceleration = this.Gravity;

        // MOVEMENTS
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Move the character in X, Y and Z axes
        /// </summary>
        /// <param name="p_x">Amount move in X</param>
        /// <param name="p_y">Amount move in Y</param>
        /// <param name="p_z">Amount move in Z</param>
        public void Move(double p_x, double p_y, double p_z) => this.Position.AddOnSelf(p_x, p_y, p_z);
        /// <summary>
        /// Move the character in X and Y axes
        /// </summary>
        /// <param name="p_x">Amount move in X</param>
        /// <param name="p_y">Amount move in Y</param>
        public void Move(double p_x, double p_y) => this.Position.AddOnSelf(p_x, p_y);
        /// <summary>
        /// Move the character in X axe
        /// </summary>
        /// <param name="p_x">Amount move in X</param>
        public void Move(double p_x) => this.Position.AddOnSelf(p_x);
    }
}
