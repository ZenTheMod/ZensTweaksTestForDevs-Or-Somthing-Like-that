using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Items.Dusts;


namespace ZensTweakstest.Items.NewZenStuff.Bosses.Pacifist
{
    public class EndurencePlantTileDeco : ModTile
    {
        public override void SetDefaults()
        {
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Endurance Plant");
			AddMapEntry(new Color(200, 0, 0), name);
			dustType = ModContent.DustType<ZenStoneDust>();
			disableSmartCursor = false;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<EndurencePlantDeco>());
		}
	}
}