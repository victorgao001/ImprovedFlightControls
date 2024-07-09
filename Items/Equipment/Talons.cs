using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ImprovedFlightControls.Items.Equipment
{
	public class Talons : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.ImprovedFlightControls.hjson file.

		public override void SetDefaults()
		{
			Item.accessory=true;
			Item.defense=8;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BerserkerGlove, 1);
			recipe.AddIngredient(ItemID.HerculesBeetle, 1);	
			recipe.AddIngredient(ItemID.SummonerEmblem, 1);		
			recipe.AddIngredient(ItemID.LihzahrdBrick, 3);
			recipe.AddTile(TileID.TinkerersWorkbench);	
			recipe.Register();
		}

		public override void UpdateAccessory(Player player,bool hideVisual ){
			player.GetKnockback(DamageClass.Summon) += 2f;
			player.GetDamage(DamageClass.Summon) += 0.12f;
			player.GetAttackSpeed(DamageClass.SummonMeleeSpeed)+= 0.12f;
			player.aggro += 400;
			player.whipRangeMultiplier+=0.1f;
			player.AddBuff(BuffID.ThornWhipPlayerBuff,2);
			player.AddBuff(BuffID.SwordWhipPlayerBuff,2);
			player.AddBuff(BuffID.ScytheWhipPlayerBuff,2);
		}
	}
}