using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ImprovedFlightControls.Buffs
{
    public class Tipsy2 : ModBuff
    {
		public override void Update(Player player, ref int buffIndex) {
			player.GetDamage(DamageClass.Melee) += 0.10f;
			player.GetCritChance(DamageClass.Melee) += 0.02f;
			player.GetAttackSpeed(DamageClass.SummonMeleeSpeed)+= 0.10f;
			player.fishingSkill+= 5;
		}
    }
}