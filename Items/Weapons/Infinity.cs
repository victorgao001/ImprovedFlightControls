using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace ImprovedFlightControls.Items.Weapons
{
	public class Infinity : ModItem
	{
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.DiamondStaff);
			Item.shoot=ModContent.ProjectileType<Projectiles.RailgunBullet>();
			Item.mana=2;
			Item.damage = 1024; 
			Item.shootSpeed=20;
			Item.knockBack=50;
			Item.useTime=Item.useAnimation=6;
			Item.DamageType=DamageClass.Default;
			Item.crit=25;
		}
		int counter=0;
		Projectile beam;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			counter++;
			var dir=velocity.ToRotation();
			var offset=(dir+(float)(Math.PI*2/4)).ToRotationVector2();
			if(counter%8==0){
				for (float i = -1.5f; i <=1.5; ++i)
				{
					var p=Projectile.NewProjectileDirect(source,position+offset*60*i, velocity, ProjectileID.MiniNukeSnowmanRocketI, damage, knockback, player.whoAmI);
					p.usesLocalNPCImmunity=true;
					p.localNPCHitCooldown=-1;
				}
			}
			if(counter%20==0){
				var p=Projectile.NewProjectileDirect(source, position, velocity, ProjectileID.ShadowBeamFriendly, damage*16, knockback, player.whoAmI);
			}
			return true;
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<Items.NeutroniumBar>(4)
				.AddIngredient<Items.Weapons.Railgun>(1)
				.AddIngredient<Items.Weapons.DwarfStar>(1)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}