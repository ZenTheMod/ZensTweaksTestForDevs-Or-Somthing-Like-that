using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Items;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Tiles
{
    public class ZenStone : ModTile
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
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileStone[Type] = true;
            TileID.Sets.Conversion.Stone[Type] = true;
            drop = mod.ItemType("ZenStone_I");

            soundType = 21;
            soundStyle = 2;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Zen Stone");

            dustType = ModContent.DustType<Smokee>();

            AddMapEntry(new Color(107, 74, 112), name);

            minPick = 50;
        }
    }
}
