using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.EventItems
{
	public class SupernovaComet : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.penetrate = 1;
			projectile.melee = true;

			projectile.width = projectile.height = 26;
			projectile.scale = 1.2f;
			drawOffsetX = 4;
			drawOriginOffsetY = 3;

			projectile.friendly = true;
			projectile.hostile = false;

			projectile.ignoreWater = false;
			projectile.tileCollide = true;

			projectile.aiStyle = 0;
		}

		public override void AI()
		{
			projectile.rotation += 0.4f;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 360);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item118, projectile.Center);

			for (int d = 0; d < 50; d++)
			{
				Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
				Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PortalBoltTrail, speed * 10, 0, Color.Blue, 1.5f);
				dust.noGravity = true;
			}
			for (int g = 0; g < 5; g++)
			{
				Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
				Gore.NewGorePerfect(projectile.Center, speed * 5, 16);
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/EventItems/SupernovaCometTrail");

			Vector2 drawOrigin = texture.Size() / 2;
			Vector2 drawPos = projectile.Center - Main.screenPosition;

			Main.spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, drawPos, null, Color.White, projectile.velocity.ToRotation() + MathHelper.PiOver2, drawOrigin - new Vector2(0, 22), 1.2f, SpriteEffects.None, 0f);

			Main.spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			return true;
		}
	}
}
