using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Audio;
using System;
using System.Collections.Generic;

namespace ImprovedFlightControls
{
    public class EnergyShieldPlayer : ModPlayer
    {
        public float shieldEnergy;
        public float shieldRegen;
        public float maxshieldRegen;
        public float maxShieldEnergy;

        public override void ResetEffects()
        {
            maxShieldEnergy=0;
            maxshieldRegen=20/60f;
        }
        public override void PostUpdateEquips (){
            shieldRegen=Math.Min(shieldRegen+1/3600f,maxshieldRegen);
            shieldEnergy=Math.Min(maxShieldEnergy,shieldEnergy+shieldRegen);
        }
        public override void OnHurt(Player.HurtInfo info){
            var x=Math.Min((int)shieldEnergy,info.Damage);
            shieldEnergy-=x;
            Player.statLife+=x;
            shieldRegen=Math.Max(0,shieldRegen-10/60f);
        }
    }
}