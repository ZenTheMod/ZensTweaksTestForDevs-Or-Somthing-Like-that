using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Tiles
{
    public class ZenitrinBar_Tile : ModTile
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
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            dustType = ModContent.DustType<Smokee>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Zenitrin Bar");
            AddMapEntry(new Color(107, 26, 26), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<Items.ZenitrinBar>());
        }
    }
}
