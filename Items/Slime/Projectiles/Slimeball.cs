using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Slime.Dusts;

namespace ZensTweakstest.Items.Slime.Projectiles
{
	public class Slimeball : ModProjectile
	{
		public override void SetDefaults() 
		{
			projectile.magic = true;

			projectile.width = 16;
			projectile.height = 16;
			projectile.scale = 1f;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 3;
			projectile.timeLeft = 600;
			projectile.ignoreWater = false;

			aiType = ProjectileID.Bullet;
		}

        public override void AI()
        {
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

			Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, ModContent.DustType<Dusts.SlimeDust>());
			dust.noGravity = true;
		}
		public override void PostAI()
		{
			float strength = 2f;
			Lighting.AddLight(projectile.position, Color.Green.ToVector3() * strength);
			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<SlimeDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Slimed, 180);
		}
		public override void Kill(int timeLeft)
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