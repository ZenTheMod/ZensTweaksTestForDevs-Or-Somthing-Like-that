using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Slime.Weapons 
{
	public class SlimeArrow : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("'It's an arrow, but slime'");
        }

        public override void SetDefaults() 
		{
			item.damage = 3;
			item.ranged = true;

			item.width = 14;
			item.height = 32;

			item.value = Item.sellPrice(silver: 3);
			item.rare = ItemRarityID.Blue;
			item.ammo = AmmoID.Arrow;
			item.maxStack = 999;
			item.consumable = true;

			item.shoot = ModContent.ProjectileType<Projectiles.SlimeArrow>();
		}

        public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Gel, 4);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 20);
			recipe.AddRecipe();
		}
	}
}