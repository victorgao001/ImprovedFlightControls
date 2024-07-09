using Terraria;
using Terraria.ModLoader;
using System;

namespace ImprovedFlightControls.Buffs
{
    public class Fastfall : ModBuff{
		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<Player1>().maxBoostedFallSpeed = Math.Max(player.GetModPlayer<Player1>().maxBoostedFallSpeed,14f);
			player.GetModPlayer<Player1>().maxBoostedGravity = Math.Max(player.GetModPlayer<Player1>().maxBoostedGravity,0.4f);
		}
    }
}