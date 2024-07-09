using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ImprovedFlightControls.Buffs
{
    public class Stoneskin : ModBuff
    {
		public override bool ReApply (Player player, int time, int buffIndex){
			return true;
		}
		public override bool RightClick(int buffIndex){
			return false;
		}
		public override void Update(Player player, ref int buffIndex) {
			if(player.buffTime[buffIndex]>20*60){
				player.endurance= (float)(0.5 +player.endurance/2);
			}
			else if(player.buffTime[buffIndex]>10*60){
				player.GetDamage(DamageClass.Generic)-=0.50f;
			}
		}
    }
}