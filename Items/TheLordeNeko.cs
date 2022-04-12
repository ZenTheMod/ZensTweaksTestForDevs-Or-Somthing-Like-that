using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items
{
	public class TheLordeSoul : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Soul of Purplight");
			Tooltip.SetDefault("Summon the one\n'wait what the hell is this'" +
				"\nDedicated to Neko.");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults() {
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<TheLordeNeko>();
			item.width = 16;
			item.height = 30;
			item.UseSound = SoundID.Item2;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = -12;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 5, 50, 0);
			item.buffType = ModContent.BuffType<TheLordeBuff>();
		}
		public override void UpdateInventory(Player player) {
			item.color = Main.DiscoColor;
		}
		public override void PostUpdate() {
			item.color = Main.DiscoColor;
			Lighting.AddLight(item.Center, item.color.ToVector3() * 0.55f * Main.essScale);
		}

		public override void UseStyle(Player player) {
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0) {
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
	public class TheLordeBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("The thing");
			Description.SetDefault("the hell is this.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<TheLordeNeko>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<TheLordeNeko>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
			if (player.controlDown && player.releaseDown) {
				if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15) {
					for (int j = 0; j < 1000; j++) {
						if (Main.projectile[j].active && Main.projectile[j].type == ModContent.ProjectileType<TheLordeNeko>() && Main.projectile[j].owner == player.whoAmI) {
							Projectile projectile = Main.projectile[j];
							if (projectile.localAI[0] == 1f) {projectile.localAI[0] = 0f;}
							else {
								projectile.localAI[0] = 1f;
								CombatText.NewText(projectile.getRect(),Main.DiscoColor,"Disco");
							}
						}
					}
				}
			}
		}
	}
	public class TheLordeNeko : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Neko the cat'nt");
			Main.projFrames[projectile.type] = 1;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.LightPet[projectile.type] = true;
		}

		public override void SetDefaults() {
			projectile.width = 30;
			projectile.height = 30;
			projectile.penetrate = -1;
			projectile.netImportant = true;
			projectile.timeLeft *= 5;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.scale = 0.8f;
			projectile.tileCollide = false;
		}
		public override void SendExtraAI(BinaryWriter writer) {
			writer.Write(projectile.localAI[0]);
		}

		public override void ReceiveExtraAI(BinaryReader reader) {
			projectile.localAI[0] = reader.ReadSingle();
		}

		public override void AI() {
			Player player = Main.player[projectile.owner];
			if (!player.active) {
				projectile.active = false;
				return;
			}
			if (player.dead) {
				projectile.Kill();
			}
			if (player.HasBuff(ModContent.BuffType<TheLordeBuff>())) {
				projectile.timeLeft = 2;
			}

			projectile.Center = Vector2.Lerp(projectile.Center,player.Center - new Vector2(0,player.height*1.1f),0.1f);
			projectile.spriteDirection = player.direction;

			projectile.ai[1]++;
			if (projectile.ai[1] > 240f) {
				string[] text = {
					"I am bored",
					"I am not a cat",
					"Funky",
					"Industrial Society",
					"Insert funny meme here",
					"Amogus",
					"Sus",
					"When the impostor is sus",
					"Terraria sucks",
					"Minecraft is 3D Terraria",
					"I have existensal crisis",
					"I cannot talk",
					"Women is bad",
					"I am a chad",
					"Stop playing terraria \nand do something productive bruh"
				};
				if (Main.rand.NextBool(20)) {
					CombatText.NewText(projectile.getRect(),projectile.localAI[0] == 1f ? Main.DiscoColor : Color.Magenta,Main.rand.Next(text));
				}
				projectile.ai[1] = 0f;
			}

			if (player.itemAnimation > 0 || player.velocity.X > 1f || player.velocity.X < -1f || player.velocity.Y > 1f || player.velocity.Y < -1f) {projectile.ai[0] = MathHelper.Lerp(projectile.ai[0],0,0.1f);}
			else {projectile.ai[0]++;}
			if (projectile.ai[0] > 600f) {
				int num = TargetNpc();
				if (num > -1) {
					NPC npc = Main.npc[num];
					int a = Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(npc.Center)*15f, ProjectileID.DeathLaser, (int)(10f*player.allDamage+1f), 1f, projectile.owner);
					Main.projectile[a].hostile = false;					
					Main.projectile[a].friendly = true;
					Main.projectile[a].tileCollide = false;
					CombatText.NewText(projectile.getRect(),Color.Pink,"Pew");
				}
				for (int i = 0; i < 25; i++) {
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
					Dust d = Dust.NewDustPerfect(projectile.Center, 182, speed * Main.rand.NextFloat(1f,5f), Scale: 1.5f);
					d.noGravity = true;
				}
				projectile.ai[0] = 0f;
			}
		}
		private int TargetNpc() {
			int b = -1;
			float distanceFromTarget = 700f;
			Vector2 targetCenter = projectile.position;
			for (int i = 0; i < Main.maxNPCs; i++) {
				NPC npc = Main.npc[i];
				if (npc.CanBeChasedBy()) {
					float between = Vector2.Distance(npc.Center, projectile.Center);
					bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
					bool inRange = between < distanceFromTarget;
					if ((closest && inRange) || b == -1) {b = i;}
				}
			}
			return b;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) {

			SpriteEffects effect = SpriteEffects.None;
			if (projectile.spriteDirection == 1) {
				effect = SpriteEffects.FlipHorizontally;
			}

			Texture2D GlowTexture = ModContent.GetTexture(Texture+"Glow");
			Texture2D Eye = ModContent.GetTexture(Texture+"_Eye");
			Texture2D texture = ModContent.GetTexture(Texture);

			Vector2 pos = projectile.Center - Main.screenPosition;

			spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

            spriteBatch.Draw(GlowTexture, pos, null, projectile.localAI[0] == 1f ? Main.DiscoColor : Color.White, projectile.rotation, GlowTexture.Size() * 0.5f, projectile.scale, effect, 0f);

			spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

			spriteBatch.Draw(texture, pos, null, projectile.localAI[0] == 1f ? Main.DiscoColor : Color.White, projectile.rotation, texture.Size()/2, projectile.scale, effect, 0f);

			spriteBatch.End();{
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
			for (int i = 0; i < 3; i++)
			{
				spriteBatch.Draw(Eye, pos, null, Color.White*(projectile.ai[0]/600f), projectile.rotation, Eye.Size()/2, projectile.scale, effect, 0f);	
			}

			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);}
			return false;
		}
	}
}