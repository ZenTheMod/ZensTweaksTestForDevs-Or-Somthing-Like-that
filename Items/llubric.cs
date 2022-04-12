using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace ZensTweakstest.Items
{
    public class llubric : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lumenyl Beam-Brick");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.buyPrice(silver: 1);
            item.value = Item.sellPrice(copper: 50);
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;

            item.maxStack = 999;
            item.useTime = 7;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Items.Tiles.lubric>();
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
