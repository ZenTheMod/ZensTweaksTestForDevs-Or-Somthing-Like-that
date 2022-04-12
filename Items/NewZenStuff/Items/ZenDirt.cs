using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class ZenDirt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unity Dirt");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.consumable = true;
            item.useStyle = 1;
            item.value = Item.buyPrice(copper: 30);
            item.value = Item.sellPrice(copper: 10);
            item.rare = ItemRarityID.Orange;
            item.useTime = 6;
            item.useAnimation = 10;
            item.autoReuse = true;
            item.createTile = mod.TileType("ZenDirtTile");
        }
    }
}
