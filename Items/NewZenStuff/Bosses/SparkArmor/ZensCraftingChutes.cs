using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Threading.Tasks;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Tree;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor
{
    public class ZensCraftingChutes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen's Crafting Chutes");//name
            Tooltip.SetDefault("You can sense the Zendothermic Pulses.");//funny line
        }

        public override void SetDefaults()
        {
            item.width = 48;//demensions
            item.height = 48;//demensions
            item.maxStack = 99;//stack
            item.value = Item.buyPrice(silver: 70);
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 7;
            item.rare = 8;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<ZCC_PLACED>();
        }

        public override void AddRecipes()
        {
            ModRecipe DUM = new ModRecipe(mod);

            DUM.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 15);
            DUM.AddIngredient(ModContent.ItemType<szsb>(), 5);
            DUM.AddIngredient(ModContent.ItemType<ZenStone_I>(), 5);
            DUM.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 7);
            DUM.AddIngredient(ModContent.ItemType<IgnisWood>(), 15);
            DUM.AddTile(TileID.HeavyWorkBench);
            DUM.SetResult(this);
            DUM.AddRecipe();
        }
    }
}
