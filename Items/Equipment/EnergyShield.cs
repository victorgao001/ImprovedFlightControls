using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ImprovedFlightControls.Items.Equipment
{
	public class EnergyShield : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.ImprovedFlightControls.hjson file.

		public override void SetDefaults()
		{
			Item.value = 10000;
			Item.rare = 7;
			Item.accessory=true;
			Item.defense=10;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<HeroShield2>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DivineDiffractor>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NeutroniumBar>(), 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}

		public override void UpdateAccessory(Player player,bool hideVisual ){
			//energy shield effect
			player.GetModPlayer<EnergyShieldPlayer>().maxShieldEnergy+=200;
			//diffractor effect
			player.onHitDodge =true;
			//shield effect
			player.noKnockback=true;
			//cross necklace
			player.longInvince = true;
			//debuff immunity
			player.noKnockback = true;
			player.fireWalk = true;
			player.buffImmune[33] = true;
			player.buffImmune[36] = true;
			player.buffImmune[30] = true;
			player.buffImmune[20] = true;
			player.buffImmune[32] = true;
			player.buffImmune[31] = true;
			player.buffImmune[35] = true;
			player.buffImmune[23] = true;
			player.buffImmune[22] = true;
			player.buffImmune[46] = true;
			//turtle/frozen turtle effect
			player.thorns+=1f;
			if(player.statLife <= player.statLifeMax2*0.5){
				player.AddBuff(62, 5);
			}
			else{
				player.endurance+=0.15f;
			}
			//paladin shield effect
			if ((double) player.statLife > (double) player.statLifeMax2 * 0.25)
			{
				player.hasPaladinShield = true;
				if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0)
				{
					int player1 = Main.myPlayer;
					if (Main.player[player1].team == player.team && player.team != 0)
					{
						double num1 = (double) player.position.X - (double) Main.player[player1].position.X;
						float num2 = player.position.Y - Main.player[player1].position.Y;
						if (Math.Sqrt(num1 * num1 + (double) num2 * (double) num2) < 800.0)
							Main.player[player1].AddBuff(43, 20);
					}
				}
			}
		}
	}
}