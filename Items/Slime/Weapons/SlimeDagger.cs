using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Slime.Weapons
{
	public class SlimeDagger : ModItem
	{
		public override void SetDefaults() 
		{
			item.damage = 11;
			item.melee = true;
			item.knockBack = 5;

			item.width = 24;
			item.height = 26;
			item.scale = 1.2f;

			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.Stabbing;
			item.value = Item.sellPrice(silver: 18);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Gel, 20);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}