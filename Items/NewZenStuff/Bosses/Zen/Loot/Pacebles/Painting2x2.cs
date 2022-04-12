using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Tropys;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Zen.Loot.Pacebles
{
    public class Painting2x2 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);

            dustType = ModContent.DustType<Smokee>();
            soundType = 21;
            soundStyle = 2;

            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Painting");
            AddMapEntry(new Color(255, 0, 0), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Painting_I>());
        }
    }
}
