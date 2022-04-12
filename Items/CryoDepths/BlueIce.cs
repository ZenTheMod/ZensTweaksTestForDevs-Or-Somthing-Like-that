using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Items;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.CryoDepths
{
    public class BlueIce : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileStone[Type] = true;
            TileID.Sets.Conversion.Ice[Type] = true;
            drop = ModContent.ItemType<BlueIceItem>();

            soundType = 21;
            soundStyle = 2;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Blue Ice");

            dustType = DustID.Water;

            AddMapEntry(new Color(137, 185, 224), name);

            minPick = 50;
        }
    }
    public class BlueIceItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Ice");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.consumable = true;
            item.useStyle = 1;
            item.value = Item.buyPrice(copper: 30);
            item.value = Item.sellPrice(copper: 10);
            item.rare = ItemRarityID.Orange;
            item.useTime = 6;
            item.useAnimation = 10;
            item.autoReuse = true;
            item.createTile = ModContent.TileType<BlueIce>();
        }
    }
}
