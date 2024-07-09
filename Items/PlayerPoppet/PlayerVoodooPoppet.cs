using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ImprovedFlightControls.Items.PlayerPoppet
{
	public class PlayerVoodooPoppet : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.ImprovedFlightControls.hjson file.

		public override void SetDefaults()
		{
			Item.value = 10000;
			Item.rare = 7;
			Item.maxStack=9;
		}

		public override void UpdateInventory(Player player ){
			player.GetModPlayer<PlayerPoppetPlayer>().maxPoppets=Math.Min(9,Item.stack);
		}
	}
}