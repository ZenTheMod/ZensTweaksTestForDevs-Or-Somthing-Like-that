using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Tiles;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class ZenStoneCHR_I : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ZenStone Chair");
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
            item.value = Item.buyPrice(silver: 25);
            item.value = Item.sellPrice(silver: 20);
            item.createTile = mod.TileType("zCHR");
        }

        public override void AddRecipes()
        {
            ModRecipe POOP = new ModRecipe(mod);

            POOP.AddIngredient(ItemID.WoodenChair, 1);
            POOP.AddIngredient(ModContent.ItemType<szsb>(), 30);
            POOP.AddIngredient(ModContent.ItemType<ZenStone_I>(), 50);
            POOP.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 50);
            POOP.AddTile(TileID.MythrilAnvil);
            POOP.SetResult(this);
            POOP.AddRecipe();

            ModRecipe POOPE = new ModRecipe(mod);

            POOPE.AddIngredient(ItemID.WoodenChair, 1);
            POOPE.AddIngredient(ModContent.ItemType<szsb>(), 10);
            POOPE.AddIngredient(ModContent.ItemType<Zen_s_Visulized_Power>(), 1);
            POOPE.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 50);
            POOPE.AddTile(TileID.MythrilAnvil);
            POOPE.SetResult(this);
            POOPE.AddRecipe();
        }
    }
}
