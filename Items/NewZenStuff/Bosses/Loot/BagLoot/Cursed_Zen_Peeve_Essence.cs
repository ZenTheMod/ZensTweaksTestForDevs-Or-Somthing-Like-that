using Terraria;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ID;
using System.Text;
using System.Threading.Tasks;
using ZensTweakstest.Items.Pet_s;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
    public class Cursed_Zen_Peeve_Essence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Flare Essence");
            Tooltip.SetDefault("Summons a sad Pet Peeve");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 26;
            item.value = Item.buyPrice(gold: 10);
            item.value = Item.sellPrice(gold: 5);
            item.CloneDefaults(ItemID.ZephyrFish);
            item.shoot = ModContent.ProjectileType<Cursed_Zen_Pet>();
            item.buffType = ModContent.BuffType<Cursed_Zen_Pet_Buff>();
            item.rare = ItemRarityID.Yellow;
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
