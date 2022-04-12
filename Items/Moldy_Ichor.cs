using Terraria;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ID;
using System.Text;
using System.Threading.Tasks;

namespace ZensTweakstest.Items
{
    public class Moldy_Ichor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moldy Ichor");
            Tooltip.SetDefault("Summons a Ichor Stingler to circle around you");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 22;
            item.value = Item.buyPrice(gold: 10);
            item.value = Item.sellPrice(gold: 5);
            item.CloneDefaults(ItemID.ZephyrFish);
            item.shoot = ModContent.ProjectileType<Items.Pet_s.ichorstinger>();
            item.buffType = ModContent.BuffType<Items.buffs.ichorpetbuff>();
            item.rare = ItemRarityID.LightRed;
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Ichor, 5);
            recipe.AddIngredient(ItemID.StoneBlock, 1);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
