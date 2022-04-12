using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items;

namespace ZensTweakstest.Items
{
    public class doorscharm3 : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Evolution Doors Charm");
            Tooltip.SetDefault("Love is an open door. Inceases magic damage by 20%");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(50);

            item.accessory = true;

            item.value = Item.buyPrice(gold: 15);
            item.value = Item.sellPrice(gold: 10);

            item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += 0.20f;
        }

        public override void AddRecipes()
        {
            ModRecipe A = new ModRecipe(mod);
            A.AddIngredient(ModContent.ItemType<doorscharm2>());
            A.AddIngredient(ItemID.FragmentNebula, 15);
            A.AddIngredient(ItemID.LunarBar, 5);
            A.AddIngredient(ItemID.CelestialEmblem, 1);
            A.AddTile(TileID.LunarCraftingStation);
            A.SetResult(this);
            A.AddRecipe();
        }
    }
}