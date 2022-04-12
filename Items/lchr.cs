using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items
{
    public class lchr : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electronic Chair");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.buyPrice(silver: 15);
            item.value = Item.sellPrice(silver: 10);
            item.createTile = mod.TileType("llchr");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(127, 36, 64); //change the color accordingly to above
                }
            }
        }
    }
}
