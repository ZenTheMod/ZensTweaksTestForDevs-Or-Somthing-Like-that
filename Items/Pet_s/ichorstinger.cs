using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Pet_s
{
    public class ichorstinger : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Stingler");
            Main.projFrames[projectile.type] = 4;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 34;
            projectile.CloneDefaults(ProjectileID.ZephyrFish);
            aiType = ProjectileID.ZephyrFish;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.zephyrfish = false; // Relic from aiType
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Charred_Life modPlayer = player.GetModPlayer<Charred_Life>();
            if (player.dead)
            {
                modPlayer.ichorpet = false;
            }
            if (modPlayer.ichorpet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}
