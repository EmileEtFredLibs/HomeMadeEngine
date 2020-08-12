using System;
using System.Collections.Generic;
using System.Linq;
using HomeMadeEngine.Math;

namespace HomeMadeEngine.Character
{
    public class CharacterTemplate
    {
        //------------------------------------------------------------------------------------------------------------
        // CONSTANTS
        //____________________________________________________________________________________________________________
        public static double[] BaseGravity = new double[3] { 0, 0, 0 };

        //------------------------------------------------------------------------------------------------------------
        // FIELDS
        //____________________________________________________________________________________________________________
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
        public CharacterTemplate(int p_cHp, int p_maxHp, int p_shield, int p_shieldTimer, SpellCost p_spellCost, 
            int p_cRessource, int p_ressource, bool p_isDead, List<StatsTemplate>p_stat, List<EquipementsTemplate>p_equip, 
            List<ActionsTemplate> p_actions, List<BuffsTemplate> p_buffs, List<DebuffsTemplate> p_debuffs,
            double p_xPox, double p_yPos, double p_zPos, double p_xVect, double p_yVect, double p_zVect)
        {
            if (p_maxHp < 0)
                throw new ArgumentException("HP MUST BE POSITIF");
            if (p_cHp > p_maxHp)
                throw new ArgumentException("CURRENT HP CAN'T BE HIGHER THAN MAX HP");
            if (p_cHp > p_maxHp)
                throw new ArgumentException("CURRENT RESSOURCE CAN'T BE HIGHER THAN MAX RESSOURCE");
            if (p_ressource < 0)
                throw new ArgumentException("RESSOURCES MUST BE POSITIF");
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
            this.Gravity = new HmVector(BaseGravity[0], BaseGravity[1], BaseGravity[2]);
        }

        // SHORTCUT CONSTRUCTORS
        //------------------------------------------------------------------------------------------------------------
        public CharacterTemplate(int p_cHp, int p_maxHp, int p_shield, int p_shieldTimer, SpellCost p_spellCost, 
            int p_cRessource, int p_ressource, bool p_isDead, double p_xPox, double p_yPos, double p_zPos, 
            double p_xVect, double p_yVect, double p_zVect) :
            this(p_cHp, p_maxHp, p_shield, p_shieldTimer, p_spellCost, p_cRessource, p_ressource, p_isDead, 
                new List<StatsTemplate>(), new List<EquipementsTemplate>(), new List<ActionsTemplate>(), 
                new List<BuffsTemplate>(), new List<DebuffsTemplate>(), p_xPox, p_yPos, p_zPos, p_xVect, p_yVect, p_zVect) { }
        public CharacterTemplate(int p_cHp, int p_maxHp, int p_cRessource, int p_ressource, bool isDead, 
            double p_xPox, double p_yPos, double p_zPos, double p_xVect, double p_yVect, double p_zVect) :
            this(p_cHp, p_maxHp, 0, 0, (SpellCost)2, p_cRessource, p_ressource, isDead, 
                p_xPox, p_yPos, p_zPos, p_xVect, p_yVect, p_zVect) { }
        public CharacterTemplate(int p_cHp, int p_maxHp, int p_cRessource, int p_ressource, bool isDead, 
            double p_xPox, double p_yPos, double p_zPos) :
            this(p_cHp, p_maxHp, p_cRessource, p_ressource, isDead, p_xPox, p_yPos, p_zPos, 0, 0, 0) { }
        public CharacterTemplate(int p_cHp, int p_maxHp, int p_cRessource, int p_ressource, bool isDead) :
            this(p_cHp, p_maxHp, p_cRessource, p_ressource, isDead, 4, 5, 0) { }
        public CharacterTemplate() : this(10, 10, 0, 0, false) { }

        //------------------------------------------------------------------------------------------------------------
        // FUNCTIONS
        //____________________________________________________________________________________________________________
        // GRAVITY
        //------------------------------------------------------------------------------------------------------------
        public void ChangeGravity(HmVector p_vect) => this.Gravity = p_vect;
        public void ChangeGravity(double p_x, double p_y, double p_z) => this.Gravity = new HmVector(p_x, p_y, p_z);
        public void ChangeGravity(double p_x, double p_y) => this.Gravity = new HmVector(p_x, p_y, 0);
        public void ChangeGravity(double x) => this.Gravity = new HmVector(x, 0, 0);

