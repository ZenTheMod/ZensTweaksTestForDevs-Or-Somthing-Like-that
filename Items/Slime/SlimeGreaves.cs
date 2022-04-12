using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Slime
{
    [AutoloadEquip(EquipType.Legs)]
    public class SlimeGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("4% Increased movement speed");
		}

		public override void SetDefaults()
		{
			item.defense = 4;

			item.width = 22;
			item.height = 18;

			item.value = Item.sellPrice(silver: 18);
			item.rare = ItemRarityID.Blue;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxRunSpeed += 0.4f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Gel, 30);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
