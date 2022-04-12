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

namespace ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor
{
    public class ZCC_PLACED : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);

			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Zen's Crafting Chutes");
			AddMapEntry(new Color(59, 59, 59), name);
			dustType = ModContent.DustType<ZenStoneDust>();
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<ZensCraftingChutes>());
		}
	}
}
