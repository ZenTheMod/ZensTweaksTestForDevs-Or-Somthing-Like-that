using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Projectilles;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Tree;
using static Terraria.ID.TileID.Sets;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.NewZenStuff.Tiles
{
	public class ZenDirtTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMerge[Type][ModContent.TileType<ZenGrassTile>()] = true;
			Main.tileBlendAll[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			drop = ModContent.ItemType<ZenDirt>();
			AddMapEntry(new Color(71, 53, 79));
			dustType = ModContent.DustType<ZenStoneDust>();
			Main.tileMerge[Type][ModContent.TileType<ZenGrassTile>()] = true;
			SetModTree(new Tree.ZenTree());
		}

		public override void ChangeWaterfallStyle(ref int style)
		{
			style = 6;
		}
		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return ModContent.TileType<ZenSapling>();
		}
	}
	public class ZenGrassTile : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileBlendAll[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			drop = ModContent.ItemType<ZenDirt>();
			AddMapEntry(new Color(248, 65, 78));
			dustType = ModContent.DustType<ZenStoneDust>();
			Main.tileMerge[Type][ModContent.TileType<ZenDirtTile>()] = true;
			SetModTree(new Tree.ZenTree());
            drop = ModContent.ItemType<ZenDirt>();

            TileID.Sets.Grass[Type] = true;
			TileID.Sets.NeedsGrassFraming[Type] = true;
			TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<ZenDirtTile>();
			TileID.Sets.Conversion.Grass[Type] = true;
		}
		public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
		{
			if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
				return false;
			toBePlaced.random = random;
			if (TileObject.Place(toBePlaced) && !mute)
				WorldGen.SquareTileFrame(x, y, true);
			return false;
		}
		public override int SaplingGrowthType(ref int style)
		{
			style = 0;
			return ModContent.TileType<ZenSapling>();
		}
		public override void ChangeWaterfallStyle(ref int style)
		{
			style = 6;
		}
        /*public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
			Tile tile = Framing.GetTileSafely(i, j);
			Color colour = Color.White;

			Texture2D glow = ModContent.GetTexture("ZensTweakstest/Items/NewZenStuff/Tiles/ZenGrassTileGlowMask");
			Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);

			spriteBatch.Draw(glow, new Vector2(i * 16, j * 16) - Main.screenPosition + zero, new Rectangle(tile.frameX, tile.frameY, 16, 16), colour);
		}*/
    }
}
