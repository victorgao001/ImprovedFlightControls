using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace ImprovedFlightControls.Items.Weapons
{
	public class Fractal : ModItem
	{
			public override void SetDefaults() {
			// This method right here is the backbone of what we're doing here; by using this method, we copy all of
			// the meowmere's SetDefault stats (such as Item.melee and Item.shoot) on to our item, so we don't have to
			// go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner; if you're
			// going to copy the stats of an item, use CloneDefaults().

			Item.CloneDefaults(ItemID.LunarFlareBook);
			// Check out ExampleCloneProjectile to see how this projectile is different from the Vanilla Meowmere projectile.

			// While we're at it, let's make our weapon's stats a bit stronger than the Meowmere, which can be done
			// by using math on each given stat.
			Item.useStyle=5;
			Item.mana=8;
			Item.damage = 6000; 
			Item.useAnimation=Item.useTime/=3;
			Item.shoot=ProjectileID.FairyQueenLance;
			Item.DamageType=DamageClass.Default;
			Item.crit=35;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {

			var mousePos=new Vector2(Main.mouseX,Main.mouseY);
			double x=(1.0*Main.mouseX/Main.screenWidth-0.5)*4/0.75,y=(1.0*Main.mouseY/Main.screenHeight-0.5)*4/0.75;
			double x1=Math.Abs(x),y1=Math.Abs(y);
			double x2=1/(2-x1),y2=1/(2-y1);
			double x4,y4;
			if(x1>1&&y1>1){
				double x3=1-1/(x2-1),y3=1-1/(y2-1);
				double z=x3*y3-Math.Sqrt((x3-1)*(y3-1)*(x3*y3-x3-y3+5))-1;
				x4=(z+x3-y3)/(2*(x3-1));
				y4=(z-x3+y3)/(2*(y3-1));
			}
			else{
				x4=(x1>1? x2:x1);
				y4=(y1>1? y2:y1);
			}
			var y5=(float)y4;
			var x5=(float)x4;
			if(x<0){
				x5*=-1;
			}
			if(y<0){
				y5*=-1;
			}
			x5=(x5*2/4*0.75f)*Main.screenWidth;
			y5=(y5*2/4*0.75f)*Main.screenHeight;
			var dir=new Vector2(-x5,y5);
			Vector2 pos;
			if(x4<y4){
				pos=new Vector2(x5+0.5f*Main.screenWidth,Main.screenHeight*0.5f);
			}
			else{
				pos=new Vector2(Main.screenWidth*0.5f,y5+0.5f*Main.screenHeight);
			}
			
			/*
			var lastMousePos=new Vector2(Main.lastMouseX,Main.lastMouseY);
			dir=mousePos-lastMousePos;
			if(dir==Vector2.Zero){
				dir=Main.rand.NextVector2Unit();
			}*/
			dir.Normalize();
			var spawnPos=pos+Main.screenPosition-dir*1000;
			var proj=Projectile.NewProjectile(source, spawnPos,Vector2.Zero, type, damage, knockback, player.whoAmI,dir.ToRotation(),Main.rand.Next(0,100)/100f);
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].hostile = false;
			Main.projectile[proj].localAI[0]=30;
			// We do not want vanilla to spawn a duplicate projectile.
			return false;
		}
	}
}