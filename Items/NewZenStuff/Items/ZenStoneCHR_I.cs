using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Tiles;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Tree;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class ZenStoneCHR_I : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ignis Wood Chair");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 8;
            item.createTile = mod.TileType("zCHR");
        }

        public override void AddRecipes()
        {
            ModRecipe POOP = new ModRecipe(mod);

            POOP.AddIngredient(ModContent.ItemType<IgnisWood>(), 4);
            POOP.AddTile(TileID.WorkBenches);
            POOP.SetResult(this);
            POOP.AddRecipe();
        }
    }
}
