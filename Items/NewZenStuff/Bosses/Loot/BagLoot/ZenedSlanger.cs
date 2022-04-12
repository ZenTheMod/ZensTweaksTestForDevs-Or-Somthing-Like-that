using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Config;
using ZensTweakstest.Items.JupiterStuff;
using ZensTweakstest.Items.NewZenStuff.Items;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
    public class ZenedSlanger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shimazu Masamune");
            Tooltip.SetDefault("Peirces 2 times!");  //The (English) text shown below your weapon's name
        }

        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;
        public override void SetDefaults()
        {
            item.damage = 65; // The damage your item deals
            item.melee = true; // Whether your item is part of the melee class
            item.width = 42; // The item texture's width
            item.height = 78; // The item texture's height
            item.useTime = 20; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            item.useAnimation = 20; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            item.knockBack = 6; // The force of knockback of the weapon. Maximum is 20
            item.value = Item.buyPrice(gold: 2); // The value of the weapon in copper coins
            item.rare = ItemRarityID.Orange; // The rarity of the weapon, from -1 to 13. You can also use ItemRarityID.TheColorRarity
            item.UseSound = SoundID.DD2_SonicBoomBladeSlash; // The sound when the weapon is being used
            item.shoot = ModContent.ProjectileType<ZenSlash>();
            item.shootSpeed = 12;
            item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button
            item.crit = 6; // The critical strike chance the weapon has. The player, by default, has 4 critical strike chance
            item.useStyle = ItemUseStyleID.SwingThrow; // 1 is the useStyle
        }
        public override void AddRecipes()
        {
            ModRecipe ppdr = new ModRecipe(mod);
            ppdr.AddIngredient(ModContent.ItemType<ZenStone_I>(), 40);
            ppdr.AddIngredient(ModContent.ItemType<CursedBone>(), 10);
            ppdr.AddTile(TileID.DemonAltar);
            ppdr.SetResult(this, 1);
            ppdr.AddRecipe();
        }
    }
    public class ZenSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zened Slash");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            
            projectile.penetrate = 2;
            projectile.CloneDefaults(ProjectileID.DD2SquireSonicBoom);
            projectile.width = 80;
            projectile.height = 23;
            aiType = ProjectileID.DD2SquireSonicBoom;
        }
        public override bool PreKill(int timeLeft)
        {
            projectile.type = ProjectileID.DD2SquireSonicBoom;
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            
            Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/NewZenStuff/Bosses/Loot/BagLoot/ZenSlashEffect2");
            Vector2 position = projectile.position - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height - texture.Height * 0.5f + 2f);
            // We redraw the item's sprite 4 times, each time shifted 2 pixels on each direction, using Main.DiscoColor to give it the color changing effect
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position + offsetPositon, null, Color.IndianRed, projectile.rotation, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }
            // Return true so the original sprite is drawn right after
            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < (50); i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<ZenStoneFlameDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }
    }
}
