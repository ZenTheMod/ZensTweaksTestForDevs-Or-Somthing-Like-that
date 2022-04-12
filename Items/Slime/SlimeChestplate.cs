using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Slime
{
    [AutoloadEquip(EquipType.Body)]
    public class SlimeChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("1.5% Increased damage and melee speed" 
				+ "\n1% Increased critical strike chance");
		}

		public override void SetDefaults()
		{
			item.defense = 5;

			item.width = 32;
			item.height = 20;

			item.value = Item.sellPrice(silver: 18);
			item.rare = ItemRarityID.Blue;
		}

		public override void UpdateEquip(Player player)
		{
			player.allDamage += 0.015f;
			player.meleeSpeed += 0.15f;

			player.meleeCrit += 1;
			player.rangedCrit += 1;
			player.magicCrit += 1;
			player.thrownCrit += 1;
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
