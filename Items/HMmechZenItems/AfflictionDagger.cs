using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.HMmechZenItems
{
    public class AfflictionDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Affliction");
            Tooltip.SetDefault("Explodes on contact"
                + "\nHealth the player on enemy hit");
        }

        public override void SetDefaults()
        {
			item.damage = 59;
            item.knockBack = 7;
            item.thrown = true;
			item.noMelee = true;

            item.shoot = ModContent.ProjectileType<AfflictionPro>();
			item.shootSpeed = 30f;

            item.width = 22;
			item.height = 64;
			item.scale = 0.75f;

            item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = false;
			item.noUseGraphic = true;

            item.value = Item.sellPrice(platinum: 1, silver: 25);
			item.rare = 4;
		}
	}

    public class AfflictionPro : ModProjectile
    {
        public override string Texture => "ZensTweakstest/Items/HMmechZenItems/AfflictionDagger";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Affliction");
        }

        public override void SetDefaults()
        {
            projectile.penetrate = 1;
            projectile.thrown = true;

            projectile.width = 32;
            projectile.height = 32;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;

            projectile.aiStyle = 0;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

            #region Dust
            if (Main.rand.NextBool(1))
            {
                Dust dust;
                dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.RainbowMk2, projectile.velocity.X / 2, projectile.velocity.Y / 2, 0, new Color(Main.DiscoR, 10, 240), 1.5f);
                dust.noGravity = true;
            }
            if (Main.rand.NextBool(2))
            {
                Dust dust;
                dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Granite, projectile.velocity.X / 2, projectile.velocity.Y / 2, 0, default, 1.5f);
                dust.noGravity = true;
            }
            #endregion

            #region Gravity
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 15f)
            {
                projectile.ai[0] = 15f;
                projectile.velocity.Y = projectile.velocity.Y + 2f;
            }
            if (projectile.velocity.Y > 20f)
            {
                projectile.velocity.Y = 20f;
            }
            #endregion
        }

        private readonly int healCount = Main.rand.Next(1, 7);

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.immortal && !target.SpawnedFromStatue)
            {
                Player player = Main.player[projectile.owner];
                player.statLife += healCount;
                player.HealEffect(healCount, true);
            }
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Main.PlaySound(SoundID.Item14, (int)projectile.position.X, (int)projectile.position.Y);

            const int NUM_DUSTS = 50;

            for (int i = 0; i < NUM_DUSTS; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(30f, 30f);
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.RainbowMk2, speed, 0, new Color(Main.DiscoR, 10, 240), 1.5f);
                dust.noGravity = true;
            }

            #region Explosion
            Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), mod.ProjectileType("AfflictionExplosion"), projectile.damage, projectile.knockBack, player.whoAmI);
            #endregion
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, 10, 240);
        }
    }

    public class AfflictionExplosion : ModProjectile
    {
        public override string Texture => "ZensTweakstest/Items/HMmechZenItems/AfflictionDagger";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Affliction");
        }

        public override void SetDefaults()
        {

            projectile.damage = 250;
            projectile.knockBack = 10f;
            projectile.penetrate = -1;
            projectile.thrown = true;

            projectile.width = 250;
            projectile.height = 250;
            projectile.alpha = 255;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.tileCollide = false;
            projectile.ignoreWater = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;

            projectile.aiStyle = 0;
            projectile.timeLeft = 3;
        }
    }
}