using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using ZensTweakstest.Items.Dusts;
using Terraria.ObjectData;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Zen.Loot.Pacebles
{
    public class Pinecone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);

            dustType = ModContent.DustType<Smokee>();
            soundType = 21;
            soundStyle = 2;

            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Pinecone");
            AddMapEntry(new Color(255, 0, 0), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Pinecone_I>());
        }
    }
}
