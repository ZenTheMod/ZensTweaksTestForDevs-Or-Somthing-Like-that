using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Graphics.Shaders;

namespace ZensTweakstest.Items.Weapons
{
    public class PrismaticStroke : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Brush Of Pain And punches" +
                "\nDedicated To Zen");
        }
        public override void SetDefaults()
        {
            item.width = 36;//change
            item.height = 54;//change
            item.value = Item.sellPrice(0, 15, 5, 5);
            item.magic = true;
            item.mana = 10;
            item.UseSound = SoundID.Item9;//92
            item.damage = 90;
            item.rare = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 5;
            item.useTime = 5;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<RainbowPunch>();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld + new Vector2(29, 10);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.IntenseRainbowDye), item, null); //use living rainbow dye shader
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
    }
    public class RainbowPunch : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.penetrate = -1;
            projectile.magic = true;

            projectile.width = 58;
            projectile.height = 40;
            projectile.scale = 1.5f;
            projectile.timeLeft = 30;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.aiStyle = -1;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Main.DiscoColor;
            return color;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center - new Vector2(49, 30), new Vector2(0, 0), ModContent.ProjectileType<PunchBlast>(), projectile.damage, 2, projectile.owner);
            for (int i = 0; i < 55; i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 212, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0, Main.DiscoColor);
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
        public override void AI()
        {
            projectile.scale -= 0.05f;
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 212, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0, Main.DiscoColor);
            if (++projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 2)
                {
                    projectile.frame = 0;
                }
            }
        }
    }
    public class PunchBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 54;
            projectile.scale = 1;

            projectile.alpha = 20;

            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor;
        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 3)
                {
                    projectile.Kill();
                }
            }
        }
    }
}
