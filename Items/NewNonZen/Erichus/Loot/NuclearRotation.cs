using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Collections.Generic;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ID;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Loot
{
    public class NuclearRotation : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(@"Spawns a Gwerp spinning fast around you.
This Gwerp will be distroyed on any enemy contact.");
        }
		public override void SetDefaults()
		{
			item.crit = 2;
			item.damage = 140; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			item.summon = true; // sets the damage type to ranged
			item.mana = 10;
			item.width = 40; // hitbox width of the item
			item.height = 20; // hitbox height of the item
			item.useTime = 30; // The item's use time in ticks (60 ticks == 1 second.)
			item.useAnimation = 30; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			item.useStyle = ItemUseStyleID.SwingThrow; // how you use the item (swinging, holding out, etc)
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			item.value = 10000; // how much the item sells for (measured in copper)
			item.rare = ItemRarityID.Pink; // the color that the item's name will be in-game
			item.UseSound = SoundID.Item11; // The sound that this item plays when used.
			item.autoReuse = true; // if you can hold click to automatically use it again
			item.shoot = ModContent.ProjectileType<GwerpSpeen>(); //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 0f; // the speed of the projectile (measured in pixels per frame)
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Projectile.NewProjectile(position, new Vector2(speedX,speedY), ModContent.ProjectileType<GwerpSpeen2>(), damage, knockBack, Main.myPlayer);
            return true;
        }
        public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return (player.ownedProjectileCounts[item.shoot] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<GwerpSpeen2>()] < 1);
		}
	}
	public class GwerpSpeen2 : ModProjectile
    {
        public override void Kill(int timeLeft)
        {
			Gore.NewGore(projectile.position, projectile.velocity, mod.GetGoreSlot("Gores/GwerpGore1"), projectile.scale);
			Gore.NewGore(projectile.position, projectile.velocity, mod.GetGoreSlot("Gores/GwerpGore2"), projectile.scale);
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy);
			}
			Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Toxiblast>(), projectile.damage, 2, projectile.owner);
		}
        public override void SetDefaults()
        {
			projectile.width = 26;
			projectile.height = 38;

			projectile.penetrate = 1;

			projectile.aiStyle = -1;

			projectile.friendly = true;
			projectile.hostile = false;

			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
        private double ERICHUS = 0.0;
        public override void AI()
        {
			Dust dust = Dust.NewDustPerfect(projectile.position + new Vector2(13, 19), DustID.GreenFairy, new Vector2(0, 0));
			projectile.rotation += 0.1f;
			if (projectile.rotation >= 2 * MathHelper.Pi)
            {
				projectile.rotation = 0.0f;
            }
			ERICHUS += 0.1;
			if (ERICHUS == 6.2)
			{
				ERICHUS = 0.0;
			}
			projectile.spriteDirection = Main.player[projectile.owner].direction * -1;
			projectile.Center = Main.player[projectile.owner].Center + Vector2.One.RotatedBy(ERICHUS) * 105;
		}
    }
	public class GwerpSpeen : ModProjectile
	{
		public override void Kill(int timeLeft)
		{
			Gore.NewGore(projectile.position, projectile.velocity, mod.GetGoreSlot("Gores/GwerpGore1"), projectile.scale);
			Gore.NewGore(projectile.position, projectile.velocity, mod.GetGoreSlot("Gores/GwerpGore2"), projectile.scale);
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy);
			}
			Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Toxiblast>(), projectile.damage, 2, projectile.owner);
		}
		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 38;

			projectile.scale = 1.5f;

			projectile.penetrate = 1;

			projectile.aiStyle = -1;

			projectile.friendly = true;
			projectile.hostile = false;

			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
		private double ERICHUS = 0.0;
		public override void AI()
		{
			Dust dust = Dust.NewDustPerfect(projectile.position + new Vector2(13, 19) * 1.5f, DustID.GreenFairy, new Vector2(0, 0));
			projectile.rotation += 0.1f;
			if (projectile.rotation >= 2 * MathHelper.Pi)
			{
				projectile.rotation = 0.0f;
			}
			ERICHUS += 0.1;
			if (ERICHUS == 6.2)
			{
				ERICHUS = 0.0;
			}
			projectile.spriteDirection = Main.player[projectile.owner].direction * -1;
			projectile.Center = Main.player[projectile.owner].Center + Vector2.One.RotatedBy(ERICHUS) * 65;
		}
	}
}
