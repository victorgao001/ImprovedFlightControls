
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
    public class DukeFishronMod : GlobalNPC
    {
        public override bool PreAI(NPC npc)
        {
            if(npc.type==NPCID.DukeFishron&&Main.expertMode && (double) npc.life <= (double) npc.lifeMax * 0.15&&npc.ai[2]==0&&npc.ai[3]>=6){
                var x=Main.rand.Next(2);
                if(x==0){
                    npc.ai[3]=4;
                }
                else{
                    npc.ai[3]=6;
                }
            }
            return true;
        }
        public override void ModifyNPCLoot (NPC npc, NPCLoot npcLoot){
            if(npc.type==NPCID.DukeFishron){
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<SeafoodSnatcher>(), 3, 2));
            }
        }
    }
}
