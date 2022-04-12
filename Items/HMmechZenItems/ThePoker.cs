using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.HMmechZenItems
{
    public class ThePoker : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Spawns beams around the enemy on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 75;
            item.knockBack = 3;
            item.melee = true;
            item.noMelee = true;

            item.shoot = ModContent.ProjectileType<ThePokerPro>();
            item.shootSpeed = 7f;

            item.width = 38;
            item.height = 38;
            item.scale = 1.5f;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = false;
            item.noUseGraphic = true;

            item.value = Item.sellPrice(gold: 12, silver: 88);
            item.rare = 4;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }

    public class ThePokerPro : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Poker");
        }

        public override void SetDefaults()
        {
            projectile.penetrate = -1;
            projectile.thrown = true;

            projectile.width = 30;
            projectile.height = 30;
            projectile.scale = 1.5f;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.aiStyle = 19;
        }

        public float MovementFactor
        {
            get => projectile.ai[0];
            set => projectile.ai[0] = value;
        }

        public override void AI()
        {
            Player projOwner = Main.player[projectile.owner];

            #region Spear AI
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);

            projectile.direction = projOwner.direction;

            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;

            projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
            projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);

            if (!projOwner.frozen)
            {
                if (MovementFactor == 0f)
                {
                    MovementFactor = 3f;
                    projectile.netUpdate = true;
                }

                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
                {
                    MovementFactor -= 2.4f;
                }
                else
                {
                    MovementFactor += 2.1f;
                }
            }

            projectile.position += projectile.velocity * MovementFactor;

            if (projOwner.itemAnimation == 0)
            {
                projectile.Kill();
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);

            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= MathHelper.ToRadians(90f);
            }
            #endregion

            #region Dust
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
            #endregion
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Vector2 circleEdge = Main.rand.NextVector2CircularEdge(10f, 10f);
            Projectile.NewProjectile(target.Center + circleEdge * 16, -circleEdge * 3, ModContent.ProjectileType<PokerBeam>(), projectile.damage, projectile.knockBack, projectile.owner);
        }
    }

    public class PokerBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Poker");

            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.penetrate = -1;
            projectile.ranged = true;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.width = 30;
            projectile.height = 30;
            projectile.scale = 1f;

            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.aiStyle = 27;
            projectile.timeLeft = 10;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;

            #region Dust
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

            if (projectile.timeLeft < 10 == false)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 0.5f);

                    if (Main.rand.NextBool(1))
                    {
                        Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.LifeDrain, speed * 5, Scale: 1.5f);
                        dust.noGravity = true;
                    }
                    if (Main.rand.NextBool(2))
                    {
                        Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.Granite, speed * 5, Scale: 1.5f);
                        dust.noGravity = true;
                    }
                }
            }
            #endregion
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void Kill(int timeLeft)
        {
            #region Dust
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 0.5f);

                if (Main.rand.NextBool(1))
                {
                    Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.LifeDrain, speed * 5, Scale: 1.5f);
                    dust.noGravity = true;
                }
                if (Main.rand.NextBool(2))
                {
                    Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.Granite, speed * 5, Scale: 1.5f);
                    dust.noGravity = true;
                }
            }
            #endregion
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}
