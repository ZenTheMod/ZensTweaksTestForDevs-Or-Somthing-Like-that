using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Tiles;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items
{
    public class ots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old Torn Shoes");
            Tooltip.SetDefault("Could be repaired");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 22;
            item.value = Item.buyPrice(copper: 80);
            item.value = Item.sellPrice(copper: 70);
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
