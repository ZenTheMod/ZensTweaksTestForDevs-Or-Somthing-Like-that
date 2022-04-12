using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Tiles
{
    public class ZenitrinOre : ModTile
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
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[Type][TileID.Ash] = true;
            Main.tileMerge[Type][TileID.Stone] = true;
            Main.tileLavaDeath[Type] = false;
            drop = mod.ItemType("ZenitrinOre_I");

            soundType = 21;
            soundStyle = 2;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Zenitrin Ore");

            dustType = ModContent.DustType<Smokee>();

            AddMapEntry(new Color(208, 84, 92), name);

            minPick = 200;
            mineResist = 3f;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = .5f;
            g = 0f;
            b = 0f;
        }
    }
}
