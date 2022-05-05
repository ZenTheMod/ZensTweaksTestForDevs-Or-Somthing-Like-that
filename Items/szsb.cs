using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ZensTweakstest.Items.NewZenStuff.Tiles;

namespace ZensTweakstest.Items
{
    public class szsb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen-Stone Brick");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.value = Item.buyPrice(silver: 1);
            item.value = Item.sellPrice(copper: 50);
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;

            item.rare = 4;

            item.maxStack = 999;
            item.useTime = 7;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = ModContent.TileType<Items.Tiles.SteamingZenStoneBrick>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(127, 36, 64); //change the color accordingly to above not...
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<ZSBWI>());

            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}
