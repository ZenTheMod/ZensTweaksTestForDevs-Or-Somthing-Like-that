using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class ZenitrinHamaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Change if needed
        }
        public override void SetDefaults()
        {
			item.damage = 25;
			item.melee = true;
			item.width = 42;
			item.height = 42;
			item.useTime = 9;
			item.useAnimation = 9;
			item.axe = 30;          //How much axe power the weapon has, note that the axe power displayed in-game is this value multiplied by 5
			item.hammer = 95;      //How much hammer power the weapon has
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 100000;
			item.tileBoost += 2;
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override Color? GetAlpha(Color lightColor)
        {
			return Color.White;
        }
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 50);
			recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 7);
			recipe.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 30);
			recipe.AddTile(ModContent.TileType<ZCC_PLACED>());
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}
