using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using System;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;
using System.Linq;

namespace ImprovedFlightControls.Npcs
{
	// The main part of the boss, usually refered to as "body"
	[AutoloadBossHead] // This attribute looks for a texture called "ClassName_Head_Boss" and automatically registers it as the NPC boss head icon
	public class EmpressEternal : ModNPC
	{/*
		// This code here is called a property: It acts like a variable, but can modify other things. In this case it uses the NPC.ai[] array that has four entries.
		// We use properties because it makes code more readable ("if (SecondStage)" vs "if (NPC.ai[0] == 1f)").
		// We use NPC.ai[] because in combination with NPC.netUpdate we can make it multiplayer compatible. Otherwise (making our own fields) we would have to write extra code to make it work (not covered here)
		public bool SecondStage {
			get => NPC.ai[0] == 1f;
			set => NPC.ai[0] = value ? 1f : 0f;
		}
		// If your boss has more than two stages, and since this is a boolean and can only be two things (true, false), concider using an integer or enum

		// Auto-implemented property, acts exactly like a variable by using a hidden backing field
		public Vector2 LastFirstStageDestination { get; set; } = Vector2.Zero;

		// This property uses NPC.localAI[] instead which doesn't get synced, but because SpawnedMinions is only used on spawn as a flag, this will get set by all parties to true.
		// Knowing what side (client, server, all) is in charge of a variable is important as NPC.ai[] only has four entries, so choose wisely which things you need synced and not synced
		public bool SpawnedMinions {
			get => NPC.localAI[0] == 1f;
			set => NPC.localAI[0] = value ? 1f : 0f;
		}

		private const int FirstStageTimerMax = 90;
		// This is a reference property. It lets us write FirstStageTimer as if it's NPC.localAI[1], essentially giving it our own name
		public ref float FirstStageTimer => ref NPC.localAI[1];

		public ref float RemainingShields => ref NPC.localAI[2];

		// We could also repurpose FirstStageTimer since it's unused in the second stage, or write "=> ref FirstStageTimer", but then we have to reset the timer when the state switch happens
		public ref float SecondStageTimer_SpawnEyes => ref NPC.localAI[3];

		// Do NOT try to use NPC.ai[4]/NPC.localAI[4] or higher indexes, it only accepts 0, 1, 2 and 3!
		// If you choose to go the route of "wrapping properties" for NPC.ai[], make sure they don't overlap (two properties using the same variable in different ways), and that you don't accidently use NPC.ai[] directly

/*
		public override void Load() {
			
		}
*/
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = 63;

			// Add this in for bosses that have a summon item, requires corresponding code in the item (See MinionBossSummonItem.cs)
			NPCID.Sets.MPAllowedEnemies[Type] = true;
			// Automatically group with other bosses
			NPCID.Sets.BossBestiaryPriority.Add(Type);

			// Specify the debuffs it is immune to
			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
			{
				SpecificallyImmuneTo = new int[] {

					BuffID.Confused // Most NPCs have this
				}
			};
			NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
			NPCID.Sets.UsesNewTargetting[Type]=true;
		}

