using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Items;

namespace ZensTweakstest.Items.NewZenStuff.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ZenitrinHood : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Peace Keeper Hood");
			Tooltip.SetDefault("Increases magic damage and decreases mana cost.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 8;
			item.defense = 17;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ZenitrinChestplate>() && legs.type == ModContent.ItemType<ZenitrinLegs>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.GetModPlayer<Charred_Life>().AllZenSet = true;
			player.magicDamage += 0.10f;
			player.manaCost -= 0.10f;
			/* Here are the individual weapon class bonuses.
			player.meleeDamage -= 0.2f;
			player.thrownDamage -= 0.2f;
			player.rangedDamage -= 0.2f;
			player.magicDamage -= 0.2f;
			player.minionDamage -= 0.2f;
			*/
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 17);
			recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 18);
			recipe.AddTile(mod.TileType("ZCC_PLACED"));
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
