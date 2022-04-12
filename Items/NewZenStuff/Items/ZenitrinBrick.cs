using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;
using ZensTweakstest.Items.NewZenStuff.Tiles;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class ZenitrinBrick : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 0;
            item.rare = 8;
            item.createTile = ModContent.TileType<ZenitrinBrickT>();
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<ZenitrinOre_I>(), 2);
            recipe.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 1);
            recipe.AddTile(ModContent.TileType<ZCC_PLACED>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
