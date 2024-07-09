using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace ImprovedFlightControls.Projectiles
{
	public class RailgunBullet : ModProjectile
	{
		public override void SetDefaults() {
			// This method right here is the backbone of what we're doing here; by using this method, we copy all of
			// the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
			// so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
			// if you're going to copy the stats of a projectile, use CloneDefaults().

			Projectile.CloneDefaults(ProjectileID.DiamondBolt);

			// To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
			// the projectile to essentially behave the same way as the vanilla projectile.
			AIType = ProjectileID.DiamondBolt;

			// After CloneDefaults has been called, we can now modify the stats to our wishes, or keep them as they are.
			// For the sake of example, lets make our projectile penetrate enemies a few more times than the vanilla projectile.
			// This can be done by modifying projectile.penetrate
			Projectile.extraUpdates=4;
			Projectile.penetrate=10;
			Projectile.usesLocalNPCImmunity=true;
			Projectile.localNPCHitCooldown=-1;

		}
		int cnt=0;

		void DoExplosion(){
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,Vector2.Zero, ProjectileID.DaybreakExplosion, Projectile.damage/5, Projectile.knockBack, Main.myPlayer,ai1:2);
			cnt=-1;
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			DoExplosion();
			if(damageDone<target.life){
				Projectile.timeLeft=2;
			}
			else if(damageDone<target.lifeMax*2){
				Projectile.damage=(int)Math.Max(1,Projectile.damage*0.5);
			}
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			DoExplosion();
            base.OnHitPlayer(target, info);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			DoExplosion();
            return base.OnTileCollide(oldVelocity);
        }        public override bool PreAI()
        {
			if(cnt>=0&&cnt++%4!=0){
				return false;
			}
            return base.PreAI();
        }
    }
}