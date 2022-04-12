using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Items;

namespace ZensTweakstest.Items.NewZenStuff.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ZenitrinChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Peace Keeper Breastplate");
            Tooltip.SetDefault("Immunity to 'On Fire!'"
                + "\n+20 max life, 4% more life regen and +5% crit for all classes.");
        }
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.OnFire] = true;
            player.lifeRegen += 4;
            player.statLifeMax2 += 20;
            player.meleeCrit += 5;
            player.magicCrit += 5;
            player.rangedCrit += 5;
            player.thrownCrit += 5;
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.rare = 8;
            item.defense = 27;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 25);
            recipe.AddTile(mod.TileType("ZCC_PLACED"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
