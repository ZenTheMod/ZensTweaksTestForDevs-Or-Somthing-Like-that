using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Items.NewZenStuff.Items;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.Dusts;

namespace ZensTweakstest.Items.NewZenStuff.Tiles
{
    public class ZenitrinBrickT : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            //Main.tileSolidTop[Type] = true;
            Main.tileSolid[Type] = true;
            //Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.Platforms[Type] = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleMultiplier = 27;
            TileObjectData.newTile.StyleWrapLimit = 27;
            TileObjectData.newTile.UsesCustomCanPlace = false;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.addTile(Type);
            dustType = ModContent.DustType<ZenStoneDust>();
            soundType = 21;
            soundStyle = 2;
            minPick = 100;
            AddMapEntry(new Color(235, 64, 52));
            mineResist = 1;
            drop = ModContent.ItemType<ZenitrinBrick>();
        }
    }
}
