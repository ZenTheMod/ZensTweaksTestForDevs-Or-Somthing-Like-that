using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using ZensTweakstest.Items;
using ZensTweakstest.Items.NewZenStuff.Items;
using Terraria;
using ZensTweakstest.Helper;

namespace ZensTweakstest.Items.NewZenStuff.Items2Because1IsTooFull
{
    public class ZenitrinGun : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 60; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			item.ranged = true; // sets the damage type to ranged
			item.width = 74; // hitbox width of the item
			item.height = 38; // hitbox height of the item
			item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
			item.useAnimation = 20; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			item.useStyle = ItemUseStyleID.HoldingOut; // how you use the item (swinging, holding out, etc)
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			item.value = 10000; // how much the item sells for (measured in copper)
			item.rare = ItemRarityID.Yellow; // the color that the item's name will be in-game
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/GlockShoot"); // The sound that this item plays when used.
			item.autoReuse = true; // if you can hold click to automatically use it again
			item.shoot = 10; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 12f; // the speed of the projectile (measured in pixels per frame)
			item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
			ItemUseGlow.AutoLoadGlow(item);
			ItemUseGlow.Offset(item, 1, 0);
		}
		public float alpha = 0f;
		private int timer;
		private bool retract;
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 pos, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Texture2D GlowTexture = ModContent.GetTexture(Texture + "_Glow");
			spriteBatch.Draw(GlowTexture, pos, frame, drawColor * alpha, 0f, origin, scale, SpriteEffects.None, 0f);
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			//PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
			Texture2D GlowTexture = ModContent.GetTexture(Texture + "_Glow");
			Vector2 pos = new Vector2(item.position.X - Main.screenPosition.X + item.width * 0.5f, item.position.Y - Main.screenPosition.Y + item.height - GlowTexture.Height * 0.5f + 2f);
			spriteBatch.Draw(GlowTexture, pos, null, alphaColor * alpha, rotation, GlowTexture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
		}
		public override void PostUpdate()
		{
			if (retract)
			{
				alpha -= 0.01f;
				timer = 0;
				if (alpha < 0f)
				{
					alpha = 0f;
					retract = false;
				}
			}
			if (alpha > 0f)
			{
				timer++;
				if (timer > 60 * 2)
				{
					retract = true;
					for (int i = 0; i < 10; i++)
					{
						Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
						Dust d = Dust.NewDustPerfect(item.Center, DustID.Clentaminator_Red, speed * 5, Scale: 1.5f);
						d.noGravity = true;
					}
					timer = 0;
				}
			}
		}
		public override void UpdateInventory(Player player)
		{
			if (retract)
			{
				alpha -= 0.04f;
				timer = 0;
				if (alpha < 0f)
				{
					alpha = 0f;
					retract = false;
				}
			}
			if (player.HeldItem == item)
			{
				//Main.NewText($"sus {timer}");
				if (retract)
				{
					player.itemRotation += player.direction * 0.01f;
				}

				timer++;
				if (timer > 60 * 2)
				{
					retract = true;
					timer = 0;
				}
			}
			else
			{
				alpha = 0f;
				timer = 0;
			}
		}
		void Explode(Vector2 position, int width, int height)
		{
			for (int num735 = 0; num735 < 20; num735++)
			{
				int num737 = Dust.NewDust(new Vector2(position.X, position.Y), width, height, 31, 0f, 0f, 100, default(Color), 1.5f);
				Dust dust39 = Main.dust[num737];
				Dust dust226 = dust39;
				dust226.velocity *= 1.4f;
			}
			for (int num738 = 0; num738 < 10; num738++)
			{
				int num739 = Dust.NewDust(new Vector2(position.X, position.Y), width, height, 6, 0f, 0f, 100, default(Color), 2.5f);
				Main.dust[num739].noGravity = true;
				Dust dust40 = Main.dust[num739];
				Dust dust226 = dust40;
				dust226.velocity *= 5f;
				num739 = Dust.NewDust(new Vector2(position.X, position.Y), width, height, 6, 0f, 0f, 100, default(Color), 1.5f);
				dust40 = Main.dust[num739];
				dust226 = dust40;
				dust226.velocity *= 3f;
			}
			int num740 = Gore.NewGore(new Vector2(position.X, position.Y), default(Vector2), Main.rand.Next(61, 64));
			Gore gore7 = Main.gore[num740];
			Gore gore40 = gore7;
			gore40.velocity *= 0.4f;
			Main.gore[num740].velocity.X += 1f;
			Main.gore[num740].velocity.Y += 1f;
			num740 = Gore.NewGore(new Vector2(position.X, position.Y), default(Vector2), Main.rand.Next(61, 64));
			gore7 = Main.gore[num740];
			gore40 = gore7;
			gore40.velocity *= 0.4f;
			Main.gore[num740].velocity.X -= 1f;
			Main.gore[num740].velocity.Y += 1f;
			num740 = Gore.NewGore(new Vector2(position.X, position.Y), default(Vector2), Main.rand.Next(61, 64));
			gore7 = Main.gore[num740];
			gore40 = gore7;
			gore40.velocity *= 0.4f;
			Main.gore[num740].velocity.X += 1f;
			Main.gore[num740].velocity.Y -= 1f;
			num740 = Gore.NewGore(new Vector2(position.X, position.Y), default(Vector2), Main.rand.Next(61, 64));
			gore7 = Main.gore[num740];
			gore40 = gore7;
			gore40.velocity *= 0.4f;
			Main.gore[num740].velocity.X -= 1f;
			Main.gore[num740].velocity.Y -= 1f;
		}
		public override Vector2? HoldoutOffset() => new Vector2(0, 0);
		public override float MeleeSpeedMultiplier(Player player) => 1f + alpha;
		public override float UseTimeMultiplier(Player player) => 1f + alpha;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (retract)
			{
				return false;
			}
			timer = 0;
			alpha += 0.05f;
			damage += (int)((float)damage * alpha);
			knockBack += alpha;
			//Main.NewText($"{alpha} {timer}");
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 20f;
			Vector2 muzzleOffset2 = Vector2.Normalize(new Vector2(speedX, speedY)) * 55f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			for (int i = 0; i < 5; i++)
			{
				Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
				Dust d = Dust.NewDustPerfect(position + muzzleOffset2, DustID.Clentaminator_Red, speed * 3, Scale: 1.5f);
				d.noGravity = true;
			}
			//for (int k = 0; k < 10; k++){
			//	Dust dust = Dust.NewDustPerfect(position + muzzleOffset2, DustID.Clentaminator_Red, new Vector2(Main.rand.NextFloat(-0.4f, 0.4f), Main.rand.NextFloat(-1f, -2f)));
			//}
			if (alpha > 1f)
			{
				if (Main.rand.NextBool(100))
				{
					CombatText.NewText(player.getRect(), Color.White, "Kapow");
				}
				for (int i = 0; i < 6; i++)
				{
					Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
					Projectile.NewProjectile(position, perturbedSpeed2, type, damage, knockBack, player.whoAmI);
				}
				Explode(position + muzzleOffset2, 10, 10);
				for (int i = 0; i < 10; i++)
				{
					Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
					Dust d = Dust.NewDustPerfect(position + muzzleOffset2, DustID.Clentaminator_Red, speed * 5, Scale: 1.5f);
					d.noGravity = true;
				}
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/DesertEagleShoot"), player.Center);
				player.CameraShake(5, 15);
				player.itemAnimation = 70;
				player.itemTime = 70;
				retract = true;
				player.velocity = new Vector2(speedX * -0.7f, speedY * -0.5f);
				return false;
			}
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 85);
			recipe.AddIngredient(ModContent.ItemType<ZenitrinBar>(), 20);
			recipe.AddTile(ModContent.TileType<Bosses.SparkArmor.ZCC_PLACED>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
