using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;

namespace ImprovedFlightControls
{
	public class ImprovedFlightControls : Mod
	{
		internal static ModKeybind FlyUpHotkey;
		internal static ModKeybind FlyLeftHotkey;
		internal static ModKeybind FlyRightHotkey;
		internal static ModKeybind FlyDownHotkey;
		internal static ModKeybind ActivateSetBonus;
		internal static ModKeybind FlipGravity;
		internal static ModKeybind[] UseItem=new ModKeybind[10];
		internal static ModKeybind Dash;
		public override void Load(){
			FlyUpHotkey = KeybindLoader.RegisterKeybind(this, "Fly Up", "W");
			FlyLeftHotkey = KeybindLoader.RegisterKeybind(this, "Fly Left", "A");
			FlyRightHotkey = KeybindLoader.RegisterKeybind(this, "Fly Right", "D");
			FlyDownHotkey = KeybindLoader.RegisterKeybind(this, "Fly Down", "S");
			ActivateSetBonus = KeybindLoader.RegisterKeybind(this, "Activate Set Bonus Ability","G");
			FlipGravity = KeybindLoader.RegisterKeybind(this, "Flip Gravity","F");
			Dash = KeybindLoader.RegisterKeybind(this, "Dash","Space");
			for(var i=0;i<10;i++){
				UseItem[i]=KeybindLoader.RegisterKeybind(this, "Use Item #"+(i+10),"Z");
			}
			if (!Main.dedServ){
				SkyManager.Instance["CelebrationEventSky"] = new CelebrationEventSky();
			}
		}
	}
}