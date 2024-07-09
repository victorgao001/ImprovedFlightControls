using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace ImprovedFlightControls.Items.Weapons
{
	public class Railgun : ModItem
	{
			public override void SetDefaults() {
			Item.CloneDefaults(ItemID.DiamondStaff);
			
			Item.shoot=ModContent.ProjectileType<Projectiles.RailgunBullet>();
			Item.mana=0;
			Item.damage = 5000; 
			Item.shootSpeed=12;
			Item.knockBack=20;
			Item.useTime=Item.useAnimation=(int)(Item.useAnimation*2);
			Item.DamageType=DamageClass.Default;
			Item.crit=50;
		}
		public override void AddRecipes() {
			CreateRecipe().AddIngredient(ItemID.LunarBar,10)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
    }
}