        // HP CHANGER
        //------------------------------------------------------------------------------------------------------------
        public void Hurt(int p_damage)
        {
            if (p_damage < 0)
                throw new ArgumentException("DAMAGE MUST BE POSITIF");
            if (this.CurrentHp < p_damage)
            {
                this.CurrentHp = 0;
                this.IsDead = true;
            }
            else
                this.CurrentHp -= p_damage;
        }
        public void Heal(int p_heal)
        {
            if (p_heal < 0)
                throw new ArgumentException("HEAL MUST BE POSITIF");
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
        public void UseAction(int p_index, CharacterTemplate[] p_target)
        {
            if (this.CurrentRessource < this.Actions[p_index].cost && (int)this.Spellcost > 1)
                Console.WriteLine("Not enough mana (Current {0} / Cost {1})", this.CurrentRessource, this.Actions[p_index].cost);
            else if (!this.Actions[p_index].action(this, p_target ))
            {
                if ((int)this.Spellcost > 1)
                    this.CurrentRessource -= this.Actions[p_index].cost;
                Console.WriteLine("{0} FAILED", this.Actions[p_index].name);
            }
            else
            {
                if ((int)this.Spellcost > 1)
                    this.CurrentRessource -= this.Actions[p_index].cost;
                Console.WriteLine("Player use {0}", this.Actions[p_index].name);
            }
        }
        public void LearnAction(string p_name, int p_cost, Func<CharacterTemplate, CharacterTemplate[], bool> p_action)
                => this.Actions.Add(new ActionsTemplate { name = p_name, cost = p_cost, action = p_action });
        public void UnlearnAction(int p_index)
                => this.Actions.Remove(this.Actions[p_index]);
        public void UnlearnAction(ActionsTemplate p_action)
        {
            foreach (ActionsTemplate action in this.Actions)
                if (action.name == p_action.name)
                    this.Actions.Remove(action);
        }
        public void UnlearnAction(string p_action)
        {
            foreach (ActionsTemplate action in this.Actions)
                if (action.name == p_action)
                    this.Actions.Remove(action);
        }

        // BUFFS AND DEBUFFS
        //------------------------------------------------------------------------------------------------------------
        public void ApplyBuff(BuffsTemplate p_buff) => this.Buffs.Add(p_buff);
        public void RemoveBuff(int p_index) => this.Buffs.Remove(this.Buffs[p_index]);
        public void RemoveBuff(BuffsTemplate p_buff)
        {
            foreach (BuffsTemplate buff in this.Buffs)
                if (buff.name==p_buff.name)
                    this.Buffs.Remove(buff);
        }
        public void RemoveBuff(Buffs p_buff)
        {
            foreach (BuffsTemplate buff in this.Buffs)
                if (buff.name == p_buff)
                    this.Buffs.Remove(buff);
        }
        public void ApplyDebuff(DebuffsTemplate p_debuff) => this.Debuffs.Add(p_debuff);
        public void RemoveDebuff(int p_index) => this.Buffs.Remove(this.Buffs[p_index]);
        public void RemoveDebuff(DebuffsTemplate p_debuff)
        {
            foreach (DebuffsTemplate debuff in this.Debuffs)
                if (debuff.name == p_debuff.name)
                    this.Debuffs.Remove(debuff);
        }
        public void RemoveDebuff(Debuffs p_debuff)
        {
            foreach (DebuffsTemplate debuff in this.Debuffs)
                if (debuff.name == p_debuff)
                    this.Debuffs.Remove(debuff);
        }

        // UPDATERS
        //------------------------------------------------------------------------------------------------------------
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
                    this.Debuffs.RemoveAt(i);
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
            VelocityUpdater();
        }
        public void UpdateTimers(int turn)
        { 
            for(int i = 0; i < turn; i++)
            {
                UpdateTimers();
            }
        }
        private void VelocityUpdater()
        {
            this.Position.AddOnSelf(this.Velocity);
            this.Velocity.AddOnSelf(this.Acceleration);
        }
        public void ResetAcc() => this.Acceleration = this.Gravity;

        // MOVEMENTS
        //------------------------------------------------------------------------------------------------------------
        public void Move(double p_x, double p_y, double p_z) => this.Position.AddOnSelf(p_x, p_y, p_z);
        public void Move(double p_x, double p_y) => this.Position.AddOnSelf(p_x, p_y);
        public void Move(double p_x) => this.Position.AddOnSelf(p_x);
    }
}
