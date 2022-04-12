using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Slime.Dusts;

namespace ZensTweakstest.Items.Slime.Projectiles
{
	public class SlimeSpike : ModProjectile
	{
		public override void SetDefaults() 
		{
			projectile.ranged = true;

			projectile.width = 8;
			projectile.height = 16;
			projectile.scale = 1f;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 200;
			projectile.ignoreWater = false;

			aiType = ProjectileID.WoodenArrowFriendly;
		}
        public override void PostAI()
        {
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<SlimeDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
		}
        public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 5; i++)
            {
				Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
				Dust d = Dust.NewDustPerfect(projectile.position, ModContent.DustType<SlimeDust>(), speed * 3, Scale: 1f);
				d.noGravity = true;
            }	
		}
    }
}