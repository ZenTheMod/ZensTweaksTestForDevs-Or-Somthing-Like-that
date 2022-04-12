using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Tropys;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Tropys
{
    public class SG_Trophy : ModTile
    {
		public override bool Autoload(ref string name, ref string texture)
		{
			if (ModContent.GetInstance<SpriteSettings>().MostClassicSprites)
			{
				texture += "OLD";
			}
			return base.Autoload(ref name, ref texture);
		}
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

			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Trophy");
			AddMapEntry(new Color(255, 0, 0), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Bosses.Loot.Tropys.SG_Trophy_I>());
		}
	}
}