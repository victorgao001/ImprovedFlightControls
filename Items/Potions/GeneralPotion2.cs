using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedFlightControls.Items.Potions
{
	public class GeneralPotion2 : BaseCombinedPotion
	{
		public override int[] buffs(){
			int[] x={BuffID.Swiftness,BuffID.Featherfall,BuffID.WaterWalking,BuffID.NightOwl,
			BuffID.Shine,BuffID.Hunter,BuffID.ObsidianSkin,BuffID.Flipper,BuffID.SugarRush,BuffID.Mining,BuffID.Builder};
			return x;
		}
    }
}