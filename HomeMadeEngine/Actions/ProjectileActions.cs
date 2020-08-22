using HomeMadeEngine.Math;
using HomeMadeEngine.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMadeEngine.Actions
{
    public class ProjectileActions
    {
        public static List<Func<CharacterTemplate, HmVector, HmAreaOfEffect?, HmAreaOfEffect?, bool>> Library = new List<Func<CharacterTemplate, HmVector, HmAreaOfEffect?, HmAreaOfEffect?, bool>>(){
            ProjectileActions.StandartProjectile
            };
        public static bool StandartProjectile(CharacterTemplate p_caster, HmVector p_velocity, HmAreaOfEffect? p_hitbox, HmAreaOfEffect? p_areaOfEffect)
        {
            return true;
        }
    }
}
