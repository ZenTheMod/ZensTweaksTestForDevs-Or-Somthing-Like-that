using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.Tiles;

namespace ZensTweakstest.Items
{
    public class gem1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Diamond Gemstone");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.createTile = TileID.Diamond;
            item.placeStyle = 0;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Diamond, 2);
            recipe.AddIngredient(ItemID.StoneBlock, 1);
            recipe.AddTile(ModContent.TileType<Items.Tiles.earthrep>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
