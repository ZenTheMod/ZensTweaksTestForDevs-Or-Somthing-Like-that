using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ZensTweakstest.Items.Tiles
{
    public class electrons : ModTile
    {
        
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileLavaDeath[Type] = true;
            drop = mod.ItemType("electrons");

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Electronics");

            AddMapEntry(new Color(56, 232, 255), name);

            minPick = 40;
        }
    }
}
