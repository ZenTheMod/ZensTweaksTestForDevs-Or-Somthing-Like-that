using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
    public class SG_Box : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Music Box (The Battle of Shine - By NoFace Music)");
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<Bosses.Loot.BagLoot.SG_Box_Placed>();
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.accessory = true;
            item.vanity = true;
        }
    }
}
