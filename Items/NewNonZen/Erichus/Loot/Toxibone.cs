using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Loot
{
    public class Toxibone : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.melee = true;

            projectile.width = 8;
            projectile.height = 8;
            projectile.scale = 1.2f;

            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = false;

            projectile.aiStyle = 2;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(1, 10))
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy, projectile.velocity.X / 10, projectile.velocity.Y / 10);
            }
            Lighting.AddLight(projectile.position, 0, 1, 0.3f);
        }

        int bounces = 1;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.DD2_SkeletonHurt, projectile.position);

            if (bounces <= 0)
            {
                projectile.Kill();
            }
            else
            {
                Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Item10, projectile.position);
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                bounces -= 1;
            }

            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Toxiblast>(), projectile.damage, 2, projectile.owner);
            Main.PlaySound(SoundID.Item14, (int)projectile.position.X, (int)projectile.position.Y);

            const int NUM_DUSTS = 10;

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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/Loot/ToxiboneAI"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
