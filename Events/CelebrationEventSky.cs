using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace ImprovedFlightControls
{
	public class CelebrationEventSky :  CustomSky
	{
        bool active=false;
        int background=0;
        public override void Activate(Vector2 position, params object[] args)
        {
            active=true;
            background=0;
        }

        public override void Deactivate(params object[] args)
        {
            active=false;
        }


        public override void Draw (SpriteBatch spriteBatch, float minDepth, float maxDepth){
            spriteBatch.Draw(ModContent.Request<Texture2D>("ImprovedFlightControls/fireworks").Value,new Microsoft.Xna.Framework.Rectangle(
                0, 0, Main.screenWidth, Main.screenHeight), Microsoft.Xna.Framework.Color.White);
		}
		public override bool IsActive(){
			return active;
		}
        public override bool IsVisible(){
			return active;
		}

        public override void Reset()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}