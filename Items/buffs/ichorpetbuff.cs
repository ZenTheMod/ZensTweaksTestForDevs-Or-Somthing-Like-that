using Terraria;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.buffs
{
    public class ichorpetbuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ichor Stingler");
            Description.SetDefault("A little baby Ichor Stickler is beside you" +
                "\nBruh it can't even aim!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<Charred_Life>().ichorpet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Items.Pet_s.ichorstinger>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Items.Pet_s.ichorstinger>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
