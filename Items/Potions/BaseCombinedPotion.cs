using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedFlightControls.Items.Potions
{
	public class BaseCombinedPotion : ModItem
	{
		public virtual int[] buffs(){
			int[] x={1};
			return x;
		}
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 20;

			// Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
			ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
				new Color(240, 240, 240),
				new Color(200, 200, 200),
				new Color(140, 140, 140)
			};
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.maxStack = 30;
			Item.consumable = true;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(gold: 1);
			Item.buffType = buffs()[0];
			Item.buffTime = 36000;
		}
		public override void UpdateInventory(Player player){
			foreach (int buff in buffs())
			{
				player.AddBuff(buff,300);
			}
		}
		public override bool? UseItem(Player player){
			foreach (int buff in buffs())
			{
				player.AddBuff(buff,36000);
			}
			return null;
		}
    }
}