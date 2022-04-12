using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Loot
{
	public class ToxicGrenadeProj : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults() 
		{
			projectile.thrown = true;

			projectile.width = 26;
			projectile.height = 26;
			projectile.scale = 1f;

			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			projectile.ignoreWater = false;

			projectile.aiStyle = 2;
		}

        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.GreenFairy, new Vector2(0, 0));
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Toxiblast>(), projectile.damage, 2, projectile.owner);
            Main.PlaySound(SoundID.Item14, (int)projectile.position.X, (int)projectile.position.Y);

            const int NUM_DUSTS = 50;

            for (int i = 0; i < NUM_DUSTS; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(10f, 10f);
                Dust.NewDustPerfect(projectile.Center, DustID.GreenFairy, speed);
            }

            for (int i = 0; i < NUM_DUSTS; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.GreenFairy, speed * 5, Scale: 1.5f);
                dust.noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(projectile.Center - new Vector2(0, 40), new Vector2(0, 0), ModContent.ProjectileType<Toxiboom>(), projectile.damage, 2, projectile.owner);
            Main.PlaySound(SoundID.Item14, (int)projectile.position.X, (int)projectile.position.Y);

            const int NUM_DUSTS = 50;

            for (int i = 0; i < NUM_DUSTS; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(10f, 10f);
                Dust.NewDustPerfect(projectile.Center, DustID.GreenFairy, speed);
            }

            for (int i = 0; i < NUM_DUSTS; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.GreenFairy, speed * 5, Scale: 1.5f);
                dust.noGravity = true;
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/Loot/ToxicGrenadeAI"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);//projectile.scale - k / (float)projectile.oldPos.Length
            }
            return true;
        }
    }
}