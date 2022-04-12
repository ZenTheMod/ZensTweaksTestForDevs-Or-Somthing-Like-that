using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;

namespace ZensTweakstest.Items
{
    public class ZWBC_I : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen-Stone Wall Book Shelf");
            Tooltip.SetDefault("Dumb But Cool");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;
            item.maxStack = 99;
            item.value = Item.buyPrice(silver: 30);
            item.value = Item.sellPrice(silver: 10);
            item.useTurn = true;
            item.autoReuse = true;
            item.rare = 8;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Items.Tiles.ZWBC_Tile>();
        }

        public override void AddRecipes()
        {
            ModRecipe DUM = new ModRecipe(mod);

            DUM.AddIngredient(ItemID.Book, 10);
            DUM.AddIngredient(ModContent.ItemType<szsb>(), 30);
            DUM.AddIngredient(ModContent.ItemType<Zen_s_Visulized_Power>(), 1);
            DUM.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 75);
            DUM.AddTile(TileID.MythrilAnvil);
            DUM.SetResult(this);
            DUM.AddRecipe();

            ModRecipe DUMe = new ModRecipe(mod);

            DUMe.AddIngredient(ItemID.Book, 10);
            DUMe.AddIngredient(ModContent.ItemType<szsb>(), 50);
            DUMe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 100);
            DUMe.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 75);
            DUMe.AddTile(TileID.MythrilAnvil);
            DUMe.SetResult(this);
            DUMe.AddRecipe();
        }
    }
}
