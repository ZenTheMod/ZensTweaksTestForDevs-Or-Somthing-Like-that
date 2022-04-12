using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Loot
{
    public class Toxiboom : ModProjectile
    {
        public override string GlowTexture => "ZensTweakstest/Items/NewNonZen/Erichus/Loot/Toxiboom";

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.width = 176;
            projectile.height = 147;

            projectile.penetrate = -1;

            projectile.friendly = true;
            projectile.hostile = false;

            projectile.alpha = 20;

            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 5)
                {
                    projectile.Kill();
                }
            }
        }
    }
}
