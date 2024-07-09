using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedFlightControls.Items.Potions
{
	public class MeleePotion : BaseCombinedPotion
	{
		public override int[] buffs(){
			int[] x={BuffID.Rage,BuffID.Sharpened,ModContent.BuffType<Buffs.Tipsy2>()};
			return x;
		}
    }
}