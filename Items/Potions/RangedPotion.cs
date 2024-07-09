using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedFlightControls.Items.Potions
{
	public class RangedPotion : BaseCombinedPotion
	{
		public override int[] buffs(){
			int[] x={BuffID.Rage,BuffID.Archery,BuffID.AmmoReservation,BuffID.AmmoBox};
			return x;
		}
    }
}