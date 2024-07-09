using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;

namespace ImprovedFlightControls.Items
{
    public class PeaceOffering : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1;
            Item.maxStack = 99;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 3;
        }

        public override bool? UseItem(Player player)
        {
            foreach (var npc in Main.npc){
                if(!npc.friendly&&(npc.position-player.position).Length()<20*16){
                    npc.friendly=true;
                    switch(npc.type){
                        case NPCID.KingSlime:
                        case NPCID.QueenSlimeBoss :
                            //npc.aiStyle=NPCAIStyleID.Slime;
                            break;
                        case NPCID.EyeofCthulhu:
                        case NPCID.Spazmatism :
                        case NPCID.Retinazer :
                            npc.aiStyle=NPCAIStyleID.Flying;
                            break;
                        case NPCID.BrainofCthulhu :
                        case NPCID.QueenBee  :
                        case NPCID.SkeletronHead  :
                            npc.aiStyle=NPCAIStyleID.Bat ;
                            break;
                        case NPCID.DukeFishron  :
                            npc.aiStyle=NPCAIStyleID.DemonEye  ;
                            break;
                        case NPCID.Plantera    :
                        case NPCID.Golem  :
                        case NPCID.CultistBoss  :
                        case NPCID.HallowBoss   :
                            npc.aiStyle=NPCAIStyleID.Passive ;
                            break;
                    }
                }
            }
            return true;
        }
        
		public override void UpdateInventory(Player player){
			player.aggro-=(int)1e6;
            foreach (var npc in Main.npc){
                if(!npc.friendly&&!npc.boss&&(npc.position-player.position).Length()<120*16){
                    npc.friendly=true;
                }
            }
		}
    }
    public class FriendlyNPC : GlobalNPC
    {
        public override bool PreAI(NPC npc){
            if(npc.friendly&&!npc.isLikeATownNPC){
                /*
                float d=900;
                foreach (var npc2 in Main.npc){
                    if(npc2.CanBeChasedBy()&&(npc2.position-npc.position).Length()<d){
                        d=(npc2.position-npc.position).Length();
                        npc.target=npc2.WhoAmIToTargettingIndex;
                        //Main.NewText(npc.SupportsNPCTargets+"a"+npc.target+" "+npc2.type);
                    }
                }*/
            }
            return true;
        }
        public override void OnSpawn (NPC npc, IEntitySource source){
            if(source is EntitySource_Parent &&((EntitySource_Parent)source).Entity is NPC){
                var parent=(NPC)((EntitySource_Parent)source).Entity;
                npc.friendly=parent.friendly;
            }
        }
        public override void OnHitNPC (NPC npc, NPC target, NPC.HitInfo hit){
            if(target.friendly){
                npc.SimpleStrikeNPC(target.damage,-hit.HitDirection);
            }
        }
    }
    public class FriendlyProjectile:GlobalProjectile{
        public override void OnSpawn (Projectile projectile, IEntitySource source){
            if(source is EntitySource_Parent &&((EntitySource_Parent)source).Entity is NPC){
                var parent=(NPC)((EntitySource_Parent)source).Entity;
                projectile.friendly=parent.friendly;
                projectile.hostile=!parent.friendly;
                if(parent.friendly&&!parent.isLikeATownNPC){
                    projectile.damage*=2;
                    projectile.damage*=Main.masterMode? 3:Main.expertMode? 2:1;
                }
            }
        }
    }
}