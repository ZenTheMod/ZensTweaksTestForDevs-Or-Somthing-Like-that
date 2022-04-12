using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Tropys;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Zen.Loot.Pacebles
{
    public class Painting_I : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Home");
            Tooltip.SetDefault("It depicts a galaxy.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 1, 20, 5);
            item.rare = ItemRarityID.Yellow;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.maxStack = 99;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<Painting2x2>();
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
