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

namespace ZensTweakstest.Items.NewZenStuff.Bosses
{
    public class MoreFXGlow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.timeLeft = 230;
            projectile.width = 260;
            projectile.height = 260;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.aiStyle = -1;
            projectile.scale = 0.1f;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        private float scale = 1.3f;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            projectile.alpha += 5;
            scale -= 0.03f;
            Texture2D Portal = mod.GetTexture("Items/NewZenStuff/Bosses/MoreFXGlow");
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(Portal, projectile.position - Main.screenPosition, null, lightColor * (1f - projectile.alpha / 255f), 0, new Vector2(Portal.Width / 2f, Portal.Height / 2f), scale, SpriteEffects.None, 0f);
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
    }
}
