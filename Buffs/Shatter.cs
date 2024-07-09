using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using Microsoft.Xna.Framework;

namespace ImprovedFlightControls.Buffs
{
	public class Shatter : ModBuff
	{
		public override void SetStaticDefaults(){
			Main.debuff[Type] = true;
		}
		const int factor=4;
		public override void Update(Player player, ref int buffIndex) {
			player.hurtCooldowns[1]=player.hurtCooldowns[1];
			if(player.immuneTime>factor){
				player.immuneTime-=factor;
			}
			else if(player.immuneTime>1){
				player.immuneTime=1;
			}
			if(player.hurtCooldowns[1]>factor){	
				player.hurtCooldowns[1]-=factor;
			}
			else if(player.hurtCooldowns[1]>1){
				player.hurtCooldowns[1]=1;
			}
			player.statDefense=player.statDefense/2-30;
			player.GetModPlayer<ShatterPlayer>().active=true;
		}
		
    public class ShatterPlayer : ModPlayer
    {
			public bool active;
			public override void ResetEffects()
			{
					active=false;
			}
			public override void UpdateLifeRegen(){
				if(active){
					if(Main.expertMode){
						Player.lifeRegen/=2;
					}
				}
			}
		}
    public class ShatterGlobalNPC : GlobalNPC
    {
			public override void OnHitByProjectile (NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone){
				
			}
		}
  }
}