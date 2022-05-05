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
        public int RandProjSprite = Main.rand.Next(1, 9);
        Color[] cycleColors = new Color[]{
            new Color(87, 0, 219),
            new Color(0, 0, 0)
        };
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
            projectile.aiStyle = 2;
        }
        public override void PostAI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int j = 0; j < 50; j++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(1f, 1f);
                Dust.NewDustPerfect(projectile.Center, DustID.Silver, speed * 10, 0, Color.DarkGray, 1f);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            Vector2 drawPos = projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            float fade = Main.GameUpdateCount % 60 / 60f;
            int index = (int)(Main.GameUpdateCount / 60 % 2);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/HMmechZen/ProjGlow"), drawPos, null, Color.Lerp(cycleColors[index], cycleColors[(index + 1) % 2], fade), projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            Texture2D Proj = null;
            if (RandProjSprite < 5)
            {
                Proj = ModContent.GetTexture("ZensTweakstest/Items/HMmechZen/PortalProj");
            }
            else if (RandProjSprite == 8)
            {
                Proj = ModContent.GetTexture("ZensTweakstest/Items/HMmechZen/PortalProj2");
            }
            else if (RandProjSprite == 7 || RandProjSprite == 6)
            {
                Proj = ModContent.GetTexture("ZensTweakstest/Items/HMmechZen/PortalProj4");
            }
            else if (RandProjSprite == 5)
            {
                Proj = ModContent.GetTexture("ZensTweakstest/Items/HMmechZen/PortalProj3");
            }
            spriteBatch.Draw(Proj, drawPos, null, Color.White, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
