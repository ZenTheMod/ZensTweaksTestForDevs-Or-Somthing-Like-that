using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items
{
    public class Teh : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shoe Box");//name
            Tooltip.SetDefault("Box containing shoe");//funny line
        }

        public override void SetDefaults()
        {
            item.width = 12;//demensions
            item.height = 12;//demensions
            item.maxStack = 99;//stack
            item.value = Item.buyPrice(copper: 70);
            item.value = Item.sellPrice(copper: 70);
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 7;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Items.Tiles.teeth>();
        }

        public override void AddRecipes()
        {
            ModRecipe DUM = new ModRecipe(mod);

            DUM.AddIngredient(ItemID.Wood, 15);
            DUM.AddIngredient(ItemID.LightningBoots, 1);
            DUM.AddTile(TileID.HeavyWorkBench);
            DUM.SetResult(this);
            DUM.AddRecipe();
        }
    }
}
