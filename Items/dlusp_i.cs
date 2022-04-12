using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Tiles;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items
{
    class dlusp_i : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daniel's Light-Up-Sneekers (Placeable)");
            Tooltip.SetDefault("The placer can be more like the mystical god Daniel");
        }
        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.rare = ItemRarityID.Expert;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 7;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Items.Tiles.dlusp>();
        }

        public override void AddRecipes()
        {
            ModRecipe A = new ModRecipe(mod);
            A.AddIngredient(ItemID.Gel, 20);
            A.AddIngredient(ModContent.ItemType<dlus>(), 1);
            A.AddTile(ModContent.TileType<Items.Tiles.teeth>());
            A.SetResult(this);
            A.AddRecipe();
        }
    }
}
