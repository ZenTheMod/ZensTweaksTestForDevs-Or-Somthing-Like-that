using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class ZenitrinBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenitrin Bar");
        }

        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 999;
            item.value = Item.buyPrice(gold: 4);
            item.value = Item.sellPrice(gold: 1, silver: 20);

            item.rare = 8;

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<Tiles.ZenitrinBar_Tile>();
            item.placeStyle = 0;
        }

        public override void AddRecipes()
        {
            ModRecipe POOP = new ModRecipe(mod);

            POOP.AddIngredient(ModContent.ItemType<ZenitrinOre_I>(), 7);
            POOP.AddIngredient(ModContent.ItemType<ZenStone_I>(), 55);
            POOP.AddTile(TileID.AdamantiteForge);
            POOP.SetResult(this);
            POOP.AddRecipe();
        }
    }
}
