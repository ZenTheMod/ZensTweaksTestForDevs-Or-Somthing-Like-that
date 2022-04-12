using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Biome.WormAttempt;
using ZensTweakstest.Items.NewZenStuff.Tiles;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ObjectData;
using ZensTweakstest.Items.Dusts;

namespace ZensTweakstest.Items.NewZenStuff.Tree
{
    public class ZenTree : ModTree
    {
        private Mod mod => ModLoader.GetMod("ZensTweakstest");
		public override int CreateDust()
		{
			return ModContent.DustType<ZenBeamDustWorm>();
		}

		public override int GrowthFXGore()
		{
			return mod.GetGoreSlot("Gores/ZenTreeFX");
		}

		public override int DropWood()
		{
			return ModContent.ItemType<IgnisWood>();
		}

        public override Texture2D GetTexture()
		{
			return mod.GetTexture("Items/NewZenStuff/Tree/ZenWood");
		}

		public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
		{
			return mod.GetTexture("Items/NewZenStuff/Tree/ZenTreeTops");
		}

		public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
		{
			return mod.GetTexture("Items/NewZenStuff/Tree/ZenBranch");
		}
	}

	public class IgnisWood : ModItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.Wood);
			item.rare = 3;
			item.createTile = ModContent.TileType<IgnisWoodTile>();
		}
    }
	public class IgnisWoodTile : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			drop = ModContent.ItemType<IgnisWood>();
			AddMapEntry(new Color(71, 53, 79));
			dustType = ModContent.DustType<ZenStoneDust>();
		}
		public override void ChangeWaterfallStyle(ref int style)
		{
			style = 6;
		}
	}
	public class ZenSapling : ModTile
    {
        public override void SetDefaults()
        {
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;//2
			TileObjectData.newTile.AnchorValidTiles = new[] { ModContent.TileType<ZenDirtTile>() };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Zen Sapling");
			AddMapEntry(new Color(53, 33, 57), name);

			sapling = true;
			dustType = ModContent.DustType<ZenBeamDustWorm>();
			adjTiles = new int[] { TileID.Saplings };
		}
		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void RandomUpdate(int i, int j)
		{
			// A random chance to slow down growth
			if (WorldGen.genRand.Next(15) == 0)
			{
				Tile tile = Framing.GetTileSafely(i, j); // Safely get the tile at the given coordinates
				bool growSucess; // A bool to see if the tree growing was sucessful.

				// Style 0 is for the ExampleTree sapling, and style 1 is for ExamplePalmTree, so here we check frameX to call the correct method.
				// Any pixels before 54 on the tilesheet are for ExampleTree while any pixels above it are for ExamplePalmTree
				growSucess = WorldGen.GrowTree(i, j);

				// A flag to check if a player is near the sapling
				bool isPlayerNear = WorldGen.PlayerLOS(i, j);

				//If growing the tree was a sucess and the player is near, show growing effects
				if (growSucess && isPlayerNear)
					WorldGen.TreeGrowFXCheck(i, j);
			}
		}
		public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
		{
			if (i % 2 == 1)
				effects = SpriteEffects.FlipHorizontally;
		}
	}
}
