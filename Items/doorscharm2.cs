using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items
{
    public class doorscharm2 : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Doors Charm");
            Tooltip.SetDefault("Lock-Picking has really opened a lot of doors for me!  Inceases magic damage by 7%");
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(50);

            item.accessory = true;

            item.value = Item.buyPrice(silver: 40);
            item.value = Item.sellPrice(silver: 25);

            item.rare = ItemRarityID.LightRed;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += 0.07f;
        }

        public override void AddRecipes()
        {
            ModRecipe A = new ModRecipe(mod);
            A.AddIngredient(ModContent.ItemType<Doorscharm1>());
            A.AddIngredient(ItemID.LavaCharm, 1);
            A.AddIngredient(ItemID.HellstoneBar, 5);
            A.AddIngredient(ItemID.SorcererEmblem, 1);
            A.AddIngredient(ItemID.PalladiumBar, 1);
            A.AddTile(TileID.Hellforge);
            A.SetResult(this);
            A.AddRecipe();

            ModRecipe pp = new ModRecipe(mod);
            pp.AddIngredient(ModContent.ItemType<Doorscharm1>());
            pp.AddIngredient(ItemID.LavaCharm, 1);
            pp.AddIngredient(ItemID.SorcererEmblem, 1);
            pp.AddIngredient(ItemID.HellstoneBar, 5);
            pp.AddIngredient(ItemID.CobaltBar, 1);
            pp.AddTile(TileID.Hellforge);
            pp.SetResult(this);
            pp.AddRecipe();
        }
    }
}
