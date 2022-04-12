using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ZensTweakstest.Items.EventItems;
using ZensTweakstest.Helper;
using QwertysRandomContent;

namespace ZensTweakstest.Items.EventItems
{
	public class Supernova : ModItem
	{
		Color[] cycleColors = new Color[] {
			new Color(65, 0, 255),
			new Color(251, 0, 255),
			new Color(255, 85, 0),
			new Color(255, 208, 0)
		};

        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Causes comets to fall from the sky"
				+ "\nCauses stars to fall from the sky on melee strikes"
				+ "\nCauses a Boulder to fall from the sky on critical melee strikes" +
				"\nMade By Event Winner: Pyxxl_RT");
        }

        public override void SetDefaults()
		{
			item.damage = 75;
			item.knockBack = 3;
			item.melee = true;

			item.shoot = ModContent.ProjectileType<SupernovaComet>();
			item.shootSpeed = 17f;

			item.width = 38;
			item.height = 38;
			item.scale = 1.5f;

			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item105;
			item.autoReuse = true;
			item.useTurn = false;

			item.value = Item.sellPrice(gold: 38, silver: 40);
			item.rare = ItemRarityID.Red;

			item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/EventItems/SupernovaGlow");
		}

		private float fade = Main.GameUpdateCount % 60 / 60f;
		private int index = (int)(Main.GameUpdateCount / 60 % 4);

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(1))
			{
				Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PortalBoltTrail, 0, 0, 0, Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 4], fade));
				dust.noGravity = true;
			}

			if (Main.rand.NextBool(2))
			{
				Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Granite);
				dust.noGravity = true;
			}

			if (Main.rand.NextBool(3))
			{
				Gore.NewGore(new Vector2(hitbox.Center.X, hitbox.Center.Y), Vector2.Zero, 16);
			}
		}


		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Vector2 targetPos = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			Vector2 position;
			float ceilingLimit = targetPos.Y;
			float speedX = 0;
			float speedY;
			int type;
			int numberProjectiles;
			int damageVal;

			if (crit)
			{
				speedY = 12;
				type = ModContent.ProjectileType<SupernovaBoulder>();
				numberProjectiles = 1;
				damageVal = damage * 2;
			}
			else
			{
				speedY = 20;
				type = ModContent.ProjectileType<SupernovaStar>();
				numberProjectiles = 3;
				damageVal = damage / 4;
			}

			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}

			for (int i = 0; i < numberProjectiles; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
				position.Y -= (100 * i);
				Vector2 heading = targetPos - position;

				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}

				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}

				heading.Normalize();
				heading *= new Vector2(speedX, speedY).Length();
				speedX = heading.X;
				speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;

				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damageVal, knockBack, player.whoAmI, 0f, ceilingLimit);
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float ceilingLimit = target.Y;

			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}

			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
				position.Y -= (100 * i);
				Vector2 heading = target - position;

				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}

				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}

				heading.Normalize();
				heading *= new Vector2(speedX, speedY).Length();
				speedX = heading.X;
				speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;

				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 2, knockBack, player.whoAmI, 0f, ceilingLimit);
			}

			return false;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine line2 in tooltips)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 4], fade);
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Starfury);
			recipe.AddIngredient(ItemID.StarWrath);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddIngredient(ItemID.FragmentSolar, 7);

			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
