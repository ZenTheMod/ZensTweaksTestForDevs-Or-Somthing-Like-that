using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Config;
using ZensTweakstest.Items.JupiterStuff;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using static ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot.ZenSlash;
using System;
using ZensTweakstest.Helper;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
	public class ZenedSlanger : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shimazu Masamune");
			Tooltip.SetDefault("Peirces 2 times!");  //The (English) text shown below your weapon's name
		}

		public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;
		public override void SetDefaults()
		{
			item.damage = 50; // The damage your item deals
			item.melee = true; // Whether your item is part of the melee class
			item.width = 42; // The item texture's width
			item.height = 78; // The item texture's height
			item.useTime = 20; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
			item.useAnimation = 20; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
			item.knockBack = 6; // The force of knockback of the weapon. Maximum is 20
			item.value = Item.buyPrice(gold: 2); // The value of the weapon in copper coins
			item.rare = ItemRarityID.Orange; // The rarity of the weapon, from -1 to 13. You can also use ItemRarityID.TheColorRarity
			item.UseSound = SoundID.DD2_SonicBoomBladeSlash; // The sound when the weapon is being used
			item.shoot = ModContent.ProjectileType<SwordForZen>();
			item.shootSpeed = 7;
			item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button
			item.crit = 6; // The critical strike chance the weapon has. The player, by default, has 4 critical strike chance
			item.useStyle = 5; // 1 is the useStyle
			item.noUseGraphic = true;
			item.noMelee = true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<ZenSlash>(), 10, knockBack, Main.myPlayer);
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe ppdr = new ModRecipe(mod);
			ppdr.AddIngredient(ModContent.ItemType<ZenStone_I>(), 40);
			ppdr.AddIngredient(ModContent.ItemType<CursedBone>(), 10);
			ppdr.AddTile(TileID.DemonAltar);
			ppdr.SetResult(this, 1);
			ppdr.AddRecipe();
		}
	}
	public class ZenSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zened Slash");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{

			projectile.penetrate = 2;
			projectile.CloneDefaults(ProjectileID.DD2SquireSonicBoom);
			projectile.width = 80;
			projectile.height = 23;
			aiType = ProjectileID.DD2SquireSonicBoom;
		}
		public override bool PreKill(int timeLeft)
		{
			projectile.type = ProjectileID.DD2SquireSonicBoom;
			return true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}

			Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/NewZenStuff/Bosses/Loot/BagLoot/ZenSlashEffect2");
			Vector2 position = projectile.position - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height - texture.Height * 0.5f + 2f);
			// We redraw the item's sprite 4 times, each time shifted 2 pixels on each direction, using Main.DiscoColor to give it the color changing effect
			for (int i = 0; i < 4; i++)
			{
				Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
				spriteBatch.Draw(texture, position + offsetPositon, null, Color.IndianRed, projectile.rotation, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			}
			// Return true so the original sprite is drawn right after
			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < (50); i++)
			{
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<ZenStoneFlameDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			}
		}
	}
	public class SwordForZen : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sword for zen");
		}
		public override void SetDefaults()
		{
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = false;
			projectile.width = 64;
			projectile.height = 64;
			projectile.aiStyle = -1;
			projectile.timeLeft = 60;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 35;
		}

		public float Balls { get => projectile.ai[0]; set => projectile.ai[0] = value; } // the max rot
																						 // for hitbox , mess around with this until you find the perfect one zen
		const float width = 64;
		const float height = 64;
		const float hitboxdistance = 15f;

		public override void AI()
		{
			// player
			Player player = Main.player[projectile.owner];

			// kill if player is ded or rotation is reached the limit
			bool shouldDed = false;
			if (projectile.melee)
			{
				float distance = Vector2.Distance(new Vector2(0, MathHelper.ToDegrees(projectile.rotation)), new Vector2(0, MathHelper.ToDegrees(Balls)));
				shouldDed = distance < 15f;
			}

			// all of the crap is handled in Helpme.cs 
			if (!player.active || player.dead || player.noItems || player.CCed || player.HeldItem.IsAir || shouldDed)
			{
				projectile.Kill();
				return;
			}
			// initialize
			if (!projectile.melee)
			{
				projectile.melee = true;
				// setting direction
				projectile.spriteDirection = player.direction;
				projectile.spriteDirection = (player.HeldItem.isBeingGrabbed ? 1 : -1) * projectile.spriteDirection;

				//setting rotation
				projectile.rotation = projectile.velocity.ToRotation();
				// rotation is based on item animation
				int minRot = 90;
				int maxRot = 90;

				// setting max and min rot
				Balls = projectile.rotation + MathHelper.ToRadians(maxRot) * projectile.spriteDirection;
				projectile.rotation -= MathHelper.ToRadians(minRot) * projectile.spriteDirection;
				projectile.spriteDirection = projectile.velocity.X > 1f ? 1 : -1;
			}
			// update projectile
			UpdateGraphic(player);
			projectile.timeLeft = 5;
		}

		void UpdateGraphic(Player player)
		{
			// update basic stuff
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.direction = projectile.direction;

			// do not set to 0.5 above
			float speed = 0.2f;

			// make hitbox based on width and height
			var cache = projectile.Center;
			projectile.width = (int)(width * projectile.scale);
			projectile.height = (int)(height * projectile.scale);
			projectile.Center = cache;

			// update rotation and pos
			projectile.rotation = MathHelper.Lerp(projectile.rotation, Balls, speed);
			projectile.velocity = projectile.rotation.ToRotationVector2() * 0.9f;
			projectile.Center = player.Center + projectile.velocity * hitboxdistance;

			// update player rotation based on velocity
			player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * player.direction, projectile.velocity.X * player.direction);
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			var player = Main.player[projectile.owner];
			player.HeldItem.isBeingGrabbed = !player.HeldItem.isBeingGrabbed;
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{

			//get player
			Player player = Main.player[projectile.owner];
			if (!player.active || player.dead || player.HeldItem.IsAir || player.noItems || player.CCed) { return false; }

			// var setup
			int dir = projectile.spriteDirection;
			if (!player.HeldItem.isBeingGrabbed) { dir = -1; }
			Texture2D texture = Main.projectileTexture[projectile.type];
			SpriteEffects spriteEffects = dir == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			float rot = projectile.velocity.ToRotation() + (MathHelper.ToRadians(30) * dir);
			// rotate 180 degree if the sword is when its the right side.
			if (dir == -1) { rot += MathHelper.ToRadians(180); }
			Vector2 pos = player.Center + projectile.velocity - Main.screenPosition;
			Color color = projectile.GetAlpha(lightColor);

			// calculate orig
			Vector2 orig = dir == 1 ? new Vector2(0, texture.Height) : new Vector2(texture.Width, texture.Height);
			// draw
			spriteBatch.Draw(texture, pos, null, color, rot, orig, projectile.scale, spriteEffects, 0);
			// dont draw vanilla way
			return false;
		}
	}
}
