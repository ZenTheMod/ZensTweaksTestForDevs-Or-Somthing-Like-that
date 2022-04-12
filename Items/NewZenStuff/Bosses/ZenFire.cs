using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace ZensTweakstest.Items.NewZenStuff.Bosses
{
    public class ZenFire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen Flame");
            Main.projFrames[projectile.type] = 6;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
        {
            projectile.width = 94;
            projectile.height = 50;
            projectile.alpha = 255;
            projectile.aiStyle = -1;
            projectile.timeLeft = 300;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Vector2 targetCenter = projectile.Center;
            bool foundTarget = false;
            
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<HolyDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            float speed = 12f;
            float inertia = 40f;
            float distanceFromTarget = 400f;

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (player.active && !player.dead)
                {
                    float between = Vector2.Distance(player.Center, projectile.Center);
                    distanceFromTarget = between;
                    targetCenter = player.Center;
                    foundTarget = true;
                    if (foundTarget)
                    {
                        projectile.rotation = projectile.DirectionTo(targetCenter).ToRotation();
                    }
                }
            }
            Vector2 direction = targetCenter - projectile.Center;
            direction.Normalize();
            direction *= speed;
            projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
            if (++projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 6)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < (Main.expertMode ? 55 : 50); i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<HolyDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }
    }
}
