using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Graphics.Effects;

namespace ImprovedFlightControls{
	// Shows setting up two basic biomes. For a more complicated example, please request.
	public class CelebrationEvent : ModBiome
	{
		// Select all the scenery
		//public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<ExampleSurfaceBackgroundStyle>();
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

		// Select Music
		//public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MysteriousMystery");

        public static int EventId=-2348;                
        //Initializing an Array that can be used in any file
        const int waveCount=18;
        public static List<List<(int,float)>> invaders(){
            List<List<(int,float)>> invaders=new();
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.GreenSlime,10),(NPCID.BlueSlime,10),(NPCID.PurpleSlime,10),(NPCID.Pinky,3),(NPCID.FlyingFish,3),(NPCID.UmbrellaSlime,10),(NPCID.AngryNimbus,5),(NPCID.WindyBalloon,5)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.Zombie,10),(NPCID.DemonEye,10),(NPCID.PossessedArmor,10),(NPCID.WanderingEye,10),(NPCID.Wraith,10),(NPCID.Werewolf,10)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.EaterofSouls,10),(NPCID.DevourerHead,10),(NPCID.Corruptor,10),(NPCID.CorruptSlime,10),(NPCID.Slimer,10),(NPCID.SeekerHead,10),(NPCID.BloodCrawler,10),(NPCID.FaceMonster,10),(NPCID.Crimera,10),(NPCID.Herpling,10),(NPCID.Crimslime,10)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.Hornet,10),(NPCID.JungleBat,10),(NPCID.JungleSlime,10),(NPCID.LacBeetle,5),(NPCID.JungleCreeper,10),(NPCID.Moth,5),(NPCID.MossHornet,10),(NPCID.BigMimicJungle,1),(NPCID.SpikedJungleSlime,10),(NPCID.Snatcher,2),(NPCID.DoctorBones,3),(NPCID.Derpling,10),(NPCID.GiantTortoise,10),(NPCID.GiantFlyingFox,10),(NPCID.AngryTrapper,2),(NPCID.KingSlime,5)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.BlackSlime,10),(NPCID.MotherSlime,5),(NPCID.GiantWormHead,5),(NPCID.Skeleton,10),(NPCID.CaveBat,10),(NPCID.Salamander,5),(NPCID.Crawdad,5),(NPCID.GiantShelly,5),(NPCID.UndeadMiner,5),(NPCID.Tim,3),(NPCID.Nymph,3),(NPCID.CochinealBeetle,5),(NPCID.ArmoredSkeleton,10),(NPCID.DiggerHead,5),(NPCID.GiantBat,10),(NPCID.RockGolem,5),(NPCID.SkeletonArcher,10),(NPCID.RuneWizard,3),(NPCID.Mimic,5),(NPCID.BlackRecluse,5),(NPCID.WallCreeper,5),(NPCID.EyeofCthulhu,2),(NPCID.BrainofCthulhu,2),(NPCID.EaterofWorldsHead,2)}));
            
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.IceTortoise,10),(NPCID.GiantTortoise,10)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.IceSlime,10),(NPCID.ZombieEskimo,10),(NPCID.IceElemental,8),(NPCID.Wolf,8),(NPCID.SnowmanGangsta,5),(NPCID.MisterStabby,5),(NPCID.SnowBalla,3),(NPCID.Vulture,5),(NPCID.Mummy,10),(NPCID.LightMummy,5),(NPCID.DarkMummy,5),(NPCID.BloodMummy,5),(NPCID.DuneSplicerHead,5),(NPCID.IceBat,10),(NPCID.SnowFlinx,10),(NPCID.SpikedIceSlime,10),(NPCID.UndeadViking,10),(NPCID.CyanBeetle,5),(NPCID.ArmoredViking,10),(NPCID.IceTortoise,10),(NPCID.IcyMerman,10),(NPCID.IceMimic,10),(NPCID.PigronCorruption,3),(NPCID.PigronCrimson,3),(NPCID.PigronHallow,3),(NPCID.GiantWalkingAntlion,10),(NPCID.WalkingAntlion,10),(NPCID.FlyingAntlion,10),(NPCID.GiantFlyingAntlion,10),(NPCID.SandSlime,10),(NPCID.TombCrawlerHead,5),(NPCID.DesertBeast,10),(NPCID.DesertScorpionWalk,10),(NPCID.DesertLamiaLight,5),(NPCID.DesertLamiaDark,5),(NPCID.DesertGhoul,4),(NPCID.DesertGhoulCorruption,4),(NPCID.DesertGhoulCrimson,4),(NPCID.DesertGhoulHallow,4),(NPCID.DesertDjinn,10),(NPCID.IceGolem,8),(NPCID.SandElemental,5)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.TheBride,5),(NPCID.TheGroom,5),(NPCID.BloodZombie,10),(NPCID.Drippler,10),(NPCID.CorruptBunny,5),(NPCID.CorruptPenguin,5),(NPCID.CrimsonBunny,5),(NPCID.CrimsonPenguin,5),(NPCID.EyeballFlyingFish,10),(NPCID.ZombieMerman,10),(NPCID.Clown,6),(NPCID.GoblinShark,3),(NPCID.BloodEelHead,3),(NPCID.BloodNautilus,2)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.Pixie,10),(NPCID.Unicorn,10),(NPCID.Gastropod,5),(NPCID.ShimmerSlime,10),(NPCID.RainbowSlime,10),(NPCID.QueenSlimeBoss,1)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.IlluminantSlime,10),(NPCID.IlluminantBat,10),(NPCID.ChaosElemental,10),(NPCID.EnchantedSword,10),(NPCID.BigMimicHallow,15),(NPCID.CorruptSlime,10),(NPCID.CursedHammer,10),(NPCID.BigMimicCorruption,15),(NPCID.CrimsonAxe,10),(NPCID.IchorSticker,10),(NPCID.FloatyGross,10),(NPCID.BigMimicCrimson,15)}));
            //
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.AngryBones,10),(NPCID.DarkCaster,5),(NPCID.CursedSkull,10),(NPCID.DungeonSlime,10),(NPCID.BlueArmoredBones,2),(NPCID.BlueArmoredBonesMace,2),(NPCID.BlueArmoredBonesNoPants,3),(NPCID.BlueArmoredBonesSword,2),(NPCID.RustyArmoredBonesAxe,3),(NPCID.RustyArmoredBonesFlail,2),(NPCID.RustyArmoredBonesSword,2),(NPCID.RustyArmoredBonesSwordNoArmor,3),(NPCID.HellArmoredBones,2),(NPCID.HellArmoredBonesMace,2),(NPCID.HellArmoredBonesSpikeShield,2),(NPCID.HellArmoredBonesSword,2),(NPCID.Necromancer,2),(NPCID.NecromancerArmored,2),(NPCID.RaggedCaster,2),(NPCID.RaggedCasterOpenCoat,2),(NPCID.DiabolistRed,1),(NPCID.DiabolistWhite,1),(NPCID.SkeletonCommando,4),(NPCID.SkeletonSniper,4),(NPCID.TacticalSkeleton,4),(NPCID.BoneLee,4),(NPCID.GiantCursedSkull,6),(NPCID.Paladin,5),(NPCID.DungeonSpirit,5),(NPCID.SkeletronHead,4)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.PirateCaptain,20),(NPCID.GoblinSummoner,30),(NPCID.Nailhead,10),(NPCID.Mothron,10),(NPCID.PirateShip,3),(NPCID.MartianSaucerCore,2),(NPCID.Golem,1)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.Raven,20),(NPCID.Scarecrow1,10),(NPCID.Splinterling,10),(NPCID.Hellhound,10),(NPCID.Poltergeist,10),(NPCID.HeadlessHorseman,15),(NPCID.MourningWood,6),(NPCID.Pumpking,5),(NPCID.GingerbreadMan,10),(NPCID.ZombieElf,10),(NPCID.ElfArcher,10),(NPCID.Nutcracker,10),(NPCID.Yeti,10),(NPCID.ElfCopter,15),(NPCID.Krampus,10),(NPCID.Flocko,10),(NPCID.Everscream,6),(NPCID.SantaNK1,6),(NPCID.IceQueen,4)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.SolarSolenian,10),(NPCID.SolarCorite,10),(NPCID.SolarSroller,10),(NPCID.SolarDrakomireRider,10),(NPCID.SolarCrawltipedeHead,3),(NPCID.VortexSoldier,10),(NPCID.VortexHornetQueen,10),(NPCID.VortexRifleman,10),(NPCID.NebulaBeast,10),(NPCID.NebulaBrain,10),(NPCID.NebulaHeadcrab,10),(NPCID.NebulaSoldier,10),(NPCID.StardustCellBig,10),(NPCID.StardustJellyfishBig,10),(NPCID.StardustSoldier,10),(NPCID.StardustSpiderBig,10),(NPCID.StardustWormHead,10),(NPCID.CultistArcherBlue,5),(NPCID.CultistArcherWhite,5),(NPCID.CultistDragonHead,10),(NPCID.AncientCultistSquidhead,10),(NPCID.ScutlixRider,10),(NPCID.MartianWalker,10),(NPCID.MartianDrone,10),(NPCID.GigaZapper,10),(NPCID.MartianEngineer,10),(NPCID.MartianOfficer,10),(NPCID.GrayGrunt,10),(NPCID.BrainScrambler,10)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.DD2Betsy,1),(NPCID.FireImp,15),(NPCID.Demon,15),(NPCID.VoodooDemon,5),(NPCID.RedDevil,10)}));
            
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.Retinazer,6),(NPCID.Spazmatism,6),(NPCID.SkeletronPrime,6)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(NPCID.CorruptBunny,6),(NPCID.CrimsonBunny,6)}));
            invaders.Add(new List<(int,float)>(new (int,float)[]{(ModContent.NPCType<Npcs.EmpressEternal>(),1)}));            
            return invaders;
        }
        public static int wave;
        static int baseInvasionSize;
        public static int spawnRate=50;
        static int progressMileStone;
        static int timerLimit=60*60;
        public static int timer;
        
        // Calculate when the biome is active.
		public override bool IsBiomeActive(Player player) {
            return Main.invasionType == EventId;
		}
        //Setup for an Invasion After setting up
        public static void StartCelebrationEvent()
        {
            Main.invasionType=0;
            //Once it is set to no invasion setup the invasion
            if (Main.invasionType == 0)
            {
                wave = 0;
                spawnRate=50;
                //Checks amount of players for scaling
                int numPlayers = 0;
                for (int i = 0; i < 255; i++){
                    numPlayers+=Main.player[i].active? 1:0;
                }
                if (numPlayers > 0)
                {
                    //Invasion setup
                    Main.invasionType = EventId; //Not going to be using an invasion that is positive since those are vanilla invasions
                    //MWorld.CelebrationEventUp = true;
                    baseInvasionSize=300 * numPlayers;
                    Main.invasionSize = baseInvasionSize;
                    Main.invasionSizeStart = Main.invasionSize;
                    Main.invasionProgress = 0;
                    Main.invasionProgressIcon = 0 + 3;
                    Main.invasionProgressMax = Main.invasionSizeStart;
                    Main.invasionX = 0.0; //Starts invasion immediately rather than wait for it to spawn
                    progressMileStone=0;
                    timer=timerLimit+60*30;
                }
            }
        }
        public static void EndEventEarly(){
            timer=-999;
        }

        //Text for messages and syncing
        public static void CelebrationEventWarning(string text)
        {
            if (Main.netMode == 0)
            {
                Main.NewText(text);
                return;
            }
            if (Main.netMode == 2)
            {
                //Sync with net
                NetMessage.SendData(25, -1, -1, Terraria.Localization.NetworkText.FromLiteral(text), 255, 175f, 75f, 255f, 0, 0, 0);
            }
        }

        public override void OnEnter(Player player) {
            CelebrationEventWarning("The Custom Invasion has started!");
            SkyManager.Instance.Activate("CelebrationEventSky");
        }
        public override void OnLeave(Player player) {
            CelebrationEventWarning("Custom Invasion is finished");
            SkyManager.Instance.Deactivate("CelebrationEventSky");
        }
        //Checks the players' progress in invasion
        public override void OnInBiome(Player player)
        {
            timer--;
            if(timer<=-60*5){
                Main.NewText("Time is up");
                Main.invasionType = 0;
            }
            if(Main.invasionProgress>=Main.invasionProgressMax*3/4&&progressMileStone==0){
                progressMileStone=1;
                switch(wave+1){
                    case 1:
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.KingSlime);
                        break;
                    case 2:
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.EyeofCthulhu);
                        break;
                    case 3:
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.EaterofWorldsHead);
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.BrainofCthulhu);
                        break;
                    case 4:
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.Deerclops);
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.QueenBee);
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.SkeletronHead);
                        break;
                    case 9:
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.Retinazer);
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.Spazmatism);
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.SkeletronPrime);
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.TheDestroyer);
                        break;
                    case 10:
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.Plantera);
                        break;
                    case 16:
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.TheDestroyer);
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.TheDestroyer);
                        NPC.SpawnOnPlayer(player.whoAmI,NPCID.TheDestroyer);
                        break;
                }
            }
            if(wave+1==18&&progressMileStone==0){

            }
            if(Main.invasionType == EventId&&Main.invasionSize <= 1&&wave+1<waveCount)
            {
                wave++;
                CelebrationEventWarning("Wave "+(wave+1));
                Main.invasionSize=Main.invasionSizeStart=baseInvasionSize;
                Main.invasionProgress=0;
                progressMileStone=0;
                spawnRate=50;
                timer+=timerLimit;
                switch (wave+1)
                {
                    case 1:
                    case 2:
                    case 3:
                        spawnRate=spawnRate*3/2;
                        break;
                    case 4:
                        spawnRate*=2;
                        break;
                    case 5:
                        break;
                    case 6:
                        Main.invasionSize=Main.invasionSizeStart=baseInvasionSize/2;
                        break;
                    case 7:
                        Main.invasionSize=Main.invasionSizeStart=baseInvasionSize*3/2;
                        break;
                    case 12:
                        Main.invasionSize=Main.invasionSizeStart=baseInvasionSize/3;
                        spawnRate/=5;
                        break;
                    case 13:
                        spawnRate/=2;
                        Main.invasionSize=Main.invasionSizeStart=baseInvasionSize*2/3;
                        timer+=timerLimit;
                        break;
                    case 14:
                        Main.invasionSize=Main.invasionSizeStart=baseInvasionSize*4/3;
                        break;
                    case 15:
                        spawnRate/=2;
                        Main.invasionSize=Main.invasionSizeStart=baseInvasionSize*2/3;
                        timer+=timerLimit;
                        break;
                    case 16:
                        spawnRate/=4;
                        Main.invasionSize=Main.invasionSizeStart=baseInvasionSize/8;
                        break;
                    case 17:
                        Main.invasionSize=Main.invasionSizeStart=baseInvasionSize/10;
                        break;
                    case 18:
                        spawnRate=0;
                        Main.invasionSize=Main.invasionSizeStart=1;
                        NPC.SpawnOnPlayer(player.whoAmI,ModContent.NPCType<Npcs.EmpressEternal>());
                        NPC.SpawnOnPlayer(player.whoAmI,ModContent.NPCType<Npcs.EmpressEternal>());
                        break;
                    default:
                        break;
                }
            }
            if(Main.invasionType == EventId&&Main.invasionSize <=0&&wave+1>=waveCount){
                wave=0;
                Main.invasionSize=0;
                Main.invasionType = 0;
                Main.invasionDelay = 0;
                Main.NewText("You win!");
            }


            //Not really sure what this is
            Main.invasionProgressNearInvasion = true;/*
            if (Main.invasionProgressMode != 2)
            {
                Main.invasionProgressNearInvasion = false;
                return;
            }*/
            //If the custom invasion is up and the enemies are at the spawn pos
            if(Main.invasionType==EventId)
            {
                //Shows the UI for the invasion
                Main.ReportInvasionProgress(Main.invasionProgress, Main.invasionSizeStart, 0, wave+1);
            }
            //Syncing start of invasion
            //*
            foreach(Player p in Main.player)
            {
                NetMessage.SendData(78, p.whoAmI, -1, null, Main.invasionSizeStart - Main.invasionSize, (float)Main.invasionSizeStart, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
            }
        }
    }
}