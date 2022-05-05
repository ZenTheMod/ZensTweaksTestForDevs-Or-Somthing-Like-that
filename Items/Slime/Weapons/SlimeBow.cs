using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Slime.Weapons
{
	public class SlimeBow : ModItem
	{
		public override void SetDefaults() 
		{
			item.damage = 6;
			item.ranged = true;
			item.knockBack = 2;
			item.noMelee = true;

			item.width = 24;
			item.height = 34;
			item.scale = 1f;

			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 18);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.useTurn = false;
			item.useAmmo = AmmoID.Arrow;
			item.shootSpeed = 8;
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
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