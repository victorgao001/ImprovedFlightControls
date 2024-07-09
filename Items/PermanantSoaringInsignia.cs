using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Terraria.GameContent.ItemDropRules;

namespace ImprovedFlightControls.Items
{
	public class PermanantSoaringInsignia : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.ImprovedFlightControls.hjson file.
		bool enabled=true;
		public override void SetDefaults()
		{
			Item.value = 10000;
			Item.rare = 7;
		}

		public override void AddRecipes()
		{
			
		}

		public override void UpdateInventory(Player player){
			player.GetModPlayer<Player1>().permanantSoaringInsignia=enabled;
		}
		public override bool CanRightClick(){
			return true;
		}
		public override void RightClick(Player player){
			Item.stack++;//tmodloader bug
			enabled=!enabled;
			if(enabled){
				Item.SetNameOverride("Permenant Soaring Insignia");
			}
			else{
				Item.SetNameOverride("Permenant Soaring Insignia (Disabled)");
			}
		}
	}
}