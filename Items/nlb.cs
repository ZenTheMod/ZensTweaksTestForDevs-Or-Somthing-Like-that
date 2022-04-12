using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items
{
    public class nlb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Natures Luxurious Bundle");
            Tooltip.SetDefault("MUSHROOM" +
                "\n and a blue flower.");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.maxStack = 999;
            item.value = Item.buyPrice(copper: 70);
            item.value = Item.sellPrice(copper: 70);
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 7;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Items.Tiles.nlb>();
        }

        public override void AddRecipes()
        {
            ModRecipe DUM = new ModRecipe(mod);


            DUM.AddIngredient(ItemID.Mushroom, 1);
            DUM.AddIngredient(ItemID.GrassSeeds, 5);
            DUM.AddIngredient(ItemID.BlueBerries, 1);
            DUM.AddTile(ModContent.TileType<Items.Tiles.earthrep>());
            DUM.SetResult(this);
            DUM.AddRecipe();
        }
    }
}
