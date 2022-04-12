using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Slime.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.Slime.Projectiles
{
	public class Slimer : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 5f;
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 200f;
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 13f;
		}

		public override void SetDefaults()
		{
			projectile.extraUpdates = 0;
			projectile.melee = true;

			projectile.width = 12;
			projectile.height = 12;
			projectile.scale = 1f;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.ignoreWater = false;

			projectile.aiStyle = 99;
			projectile.scale = 1f;
			projectile.ownerHitCheck = true;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, Main.projectileTexture[projectile.type].Frame(1, Main.projFrames[projectile.type], 0, projectile.frame), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
        public override void PostAI()
        {
			Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<SlimeDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			if (Main.rand.Next(0, 20) == 3)
            {
				for (int i = 0; i < 15; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
					Dust d = Dust.NewDustPerfect(projectile.position, ModContent.DustType<SlimeDust>(), speed * 5, Scale: 1f);
					d.noGravity = true;
				}
            }
		}
    }
}
