using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace ImprovedFlightControls.Items.Weapons
{
	public class Unity : ModItem
	{
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.FairyQueenMagicItem);
			
			Item.shoot=ProjectileID.FairyQueenMagicItemShot;
			Item.mana=3;
			Item.shootSpeed=10;
			Item.damage = 1000; 
			Item.useTime=Item.useAnimation=3;
			Item.DamageType=DamageClass.Default;
			Item.crit=30;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			var mousePos=Main.screenPosition+new Vector2(Main.mouseX,Main.mouseY);
			float target=-1,dist=16*10;
			foreach (var npc in Main.npc){
				if(!npc.friendly&&npc.CanBeChasedBy()&&(npc.position-mousePos).Length()<dist){
					dist=(npc.position-mousePos).Length();
					target=npc.whoAmI;
				}
			}
			var hue=Main.rand.NextFloat();
			for (int index = -2; index <=2; ++index)
			{
				var dir=velocity.ToRotation();
				dir+=(float)(index*Math.PI/12);
				var newVelocity=dir.ToRotationVector2()*velocity.Length()*-1;
				Projectile.NewProjectile(source, player.Center+newVelocity*5, newVelocity, ProjectileID.FairyQueenMagicItemShot, damage, 2.5f, Main.myPlayer, target, hue);
			}
			return false;
		}
	}
}