using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ZensTweakstest.Items
{
    public class electrons : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electronic Bits");
            Tooltip.SetDefault("Bits, Not Those Bits");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.consumable = true;
            item.useStyle = 1;
            item.value = Item.buyPrice(copper: 30);
            item.value = Item.sellPrice(copper: 10);
            item.useTime = 10;
            item.useAnimation = 10;
            item.autoReuse = true;
            item.createTile = mod.TileType("electrons");

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 1);
            recipe.AddIngredient(ItemID.Wire, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this,10);
            recipe.AddRecipe();

            ModRecipe pp = new ModRecipe(mod);
            pp.AddIngredient(ItemID.LeadBar, 1);
            pp.AddIngredient(ItemID.Wire, 1);
            pp.AddTile(TileID.Anvils);
            pp.SetResult(this, 10);
            pp.AddRecipe();

            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.LifeFruit, 3);
            r.AddIngredient(ItemID.HallowedBar, 10);
            r.AddTile(TileID.MythrilAnvil);
            r.SetResult(ItemID.ChlorophyteBar, 10);
            r.AddRecipe();
        }
    }
}
