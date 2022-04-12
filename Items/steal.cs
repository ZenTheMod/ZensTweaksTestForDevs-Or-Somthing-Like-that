using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items
{
    public class steal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reinforced Steel Plating");
            Tooltip.SetDefault("Hand-made with steel... wait...");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 99;
            item.value = Item.buyPrice(silver: 10);
            item.value = Item.sellPrice(silver: 5);
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
