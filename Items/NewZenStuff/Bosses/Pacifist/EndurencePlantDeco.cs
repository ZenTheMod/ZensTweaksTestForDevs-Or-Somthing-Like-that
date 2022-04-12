using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.ID;
using Terraria.ModLoader;
using System.Threading.Tasks;
using Terraria;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Pacifist
{
    public class EndurencePlantDeco : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Potted Endurance Plant");
			Tooltip.SetDefault("A Small Reward...");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 48;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.rare = 8;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = Item.sellPrice(0, 1, 20, 5);
			item.createTile = ModContent.TileType<EndurencePlantTileDeco>();
		}
	}
}
