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
    public class ZenHandLeft : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mech Hand (left)");
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 20;

            npc.aiStyle = -1;
            npc.npcSlots = 5f;

            npc.lifeMax = 6000;
            npc.damage = 40;
            npc.defense = 17;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(gold: 5);

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath43;
        }//GlitchGlitchy
        private int Enterance;
        private int ChopTime;
        public override void AI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<MechZen>()))
            {
                npc.active = false;
            }
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;
            Vector2 direction2 = npc.DirectionTo(player.Center);
            Enterance++;
            if (Enterance < 30)
            {
                npc.rotation = 0;
                npc.velocity = new Vector2(-5f, 0f);
            }
            if (Enterance > 30)
            {
                ChopTime++;
                npc.rotation = (npc.Center - player.Center).ToRotation();
                npc.TargetClosest();  //Get a target
                Vector2 direction = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
                npc.velocity += direction * 12f / 60f;//SPEEEED
                if (npc.velocity.LengthSquared() > 12 * 12)
                    npc.velocity = Vector2.Normalize(npc.velocity) * 12;
                if (npc.DistanceSQ(player.Center) < 250f * 250f)
                {
                    if (ChopTime == 30)
                    {
                        Projectile.NewProjectile(npc.Center, direction2 * 15f, ModContent.ProjectileType<ZenHandLeftClone>(), 20, 2f);
                        Main.PlaySound(SoundID.Item9, npc.position);//15
                    }
                }
                if (ChopTime == 31)
                {
                    ChopTime = 0;
                }
            }
        }
        public override void NPCLoot()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<ZenHandRight>()))
            {
                Main.NewText("HEY my hands...", Color.Red, false);
            }
        }
    }
    public class ZenHandLeftClone : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 20;

            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = false;
            projectile.aiStyle = 0;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;

            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Items.Dusts.ZenStoneDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0);
        }
    }
}
