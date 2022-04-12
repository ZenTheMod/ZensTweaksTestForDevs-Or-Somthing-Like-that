using Terraria;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.Tiles
{
    public class lubric : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileLavaDeath[Type] = false;
            drop = mod.ItemType("llubric");
            soundType = 21;
            soundStyle = 2;
            dustType = ModContent.DustType<Smokee>();

            minPick = 30;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Lumenyl Brick");
            AddMapEntry(new Color(115, 220, 255), name);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.0f;
            g = 0.3f;
            b = 1.5f;
        }
    }
}
