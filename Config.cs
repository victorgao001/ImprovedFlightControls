using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ImprovedFlightControls
{
	public class Config : ModConfig
	{
		[Header("Flight")]
		[Label("Flight Timer")]
		[Tooltip("Adds a timer next to the player to show how much wing time is left. False for no timer, true for timer.")]
		public bool FlightTimer=true;

		[Header("Potions")]
		[Label("Disable Buff Icon Interaction")]
		[Tooltip("Prevents Buff Icon from interfering with cursor")]
		public bool DisableBuffButton=false;

		[Label("Hide Buff Icons")]
		public bool HideBuffIcons=false;

		[Label("Hide Inferno Potion")]
		public bool HideInferno=false;

		[Header("MinionAutosummon")]
		[Label("Minion Autosummon Count")]
		[Tooltip("How many times to summon the minions favorited or in bank")]
		public List<int> summonCount=new() {11};

		
		[Label("Include Piggy Bank")]
		[Tooltip("Automatically summon minions in piggy bank")]
		public bool summonBank=true;

		public override ConfigScope Mode => ConfigScope.ClientSide;
	}
}