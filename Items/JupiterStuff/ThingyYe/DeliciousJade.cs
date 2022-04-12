using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using QwertysRandomContent;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.JupiterStuff.ThingyYe
{
    public class DeliciousJade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons an inaccurate gravity affected blade");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.knockBack = 5.5f;

            item.shoot = ModContent.ProjectileType<DeliciousJadeProj>();
            item.shootSpeed = 15f;

            item.width = 40;
            item.height = 54;
            item.scale = 1.2f;

            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = false;

            item.value = Item.sellPrice(gold: 28, silver: 15);
            item.rare = ItemRarityID.Pink;

            if (!Main.dedServ)
            {
                item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/JupiterStuff/ThingyYe/DeliciousJade_Glow");
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(45));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Vortex);
                dust.noGravity = true;
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/JupiterStuff/ThingyYe/DeliciousJade_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MythrilSword);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.SoulofSight, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 10);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class DeliciousJadeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;

            projectile.width = 40;
            projectile.height = 54;
            projectile.scale = 1.2f;

            projectile.alpha = 255;

            //projectile.GetAlpha(Color.White);

            projectile.ignoreWater = false;
            projectile.tileCollide = false;

            projectile.aiStyle = 2;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = height = 16;
            return true;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.position, 0.055f, 0.233f, 0.159f);

            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 110f)
            {
                projectile.alpha += 25;
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                }
            }
            else
            {
                projectile.alpha -= 25;
                if (projectile.alpha < 100)
                {
                    projectile.alpha = 100;
                }
            }

            if (projectile.ai[0] >= 120f)
            {
                projectile.Kill();
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.PaleTurquoise * (1f - (float)projectile.alpha / 255f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(Color.PaleTurquoise) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}