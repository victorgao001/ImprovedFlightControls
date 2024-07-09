using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedFlightControls.Items.Potions
{
	public class MagicPotion:BaseCombinedPotion
	{
		public override int[] buffs(){
			int[] x={BuffID.Rage,BuffID.ManaRegeneration,BuffID.MagicPower,BuffID.Clairvoyance};
			return x;
		}
    }
}