using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class ZenitrinOre_I : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenitrin Ore");
        }

        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.consumable = true;
            item.useStyle = 1;
            item.value = Item.buyPrice(silver: 30);
            item.value = Item.sellPrice(silver: 15);
            item.rare = ItemRarityID.Yellow;
            item.useTime = 6;
            item.useAnimation = 10;
            item.autoReuse = true;
            item.createTile = mod.TileType("ZenitrinOre");
        }
    }
}
