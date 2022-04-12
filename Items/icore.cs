using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Text;
using System.Threading.Tasks;
using QwertysRandomContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items
{
    public class icore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Spire");
            Tooltip.SetDefault("Great you cloged the toilet without the paper!");
        }

        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;
            item.shoot = ProjectileID.IchorSplash;
            item.shootSpeed = 5f;
            item.rare = ItemRarityID.LightRed;
            item.value = Item.buyPrice(gold: 1);
            item.value = Item.sellPrice(silver: 50);
            item.melee = true;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTime = 36;
            item.useAnimation = 20;
            item.useStyle = 1;

            item.damage = 70;
            item.knockBack = 1.5f;
            if (!Main.dedServ)
            {
                item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/icore_Glow");
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/icore_Glow");
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
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Ichor);
        }
        public override void AddRecipes()
        {
            ModRecipe DUMM = new ModRecipe(mod);

            DUMM.AddIngredient(ItemID.CrimstoneBlock, 30);
            DUMM.AddIngredient(ItemID.Ichor, 10);
            DUMM.AddTile(TileID.DemonAltar);
            DUMM.SetResult(this);
            DUMM.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 60);
        }
    }
    public class CursedTizona : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 44;
            item.shoot = ProjectileID.CursedFlameFriendly;
            item.shootSpeed = 10f;
            item.rare = ItemRarityID.LightRed;
            item.value = Item.buyPrice(gold: 1);
            item.value = Item.sellPrice(silver: 50);
            item.melee = true;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTime = 36;
            item.useAnimation = 20;
            item.useStyle = 1;

            item.damage = 70;
            item.knockBack = 1.5f;
            if (!Main.dedServ)
            {
                item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/CursedTizona_Glow");
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/CursedTizona_Glow");
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
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CursedTorch);
        }
        public override void AddRecipes()
        {
            ModRecipe DUMM = new ModRecipe(mod);

            DUMM.AddIngredient(ItemID.EbonstoneBlock, 30);
            DUMM.AddIngredient(ItemID.CursedFlame, 10);
            DUMM.AddTile(TileID.DemonAltar);
            DUMM.SetResult(this);
            DUMM.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 60);
        }
    }
}
