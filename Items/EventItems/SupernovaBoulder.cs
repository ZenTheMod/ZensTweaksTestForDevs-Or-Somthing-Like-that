using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Helper;

namespace ZensTweakstest.Items.EventItems
{
	public class SupernovaBoulder : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.penetrate = -1;
			projectile.melee = true;

			projectile.width = projectile.height = 75;
			projectile.scale = 1.2f;

			projectile.friendly = true;
			projectile.hostile = false;

			projectile.ignoreWater = false;
			projectile.tileCollide = true;

			projectile.aiStyle = 0;
		}

		public override void AI()
		{
			projectile.rotation += 0.04f;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = height = 60;
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Daybreak, 360);
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];

			player.CameraShake(10, 10);
			Main.PlaySound(SoundID.Item62, projectile.Center);

			for (int i = 0; i < Main.rand.Next(3, 8); i++)
			{
				Projectile.NewProjectile(projectile.Center, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-10, -5)), ModContent.ProjectileType<SupernovaRock>(), projectile.damage, projectile.knockBack);
			}

			for (int j = 0; j < 50; j++)
			{
				Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);

				if (Main.rand.NextBool(1))
				{
					Dust.NewDustPerfect(projectile.Center, DustID.Silver, speed * 10, 0, Color.DarkRed, 2f);
				}

				if (Main.rand.NextBool(2))
				{
					Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.SolarFlare, speed * 10, 0, default, 1.2f);
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
				Color color = new Color(255, Main.DiscoG, 0) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/EventItems/SupernovaBoulderTrail"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}

			return true;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			Vector2 drawPos = projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
			spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/EventItems/SupernovaBoulderGlow"), drawPos, null, new Color(255, Main.DiscoG, 0), projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
		}
	}
}
