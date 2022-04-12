using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Items.Dusts;

namespace ZensTweakstest.Items.Tiles
{
    public class zens : ModTile
    {
        public override void SetDefaults()
        {
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);

			dustType = ModContent.DustType<Smokee>();
			soundType = 21;
			soundStyle = 2;

			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Embeded Zen Wall Deco");
			AddMapEntry(new Color(255, 0, 0), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Zen_s_Visulized_Power>());
		}
	}
}
