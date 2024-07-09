
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
    public class CelebrationEventNPC : GlobalNPC
    {
        //Change the spawn pool
        public override void EditSpawnPool(IDictionary< int, float > pool, NPCSpawnInfo spawnInfo)
        {
            //If the custom invasion is up and the invasion has reached the spawn pos
            if(Main.invasionType==CelebrationEvent.EventId)
            {
                //Clear pool so that only the stuff you want spawns
                pool.Clear();
   
                //key = NPC ID | value = spawn weight
                //pool.add(key, value)
       
                //For every ID inside the invader array in our CelebrationEvent file
                foreach((int,float) i in CelebrationEvent.invaders()[CelebrationEvent.wave])
                {
                    pool.Add(i.Item1, i.Item2); //Add it to the pool with the same weight of 1
                }
            }
        }

        //Changing the spawn rate
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            //Change spawn stuff if invasion up and invasion at spawn
            if(Main.invasionType==CelebrationEvent.EventId&&CelebrationEvent.spawnRate>0)
            {
                spawnRate /= CelebrationEvent.spawnRate; //Higher the number, the more spawns
                maxSpawns *= CelebrationEvent.spawnRate; //Max spawns of NPCs depending on NPC value
            }
        }
        
        static bool dayTime;
        public override bool PreAI(NPC npc)
        {
            if(Main.invasionType==CelebrationEvent.EventId){
                dayTime=Main.dayTime;
                Main.dayTime=false;
                Main.eclipse=true;
            }
            return true;
        }
        //Adding to the AI of an NPC
        public override void PostAI(NPC npc)
        {
            if(Main.invasionType==CelebrationEvent.EventId)
            {
                npc.boss=false;
                Main.dayTime=dayTime;
                Main.eclipse=false;
            }
        }

        public override void OnKill(NPC npc)
        {
            //When an NPC (from the invasion list) dies, add progress by decreasing size
            if(Main.invasionType==CelebrationEvent.EventId)
            {
                //Gets IDs of invaders from CelebrationEvent file
                foreach((int,float) invader in CelebrationEvent.invaders()[CelebrationEvent.wave])
                {
                    //If npc type equal to invader's ID decrement size to progress invasion
                    if(npc.type == invader.Item1)
                    {
                        Main.invasionSize -= 1;
                        Main.invasionProgress++;
                    }
                }
            }
        }
    }
}
