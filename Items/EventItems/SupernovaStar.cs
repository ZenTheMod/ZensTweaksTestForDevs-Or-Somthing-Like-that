using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace ZensTweakstest.Items.EventItems
{
	public class SupernovaStar : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.penetrate = 1;
			projectile.melee = true;

			projectile.width = projectile.height = 22;
			projectile.scale = 1f;

			projectile.friendly = true;
			projectile.hostile = false;

			projectile.ignoreWater = false;
			projectile.tileCollide = false;

			projectile.aiStyle = 0;
		}

		bool target = false;

		public override void AI()
		{
			projectile.rotation += 0.4f;

			projectile.rotation += projectile.velocity.ToRotation();
			if (projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}

			Vector2 move = Vector2.Zero;
			float distance = 400f;

			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].immortal && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
				{
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}

			if (target)
			{
				AdjustMagnitude(ref move);
				projectile.velocity = (16 * projectile.velocity + move) / 18f;
				AdjustMagnitude(ref projectile.velocity);
				projectile.timeLeft = 100;
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 60f)
			{
				vector *= 60f / magnitude;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 100);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item118, projectile.Center);

			for (int i = 0; i < 10; i++)
			{
				Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
				Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PortalBoltTrail, speed * 5, 0, Color.Yellow, 1.2f);
				dust.noGravity = true;
			}
		}
	}
}
