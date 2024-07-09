
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using System;
using Terraria.GameContent.ItemDropRules;
using ImprovedFlightControls.Items;
using ImprovedFlightControls.Items.Equipment;

namespace ImprovedFlightControls
{
    public class ModGlobalNPC : GlobalNPC
    {
        public override void SetDefaults(NPC npc){
            NPC.LunarShieldPowerMax=NPC.LunarShieldPowerNormal=0;
            switch(npc.type){
                case NPCID.LunarTowerNebula:
                case NPCID.LunarTowerSolar:
                case NPCID.LunarTowerStardust:
                case NPCID.LunarTowerVortex:
                    npc.lifeMax= 200000;
                    break;
            }
        }
        public override void BuffTownNPC(ref float damageMult,ref int defense)
        {
			if(Main.expertMode){
				defense=(int)(defense * 1.5);
			}
			if(Main.masterMode){
				defense*=2;
			}
        }
        public override void ModifyNPCLoot (NPC npc, NPCLoot npcLoot){
            if(npc.type==NPCID.HallowBoss){
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.EmpressOfLightIsGenuinelyEnraged(),ModContent.ItemType<PermanantSoaringInsignia>()));
            }
        }
    }
}
