using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Items.NewZenStuff.NpcSS;

namespace ZensTweakstest.Items.NewZenStuff.Tiles
{
    public class ZenStoneSlimeBanner : ModTile
    {
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.StyleWrapLimit = 111;
			TileObjectData.addTile(Type);
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Banner");
			AddMapEntry(new Color(130, 13, 13), name);
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<Items.ZenStoneSlimeBanner_I>());
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;

			player.NPCBannerBuff[mod.NPCType("ZenStoneSlime")] = true;
			player.hasBanner = true;
		}
	}
}
