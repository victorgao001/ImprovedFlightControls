using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using System;

namespace ImprovedFlightControls
{
    public class ModGlobalItem : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual){
            switch(item.type){
                case ItemID.RainbowWings:
                case ItemID.WingsStardust:
                case ItemID.WingsSolar:
                case ItemID.LongRainbowTrailWings:
                    player.GetModPlayer<Player1>().maxBoostedFallSpeed=Math.Max(player.GetModPlayer<Player1>().maxBoostedFallSpeed,20);
                    player.GetModPlayer<Player1>().maxBoostedGravity=Math.Max(player.GetModPlayer<Player1>().maxBoostedGravity,0.6f);
                    break;
            }
        }
        public override void UpdateEquip(Item item, Player player){
            switch(item.type){
                case ItemID.StardustHelmet:
                case ItemID.SpookyHelmet:
                    player.setSquireT3=true;
                    player.setHuntressT3=true;
                    player.setApprenticeT3=true;
                    player.setMonkT3=true;
                    player.AddBuff(BuffID.BallistaPanic,2);
                    player.maxTurrets+=2;
                    break;
                case ItemID.TikiMask:
                case ItemID.SpiderMask:
                    player.maxTurrets+=1;
                    break;
            }
        }
		public override void AddRecipes(){
			Recipe recipe = Recipe.Create(ItemID.Hellforge);
			recipe.AddIngredient(ItemID.Furnace);
			recipe.AddIngredient(ItemID.Hellstone,15);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}