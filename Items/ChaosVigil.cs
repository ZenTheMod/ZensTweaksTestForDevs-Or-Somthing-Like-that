using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.Localization; 
using Terraria.Utilities;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.DataStructures;
using ZensTweakstest.Helper;

namespace ZensTweakstest.Items
{
	/*
	
	Ignore this ever existed
	I dont need this anymore

	public class DrawCache
	{
		public Color color;
		public Vector2 position;
		public float rotation;
		public float scale;
		public Rectangle? rectangle;
		public DrawCache(Vector2 pos,float rotation,float scale,Color color,Rectangle? rectangle = null) {
			this.position = pos;
			this.rotation = rotation;
            this.scale = scale;
			this.color = color;
			this.rectangle = rectangle;
		}
		public void Draw(Texture2D texture,float alpha = -1f,SpriteBatch spriteBatch = null) {
			if (spriteBatch == null) {spriteBatch = Main.spriteBatch;}
			position += texture.Size()/2;
			if (alpha > -1) {
				spriteBatch.Draw(texture, position, rectangle, color*alpha, rotation, texture.Size()/2, scale, SpriteEffects.None, 0f);
			}
			else {
				spriteBatch.Draw(texture, position, rectangle, color, rotation, texture.Size()/2, scale, SpriteEffects.None, 0f);
			}
		}
	}*/
	public class ChaosVigilBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Chaos Vigiled");
			Description.SetDefault("The chaotic portal is protecting you ?");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ChaosPortal>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}

	public class ChaosVigil : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Summon a chaotic portal to succ enemies soul"+
			"\nDeals more damage in the night"+"\n'Portal to hell'");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults() {
			item.damage = 30;
			item.knockBack = 3f;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 30, 0, 0);
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			item.noMelee = true;
			item.summon = true;
			item.buffType = ModContent.BuffType<ChaosVigilBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			item.shoot = ModContent.ProjectileType<ChaosPortal>();
		}
		public override void UpdateInventory(Player player) {
			item.color = Color.Lerp(item.color,Main.dayTime ? new Color(10,10,10) : Color.White ,0.1f);
		}
		public override void PostUpdate() {
			item.color = Color.Lerp(item.color,Main.dayTime ? new Color(10,10,10) : Color.White ,0.1f);
		}
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
			if (!Main.dayTime) {add += 5f;}
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
			return true;
		}
	}
	public class ChaosPortal : ModProjectile
	{
		//Copy pasted from example mod, i dont care

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Chaos Portal");
			// Sets the amount of frames this minion has on its spritesheet

			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

			// These below are needed for a minion
			// Denotes that this projectile is a pet or minion
			Main.projPet[projectile.type] = true;
			// This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			// Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public sealed override void SetDefaults() {
			projectile.width = 95;
			projectile.height = 95;
			// Makes the minion go through tiles freely
			projectile.tileCollide = false;

			// These below are needed for a minion weapon
			// Only controls if it deals damage to enemies on contact (more on that later)
			projectile.friendly = true;
			// Only determines the damage type
			projectile.minion = true;
			// Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
			projectile.minionSlots = 1f;
			// Needed so the minion doesn't despawn on collision with enemies or tiles
			projectile.penetrate = -1;
		}

		// Here you can decide if your minion breaks things like grass or pots
		public override bool? CanCutTiles() {
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage() {
			return true;
		}

		public override void AI() {
			Player player = Main.player[projectile.owner];

			#region Active check
			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			if (player.dead || !player.active) {
				player.ClearBuff(ModContent.BuffType<ChaosVigilBuff>());
			}
			if (player.HasBuff(ModContent.BuffType<ChaosVigilBuff>())) {
				projectile.timeLeft = 2;
			}
			#endregion

			#region General behavior
			Vector2 idlePosition = player.Center;
			idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

			// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
			// The index is projectile.minionPos
			float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

			// Teleport to player if distance is too big
			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();
			if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f) {
				// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
				// and then set netUpdate to true
				projectile.position = idlePosition;
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}

			// If your minion is flying, you want to do this independently of any conditions
			float overlapVelocity = 0.04f;
			for (int i = 0; i < Main.maxProjectiles; i++) {
				// Fix overlap with other minions
				Projectile other = Main.projectile[i];
				if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width) {
					if (projectile.position.X < other.position.X) projectile.velocity.X -= overlapVelocity;
					else projectile.velocity.X += overlapVelocity;

					if (projectile.position.Y < other.position.Y) projectile.velocity.Y -= overlapVelocity;
					else projectile.velocity.Y += overlapVelocity;
				}
			}
			#endregion

			#region Find target
			// Starting search distance
			float distanceFromTarget = 700f;
			int target;
			Vector2 targetCenter = projectile.position;
			foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (player.HasMinionAttackTargetNPC) {
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, projectile.Center);
				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 2000f) {
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}
			if (!foundTarget) {
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++) {
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy()) {
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall)) {
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			projectile.friendly = foundTarget;
			#endregion

			#region Movement

			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;
			if (foundTarget) {
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f) {
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - projectile.Center;
					direction.Normalize();
					direction *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
				}
			}
			else {
				// Minion doesn't have a target: return to player and idle
				if (distanceToIdlePosition > 600f) {
					// Speed up the minion if it's away from the player
					speed = 12f;
					inertia = 60f;
				}
				else {
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}
				if (distanceToIdlePosition > 20f) {
					// The immediate range around the player (when it passively floats about)

					// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (projectile.velocity == Vector2.Zero) {
					// If there is a case where it's not moving at all, give it a little "poke"
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}
			}
			#endregion

			//What i only did for the ai
			projectile.scale = MathHelper.Lerp(projectile.scale,1f + projectile.ai[0]/10f,0.1f);
			projectile.rotation += (projectile.ai[0]/50f)+0.01f;

			if (projectile.ai[0] > 10f) {projectile.ai[0] = 10f;}
			if (projectile.ai[0] < 0f) {projectile.ai[0] = 0f;}

			projectile.ai[1] += 1f;
			if (projectile.ai[1] > 60f) {
				projectile.ai[0] -= 1f;
				projectile.ai[1] = 50f;
			}

			if (Main.rand.NextBool(20))
			{
				Dust d = Main.dust[Dust.NewDust(projectile.Center + new Vector2(-10,10), 10, 10, Main.dayTime ? 240 : 182, 0, 0f, 0, color, 1f)];
				d.noGravity = true;
			}

			//set ai to targetcenter for drawing method
			Chain = targetCenter;
			color = Color.Lerp(color,Main.dayTime ? Color.Black : Color.White,0.1f);
			Lighting.AddLight(projectile.Center, (Main.dayTime ? Color.Black : Color.Red).ToVector3() * 0.78f);
		}

		//instances
		bool foundTarget;
		Vector2 Chain;
		Color color;

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
			projectile.ai[0] += 1f;
			projectile.ai[1] = 0f;
			damage += (int)projectile.ai[0]*4;

			Player player = Main.player[projectile.owner];

			//Succ enemies , has if statement to prevent things like worms glitch out
			if (target.realLife < 0){target.velocity += target.DirectionTo(projectile.Center)*2f;}

			//stat increase
			float thing = projectile.ai[0];
			player.endurance += thing/100f;
			player.lifeRegen += 1 + (int)thing/10;
			player.lifeRegenCount += 1 + (int)thing/10;
			player.lifeRegenTime += 1 + (int)thing/10;
			player.moveSpeed += thing/60f;
			player.meleeSpeed += thing/60f;

			//dust
			for (int i = 0; i < Main.rand.Next(30,41); i++) {
				Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
				Dust d = Dust.NewDustPerfect(target.Center, Main.dayTime ? 240 : 182, speed * Main.rand.Next(3,5), Scale: 1.5f);
				d.noGravity = true;
			}

			//life steal
			if (thing > 7f && Main.rand.NextBool(20) && player.statLife < player.statLifeMax2) {

				//randomize
				int ran = Main.rand.Next(5,11);
				player.statLife += ran;

				//secret mana heal
				if (player.statMana < player.statManaMax2) {
					player.statMana += ran;
				}

				//combat text
				CombatText.NewText(player.getRect(),Main.dayTime ? Color.Black : Color.Red ,ran);

				//dust again for player
				for (int i = 0; i < ran; i++) {
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
					speed += new Vector2(Main.rand.Next(-5,6),Main.rand.Next(-5,6));
					Dust d = Dust.NewDustPerfect(Main.player[projectile.owner].Center, Main.dayTime ? 240 : 182, speed * 2, Scale: 1f);
					d.noGravity = true;
				}
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {

			Texture2D asd = ModContent.GetTexture(Texture);

			//check if its not daytime for additive blending
			if (!Main.dayTime) {
				spriteBatch.End();
				//Immediate for shader
            	spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
			}

			//There is no shader that can make this look good
			//GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<NewNonZen.StrokeDye>()), projectile, null); //use dye

			spriteBatch.Draw(asd, projectile.Center - Main.screenPosition, null, color, projectile.rotation, asd.Size()/2, projectile.scale, SpriteEffects.None, 0f);

			//draw the thing on the player
			spriteBatch.Draw(asd, Main.player[projectile.owner].Center - Main.player[projectile.owner].velocity - Main.screenPosition, null, color*(projectile.ai[0] / 10f), projectile.rotation, asd.Size()/2, projectile.scale/2, SpriteEffects.None, 0f);

			//draw the chain
			if (foundTarget) {
				//originated from helpme extension
				spriteBatch.DrawChain(color,projectile.Center,Chain,Texture+"Chain",true);
			}

			//reset to alphablend
			if (!Main.dayTime) {
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
			}
			return false;
		}

		//unused hook
		//public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {}
		//public override Color? GetAlpha(Color lightColor) {return Color.White;}//Main.dayTime ? Color.BlackPurple : Color.White
	}
}