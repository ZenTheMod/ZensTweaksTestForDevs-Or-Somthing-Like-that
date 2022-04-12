using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Slime.Weapons
{
	public class SlimeStaff : ModItem
	{
        public override void SetStaticDefaults()
        {
			Item.staff[item.type] = true;
        }
        public override void SetDefaults() 
		{
			item.damage = 12;
			item.magic = true;
			item.knockBack = 4;
			item.noMelee = true;

			item.width = 34;
			item.height = 34;
			item.scale = 1f;

			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 18);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.useTurn = false;

			item.shoot = ModContent.ProjectileType<Projectiles.Slimeball>();
			item.shootSpeed = 5;
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