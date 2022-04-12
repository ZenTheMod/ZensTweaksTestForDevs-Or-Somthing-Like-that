using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;
using System.Numerics;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Loot
{
    public class Toxyth : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.penetrate = 1;

            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.friendly = true;
            projectile.hostile = false;
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
        }

        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.GreenFairy, new Vector2(0, 0));
            dust.scale = 0.4f;
            //Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy, projectile.velocity.X * -1, projectile.velocity.Y * -1);
            Lighting.AddLight(projectile.position, 0, 1, 0.3f);

            //Rotation By Jupiter VVVVV : hi
            projectile.rotation += 0.5f;

            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 400f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                projectile.velocity = (15 * projectile.velocity + move) / 16f;
                AdjustMagnitude(ref projectile.velocity);
            }
            for (int i = 0; i < 3; i++)
            {
                Vector2 offset = new Vector2(0, -20).RotatedBy(projectile.rotation);
                offset = offset.RotatedByRandom(MathHelper.Pi / 6);
                int d = Dust.NewDust(projectile.Center, 0, 0, DustID.GreenFairy, 0f, 0f, 150);
                Main.dust[d].position += offset;
                float velrando = Main.rand.Next(20, 31) / 10;
                Main.dust[d].velocity = projectile.velocity / velrando;
                Main.dust[d].noGravity = true;
            }
            for (int i = 0; i < 3; i++)
            {
                Vector2 offset = new Vector2(0, -20).RotatedBy(-projectile.rotation);
                offset = offset.RotatedByRandom(MathHelper.Pi / 6);
                int d = Dust.NewDust(projectile.Center, 0, 0, DustID.GreenFairy, 0f, 0f, 150);
                Main.dust[d].position += offset;
                float velrando = Main.rand.Next(20, 31) / 10;
                Main.dust[d].velocity = projectile.velocity / velrando;
                Main.dust[d].noGravity = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/Loot/Toxyth"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 15f)
            {
                vector *= 15f / magnitude;
            }
        }
    }
}