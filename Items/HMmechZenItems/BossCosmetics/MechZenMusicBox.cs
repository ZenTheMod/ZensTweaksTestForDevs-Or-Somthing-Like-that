using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ZensTweakstest.Items.HMmechZenItems.BossCosmetics
{
    public class MechZenMusicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Music Box (Mechanical Base - By Zen)");
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<MechZenMusicBoxPlaced>();
            item.width = 24;
            item.height = 24;
            item.rare = 4;
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.accessory = true;
            item.vanity = true;
        }
    }
    public class MechZenMusicBoxPlaced : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Music Box");
            AddMapEntry(new Color(200, 200, 200), name);

            dustType = DustID.WhiteTorch;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = ModContent.ItemType<MechZenMusicBox>();
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 48, ModContent.ItemType<MechZenMusicBox>());
        }
    }
}
