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

namespace ZensTweakstest.Items.CryoDepths
{
    public class BlueIceWall : ModWall
    {
        public override void SetDefaults()
        {
			Main.tileLighted[Type] = true;
            Main.wallHouse[Type] = true;
            dustType = DustID.Water;
            drop = ModContent.ItemType<BlueIceWallItem>();
            AddMapEntry(new Color(39, 35, 55));
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
			Tile tile = Main.tile[i, j];
			if (tile.frameX == 0)
			{
				// We can support different light colors for different styles here: switch (tile.frameY / 54)
				r = 0f;
				g = 0.075f;
				b = 0.1f;
			}
		}
    }
    public class BlueIceWallItem : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue Ice Wall");
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
			item.createWall = ModContent.WallType<BlueIceWall>();
		}
	}
}
