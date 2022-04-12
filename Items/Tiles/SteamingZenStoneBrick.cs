using Terraria;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.Tiles
{
    public class SteamingZenStoneBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileLavaDeath[Type] = false;
            drop = mod.ItemType("szsb");
            dustType = ModContent.DustType<Smokee>();
            soundType = 21;
            soundStyle = 2;

            minPick = 30;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Zen-Stone Brick");
            AddMapEntry(new Color(255, 97, 97), name);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1.5f;
            g = 0.0f;
            b = 0.0f;
        }
    }
}
