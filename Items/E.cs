using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ZensTweakstest.Config;
using ZensTweakstest.Helper;
using QwertysRandomContent;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items
{
	public class E : ModItem
	{
		Color[] cycleColors = new Color[]{
			new Color(255, 231, 66, 100),
			new Color(0, 242, 170, 100),
			new Color(255, 31, 174, 100),
			new Color(35, 200, 254, 100)
		};
		public override bool CloneNewInstances => true;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bane of The Lord");
			Tooltip.SetDefault("Strike enemies to charge the blade"
				+ "\n'Born from the very essence of existence'");
		}
		public override void SetDefaults()
		{
			item.damage = 250;
			item.knockBack = 7;
			item.melee = true;

			item.width = item.height = 32;
			item.scale = 1.5f;

			item.useTime = 9;
			item.useAnimation = 9;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;

			item.value = Item.sellPrice(gold: 12, silver: 7);
			item.rare = ItemRarityID.Red;

			if (!Main.dedServ)
				item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/MoonlordsBaneGlow");
		}

		public int MoonlordsBaneCharge;
		private bool canCharge = true;

		public override void UpdateInventory(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				if (MoonlordsBaneCharge >= 420)
				{
					canCharge = false;

					player.CameraShake(1, 420);
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Overdrive"), player.Center);
					CombatText.NewText(player.getRect(), Color.Red, "Overdrive!", true);
					item.useTime -= item.useTime / 3;
					item.useAnimation -= item.useTime / 3;
					item.damage *= 2;
					item.crit *= 4;
				}

				if (MoonlordsBaneCharge <= 0)
				{
					canCharge = true;
					item.useTime = 9;
					item.useAnimation = 9;
					item.damage = 250;
					item.crit = 0;
				}

				if (MoonlordsBaneCharge > 420)
                {
					MoonlordsBaneCharge = 420;
                }

				if (!canCharge)
				{
					MoonlordsBaneCharge--;
				}
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var chargeMeterTooltip = new TooltipLine(mod, "MoonlordsBaneCharge", $"Current charge: { MoonlordsBaneCharge }");
			chargeMeterTooltip.overrideColor = Color.Red;
			tooltips.Add(chargeMeterTooltip);
		}

		public override TagCompound Save()
		{
			return new TagCompound
			{
				[nameof(MoonlordsBaneCharge)] = MoonlordsBaneCharge,
			};
		}

		public override void Load(TagCompound tag)
		{
			MoonlordsBaneCharge = tag.GetInt(nameof(MoonlordsBaneCharge));
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(MoonlordsBaneCharge);
		}

		public override void NetRecieve(BinaryReader reader)
		{
			MoonlordsBaneCharge = reader.ReadInt32();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (canCharge)
			{
				MoonlordsBaneCharge += 7;
				CombatText.NewText(player.getRect(), Color.Red, "+1");
			}

			if (crit || !canCharge)
			{
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/nightBeam"));

				for (int k = 0; k < 1; k++)
				{
					Vector2 circleEdge = Main.rand.NextVector2CircularEdge(10f, 10f);
					Projectile.NewProjectile(target.Center + circleEdge * 20, circleEdge * -2, ModContent.ProjectileType<MoonBeam>(), damage / 2, knockBack / 2, player.whoAmI);
				}
			}
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/MoonlordsBaneGlow");
			spriteBatch.Draw
			(
				texture,
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale,
				SpriteEffects.None,
				0f
			);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ItemID.TerraBlade);
			recipe.AddIngredient(ItemID.Meowmere);
			recipe.AddIngredient(ItemID.LunarBar, 12);
			recipe.AddIngredient(ItemID.FragmentSolar, 6);
			recipe.AddIngredient(ItemID.FragmentNebula, 6);
			recipe.AddIngredient(ItemID.FragmentVortex, 6);
			recipe.AddIngredient(ItemID.FragmentStardust, 6);

			recipe.AddTile(TileID.LunarCraftingStation);

			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	public class MoonBeam : ModProjectile
	{
		Color[] cycleColors = new Color[]{
			new Color(255, 231, 66, 100),
			new Color(0, 242, 170, 100),
			new Color(255, 31, 174, 100),
			new Color(35, 200, 254, 100)
		};

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.penetrate = -1;
			projectile.ranged = true;

			projectile.friendly = true;
			projectile.hostile = false;

			projectile.width = projectile.height = 64;
			projectile.scale = 1f;

			projectile.alpha = 50;

			projectile.ignoreWater = true;
			projectile.tileCollide = false;

			projectile.aiStyle = 0;
			projectile.timeLeft = 20;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;

			if (projectile.timeLeft == 20)
			{
				for (int i = 0; i < 50; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 0.5f);

					Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.RainbowMk2, speed.RotatedBy(projectile.velocity.ToRotation() + MathHelper.PiOver2) * 5, 0, Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 4], fade), 1.5f);
					dust.noGravity = true;
				}
			}

			Dust dust1 = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.RainbowMk2, projectile.velocity.X / 2, projectile.velocity.Y / 2, 0, Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 4], fade), 1.5f);
			dust1.noGravity = true;
		}

		float fade = Main.GameUpdateCount % 60 / 60f;
		int index = (int)(Main.GameUpdateCount / 60 % 4);

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 4], fade);
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 50; i++)
			{
				Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 0.5f);

				Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.RainbowMk2, speed.RotatedBy(projectile.velocity.ToRotation() + MathHelper.PiOver2) * 5, 0, Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 4], fade), 1.5f);
				dust.noGravity = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);

			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}