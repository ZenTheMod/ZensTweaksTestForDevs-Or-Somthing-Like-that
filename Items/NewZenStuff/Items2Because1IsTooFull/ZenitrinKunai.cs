using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.NewZenStuff.Items;
using Terraria.DataStructures;

namespace ZensTweakstest.Items.NewZenStuff.Items2Because1IsTooFull
{
    public class ZenitrinKunai : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 70;
			item.thrown = true;
			item.knockBack = 6;
			item.noMelee = true;

			item.width = 52;
			item.height = 52;
			item.scale = 1f;

			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item39;
			item.autoReuse = true;
			item.useTurn = false;
			item.noUseGraphic = true;

			item.shoot = ModContent.ProjectileType<ZenitrinKunaiProj>();
			item.shootSpeed = 80;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(10);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 10f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 80);
			recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 25);
			recipe.AddTile(ModContent.TileType<Bosses.SparkArmor.ZCC_PLACED>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
