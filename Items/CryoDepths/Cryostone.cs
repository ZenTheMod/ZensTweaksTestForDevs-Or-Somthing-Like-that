using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Items;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.CryoDepths
{
    public class Cryostone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileStone[Type] = true;
            TileID.Sets.Conversion.Stone[Type] = true;
            drop = ModContent.ItemType<CryostoneItem>();

            soundType = 21;
            soundStyle = 2;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Cryo Stone");

            dustType = DustID.Water;

            AddMapEntry(new Color(107, 178, 229), name);

            minPick = 50;
        }
    }
    public class CryostoneItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryo Stone");
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
            item.createTile = ModContent.TileType<Cryostone>();
        }
    }
}
