﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Tiles;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot.Solutions
{
	public class DeepBlackSolutionProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zen-Stone Spray");
		}

		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.extraUpdates = 2;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}

		public override void AI()
		{
			//Set the dust type to ExampleSolution
			int dustType = ModContent.DustType<Dusts.DeepBlackSolutionDust>();

			if (projectile.owner == Main.myPlayer)
				Convert((int)(projectile.position.X + projectile.width / 2) / 16, (int)(projectile.position.Y + projectile.height / 2) / 16, 2);

			if (projectile.timeLeft > 133)
				projectile.timeLeft = 133;

			if (projectile.ai[0] > 7f)
			{
				float dustScale = 1f;

				if (projectile.ai[0] == 8f)
					dustScale = 0.2f;
				else if (projectile.ai[0] == 9f)
					dustScale = 0.4f;
				else if (projectile.ai[0] == 10f)
					dustScale = 0.6f;
				else if (projectile.ai[0] == 11f)
					dustScale = 0.8f;

				projectile.ai[0] += 1f;

				for (int i = 0; i < 1; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
					Dust dust = Main.dust[dustIndex];
					dust.noGravity = true;
					dust.scale *= 1.75f;
					dust.velocity.X *= 2f;
					dust.velocity.Y *= 2f;
					dust.scale *= dustScale;
				}
			}
			else
				projectile.ai[0] += 1f;

			projectile.rotation += 0.3f * projectile.direction;
		}

		public void Convert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
					{
						int type = Main.tile[k, l].type;
						int wall = Main.tile[k, l].wall;

						if (TileID.Sets.Conversion.Stone[type])
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<ZenStone>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (TileID.Sets.Conversion.Moss[type])
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<ZenStone>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}

						if (TileID.Sets.Conversion.Sand[type])
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<ZenSand>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == TileID.Dirt)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<ZenDirtTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (TileID.Sets.Conversion.Grass[type])
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<ZenGrassTile>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
						if (type == TileID.Stalactite)
						{
							Main.tile[k, l].type = (ushort)ModContent.TileType<AmbeintZen1X2T>();
							//Main.tile[k, l - 1].type = (ushort)ModContent.TileType<AmbeintZen1X2T>();
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
					}
				}
			}
		}
	}
}
