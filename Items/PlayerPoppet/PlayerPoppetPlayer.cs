using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Audio;
using System;
using System.Collections.Generic;

namespace ImprovedFlightControls.Items.PlayerPoppet
{
    public class PlayerPoppetPlayer : ModPlayer
    {
        public int maxPoppets;
        public int poppets;
        int poppetTime;

        public override void ResetEffects()
        {
            maxPoppets=0;
        }
        public override void PostUpdateEquips (){
            
            if(poppetTime>0){
                int maxPoppetHealth=Player.statLifeMax2*3/2;
                Player.lifeRegenCount+=2*maxPoppetHealth/10;
            }
            if(poppetTime<-60*60){
                poppetTime+=60*30;
                poppets++;
            }
            poppets=Math.Min(poppets,maxPoppets);
            poppetTime--;
        }
        public override void OnHurt(Player.HurtInfo info){
            poppetTime=0;
            int poppetHealth=0,maxPoppetHealth=(Player.statLifeMax2)*3/2;
            while(info.Damage>Player.statLife-1&&poppets>0){
                poppetHealth=maxPoppetHealth;
                var x=Math.Min(poppetHealth,info.Damage-Player.statLife+1);
                poppetHealth-=x;
                Player.statLife+=x;
                poppets--;
            }
            poppetTime=Math.Max(poppetTime,10*60*poppetHealth/maxPoppetHealth);
        }
        
        public override void OnRespawn(){
            poppetTime=-99999;
        }
        public override void OnEnterWorld(){
            poppetTime=-99999;
        }
        /*
        public override void SaveData(TagCompound tag) {
			tag["maxPoppets"] = maxPoppets;
		}

		public override void LoadData(TagCompound tag) {
			maxPoppets = tag.GetInt("maxPoppets");
		}*/
    }
}