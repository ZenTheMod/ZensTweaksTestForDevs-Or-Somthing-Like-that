using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Slime
{
    [AutoloadEquip(EquipType.Head)]
    public class SlimeHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Mana increased by 10"
				+ "\nIncreases max amount of minions by 1");
		}

		public override void SetDefaults()
		{
			item.defense = 4;

			item.width = 22;
			item.height = 34;

			item.value = Item.sellPrice(silver: 18);
			item.rare = ItemRarityID.Blue;
		}

		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += 10;
			player.maxMinions ++;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SlimeChestplate>() && legs.type == ModContent.ItemType<SlimeGreaves>();
		}

        public override void UpdateArmorSet(Player player)
        {
			player.setBonus = "The player releases Slime Spikes after taking a hit.";
			player.GetModPlayer<Charred_Life>().SlimeSetBounes = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Gel, 30);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
