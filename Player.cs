using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Audio;
using System;
using System.Collections.Generic;
using Terraria.GameInput;

namespace ImprovedFlightControls
{
    public class Player1 : ModPlayer
    {
        public float maxBoostedFallSpeed;
        public float maxBoostedGravity;
        public bool permanantSoaringInsignia;
        int resetDirection=0;
        bool returnVortexStealth=false;
        List<Item> toResummon=new();
        Item initalResummonSelectedItem=null;
        int prevSelectedItem,prevQuickUse;
        public override void ResetEffects()
        {
            maxBoostedFallSpeed = 0;
            maxBoostedGravity = 0;
            for(int i=0;i<4;i++){
                Player.doubleTapCardinalTimer[i]=0;
            }
            Player.dashTime=0;
        }
        public override void PostUpdateBuffs(){
            if(ModContent.GetInstance<Config>().HideInferno){
                Player.inferno=false;
            }
        }
        public override void PostUpdateEquips (){
            if(permanantSoaringInsignia){
                Player.empressBrooch=true;
            }
            if(ImprovedFlightControls.Dash.JustPressed){
                if(ImprovedFlightControls.FlyLeftHotkey.Current){
                    Player.dashTime=-999;
                    Player.controlLeft=Player.releaseLeft=true;
                }
                else if(ImprovedFlightControls.FlyRightHotkey.Current){
                    Player.dashTime=999;
                    Player.controlRight=Player.releaseRight=true;
                }
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            if((Player.gravControl||Player.gravControl2)){
                if(ImprovedFlightControls.FlipGravity.JustPressed){
                    Player.gravDir *= -1f;
                    Player.fallStart = (int) ((double) Player.position.Y / 16.0);
                    Player.jump = 0;
                    SoundEngine.PlaySound(SoundID.Item8, Player.position);
                }
                if(Player.controlUp && Player.releaseUp){
                    Player.gravDir*=-1f;
                }
            }
            if (Player.TryingToHoverDown)
            {
                if (maxBoostedFallSpeed > 0)
                {
                    Player.maxFallSpeed = Math.Max(Player.maxFallSpeed, maxBoostedFallSpeed);
                }
                if (maxBoostedGravity > 0)
                {
                    if (Player.velocity.Y * Player.gravDir < 0)
                    {
                        Player.velocity.Y *= 0.9f;
                    }
                    Player.gravity = Math.Max(Player.gravity, maxBoostedGravity);
                }
            }
        }
        public override void PreUpdate(){
            if(toResummon.Count>0){
                Player.itemTime=0;
                Player.inventory[Player.selectedItem]= toResummon[0];
                Player.controlUseItem = true;
                Player.ItemCheck();
                toResummon.RemoveAt(0);
            }
            else if(initalResummonSelectedItem!=null){
                Player.inventory[Player.selectedItem]=initalResummonSelectedItem;
                initalResummonSelectedItem=null;
            }
            foreach(Item item in Main.item){
                if(ItemID.Sets.NebulaPickup[item.type]&&Player.manaMagnet){
                    int itemGrabRange=Player.defaultItemGrabRange+700;
                    if(new Microsoft.Xna.Framework.Rectangle((int) Player.position.X - itemGrabRange, (int) Player.position.Y - itemGrabRange, Player.width + itemGrabRange * 2, Player.height + itemGrabRange * 2).Intersects(item.getRect())){
                        float num1 = Player.Center.X - item.position.X + (float) (item.width / 2);
                        float num2 = Player.Center.Y - item.position.Y + (float) (item.height / 2);
                        float num3 = (float) Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
                        float num4 = 20 / num3;
                        float num5 = num1 * num4;
                        float num6 = num2 * num4;
                        item.position.X += num5;
                        item.position.Y += num6;
                        
                    }
                }
            }
        }
        public override void SetControls()
        {

            //set bonus
            if (ImprovedFlightControls.ActivateSetBonus.JustPressed)
            {
                Player.KeyDoubleTap(0);
                Player.KeyDoubleTap(1);
                returnVortexStealth=!returnVortexStealth;
            }
            if(returnVortexStealth&&Player.setVortex && !Player.mount.Active){
                Player.vortexStealthActive=true;    
            }


            //wings autopilot
            WingStats wings = Player.GetWingStats(Player.wingsLogic);
            if (ImprovedFlightControls.FlyUpHotkey.Current)
            {
                Player.controlJump = true;
                if (Player.wingsLogic != 4)
                {
                    //if wing is not jetpack and not at max boost speed
                    Player.controlUp = -Player.velocity.Y < Player.jumpSpeed * Player.gravDir * 3;
                }
            }
            //for some reason the game uses velocity==0 to check grounded or grappled, but when hovering velocity=1e-5
            if (Player.velocity.Y != 0.0 && !Player.mount.Active)
            {
                if (!ImprovedFlightControls.FlyDownHotkey.Current && !ImprovedFlightControls.FlyUpHotkey.Current)
                {
                    if (wings.HasDownHoverStats && Player.wingTime > 0)
                    {
                        Player.controlDown = true;
                        Player.controlJump = true;
                        if(ImprovedFlightControls.FlyLeftHotkey.Current||ImprovedFlightControls.FlyRightHotkey.Current){
                            resetDirection=0;
                        }
                        else if (Player.velocity.X < 0 &&(resetDirection==-1||resetDirection==0))
                        {
                            Player.controlRight = true;
                            resetDirection=-1;
                        }
                        else if (Player.velocity.X > 0 &&(resetDirection==1||resetDirection==0))
                        {
                            Player.controlLeft = true;
                            resetDirection=1;
                        }
                        else if(resetDirection!=2){
                            if(resetDirection==-1){
                                Player.controlLeft = true;
                            }
                            else if(resetDirection==1){
                                Player.controlRight = true;
                            }
                            resetDirection=2;
                        }
                    }
                    else if (Player.slowFall)
                    {
                        Player.controlUp = true;
                    }
                    else if (Player.wingsLogic > 0 && Player.wingTime == 0)
                    {
                        Player.controlJump = true;
                    }
                }
            }

        }
        public override void ProcessTriggers(TriggersSet triggersSet){
            if(triggersSet.Hotbar1){
                Player.selectedItem=0;
            }
            if(triggersSet.Hotbar2){
                Player.selectedItem=1;
            }
            if(triggersSet.Hotbar3){
                Player.selectedItem=2;
            }
            if(triggersSet.Hotbar4){
                Player.selectedItem=3;
            }
            if(triggersSet.Hotbar5){
                Player.selectedItem=4;
            }
            if(triggersSet.Hotbar6){
                Player.selectedItem=5;
            }
            if(triggersSet.Hotbar7){
                Player.selectedItem=6;
            }
            if(triggersSet.Hotbar8){
                Player.selectedItem=7;
            }
            if(triggersSet.Hotbar9){
                Player.selectedItem=8;
            }
            if(triggersSet.Hotbar10){
                Player.selectedItem=9;
            }
            bool quickUse=false,firstQuickUse;
            for(var i=0;i<10;i++){
                if(ImprovedFlightControls.UseItem[i].Current){
                    if(prevSelectedItem==-1){
                        prevSelectedItem=Player.selectedItem;
                    }
                    Player.selectedItem=i+10;
                    quickUse=true;
                    break;
                }
            }
            if(quickUse){
                firstQuickUse=prevQuickUse!=Player.selectedItem;
                prevQuickUse=Player.selectedItem;
                if(firstQuickUse&&Player.inventory[Player.selectedItem].damage<=0){
                    Player.itemTime=Player.itemAnimation=0;
                }
                Player.controlUseItem=true;
            }
            else{
                if(prevSelectedItem!=-1){
                    Player.selectedItem=prevSelectedItem;
                    prevSelectedItem=-1;
                }
                prevQuickUse=-1;
            }
        }
        void ResummonMinions(){
            initalResummonSelectedItem=Player.inventory[Player.selectedItem];
            var a=0;
            var summonCount=ModContent.GetInstance<Config>().summonCount;
            foreach(Item item in Player.inventory){
                if(item.favorited&&ContentSamples.ProjectilesByType[item.shoot].minion){
                    if(a<summonCount.Count){
                        for(var i=0;i<summonCount[a];i++){
                            toResummon.Add(item);
                        }
                        a++;
                    }
                }
            }
            if(ModContent.GetInstance<Config>().summonBank){
                foreach(Item item in Player.bank.item){
                    if(ContentSamples.ProjectilesByType[item.shoot].minion){
                        if(a<summonCount.Count){
                            for(var i=0;i<summonCount[a];i++){
                                toResummon.Add(item);
                            }
                            a++;
                        }
                    }
                }
            }
            foreach(Item item in Player.bank4.item){
                if(item.favorited&&ContentSamples.ProjectilesByType[item.shoot].minion){
                    if(a<summonCount.Count){
                        for(var i=0;i<summonCount[a];i++){
                            toResummon.Add(item);
                        }
                        a++;
                    }
                }
            }
        }
        public override void OnRespawn(){
            ResummonMinions();
        }
        public override void OnEnterWorld(){
            ResummonMinions();
        }
        public override void SaveData(TagCompound tag) {
			tag["permanantSoaringInsignia"] = permanantSoaringInsignia;
		}

		public override void LoadData(TagCompound tag) {
			permanantSoaringInsignia = tag.GetBool("permanantSoaringInsignia");
		}
    }
}