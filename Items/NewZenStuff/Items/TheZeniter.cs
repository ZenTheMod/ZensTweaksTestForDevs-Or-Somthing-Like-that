using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Projectilles;
using ZensTweakstest.Config;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;
using ZensTweakstest.Items.NewZenStuff.Bosses;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class TheZeniter : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yo-Yang");
			Tooltip.SetDefault(@"Shoots out a Zenitrin yoyo
Very Dusty");

			// These are all related to gamepad controls and don't seem to affect anything else
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.width = 24;
			item.height = 24;
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 17f;
			item.knockBack = 2.5f;
			item.damage = 125;
			item.rare = 8;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = Item.sellPrice(silver: 15);
			item.shoot = ModContent.ProjectileType<TheZeniterProjectile>();
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override void AddRecipes()
		{
			ModRecipe POOP = new ModRecipe(mod);

			POOP.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 5);
			POOP.AddIngredient(ModContent.ItemType<ZenStone_I>(), 10);
			POOP.AddIngredient(ModContent.ItemType<Zen_Peeve_Essence>(), 15);
			POOP.AddIngredient(ItemID.Cobweb, 20);
			POOP.AddTile(ModContent.TileType<ZCC_PLACED>());
			POOP.SetResult(this);
			POOP.AddRecipe();
		}
	}
	public class TheZeniterProjectile : ModProjectile
    {
		private float RotationE = 0;
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Vector2 og = new Vector2(32, 32) / 2f;
			RotationE += 0.1f;
			spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Items/OrbitYOYO"), projectile.Center - Main.screenPosition, null, new Color(254, 84, 92), RotationE, og, 1.5f, SpriteEffects.None, 0f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, Main.projectileTexture[projectile.type].Frame(1, Main.projFrames[projectile.type], 0, projectile.frame), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
        public override void SetStaticDefaults()
        {
			// The following sets are only applicable to yoyo that use aiStyle 99.
			// YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
			// Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1;
			// YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
			// Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 340f;
			// YoyosTopSpeed is top speed of the yoyo projectile. 
			// Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 18f;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override void SetDefaults()
		{
			projectile.extraUpdates = 0;
			projectile.width = 12;
			projectile.height = 12;
			// aiStyle 99 is used for all yoyos, and is Extremely suggested, as yoyo are extremely difficult without them
			projectile.aiStyle = 99;
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.scale = 1f;
			projectile.ownerHitCheck = true;
			projectile.damage = 125;
		}
		public override void PostAI()
        {
			float strength = 2f;
			Lighting.AddLight(projectile.position, Color.Red.ToVector3() * strength);
			Vector2 shootPos = projectile.Center;
			if (Main.rand.Next(0, 50) == 7)
            {
				for (int i = 0; i < 15; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
					Dust dust;
					dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.LifeDrain, speed.X, speed.Y, 0, default, 1.5f);
					dust.noGravity = true;
				}
				Projectile.NewProjectile(shootPos.X, shootPos.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("RedYang"), projectile.damage, 5f, Main.myPlayer);
            }
			if (Main.rand.Next(0, 50) == 7)
            {
				for (int i = 0; i < 15; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
					Dust dust;
					dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Granite, speed.X, speed.Y, 0, default, 1.5f);
					dust.noGravity = true;
				}
				Projectile.NewProjectile(shootPos.X, shootPos.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("BlackYang"), projectile.damage, 5f, Main.myPlayer);
			}
		}
	}
	public class RedYang : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zen Yang");
			Main.projFrames[projectile.type] = 4;
			// Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
			//ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.alpha = 255;
			projectile.aiStyle = 0;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.scale = 0.5f;
			projectile.timeLeft = 400;
		}
		public override void AI()
        {
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}
			Dust dust;
			dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.LifeDrain, projectile.velocity.X, projectile.velocity.Y, 0, default, 1.5f);
			dust.noGravity = true;
			float speed = 10f;
			float inertia = 40f;
			float distanceFromTarget = 700f;
			Vector2 targetCenter = projectile.Center;
			bool foundTarget = false;
			if (!foundTarget)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}
			Vector2 direction = targetCenter - projectile.Center;
			projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 30; i++)
			{
				Dust dust;
				dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.LifeDrain, projectile.velocity.X, projectile.velocity.Y, 0, default, 1.5f);
				dust.noGravity = true;
			}
		}
	}
	public class BlackYang : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zen Yang");
			Main.projFrames[projectile.type] = 4;
			// Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
			//ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.alpha = 255;
			projectile.aiStyle = 0;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.scale = 0.5f;
			projectile.timeLeft = 400;
		}
		public override void AI()
		{
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}
			Dust dust;
			dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Granite, projectile.velocity.X, projectile.velocity.Y, 0, default, 1.5f);
			dust.noGravity = true;
			float speed = 10f;
			float inertia = 40f;
			float distanceFromTarget = 700f;
			Vector2 targetCenter = projectile.Center;
			bool foundTarget = false;
			if (!foundTarget)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}
			Vector2 direction = targetCenter - projectile.Center;
			projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 30; i++)
			{
				Dust dust;
				dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Granite, projectile.velocity.X, projectile.velocity.Y, 0, default, 1.5f);
				dust.noGravity = true;
			}
		}
	}
}
