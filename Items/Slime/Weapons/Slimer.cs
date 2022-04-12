using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Slime.Weapons
{
	public class Slimer : ModItem
	{
		public override void SetDefaults() 
		{
			item.damage = 11;
			item.melee = true;
			item.knockBack = 6;
			item.noMelee = true;

			item.width = 30;
			item.height = 24;
			item.scale = 1f;

			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(silver: 18);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = false;
			item.noUseGraphic = true;
			item.channel = true;

			item.shoot = ModContent.ProjectileType<Projectiles.Slimer>();
			item.shootSpeed = 7;
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