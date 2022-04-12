using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Loot
{
    public class ToxicBarrel : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 120;
			item.magic = true;
			item.knockBack = 4;
			item.noMelee = true;

			item.width = 26;
			item.height = 26;
			item.scale = 1.5f;
			item.mana = 8;

			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(gold: 12);
			item.rare = ItemRarityID.Pink;
			item.UseSound = SoundID.Item71;
			item.autoReuse = true;
			item.useTurn = false;

			item.shoot = ModContent.ProjectileType<Toxyth>();
			item.shootSpeed = 15f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberofproj = 8;
			for (int i = 0; i < numberofproj; i++)
			{
				Vector2 spawnPos = new Vector2(player.position.X, player.position.Y);
				Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

				VelPos.Normalize();
				Projectile.NewProjectile(spawnPos, VelPos * 15f, ModContent.ProjectileType<Toxyth>(), item.damage, 5f, player.whoAmI);
			}
			return false;
		}
	}
}
