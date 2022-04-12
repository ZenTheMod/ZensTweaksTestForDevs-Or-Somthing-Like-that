using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Tropys;
using System.Collections.Generic;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Zen.Loot.Pacebles
{
    public class Pinecone_I : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pinecone");
            Tooltip.SetDefault("It represents his past.");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 30;
            item.value = Item.sellPrice(0, 10, 20, 0);
            item.rare = ItemRarityID.Yellow;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.maxStack = 99;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<Pinecone>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(182, 230, 99);
                }
            }
        }
    }
}
