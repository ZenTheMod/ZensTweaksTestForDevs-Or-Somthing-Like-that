using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ZensTweakstest.Helper;
using ZensTweakstest;
using QwertysRandomContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.NewNonZen
{
    public class slimy : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Faceless");
			Tooltip.SetDefault("Control spirits of the hyper cold." +
				"\nDedicated to NoFace Music");
		}
		public override void SetDefaults()
		{
			item.damage = 81;
			item.knockBack = 10;
			item.melee = true;

			item.width = 62;
			item.height = 62;
			item.scale = 1.0f;

			item.useTime = 11;
			item.useAnimation = 11;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;

			item.value = Item.sellPrice(gold: 9, silver: 99);
			item.rare = ItemRarityID.Lime;
			item.GetGlobalItem<ItemUseGlow>().glowTexture = mod.GetTexture("Items/NewNonZen/slimy_Glow");
			item.shoot = ModContent.ProjectileType<SpiritFace>();
			item.shootSpeed = 6;
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/NewNonZen/slimy_Glow");
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
	}
	public class SpiritFace : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.timeLeft = 100;
			projectile.melee = true;

			projectile.width = 24;
			projectile.height = 46;
			projectile.scale = 1.3f;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.ownerHitCheck = true;
			projectile.tileCollide = false;
		}
        public override void Kill(int timeLeft)
        {
			Player player = Main.player[projectile.owner];
			Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<SpiritBoom>(), projectile.damage, 2, projectile.owner);
			player.CameraShake(2, 15);
			Main.PlaySound(SoundID.Item74, (int)projectile.position.X, (int)projectile.position.Y);
		}
        public override void AI()
        {
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
	}
	public class SpiritBoom : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			projectile.width = 52;
			projectile.height = 52;
			projectile.scale = 1;

			projectile.alpha = 20;

			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}

		public override void AI()
		{
			if (++projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 7)
				{
					projectile.Kill();
				}
			}
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
