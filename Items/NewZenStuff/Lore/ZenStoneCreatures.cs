using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Items;
using Microsoft.Xna.Framework;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Lore
{
    public class ZenStoneCreatures : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen Stone Creatures");
            Tooltip.SetDefault("Creations of a once great God, Barely clinging on to life;" +
                "\nThese abominations hold celestial evils inside their once pure cores." +
                "\nThey've found safety down in the underworld feeding off its heat.");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.maxStack = 1;
            item.rare = ItemRarityID.Orange;
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 3;
        }

        public override bool GrabStyle(Player player)
        {
            Vector2 vectorItemToPlayer = player.Center - item.Center;
            Vector2 movement = -vectorItemToPlayer.SafeNormalize(default(Vector2)) * 0.1f;
            item.velocity = item.velocity + movement;
            item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
            return true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Red.ToVector3() * 0.75f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe POOP = new ModRecipe(mod);

            POOP.AddIngredient(ItemID.HellstoneBar, 1);
            POOP.AddIngredient(ModContent.ItemType<ZenitrinOre_I>(), 30);
            POOP.AddIngredient(ModContent.ItemType<ZenStone_I>(), 500);
            POOP.AddTile(TileID.MythrilAnvil);
            POOP.SetResult(this);
            POOP.AddRecipe();
        }
    }
}
