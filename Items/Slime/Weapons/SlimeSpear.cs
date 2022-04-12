using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Slime.Weapons
{
	public class SlimeSpear : ModItem
	{
		public override void SetDefaults() 
		{
			item.damage = 10;
			item.melee = true;
			item.knockBack = 6;
			item.noMelee = true;

			item.width = 42;
			item.height = 42;
			item.scale = 1f;

			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 18);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = false;
			item.noUseGraphic = true;

			item.shoot = ModContent.ProjectileType<Projectiles.SlimeSpear>();
			item.shootSpeed = 3;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1;
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