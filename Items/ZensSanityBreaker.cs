using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using QwertysRandomContent;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZensTweakstest.Helper;

namespace ZensTweakstest.Items
{
    public class ZensSanityBreaker : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen's Mirror Blade");
            Tooltip.SetDefault("You've lost your sanity. And what do you end up with?");
        }
        Color[] cycleColors = new Color[]{
            new Color(245, 12, 59),
            new Color(13, 9, 51)
        };

        public override void SetDefaults()
        {
            item.damage = 420;
            item.knockBack = 7;
            item.melee = true;

            item.width = item.height = 64;
            item.scale = 1.5f;

            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;

            item.value = Item.sellPrice(gold: 38, silver: 40);
            item.rare = ItemRarityID.Red;
        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (!Main.dedServ)
			{
				float fade = Main.GameUpdateCount % 60 / 60f;
				int index = (int)(Main.GameUpdateCount / 60 % 2);

				item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/ZensMirrorBladeGlow");
				item.GetGlobalItem<ItemUseGlow>().glowColor = Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 2], fade);
			}
		}

		private float cooldown = 100;

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (target.life <= 0 && cooldown == 0)
			{
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/shatter"));
				Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<Shatter>(), default, 0);
				player.CameraShake(10, 10);
				cooldown = 100;

				for (int i = 0; i < 50; i++)
				{
					Vector2 speed = Main.rand.NextVector2Circular(10f, 10f);

					if (Main.rand.NextBool(1))
					{
						Dust dust = Dust.NewDustPerfect(target.Center + speed * 20, DustID.DiamondBolt, speed * -2, Scale: 3f);
						dust.noGravity = true;
					}
				}
			}
			else
			{
				target.AddBuff(ModContent.BuffType<MirrorBladeFlames>(), 60);

				for (int i = 0; i < 50; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

					if (Main.rand.NextBool(1))
					{
						Dust dust = Dust.NewDustPerfect(target.Center, DustID.LifeDrain, speed * 10, Scale: 1.5f);
						dust.noGravity = true;
					}

					if (Main.rand.NextBool(2))
					{
						Dust dust = Dust.NewDustPerfect(target.Center, DustID.Granite, speed * 10, Scale: 1.5f);
						dust.noGravity = true;
					}
				}

				if (crit && target.life > item.damage * 1.5f)
				{
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/nightBeam"));

					for (int k = 0; k < 3; k++)
					{
						Vector2 circleEdge = Main.rand.NextVector2CircularEdge(10f, 10f);
						Projectile.NewProjectile(target.Center + circleEdge * 20, circleEdge * -2, ModContent.ProjectileType<MirrorBeam>(), damage / 2, knockBack / 2, player.whoAmI);
					}
				}
			}
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			float fade = Main.GameUpdateCount % 60 / 60f;
			int index = (int)(Main.GameUpdateCount / 60 % 2);

			Texture2D texture = mod.GetTexture("Items/ZensMirrorBladeGlow");

			spriteBatch.Draw(texture, new Vector2(item.position.X - Main.screenPosition.X + item.width * 0.5f, item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f),
				new Rectangle(0, 0, texture.Width, texture.Height), Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 2], fade), rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			float fade = Main.GameUpdateCount % 60 / 60f;
			int index = (int)(Main.GameUpdateCount / 60 % 2);

			Texture2D texture = mod.GetTexture("Items/ZensMirrorBladeGlow");
			spriteBatch.Draw(texture, position, frame, Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 2], fade), 0f, origin, scale, SpriteEffects.None, 0f);
		}

		public override void UpdateInventory(Player player)
		{
			cooldown--;
			if (cooldown <= 0f)
			{
				cooldown = 0f;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine line2 in tooltips)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					float fade = Main.GameUpdateCount % 60 / 60f;
					int index = (int)(Main.GameUpdateCount / 60 % 2);
					line2.overrideColor = Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 2], fade);
				}
			}
		}
	}
	public class MirrorBeam : ModProjectile
	{
		Color[] cycleColors = new Color[]{
			new Color(245, 12, 59, 100),
			new Color(13, 9, 51, 100)
		};

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.penetrate = -1;
			projectile.ranged = true;

			projectile.friendly = true;
			projectile.hostile = false;

			projectile.width = projectile.height = 40;
			projectile.scale = 1f;

			projectile.alpha = 50;

			projectile.ignoreWater = true;
			projectile.tileCollide = false;

			projectile.aiStyle = 0;
			projectile.timeLeft = 20;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;

			if (Main.rand.NextBool(1))
			{
				Dust dust;
				dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.LifeDrain, projectile.velocity.X / 2, projectile.velocity.Y / 2, 0, default, 1.5f);
				dust.noGravity = true;
			}
			if (Main.rand.NextBool(2))
			{
				Dust dust;
				dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Granite, projectile.velocity.X / 2, projectile.velocity.Y / 2, 0, default, 1.5f);
				dust.noGravity = true;
			}

			if (projectile.timeLeft == 20)
			{
				for (int i = 0; i < 50; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 0.5f);

					if (Main.rand.NextBool(1))
					{
						Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.LifeDrain, speed.RotatedBy(projectile.velocity.ToRotation() + MathHelper.PiOver2) * 5, Scale: 1.5f);
						dust.noGravity = true;
					}
					if (Main.rand.NextBool(2))
					{
						Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.Granite, speed.RotatedBy(projectile.velocity.ToRotation() + MathHelper.PiOver2) * 5, Scale: 1.5f);
						dust.noGravity = true;
					}
				}
			}
		}

		float fade = Main.GameUpdateCount % 60 / 60f;
		int index = (int)(Main.GameUpdateCount / 60 % 2);

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 2], fade);
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 50; i++)
			{
				Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 0.5f);

				if (Main.rand.NextBool(1))
				{
					Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.LifeDrain, speed.RotatedBy(projectile.velocity.ToRotation() + MathHelper.PiOver2) * 5, Scale: 1.5f);
					dust.noGravity = true;
				}
				if (Main.rand.NextBool(2))
				{
					Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.Granite, speed.RotatedBy(projectile.velocity.ToRotation() + MathHelper.PiOver2) * 5, Scale: 1.5f);
					dust.noGravity = true;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);

			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}

			return true;
		}
	}

	public class MirrorBladeFlames : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = "ZensTweakstest/Items/buffs/BuffTemplate";
			return base.Autoload(ref name, ref texture);
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<MirrorBladeNPC>().hasBuff = true;
		}
	}

	public class MirrorBladeNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public bool hasBuff;

		public override void ResetEffects(NPC npc)
		{
			hasBuff = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (hasBuff)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}

				for (int i = 0; i < 60; i++)
				{
					npc.lifeRegen -= 2;
				}
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (hasBuff)
			{
				Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.PortalBoltTrail, 0, Main.rand.NextFloat(-0.1f, -3f), 0, Color.Red, 2f);
				dust.noGravity = true;
			}
		}

		public override Color? GetAlpha(NPC npc, Color drawColor)
		{
			if (hasBuff)
			{
				return new Color(245, 12, 59, 50);
			}
			return drawColor;
		}
	}
	public class Shatter : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_0";
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 220;

			projectile.friendly = true;
			projectile.hostile = false;

			projectile.ignoreWater = true;
			projectile.tileCollide = false;

			projectile.aiStyle = 0;
		}

		private bool runOnce = true;

		public override void AI()
		{
			if (runOnce)
			{
				projectile.rotation = Main.rand.NextFloat(0f, 360f);
				runOnce = false;
			}
		}

		private float alphaTimer = 1;

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			projectile.ai[0]++;
			if (projectile.ai[0] >= 60)
			{
				projectile.ai[0] = 60;
				alphaTimer -= 0.01f;
			}

			Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/Shatter");

			Vector2 drawOrigin = texture.Size() / 2;
			Vector2 drawPos = projectile.Center - Main.screenPosition;

			Main.spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, drawPos, null, Color.White * alphaTimer, projectile.rotation, drawOrigin, 2f, SpriteEffects.None, 0f);

			Main.spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}
}
