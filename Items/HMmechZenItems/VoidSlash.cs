using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.HMmechZenItems
{
    public class VoidSlash : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unleashes a flurry of void arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.knockBack = 4;
            item.crit = 3;
            item.ranged = true;
            item.noMelee = true;

            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 30f;
            item.useAmmo = AmmoID.Arrow;

            item.width = 32;
            item.height = 70;
            item.scale = 1.2f;

            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.useTurn = false;

            item.value = Item.sellPrice(gold: 12, silver: 88);
            item.rare = ItemRarityID.Yellow;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12f, 0f);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = ModContent.ProjectileType<VoidArrow>();

            Vector2 perturbedSpeed = new Vector2(speedX * 3, speedY * 3).RotatedByRandom(MathHelper.ToRadians(50));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
    }

    public class VoidArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.penetrate = 1;
            projectile.ranged = true;

            projectile.width = 16;
            projectile.height = 16;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.aiStyle = 0;
            projectile.timeLeft = 300;
        }

        bool target = false;

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }

            Vector2 move = Vector2.Zero;
            float distance = 400f;

            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
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
                projectile.velocity = (8 * projectile.velocity + move) / 9f;
                AdjustMagnitude(ref projectile.velocity);
            }

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
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 30f)
            {
                vector *= 30f / magnitude;
            }
        }
    }
}