		public override void SetDefaults()
		{
			NPC.width = 120;
			NPC.height = 120;
			NPC.damage = 80;
			NPC.lifeMax = 1000000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.value = Item.buyPrice(gold: 5);
			NPC.SpawnWithHigherTime(30);
			NPC.boss = true;
			NPC.npcSlots = 10f; // Take up open spawn slots, preventing random NPCs from spawning during the fight
			NPC.scale = 0.5f;

			// Don't set immunities like this as of 1.4:
			// NPC.buffImmune[BuffID.Confused] = true;
			// immunities are handled via dictionaries through NPCID.Sets.DebuffImmunitySets

			// Custom AI, 0 is "bound town NPC" AI which slows the NPC down and changes sprite orientation towards the target
			//NPC.aiStyle=-1;
			NPC.aiStyle = 120;
			AIType = 636;
			//AnimationType = 636;

			// The following code assigns a music track to the boss in a simple way.
			if (!Main.dedServ)
			{
				//Music = MusicLoader.GetMusicSlot(Mod, "/");
			}
		}
		int headCount = 1;
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
			NPC.damage = NPC.damage * 110 / 160;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Sets the description of this NPC that is listed in the bestiary
			bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
				new MoonLordPortraitBackgroundProviderBestiaryInfoElement(), // Plain black background
				new FlavorTextBestiaryInfoElement("Example Minion Boss that spawns minions on spawn, summoned with a spawn item. Showcases boss minion handling, multiplayer conciderations, and custom boss bar.")
			});
		}
		public override void UpdateLifeRegen(ref int damage)
		{
			NPC.lifeRegen += 2 * (NPC.ai[3]%2==0? 2000:5000);

		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.FlatBonusDamage += target.lifeMax * .05f + target.life * 0.45f;
		}
		public override void ModifyHitPlayer (Player target, ref Player.HurtModifiers modifiers)
		{
			if(NPC.ai[0]==8||NPC.ai[0]==9){
				modifiers.SourceDamage+=160/110f;
				modifiers.SourceDamage.Flat += target.statLifeMax2*.5f;
			}
		}
		float prevAI;
		float prevAttack;
		//
		readonly float[] attacks = { 2, 4, 5, 6, 7, 8, 11, 12 };
		public void SummonFastHoming(float hue)
		{
			if(NPC.ai[3]>=2){
				hue=0.145278f+0.5f;
			}
			Vector2 vector2_24 = Main.rand.NextVector2Circular(1f, 1f) + Main.rand.NextVector2CircularEdge(3f, 3f);
			if ((double)vector2_24.Y > 0.0)
				vector2_24.Y *= -1f;
			var dmg = NPC.ai[3]>=2? 20000: Main.expertMode ? 100 / 2 / 2 : 50 / 2;
			var id = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vector2_24, ProjectileID.FairyQueenMagicItemShot, dmg, 2.5f, Main.myPlayer, NPC.TranslatedTargetIndex, hue);
			//Main.projectile[id].friendly = NPC.friendly;;
			//Main.projectile[id].hostile = !NPC.friendly;
		}
		/*
		homing: 2,12
		spiral: 5
		spinning blades: 6
		projectiles: 7, 4, 11
		dash: 8
		*/
		int fastHomingAttack = 0;
		Vector2 dashDir;
		public override bool PreAI()
		{
			if(NPC.HasNPCTarget){
				Main.npc[NPC.TranslatedTargetIndex].AddBuff(ModContent.BuffType<Buffs.Shatter>(), 600);
			}
			else{
				Main.player[NPC.target].AddBuff(ModContent.BuffType<Buffs.Shatter>(), 600);
			}
			var player=NPC.GetTargetData();
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (NPC.realLife == -1)
				{
					foreach (var npc in Main.npc) if (npc.type == ModContent.NPCType<EmpressEternal>())
						{
							if (npc.realLife == -1 && NPC.whoAmI != npc.whoAmI)
							{
								headCount++;
								npc.realLife = NPC.whoAmI;
								NPC.lifeMax += npc.lifeMax;
								NPC.life += npc.life;
							}
						}
				}
				if (prevAI == 10 && NPC.ai[0] != 10)
				{
					NPC.lifeMax /= headCount;
					NPC.life /= headCount;
					foreach (var npc in Main.npc) if (npc.type == ModContent.NPCType<EmpressEternal>())
						{
							if (npc.realLife == NPC.whoAmI && npc.life > 0)
							{
								npc.EncourageDespawn(10);
								npc.life = -9999;
							}
						}
				}
				if (prevAI == 1 && attacks.Contains(NPC.ai[0]))
				{ 
					var x = Main.rand.Next(attacks.Length);
					NPC.ai[0] = attacks[x];

					if (x == 7) { }//do the other type of attack

					if (NPC.ai[3] % 2 == 0)
					{
						if ((NPC.Center - player.Center).Length() < 320 && Main.rand.Next(2) == 0)
						{
							NPC.ai[0] = 5;
						}
					}
					else
					{
						while (NPC.ai[0] == 5)
						{
							x = Main.rand.Next(attacks.Length);
							NPC.ai[0] = attacks[x];
						}
						if(Main.rand.Next(25)==0){
							NPC.ai[0]=5;
						}
						if (Main.rand.Next(8) == 0)
						{
							var hue=Main.rand.NextFloat();
							for (var i = 0; i < 4; i++)
							{
								SummonFastHoming(hue);
							}
						}
					}

					prevAttack = NPC.ai[0];
				}
				if (NPC.ai[3] % 2 == 1)
				{
					switch (NPC.ai[0])
					{
						case 7:
						case 11:
						case 4:
							var newDelay = NPC.ai[0] == 11 ? 50 : NPC.ai[0] == 4 ? 40 : 30;
							foreach (var proj in Main.projectile)
							{
								if (proj.type == ProjectileID.FairyQueenLance && proj.localAI[0] < 2)
								{
									proj.localAI[0] = newDelay;
								}
							}
							break;
						case 2:
						case 12:
							if (NPC.ai[1] > 180 + 20)
							{
								NPC.ai[1] = 999;
							}
							break;
						case 6:
							if (NPC.ai[1] > 60 + 10)
							{
								NPC.ai[1] = 999;
							}
							break;
						case 8:
						case 9:
							if (NPC.ai[1] < 40)
							{
								dashDir = Vector2.Zero;
							}
							else if (NPC.ai[1] < 89)
							{
								if(Main.expertMode){
								NPC.scale=1;
								}
								if (NPC.ai[1] == 40)
								{
									dashDir = Vector2.Normalize(player.Position - NPC.position);
								}
								var proj = Math.Max(0, Vector2.Dot(NPC.velocity, dashDir));
								var t = Math.Min(1, (NPC.ai[1] - 40) / (Main.expertMode?20f:30f));
								t = (float)Math.Pow(t, 4) * 250;
								t = Math.Max(proj, t);
								NPC.velocity = dashDir * t;
							}
							else{
								NPC.scale=0.5f;
							}
							break;
						case 5:
							if(NPC.ai[1]%3==0){
								SummonFastHoming(NPC.ai[1]/60f%1f);
							}
							if(NPC.ai[1]<300){
								NPC.ai[1]++;
								prevAI = NPC.ai[0];
								return false;
							}
							break;
					}
				}
			}
			prevAI = NPC.ai[0];
			return true;
		}
		Vector2 velocityReplace;
		int teleportTimer=0;
		public override void PostAI()
		{
			float[] attacks = { 8, 9 };
			if (!attacks.Contains(NPC.ai[0]))// && (NPC.velocity.Length() < 60 || velocityReplace.Length() > 16))
			{
				DoFirstStage(NPC.GetTargetData().Position);
				//prevent contact damage due to erratic AI (not my fault)
				if(NPC.velocity.Length()>5&&(NPC.GetTargetData().Position-NPC.position).Length()<80){
					Main.NewText(NPC.velocity.Length()+" "+NPC.damage);
					teleportTimer=20;
				}
				if(teleportTimer>0){
					teleportTimer--;
					NPC.damage=1;
				}
			}
			velocityReplace = NPC.velocity;
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			// Trophies are spawned with 1/10 chance
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.NovaFragment>(), 1, 60, 100));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.Fractal>(), 2));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.Unity>(), 2));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.EmpressOfLightIsGenuinelyEnraged(), ModContent.ItemType<Items.PlayerPoppet.PlayerVoodooPoppet>()));
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.EmpressOfLightIsGenuinelyEnraged(), ModContent.ItemType<Items.PeaceOffering>()));
		}
		/*
				public override void OnKill() {
					// This sets downedMinionBoss to true, and if it was false before, it initiates a lantern night
					//NPC.SetEventFlagCleared(ref DownedBossSystem.downedMinionBoss, -1);

					// Since this hook is only ran in singleplayer and serverside, we would have to sync it manually.
					// Thankfully, vanilla sends the MessageID.WorldData packet if a BOSS was killed automatically, shortly after this hook is ran

					// If your NPC is not a boss and you need to sync the world (which includes ModSystem, check DownedBossSystem), use this code:
					/*
					if (main.newtMode == NetmodeID.Server) {
						NetMessage.SendData(MessageID.WorldData);
					}

				}
		/*
				public override void BossLoot(ref string name, ref int potionType) {
					// Here you'd want to change the potion type that drops when the boss is defeated. Because this boss is early pre-hardmode, we keep it unchanged
					// (Lesser Healing Potion). If you wanted to change it, simply write "potionType = ItemID.HealingPotion;" or any other potion type
				}

				public override bool CanHitPlayer(Player target, ref int cooldownSlot) {
					cooldownSlot = ImmunityCooldownID.Bosses; // use the boss immunity cooldown counter, to prevent ignoring boss attacks by taking damage from other sources
					return true;
				}
		*/
		public override void FindFrame(int frameHeight)
		{
			// This NPC animates with a simple "go from start frame to final frame, and loop back to start frame" rule
			// In this case: First stage: 0-1-2-0-1-2, Second stage: 3-4-5-3-4-5, 5 being "total frame count - 1"
			int startFrame = 0;
			int finalFrame = 62;

			int frameSpeed = 2;
			NPC.frameCounter += 0.5f;
			NPC.frameCounter += NPC.velocity.Length() * NPC.velocity.Length() / 10f; // Make the counter go faster with more movement speed
			if (NPC.frameCounter > frameSpeed)
			{
				NPC.frameCounter = 0;
				NPC.frame.Y += frameHeight;

				if (NPC.frame.Y > finalFrame * frameHeight)
				{
					NPC.frame.Y = startFrame * frameHeight;
				}
			}
		}

		readonly int[] ownProjectles = { ProjectileID.HallowBossLastingRainbow, ProjectileID.FairyQueenLance, ProjectileID.HallowBossRainbowStreak, ProjectileID.FairyQueenSunDance };
		private void DoFirstStage(Vector2 target)
		{
			// Each time the timer is 0, pick a random position a fixed distance away from the player but towards the opposite side
			// The NPC moves directly towards it with fixed speed, while displaying its trajectory as a telegraph

			float speed = 15;
			float acceleration = 1.5f;
			float followDistance = 320;
			float teleportDistance = 900000;
			//NPC.DiscourageDespawn(1000);

			Vector2 fromPlayer = NPC.Center - target;

			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				// Important multiplayer concideration: drastic change in behavior (that is also decided by randomness) like this requires
				// to be executed on the server (or singleplayer) to keep the boss in sync
				if (NPC.ai[3] >= 2)
				{
					speed = 20;
					acceleration = 4;
				}
				if (fromPlayer.Length() > teleportDistance)
				{
					NPC.position = target + new Vector2(0, -320);
					//NPC.velocity = Vector2.Zero;
				}
				else if (fromPlayer.Length() > followDistance)
				{
					if (fromPlayer.Length() > followDistance * 2.2)
					{
						speed *= 1 + (fromPlayer.Length() - followDistance * 2.2f) / 100;
					}
					NPC.velocity = Vector2.Lerp(NPC.velocity, Vector2.Normalize(NPC.velocity * 0.9f + (target - NPC.position) * (speed * 0.1f + acceleration)) * speed, 0.5f);
				}
				else
				{
					NPC.velocity *= 0.9f;
				}
				NPC.netUpdate = true;
			}
		}
	};
}
