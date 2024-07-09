using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ImprovedFlightControls.Items.Equipment
{
	public class DivineDiffractor : ModItem
	{
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 25);
			recipe.AddIngredient(ItemID.CrossNecklace, 1);
			recipe.AddIngredient(ItemID.LastPrism, 1);
			recipe.AddIngredient<NovaFragment>(10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register(); 
		}
		public override void SetDefaults()
		{
			Item.value = 10000;
			Item.rare = 7;
			Item.accessory=true;
			Item.defense=10;
		}

		public override void UpdateAccessory(Player player,bool hideVisual ){
			player.onHitDodge =true;
		}
	}
}