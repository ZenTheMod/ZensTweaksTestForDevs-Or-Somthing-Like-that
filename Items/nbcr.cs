using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items
{
    public class nbcr : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nature's Building Gift");
			Tooltip.SetDefault("Nature's power of a true builder.");
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.value = Item.buyPrice(gold: 5);
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Expert;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.autoPaint = true;
			player.autoActuator = true;
			player.blockRange += 5;
			player.slowFall = true;
			player.tileSpeed += 3;
		}

		public override void AddRecipes()
		{
			ModRecipe DUM = new ModRecipe(mod);

			DUM.AddIngredient(ItemID.StaffofRegrowth, 1);
			DUM.AddIngredient(ItemID.GrassSeeds, 10);
			DUM.AddIngredient(ItemID.StoneBlock, 30);
			DUM.AddIngredient(ItemID.ArchitectGizmoPack, 1);
			DUM.AddTile(ModContent.TileType<Items.Tiles.earthrep>());
			DUM.SetResult(this);
			DUM.AddRecipe();
		}
	}
}
