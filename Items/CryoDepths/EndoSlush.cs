using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Items;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.CryoDepths
{
    public class EndoSlush : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileStone[Type] = true;
            TileID.Sets.Conversion.Sand[Type] = true;
            drop = ModContent.ItemType<EndoSlushItem>();

            soundType = SoundID.Dig;
            soundStyle = 2;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Endo Slush");

            dustType = DustID.Water;

            AddMapEntry(new Color(104, 156, 196), name);

            minPick = 50;
        }
    }
    public class EndoSlushItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endo Slush");
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
            item.createTile = ModContent.TileType<EndoSlush>();
        }
    }
}
