using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Items.NewZenStuff.NpcSS;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Biome.WormAttempt;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Bosses;

namespace ZensTweakstest.Items.TheBanners
{
    public class ZenStoneSwordBanner : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Nearby players get a bonus against: Zen Stone Sword");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 48;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.rare = ItemRarityID.Orange;
			item.useStyle = 1;
			item.consumable = true;
			item.value = Item.buyPrice(silver: 75);
			item.value = Item.sellPrice(silver: 65);
			item.createTile = ModContent.TileType<ZenStoneSwordBannerPlaced>();
		}
	}
	public class ZenStoneSwordBannerPlaced : ModTile
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
		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;

			player.NPCBannerBuff[ModContent.NPCType<ZenSwordNPC>()] = true;
			player.hasBanner = true;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<ZenStoneSwordBanner>());
		}
	}
}
