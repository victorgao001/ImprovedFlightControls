using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace ImprovedFlightControls.Items.Weapons
{
	public class DwarfStar : ModItem
	{
		public override string Texture => "Terraria/Images/Item_" + ItemID.PrincessWeapon;
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.DiamondStaff);
			Item.mana=14;
			Item.damage = 750; 
			Item.shootSpeed=20;
			Item.useTime=Item.useAnimation=(int)(Item.useAnimation);
			Item.DamageType=DamageClass.Default;
			Item.crit=88;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			for (int index = -2; index <=2; ++index)
			{
				var dir=velocity.ToRotation();
				dir+=(float)(index*Math.PI/12);
				var newVelocity=dir.ToRotationVector2()*velocity.Length();
				var projectile=Projectile.NewProjectileDirect(source,position, newVelocity, index==0? ProjectileID.PrincessWeapon: type, (int)(index==0? damage*1.5:damage), knockback, player.whoAmI);
				projectile.usesLocalNPCImmunity=true;
				projectile.localNPCHitCooldown=-1;
			}

			// We do not want vanilla to spawn a duplicate projectile.
			return false;
		}
		public override void AddRecipes() {
			CreateRecipe().AddIngredient(ItemID.LunarBar,10)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
    }
}