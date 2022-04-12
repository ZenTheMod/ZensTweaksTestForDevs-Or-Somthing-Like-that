using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ZensTweakstest.Items.Tiles
{
    public class nlb : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);

			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Natures Luxurious Bundle");
			AddMapEntry(new Color(59, 59, 59), name);
		}
	}
}
