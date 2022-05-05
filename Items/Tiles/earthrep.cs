using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ZensTweakstest.Items.Tiles
{
    public class earthrep : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.CoordinateHeights = new[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);

			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Earth Replicator");
			AddMapEntry(new Color(59, 59, 59), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Items.erep>());
		}
	}
}
