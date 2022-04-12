using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace ZensTweakstest.Items
{
    public class nbw_Item : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Navy Brick Wall");
            Tooltip.SetDefault("Made with Calamity's Navy Stone");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 999;
            item.value = Item.buyPrice(copper: 25);
            item.value = Item.sellPrice(copper: 20);
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 7;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createWall = ModContent.WallType<Items.Navybrickwall>();
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
