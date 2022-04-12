using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
    public class Cursed_Zen_Pet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pet Peeve");
            Main.projFrames[projectile.type] = 4;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
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
                modPlayer.Zenpet = false;
            }
            if (modPlayer.Zenpet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}
