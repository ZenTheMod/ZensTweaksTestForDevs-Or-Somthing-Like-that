using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Projectilles;
using ZensTweakstest.Config;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
	public class ZenitrinPick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lucid Clear Pickaxe");
		}

		public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;

		public override void SetDefaults()
		{
			item.damage = 85;
			item.melee = true;
			item.width = 34;
			item.height = 34;
			item.useTime = 5; //Actual Break 1 = FAST 50 = SUPER SLOW
			item.useAnimation = 10; //Looks: 1 = FAST 50 = SLOW
			item.pick = 200;
			item.tileBoost += 4;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = Item.buyPrice(gold: 5);
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
        {
			ModRecipe POOP = new ModRecipe(mod);

			POOP.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 10);
			POOP.AddIngredient(ModContent.ItemType<ZenStone_I>(), 10);
			POOP.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 75);
			POOP.AddTile(ModContent.TileType<ZCC_PLACED>());
			POOP.SetResult(this);
			POOP.AddRecipe();
		}
        public override Color? GetAlpha(Color lightColor)
        {
			return Color.White;
        }
    }
}
