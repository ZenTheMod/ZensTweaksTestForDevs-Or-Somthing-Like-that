using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Slime.Dusts;

namespace ZensTweakstest.Items.Slime.Projectiles
{
	public class SlimeArrow : ModProjectile
	{
		public override void SetDefaults() 
		{
			projectile.ranged = true;

			projectile.width = 14;
			projectile.height = 32;
			projectile.scale = 1f;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 250;
			projectile.ignoreWater = false;

			projectile.aiStyle = 1;
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