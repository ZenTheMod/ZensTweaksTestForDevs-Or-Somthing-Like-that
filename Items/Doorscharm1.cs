using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items
{
    public class Doorscharm1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doors Charm");
            Tooltip.SetDefault("As a door im open to new things... Increases magic damage by 2%");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(50);

            item.accessory = true;

            item.value = Item.buyPrice(copper: 10);
            item.value = Item.sellPrice(copper: 7);

            item.rare = ItemRarityID.White;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += 0.02f;
        }

        public override void AddRecipes()
        {
            ModRecipe A = new ModRecipe(mod);
            A.AddIngredient(ItemID.Chain, 2);
            A.AddIngredient(ItemID.WoodenDoor, 1);
            A.AddTile(TileID.Anvils);
            A.SetResult(this);
            A.AddRecipe();
        }
    }
}
