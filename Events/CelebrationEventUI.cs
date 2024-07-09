using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;
using Terraria.Localization;

namespace ImprovedFlightControls
{
	internal class CelebrationEventUI : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
		private UIText text;
		private UIElement area;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding.
			area = new UIElement();
            area.VAlign = 1.0f;
			area.Left.Set(-area.Width.Pixels - 300, 1f); // Place the resource bar to the left of the hearts.
			//area.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);


			text = new UIText("0/0", 1.5f); // text to show stat
			//text.Width.Set(138, 0f);
			//text.Height.Set(50, 0f);
			text.Top.Set(0, 0f);
			text.Left.Set(0, 0f);

			area.Append(text);
			Append(area);
		}
		public override void Draw(SpriteBatch spriteBatch) {
			// This prevents drawing unless we are using an ExampleCustomResourceWeapon
			if (Main.invasionType!=CelebrationEvent.EventId|| Main.gameMenu ||Main.LocalPlayer.dead)
				return;
			base.Draw(spriteBatch);
		}
		public override void Update(GameTime gameTime) {
			if(Main.invasionType!=CelebrationEvent.EventId){
				return;
			}
			// Setting the text per tick to update and show our resource values.
			text.SetText(CelebrationEvent.timer/60/60+":"+CelebrationEvent.timer/60%60);
			base.Update(gameTime);
		}
	}

	// This class will only be autoloaded/registered if we're not loading on a server
	[Autoload(Side = ModSide.Client)]
	internal class CelebrationEventUISystem : ModSystem
	{
		private UserInterface ExampleResourceBarUserInterface;

		internal CelebrationEventUI ExampleResourceBar;

		public static LocalizedText ExampleResourceText { get; private set; }

		public override void Load() {
			ExampleResourceBar = new();
			ExampleResourceBarUserInterface = new();
			ExampleResourceBarUserInterface.SetState(ExampleResourceBar);

			string category = "UI";
			ExampleResourceText ??= Language.GetOrRegister(Mod.GetLocalizationKey($"{category}.ExampleResource"));
		}

		public override void UpdateUI(GameTime gameTime) {
			ExampleResourceBarUserInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1) {
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"ExampleMod: Example Resource Bar",
					delegate {
						ExampleResourceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}