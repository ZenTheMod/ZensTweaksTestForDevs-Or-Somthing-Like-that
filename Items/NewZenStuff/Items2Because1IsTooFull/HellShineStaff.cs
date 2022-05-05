using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Helper;
using Terraria.Graphics.Shaders;
using ZensTweakstest.Items.NewZenStuff.Bosses;

namespace ZensTweakstest.Items.NewZenStuff.Items2Because1IsTooFull
{
    public class HellShineStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Rise lava from below.");
			Item.staff[item.type] = true;
		}
        public override void SetDefaults()
        {
			item.damage = 90;
			item.magic = true;
			item.knockBack = 12;
			item.noMelee = true;
			item.mana = 10;

			item.width = 52;
			item.height = 52;
			item.scale = 1f;

			item.useTime = 60;
			item.useAnimation = 60;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Yellow;
			item.UseSound = SoundID.Item39;
			item.autoReuse = true;
			item.useTurn = false;

			item.shoot = ModContent.ProjectileType<ProjFollow>();
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld + new Vector2(29, 10);
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
	}
	public class ProjFollow : ModProjectile
    {
		public override void SetDefaults()
		{
			projectile.timeLeft = 500;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.aiStyle = -1;
			projectile.scale = 1f;
			projectile.width = 10;
			projectile.height = 10;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.penetrate = 100;
		}
		public override void AI()
        {
			Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<lavestave>(), 35, 9f, projectile.owner);
			Vector2 direction = projectile.DirectionTo(Main.MouseWorld);  //Get a direction to the player from the NPC
			projectile.velocity += direction * 15f / 60f;//SPEEEED
			if (projectile.velocity.LengthSquared() > 15 * 15)
				projectile.velocity = Vector2.Normalize(projectile.velocity) * 15;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			return false;
        }
	}
	public class lavestave : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.timeLeft = 230;
			projectile.width = 110;
			projectile.height = 110;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.aiStyle = -1;
			projectile.scale = 1f;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.penetrate = 100;
		}
		private float scale = 1.3f;
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			projectile.alpha += 5;
			scale -= 0.03f;
			Texture2D Portal = mod.GetTexture("Items/NewZenStuff/Bosses/MoreFXGlow");
			Main.spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(Portal, projectile.Center - Main.screenPosition, null, lightColor * (1f - projectile.alpha / 255f), 0, new Vector2(Portal.Width / 2f, Portal.Height / 2f), scale, SpriteEffects.None, 0f);
			Main.spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
		public override void AI()
		{
			if (scale < 0f)
			{
				projectile.Kill();
			}
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			int debuff = GetDebuff();
			if (debuff >= 0)
			{
				target.AddBuff(debuff, 10, true);
			}
		}
        public int GetDebuff()
		{
			return BuffID.OnFire;
		}
	}
}
