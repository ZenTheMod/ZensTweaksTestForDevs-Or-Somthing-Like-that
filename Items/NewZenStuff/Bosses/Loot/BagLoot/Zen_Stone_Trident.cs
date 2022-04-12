using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.HMmechZenItems;
using ZensTweakstest.Items.NewZenStuff.Projectilles;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
	public class Zen_Stone_Trident : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Flame Trident");
			Tooltip.SetDefault("Fires things from the cursor.");
		}

		public override void SetDefaults()
		{
			item.damage = 100;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 20;
			item.useTime = 26;
			item.shootSpeed = 3f;
			item.knockBack = 6.5f;
			item.width = 44;
			item.height = 44;
			item.scale = 1f;
			item.rare = ItemRarityID.Yellow;
			item.value = Item.sellPrice(silver: 75);

			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<Zen_Stone_Trident_PROJECTILLE>();
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			for (int i = 0; i < 7; i++)
            {
				Vector2 circleEdge = Main.rand.NextVector2CircularEdge(10f, 10f);
				Projectile.NewProjectile(Main.MouseWorld + circleEdge * 16, -circleEdge * 3, ModContent.ProjectileType<PokerBeam>(), item.damage, item.knockBack, Main.myPlayer);
			}
			return true;
        }

        public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}
    }
}
