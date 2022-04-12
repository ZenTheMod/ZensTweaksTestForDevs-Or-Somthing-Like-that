using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;

namespace ZensTweakstest.Items.NewZenStuff.Projectilles
{
    public class ZenitrinBobber : ModProjectile
    {
		private bool initialized = false;
		private Color fishingLineColor;
		public Color[] PossibleLineColors = new Color[]
		{
			new Color(235, 64, 52), //a gold color
			new Color(255, 112, 112) // a blue color
		};

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zenitrin Bobber");
		}

		public override void SetDefaults()
		{
			//These are copied through the CloneDefaults method
			//projectile.width = 14;
			//projectile.height = 14;
			//projectile.aiStyle = 61;
			//projectile.bobber = true;
			//projectile.penetrate = -1;
			projectile.CloneDefaults(ProjectileID.BobberWooden);
			drawOriginOffsetY = -8; // adjusts the draw position
		}

		//What if we want to randomize the line color
		public override void AI()
		{
			if (!initialized)
			{
				//Decide color of the pole by randomizing the array
				fishingLineColor = Main.rand.Next(PossibleLineColors);
				initialized = true;
			}
		}

		public override bool PreDrawExtras(SpriteBatch spriteBatch)
		{
			//Create some light based on the color of the line; this could also be in the AI function
			float strength = 2f;
			Lighting.AddLight(projectile.position, Color.Red.ToVector3() * strength);

			//Change these two values in order to change the origin of where the line is being drawn
			int xPositionAdditive = 45;
			float yPositionAdditive = 35f;

			Player player = Main.player[projectile.owner];
			if (!projectile.bobber || player.inventory[player.selectedItem].holdStyle <= 0)
				return false;

			Vector2 lineOrigin = player.MountedCenter;
			lineOrigin.Y += player.gfxOffY;
			int type = player.inventory[player.selectedItem].type;
			//This variable is used to account for Gravitation Potions
			float gravity = player.gravDir;

			if (type == ModContent.ItemType<ZenitrinPole>())
			{
				lineOrigin.X += xPositionAdditive * player.direction;
				if (player.direction < 0)
				{
					lineOrigin.X -= 13f;
				}
				lineOrigin.Y -= yPositionAdditive * gravity;
			}

			if (gravity == -1f)
			{
				lineOrigin.Y -= 12f;
			}
			// RotatedRelativePoint adjusts lineOrigin to account for player rotation.
			lineOrigin = player.RotatedRelativePoint(lineOrigin + new Vector2(8f), true) - new Vector2(8f);
			Vector2 playerToProjectile = projectile.Center - lineOrigin;
			bool canDraw = true;
			if (playerToProjectile.X == 0f && playerToProjectile.Y == 0f)
				return false;

			float playerToProjectileMagnitude = playerToProjectile.Length();
			playerToProjectileMagnitude = 12f / playerToProjectileMagnitude;
			playerToProjectile *= playerToProjectileMagnitude;
			lineOrigin -= playerToProjectile;
			playerToProjectile = projectile.Center - lineOrigin;

			// This math draws the line, while allowing the line to sag.
			while (canDraw)
			{
				float height = 12f;
				float positionMagnitude = playerToProjectile.Length();
				if (float.IsNaN(positionMagnitude) || float.IsNaN(positionMagnitude))
					break;

				if (positionMagnitude < 20f)
				{
					height = positionMagnitude - 8f;
					canDraw = false;
				}
				playerToProjectile *= 12f / positionMagnitude;
				lineOrigin += playerToProjectile;
				playerToProjectile.X = projectile.position.X + projectile.width * 0.5f - lineOrigin.X;
				playerToProjectile.Y = projectile.position.Y + projectile.height * 0.1f - lineOrigin.Y;
				if (positionMagnitude > 12f)
				{
					float positionInverseMultiplier = 0.3f;
					float absVelocitySum = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
					if (absVelocitySum > 16f)
					{
						absVelocitySum = 16f;
					}
					absVelocitySum = 1f - absVelocitySum / 16f;
					positionInverseMultiplier *= absVelocitySum;
					absVelocitySum = positionMagnitude / 80f;
					if (absVelocitySum > 1f)
					{
						absVelocitySum = 1f;
					}
					positionInverseMultiplier *= absVelocitySum;
					if (positionInverseMultiplier < 0f)
					{
						positionInverseMultiplier = 0f;
					}
					absVelocitySum = 1f - projectile.localAI[0] / 100f;
					positionInverseMultiplier *= absVelocitySum;
					if (playerToProjectile.Y > 0f)
					{
						playerToProjectile.Y *= 1f + positionInverseMultiplier;
						playerToProjectile.X *= 1f - positionInverseMultiplier;
					}
					else
					{
						absVelocitySum = Math.Abs(projectile.velocity.X) / 3f;
						if (absVelocitySum > 1f)
						{
							absVelocitySum = 1f;
						}
						absVelocitySum -= 0.5f;
						positionInverseMultiplier *= absVelocitySum;
						if (positionInverseMultiplier > 0f)
						{
							positionInverseMultiplier *= 2f;
						}
						playerToProjectile.Y *= 1f + positionInverseMultiplier;
						playerToProjectile.X *= 1f - positionInverseMultiplier;
					}
				}
				//This color decides the color of the fishing line. The color is randomized as decided in the AI.
				Color lineColor = Lighting.GetColor((int)lineOrigin.X / 16, (int)(lineOrigin.Y / 16f), fishingLineColor);
				float rotation = playerToProjectile.ToRotation() - MathHelper.PiOver2;
				Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(lineOrigin.X - Main.screenPosition.X + Main.fishingLineTexture.Width * 0.5f, lineOrigin.Y - Main.screenPosition.Y + Main.fishingLineTexture.Height * 0.5f), new Rectangle(0, 0, Main.fishingLineTexture.Width, (int)height), lineColor, rotation, new Vector2(Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
			}
			return false;
		}

		public class ZenitrinPole : ModItem
		{
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("Koi Rod");
				Tooltip.SetDefault("Can fish in lava.\n" +
					"The fishing line never snaps.");
				//Allows the pole to fish in lava
				ItemID.Sets.CanFishInLava[item.type] = true;
			}

			public override void SetDefaults()
			{
				//These are copied through the CloneDefaults method
				//item.useStyle = 1;
				//item.useAnimation = 8;
				//item.useTime = 8;
				//item.width = 24;
				//item.height = 28;
				//item.UseSound = SoundID.Item1;
				item.CloneDefaults(ItemID.WoodFishingPole);
				item.rare = 8;
				item.scale = 1.2f;
				//Sets the poles fishing power
				item.fishingPole = 75;

				//Sets the speed in which the bobbers are launched, Wooden Fishing Pole is 9f and Golden Fishing Rod is 17f
				item.shootSpeed = 20f;

				//The Bobber projectile
				item.shoot = ModContent.ProjectileType<ZenitrinBobber>();
			}
			public override void HoldItem(Player player)
			{
				player.accFishingLine = true;
			}

			public override void AddRecipes()
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 15);
				recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 5);
				recipe.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 25);
				recipe.AddTile(ModContent.TileType<ZCC_PLACED>());
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}
