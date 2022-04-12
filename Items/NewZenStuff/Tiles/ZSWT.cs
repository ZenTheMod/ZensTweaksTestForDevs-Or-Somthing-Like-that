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
    public class ZSWT : ModWall
    {
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = true;
			dustType = ModContent.DustType<ZenStoneDust>();
			drop = ModContent.ItemType<ZSWI>();
			AddMapEntry(new Color(60, 41, 63));
		}
	}
	public class ZSWI : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zen-Stone Wall");
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
			item.rare = 3;
			item.useTime = 7;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createWall = ModContent.WallType<ZSWT>();
		}
	}
}
