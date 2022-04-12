using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Items;

namespace ZensTweakstest.Items.NewZenStuff.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ZenitrinLegs : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Peace Keeper Legs");
			Tooltip.SetDefault("5% increased movement speed.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 8;
			item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 15);
			recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 10);
			recipe.AddTile(mod.TileType("ZCC_PLACED"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
