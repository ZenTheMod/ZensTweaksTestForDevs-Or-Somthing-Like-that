using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Slime.Dusts;

namespace ZensTweakstest.Items.Slime.Tools
{
	public class SlimePickaxe : ModItem
	{
		public override void SetDefaults() 
		{
			item.damage = 12;
			item.knockBack = 2;
			item.pick = 50;

			item.width = 30;
			item.height = 30;
			item.scale = 1.3f;

			item.useTime = 12;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
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

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			//Emit dusts when the sword is swung
			Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<SlimeDust>());
		}
	}
}