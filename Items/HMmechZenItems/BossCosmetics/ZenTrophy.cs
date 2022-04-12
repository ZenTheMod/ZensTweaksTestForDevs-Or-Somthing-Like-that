using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ZensTweakstest.Items.HMmechZenItems.BossCosmetics
{
    public class ZenTrophy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mech Zen Trophy");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 4;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.maxStack = 99;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<ZenTrophyTile>();
        }
    }
    public class ZenTrophyTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 36;
            TileObjectData.addTile(Type);

            dustType = DustID.PinkFairy;

            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Trophy");
            AddMapEntry(new Color(255, 0, 0), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<ZenTrophy>());
        }
    }
}
