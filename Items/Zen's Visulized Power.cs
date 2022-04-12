using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace ZensTweakstest.Items
{
    public class Zen_s_Visulized_Power : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen's Visulized Power");
            Tooltip.SetDefault("A physical representation of Zen's Power");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;
            item.maxStack = 999;
            item.value = Item.buyPrice(gold: 1);
            item.value = Item.sellPrice(silver: 75);
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = ItemRarityID.Expert;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Items.Tiles.zens>();
        }
    }
}
