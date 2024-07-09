using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedFlightControls.Items.Potions
{
	public class SummonPotion : BaseCombinedPotion
	{
		public override int[] buffs(){
			int[] x={ModContent.BuffType<Buffs.Tipsy2>()};
			return x;
		}
    }
}