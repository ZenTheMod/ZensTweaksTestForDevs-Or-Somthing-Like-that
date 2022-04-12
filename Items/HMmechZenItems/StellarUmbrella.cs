using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ZensTweakstest.Helper;

namespace ZensTweakstest.Items.HMmechZenItems
{
    public class StellarUmbrella : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a star rocket");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 75;
            item.knockBack = 7;
            item.magic = true;
            item.noMelee = true;

            item.shoot = ModContent.ProjectileType<StarRocket>();
            item.shootSpeed = 7f;

            item.width = 22;
            item.height = 64;
            item.scale = 1.5f;

            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item92;
            item.autoReuse = true;
            item.useTurn = false;

            item.value = Item.sellPrice(gold: 12, silver: 88);
            item.rare = 4;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                float scale = 1f - (Main.rand.NextFloat() * 0.9f) + 0.5f;
                perturbedSpeed *= scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<StarBlast>(), damage, knockBack, player.whoAmI);
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<StarRocket>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    public class StarRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.penetrate = 1;
            projectile.magic = true;

            projectile.width = 30;
            projectile.height = 30;
            projectile.scale = 1.5f;
            drawOffsetX = 10;
            drawOriginOffsetY = 7;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.ignoreWater = false;
            projectile.tileCollide = true;

            projectile.aiStyle = 0;
            projectile.timeLeft = 300;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            projectile.velocity *= new Vector2(1.05f, 1.05f);
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

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(10f, 10f);
                Projectile.NewProjectile(projectile.Center, speed, ModContent.ProjectileType<StarBlast>(), projectile.damage, projectile.knockBack, player.whoAmI);
            }

            Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<Starsplosion>(), projectile.damage, projectile.knockBack, player.whoAmI);
            player.CameraShake(2, 30);
            Main.PlaySound(SoundID.Item74, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }

    public class StarBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.penetrate = 1;
            projectile.magic = true;

            projectile.width = 14;
            projectile.height = 14;
            projectile.scale = 1f;

            projectile.friendly = false;
            projectile.hostile = false;

            projectile.ignoreWater = false;
            projectile.tileCollide = false;

            projectile.aiStyle = 0;
            projectile.timeLeft = 100 + Main.rand.Next(50);
        }

        private bool randomizeAI = true;
        private bool useAltAI;

        public override void AI()
        {
            projectile.rotation += projectile.velocity.X + projectile.velocity.Y;
            projectile.velocity *= new Vector2(0.97f, 0.97f);

            if (useAltAI == false)
            {
                projectile.frame = 0;
                if (Main.rand.NextBool(100))
                {
                    Vector2 speed = Main.rand.NextVector2Unit();
                    Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PortalBoltTrail, speed * 3, 0, new Color(255, 251, 105), 1.5f);
                    dust.noGravity = true;
                }
            }
            else
            {
                projectile.frame = 1;
                if (Main.rand.NextBool(100))
                {
                    Vector2 speed = Main.rand.NextVector2Unit();
                    Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PortalBoltTrail, speed * 3, 0, new Color(105, 255, 255), 1.5f);
                    dust.noGravity = true;
                }
            }

            if (randomizeAI)
            {
                useAltAI = Main.rand.NextBool(2);
                randomizeAI = false;
            }

            if (projectile.timeLeft < 100)
            {
                projectile.friendly = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 50);
        }

        public override void Kill(int timeLeft)
        {
            if (useAltAI == false)
            {
                for (int i = 0; i < 20; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(5f, 5f);
                    Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PortalBoltTrail, speed, 0, new Color(255, 251, 105), 1.5f);
                    dust.noGravity = true;
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(5f, 5f);
                    Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PortalBoltTrail, speed, 0, new Color(105, 255, 255), 1.5f);
                    dust.noGravity = true;
                }
            }
        }
    }

    public class Starsplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 7;
        }

        public override void SetDefaults()
        {

            projectile.damage = 250;
            projectile.knockBack = 10f;
            projectile.penetrate = -1;
            projectile.thrown = true;

            projectile.width = 250;
            projectile.height = 250;
            drawOffsetX = 200;
            drawOriginOffsetY = 200;
            projectile.scale = 2f;
            projectile.frameCounter = 5;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.aiStyle = 0;
            projectile.timeLeft = 50;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 7)
                {
                    projectile.frame = 0;
                    projectile.Kill();
                }
            }

            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(30f, 30f);
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.PortalBoltTrail, speed, 0, new Color(105, 255, 255), 1.5f);
                dust.noGravity = true;
            }
        }
    }
}
