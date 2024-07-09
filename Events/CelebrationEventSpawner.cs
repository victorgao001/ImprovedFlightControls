using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ImprovedFlightControls.Events
{
    public class CelebrationEventSpawner : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1;
            Item.maxStack = 99;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 3;
        }

        public override bool? UseItem(Player player)
        {
            if(Main.invasionType!=CelebrationEvent.EventId){
                CelebrationEvent.StartCelebrationEvent();
            }
            else{
                CelebrationEvent.EndEventEarly();
            }
            return true;
        }
        /*public override bool ConsumeItem(Player player){
            return Main.invasionType!=CelebrationEvent.EventId;
        }*/
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CelestialSigil, 1);
			recipe.AddCondition(Condition.NearShimmer);
			recipe.Register();
		}
    }
}