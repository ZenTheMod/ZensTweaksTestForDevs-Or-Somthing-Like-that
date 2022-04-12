using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
    [AutoloadEquip(EquipType.Shield)]
    public class ZSS_Vanity : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stronghold Shield");
        }

        public override void SetDefaults()
        {
            item.vanity = true;
            item.width = 30;
            item.height = 28;
            item.accessory = true;
            item.rare = ItemRarityID.Yellow;
        }
    }
}
