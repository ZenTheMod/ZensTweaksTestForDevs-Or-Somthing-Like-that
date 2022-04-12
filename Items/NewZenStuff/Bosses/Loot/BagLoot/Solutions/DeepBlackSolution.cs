using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot.Solutions;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot.Solutions
{
    public class DeepBlackSolution : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Deep Black Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nContains the ability to reform the living. " +
				"\nCan reform trees into twisting nether.");
		}

		public override void SetDefaults()
		{
			item.shoot = ModContent.ProjectileType<DeepBlackSolutionProjectile>() - ProjectileID.PureSpray;
			item.ammo = AmmoID.Solution;
			item.width = 10;
			item.height = 12;
			item.value = Item.buyPrice(0, 0, 25, 0);
			item.rare = ItemRarityID.Yellow;
			item.maxStack = 999;
			item.consumable = true;
		}

        public override void AddRecipes()
        {
			ModRecipe POOP = new ModRecipe(mod);

			POOP.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 3);
			POOP.AddIngredient(ItemID.Bottle, 1);
			POOP.AddIngredient(ModContent.ItemType<ZenStone_I>(), 1);
			POOP.AddTile(ModContent.TileType<ZCC_PLACED>());
			POOP.SetResult(this, 1);
			POOP.AddRecipe();
		}
    }
}
