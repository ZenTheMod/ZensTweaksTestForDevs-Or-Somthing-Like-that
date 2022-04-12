using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    [AutoloadEquip(EquipType.Wings)]
    public class ZenSpreadWings : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fly high but dont die!");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 36;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
		}
		//these wings use the same values as the solar wings
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 160;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 9f;
			acceleration *= 2.3f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(),100);
			recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 20);
			recipe.AddTile(ModContent.TileType<Bosses.SparkArmor.ZCC_PLACED>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
