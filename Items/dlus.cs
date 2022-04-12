using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Tiles;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items
{
    [AutoloadEquip(EquipType.Shoes)]
    public class dlus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daniel's Light-Up-Sneekers");
            Tooltip.SetDefault("The wearer can be more like the mystical god Daniel");
        }
        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.accessory = true; // Makes this item an accessory.
            item.rare = ItemRarityID.Expert;
            item.value = Item.sellPrice(gold: 1); // Sets the item sell price to one gold coin.
            item.value = Item.buyPrice(gold: 5); // Sets the item buy price to one gold coin.
            item.vanity = true;
        }

        public override void AddRecipes()
        {
            ModRecipe A = new ModRecipe(mod);
            A.AddIngredient(ItemID.Leather, 20);
            A.AddIngredient(ModContent.ItemType<electrons>(), 10);
            A.AddIngredient(ModContent.ItemType<ots>());
            A.AddTile(TileID.TinkerersWorkbench);
            A.SetResult(this);
            A.AddRecipe();

            ModRecipe AA = new ModRecipe(mod);
            AA.AddIngredient(ModContent.ItemType<dlusp_i>(), 1);
            AA.AddTile(TileID.TinkerersWorkbench);
            AA.SetResult(this);
            AA.AddRecipe();
        }
    }
}
