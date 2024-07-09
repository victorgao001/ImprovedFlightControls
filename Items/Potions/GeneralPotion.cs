using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedFlightControls.Items.Potions
{
	public class GeneralPotion : BaseCombinedPotion
	{
		public override int[] buffs(){
			int[] x={BuffID.Lifeforce,BuffID.Endurance,BuffID.Heartreach,BuffID.Ironskin,BuffID.Regeneration,BuffID.Thorns,
			BuffID.Warmth,BuffID.Wrath,BuffID.Titan,BuffID.WellFed3,BuffID.Summoning,BuffID.Bewitched,BuffID.WarTable};
			return x;
		}
    }
}