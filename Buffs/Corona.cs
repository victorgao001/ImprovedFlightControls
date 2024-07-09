using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;

namespace ImprovedFlightControls.Buffs
{
	public class Corona : ModBuff
	{
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CoronaPlayer>().corona = true;

			int[] debuffs = { BuffID.Venom, BuffID.OnFire, BuffID.OnFire3, BuffID.CursedInferno, BuffID.Frostburn, BuffID.Frostburn2, BuffID.ShadowFlame };
			int effectiveRange = 400;
			foreach (NPC target in Main.npc)
			{
				if (target.active && !target.friendly && !target.CountsAsACritter
					&& player.CanNPCBeHitByPlayerOrPlayerProjectile(target) 
					&& (double)Vector2.Distance(player.Center, target.Center) <= (double)effectiveRange)
				{
					foreach (int debuff in debuffs)
					{
						if (!target.buffImmune[debuff])
						{
							target.AddBuff(debuff, 300);
						}
					}
				}
			}
			player.AddBuff(BuffID.OnFire3,5);
		}
	}
  public class CoronaPlayer : ModPlayer{
    public bool corona;
		public override void ResetEffects(){
			corona=false;
		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
				if(target.friendly){//why do old one army, moon lord, empress of light attacks go here???
						return;
				}

				if(corona){
						int[] debuffs = { BuffID.Poisoned, BuffID.Venom, BuffID.OnFire, BuffID.OnFire3, BuffID.CursedInferno, BuffID.Frostburn, BuffID.Frostburn2, BuffID.ShadowFlame };
						foreach (int debuff in debuffs)
						{
								if (!target.buffImmune[debuff])
								{
										target.AddBuff(debuff, 300);
								}
						}
				}
		}
	}
}