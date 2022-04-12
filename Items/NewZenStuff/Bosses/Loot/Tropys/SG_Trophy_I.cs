using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Tropys;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Tropys
{
    public class SG_Trophy_I : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spark Guardian Trophy");
        }
        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = ItemRarityID.Yellow;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.maxStack = 99;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<Bosses.Loot.Tropys.SG_Trophy>();
        }
    }
}