using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using ZensTweakstest.Items;
using ZensTweakstest.Items.Tiles;

namespace ZensTweakstest.Items
{
    public class electronicbox : ModItem
    {

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Electronic Box");
        }

        public override void SetDefaults()
        {
			item.width = 30;
			item.height = 30;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.rare = ItemRarityID.Blue;
			item.createTile = ModContent.TileType<electronsbox>();
			item.placeStyle = 0;
		}

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<electrons>(), 30);
			recipe.AddIngredient(ModContent.ItemType<steal>(), 10);
			recipe.AddIngredient(ItemID.Cog, 5);
			recipe.AddIngredient(ItemID.Wire, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}