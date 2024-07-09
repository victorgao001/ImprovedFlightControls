using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ImprovedFlightControls.Items
{
	public class SeafoodSnatcher : ModItem
	{
		public override void SetDefaults() {
			// These are copied through the CloneDefaults method:
			Item.CloneDefaults(ItemID.GoldenFishingRod);

			Item.fishingPole = 7777; // Sets the poles fishing power
			Item.shootSpeed = 25f; // Sets the speed in which the bobbers are launched. Wooden Fishing Pole is 9f and Golden Fishing Rod is 17f.
			Item.shoot = ProjectileID.BobberWooden; // The Bobber projectile.
		}

		// Overrides the default shooting method to fire multiple bobbers.
		// NOTE: This will allow the fishing rod to summon multiple Duke Fishrons with multiple Truffle Worms in the inventory.
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			int bobberAmount = Main.rand.Next(3, 6); // 3 to 5 bobbers
			float spreadAmount = 75f; // how much the different bobbers are spread out.

			for (int index = 0; index < bobberAmount; ++index) {
				Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f, Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f);

				// Generate new bobbers
				Projectile.NewProjectile(source, position, bobberSpeed, type, 0, 0f, player.whoAmI);
			}
			return false;
		}
	}
}