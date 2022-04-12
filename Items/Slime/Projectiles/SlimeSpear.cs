using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ZensTweakstest.Items.Slime.Dusts;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.Slime.Projectiles
{
	public class SlimeSpear : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.melee = true;

			projectile.width = 60;
			projectile.height = 60;
			projectile.scale = 1.3f;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = -1;

			projectile.hide = true;
			projectile.ownerHitCheck = true;
			projectile.tileCollide = false;
		}

		public float movementFactor
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		public override void AI()
		{
			Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<SlimeDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			Player projOwner = Main.player[projectile.owner];

			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			projectile.direction = projOwner.direction;
			projOwner.heldProj = projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
			projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);

			if (!projOwner.frozen) {
				if (movementFactor == 0f)
				{
					movementFactor = 3f;
					projectile.netUpdate = true;
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
				{
					movementFactor -= 2.4f;
				}
				else
				{
					movementFactor += 2.1f;
				}
			}

			projectile.position += projectile.velocity * movementFactor;

			if (projOwner.itemAnimation == 0)
			{
				projectile.Kill();
			}

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);

			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= MathHelper.ToRadians(90f);
			}
		}
	}
}
