using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedFlightControls.Items.Potions
{
	public class ArenaPotion : BaseCombinedPotion
	{
		public override int[] buffs(){
			int[] x={BuffID.Campfire,BuffID.HeartLamp,BuffID.CatBast,BuffID.Honey};
			return x;
		}
    }
}