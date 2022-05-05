using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Bosses;

namespace ZensTweakstest.Items.NewZenStuff.Tiles
{
    public class ZSPWT : ModWall
    {
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = true;
			dustType = ModContent.DustType<ZenStoneFlameDust>();
			drop = ModContent.ItemType<ZSPWI>();
			AddMapEntry(new Color(175, 0, 0));
		}
	}

	public class ZSPWI : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zen Stone Pillar Wall");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.value = Item.buyPrice(silver: 5);
			item.rare = 4;
			item.useTime = 7;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createWall = ModContent.WallType<ZSPWT>();
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
