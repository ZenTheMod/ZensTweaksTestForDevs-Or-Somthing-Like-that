using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.EventItems
{
	public class SupernovaRock : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.penetrate = 1;
			projectile.melee = true;

			projectile.width = projectile.height = 22;

			projectile.friendly = true;
			projectile.hostile = false;

			projectile.ignoreWater = false;
			projectile.tileCollide = true;

			projectile.aiStyle = 2;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = height = 8;
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            if (Main.rand.NextBool(4))
			{
				target.AddBuff(BuffID.Daybreak, 360);
			}
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];

			Main.PlaySound(SoundID.Item62, projectile.Center);

			for (int j = 0; j < 10; j++)
			{
				Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
				if (Main.rand.NextBool(1))
				{
					Dust.NewDustPerfect(projectile.Center, DustID.Silver, speed * 2, 0, Color.DarkRed, 1.2f);
				}

				if (Main.rand.NextBool(2))
				{
					Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.SolarFlare, speed * 2, 0, default, 1f);
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
				Color color = lightColor * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}