using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items
{
    public class navybrickwb_i : ModItem
    {

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Navy Brick Workbench");
			Tooltip.SetDefault("Acts like a workbench.");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 14;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = Item.buyPrice(silver: 10);
			item.value = Item.sellPrice(silver: 15);
			item.createTile = ModContent.TileType<Items.navybrickwb>();
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
