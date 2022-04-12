using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Tiles;

namespace ZensTweakstest.Items
{
    public class erep : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earth Replicator");
			Tooltip.SetDefault("Used for speacial crafting");
		}

		public override void SetDefaults()
		{
			item.width = 88;
			item.height = 60;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.rare = ItemRarityID.Blue;
			item.createTile = ModContent.TileType<earthrep>();
			item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 30);
			recipe.AddIngredient(ItemID.GrassSeeds, 10);
			recipe.AddIngredient(ModContent.ItemType<electrons>(), 5);
			recipe.AddTile(TileID.HeavyWorkBench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
