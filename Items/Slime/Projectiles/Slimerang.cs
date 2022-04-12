using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Slime.Dusts;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.Slime.Projectiles
{
	public class Slimerang : ModProjectile
	{
		public override void SetDefaults() 
		{
			projectile.thrown = true;

			projectile.width = 18;
			projectile.height = 32;
			projectile.scale = 1f;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
			projectile.ignoreWater = false;

			projectile.aiStyle = 3;
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