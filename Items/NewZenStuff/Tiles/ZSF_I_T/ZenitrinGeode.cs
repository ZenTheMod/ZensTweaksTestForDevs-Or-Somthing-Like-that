using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;

namespace ZensTweakstest.Items.NewZenStuff.Tiles.ZSF_I_T
{
    public class ZenitrinGeode : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Zenitrin Crystal");
            dustType = DustID.LifeDrain;
            AddMapEntry(new Color(100, 100, 100), name);
        }
    }

    public class ZenitrinCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Does not drop on break.");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.rare = 8;
            item.createTile = ModContent.TileType<ZenitrinGeode>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<ZenitrinOre_I>(), 35);
            recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 5);
            recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 15);
            recipe.AddTile(ModContent.TileType<ZCC_PLACED>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
