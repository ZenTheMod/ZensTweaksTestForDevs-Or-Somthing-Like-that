using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.IO;
using ZensTweakstest.Items.NewZenStuff.Tiles;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class PeeveBanner_I : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen Stone Peeve Banner");
            Tooltip.SetDefault("Nearby players get a bonus against: Zen Stone Peeve");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 48;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Yellow;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.buyPrice(silver: 75);
            item.value = Item.sellPrice(silver: 65);
            item.createTile = mod.TileType("PeeveBanner");
        }
    }
}
