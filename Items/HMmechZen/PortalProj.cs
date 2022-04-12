using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ZensTweakstest.Config;
using ZensTweakstest.Helper;
using QwertysRandomContent;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.HMmechZen
{
    public class PortalProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 38;
            projectile.scale = 1.3f;

            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = false;
            projectile.aiStyle = 0;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor * 0.6f;
        }
        public override void PostAI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 212, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0, Main.DiscoColor);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 55; i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 212, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0, Main.DiscoColor);
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
    }
}
