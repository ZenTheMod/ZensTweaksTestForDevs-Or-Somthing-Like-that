using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Projectilles;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Bosses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class ZenBullet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zenitrin Bullet");
			Tooltip.SetDefault("Reflects off the walls.");
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.ranged = true;
			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 1.5f;
			item.value = Item.buyPrice(silver: 1);
			item.rare = ItemRarityID.Yellow;
			item.shoot = ModContent.ProjectileType<ZenBulletP>();   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 16f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}
		public override void OnConsumeAmmo(Player player)
		{
			for (int i = 0; i < (Main.expertMode ? 25 : 20); i++)
			{
				Dust.NewDust(player.position + player.velocity, player.width, player.height, ModContent.DustType<ZenStoneFlameDust>(), player.velocity.X * 0.5f, player.velocity.Y * 0.5f);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 15);
			recipe.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 75);
			recipe.AddTile(ModContent.TileType<ZCC_PLACED>());
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}
}
