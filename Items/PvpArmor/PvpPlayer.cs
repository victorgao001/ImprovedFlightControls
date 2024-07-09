using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Audio;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ImprovedFlightControls.Items.PvpArmor
{
    public class PvpPlayer : ModPlayer
    {
        public bool pvpMode;
        public float speed,acceleration;
        public bool overrideMovement,dashing=false;
        public float dashDuration,dashCooldown,dashSpeed,dashTimer=9999;
        public bool slowProjectiles;
        public float regen;

        public override void ResetEffects()
        {
            pvpMode=false;
            speed=-1;
            acceleration=1;
            overrideMovement=false;
            dashDuration=dashCooldown=dashSpeed=0;
            slowProjectiles=false;
            regen=1;
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers){
            if(pvpMode){
                modifiers.FinalDamage*=0.1f;
            }
        }
        public override void PostUpdateEquips ()
        {
            if(dashing&&Player.velocity.Length()<speed+3){
                Player.maxRunSpeed=speed+3;
                dashing=false;
            }
			//Player.lifeRegen+=(int)(Player.lifeRegenTime/60*Player.statLifeMax2/100*0.2*regen);
        }
        public override void PostUpdateRunSpeeds(){
            if(speed!=-1&&Player.velocity.Y != 0.0 && !Player.mount.Active){
                Player.gravity=0;
                if(Player.whoAmI == Main.myPlayer){
                    overrideMovement=true;
                }
            }
            if(dashSpeed>0){
                dashTimer++;
                if(ImprovedFlightControls.Dash.Current&&dashTimer>dashCooldown){
                    dashTimer=0;
                }
                if(dashTimer<dashDuration){
                    speed=dashSpeed;
                    Player.maxFallSpeed=dashSpeed;
                    Player.jumpSpeed=dashSpeed;
                }
                if(dashTimer<5){
                    acceleration=speed/4;
                }
                if(dashTimer==dashDuration){
                    if(overrideMovement&&Player.velocity.Length()>speed){
                        Player.velocity.Normalize();
                        Player.velocity*=speed;
                    }
                }
            }
        }
        public override void PreUpdateMovement()
        {
			if(overrideMovement){
				Vector2 accelerationDir=Vector2.Zero;
				if(ImprovedFlightControls.FlyUpHotkey.Current){
					accelerationDir+=new Vector2(0,-1);
				}
				if(ImprovedFlightControls.FlyDownHotkey.Current){
					accelerationDir+=new Vector2(0,1);
				}
				if(ImprovedFlightControls.FlyLeftHotkey.Current){
					accelerationDir+=new Vector2(-1,0);
				}
				if(ImprovedFlightControls.FlyRightHotkey.Current){
					accelerationDir+=new Vector2(1,0);
				}
				if(accelerationDir==Vector2.Zero){
					Player.velocity*=0.5f;
				}
				else{					
                    if(Player.velocity.Length()<speed+3){
                        Vector2 proj=Math.Max(0,Vector2.Dot(accelerationDir,Player.velocity))*accelerationDir;
                        Player.velocity=(Player.velocity-proj)*0.5f+proj;
                        Player.velocity+=accelerationDir*acceleration;
                        if(Player.velocity.Length()>speed){
                            Player.velocity.Normalize();
                            Player.velocity*=speed;
                        }
                    }
                    else{
                        dashing=true;
                        acceleration/=3;
                        
                        speed=Player.velocity.Length();
                        Vector2 proj=Math.Max(0,Vector2.Dot(accelerationDir,Player.velocity))*Player.velocity/Player.velocity.LengthSquared();
                        accelerationDir-=proj;
                        Player.velocity+=accelerationDir*acceleration;
                    }
				}
			}
        }

    }
}