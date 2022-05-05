using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.Dusts;

namespace ZensTweakstest.Items.NewZenStuff.Projectilles
{
    public class ZenSwordBeamEvil : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen Beam");
        }

        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.tileCollide = false;
            projectile.aiStyle = 27;
            projectile.hostile = true;
            projectile.penetrate = 5;
            projectile.magic = true;
            projectile.light = 1;
            projectile.timeLeft = 425;
        }
        public override void AI()
        {
            float strength = 2f;
            Lighting.AddLight(projectile.position, Color.Red.ToVector3() * strength);
            projectile.velocity.Y += projectile.ai[0];
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<ZenStoneDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<ZenStoneDust>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Main.PlaySound(SoundID.Item118, projectile.position);
        }
    }
}
