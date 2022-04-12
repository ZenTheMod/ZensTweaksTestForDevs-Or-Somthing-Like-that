using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ZensTweakstest.Config;
using ZensTweakstest.Helper;
using QwertysRandomContent;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.HMmechZenItems
{
    public class ZenicFlamethrower : ModItem
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Conflagration");
			Tooltip.SetDefault("Ignores a substancial amount enemy iFrames"
				+ "'\nBurn the world down!'");
		}
		public override void SetDefaults()
		{
			item.damage = 20;
			item.knockBack = 4;
			item.crit = 3;
			item.ranged = true;
			item.noMelee = true;

			item.shoot = ModContent.ProjectileType<ConflagrationFlames>();
			item.shootSpeed = 16f;
			item.useAmmo = AmmoID.Gel;

			item.width = 72;
			item.height = 22;
			item.scale = 1.2f;

			item.useTime = 5;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.UseSound = SoundID.Item34;
			item.autoReuse = true;
			item.useTurn = false;

			item.value = Item.sellPrice(silver: 12);
			item.rare = 4;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12f, 0f);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
	}
    public class ConflagrationFlames : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conflagration");
            Main.projFrames[projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            projectile.penetrate = -1;
            projectile.ranged = true;

            projectile.width = 98;
            projectile.height = 98;
            projectile.frameCounter = 5;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.ignoreWater = false;
            projectile.tileCollide = false;

            projectile.aiStyle = 0;
            projectile.timeLeft = 100;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(1))
            {
                Dust dust;
                dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.LifeDrain, projectile.velocity.X / 2, projectile.velocity.Y / 2, 0, default, 1.5f);
                dust.noGravity = true;
            }
            if (Main.rand.NextBool(2))
            {
                Dust dust;
                dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Granite, projectile.velocity.X / 2, projectile.velocity.Y / 2, 0, default, 1.5f);
                dust.noGravity = true;
            }

            if (projectile.wet)
            {
                projectile.Kill();
            }

            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 30f)
            {
                projectile.ai[0] = 30f;
                projectile.netUpdate = true;

                projectile.velocity = projectile.velocity * 0.8f;
            }

            if (++projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 7)
                {
                    projectile.frame = 0;
                    projectile.Kill();
                }
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = height = 8;
            return true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 90);
        }
    }

    public class EndlessGel : ModItem
    {
        public override void SetDefaults()
        {
            item.ranged = true;
            item.ammo = AmmoID.Gel;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Gel, 3996);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
