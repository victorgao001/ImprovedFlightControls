using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace ImprovedFlightControls
{
	public class RemoveBuffIcons : GlobalBuff 
	{
		public override bool PreDraw (SpriteBatch spriteBatch, int type, int buffIndex, ref BuffDrawParams drawParams){
			if(ModContent.GetInstance<Config>().DisableBuffButton||ModContent.GetInstance<Config>().HideBuffIcons){
				drawParams.MouseRectangle.Width=drawParams.MouseRectangle.Height=0;
			}
			if(ModContent.GetInstance<Config>().HideBuffIcons){
				drawParams.TextPosition=new(-100,-100);
				return false;
			}
			return true;
		}
	}
}