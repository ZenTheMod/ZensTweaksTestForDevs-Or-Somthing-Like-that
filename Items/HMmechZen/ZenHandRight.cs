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
    public class ZenHandRight : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mech Hand (Right)");
        }
        public override void SetDefaults()
        {
            npc.width = 22;
            npc.height = 24;

            npc.aiStyle = -1;
            npc.npcSlots = 5f;

            npc.lifeMax = 6001;
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
        private int flame;
        private int CounterS;
        private int SpinF;
        private bool spriralFlame;
        public override void AI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<MechZen>()))
            {
                npc.active = false;
            }
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;

            #region Spiral
            CounterS++;
            if (CounterS == 200)
            {
                Main.PlaySound(SoundID.Item119, npc.position);//15
            }
            if (CounterS > 200 && CounterS < 222)
            {
                npc.velocity = Vector2.Zero;
                spriralFlame = true;
                SpinF++;
                Projectile.NewProjectile(npc.Center + new Vector2(0, -10), new Vector2(7f).RotatedBy(MathHelper.ToRadians(SpinF * 8)), ModContent.ProjectileType<ZenFlameHand>(), 35, 9f);
                Projectile.NewProjectile(npc.Center + new Vector2(0, -10), new Vector2(7f).RotatedBy(MathHelper.ToRadians(SpinF * 8 + 180)), ModContent.ProjectileType<ZenFlameHand>(), 35, 9f);
            }
            else
            {
                spriralFlame = false;
            }
            if (CounterS == 246)
            {
                SpinF = 0;
                CounterS = 0;
            }
            #endregion

            Enterance++;
            if (Enterance < 30)
            {
                npc.rotation = 0;
                npc.velocity = new Vector2(5f,0f);
            }
            if (Enterance > 30)
            {
                flame++;
                npc.rotation = (npc.Center - player.Center).ToRotation() + 3.6f;
                if (!spriralFlame)
                {
                    npc.TargetClosest();  //Get a target
                    Vector2 direction = npc.DirectionTo(player.Center + new Vector2(0, -210));  //Get a direction to the player from the NPC
                    npc.velocity += direction * 12f / 60f;//SPEEEED
                    if (npc.velocity.LengthSquared() > 12 * 12)
                        npc.velocity = Vector2.Normalize(npc.velocity) * 12;
                }
                Vector2 direction2 = npc.DirectionTo(player.Center);
                if (flame == 30)
                {
                    if (!spriralFlame)
                    {
                        Main.PlaySound(SoundID.Item9, npc.position);//15
                        Projectile.NewProjectile(npc.Center, direction2 * 12f, ModContent.ProjectileType<ZenFlameHand>(), 20, 2f);
                    }
                    flame = 0;
                    if (Main.rand.Next(0,2) == 1)
                    {
                        int goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                        goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    }
                }
            }
        }
        public override void NPCLoot()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<ZenHandLeft>()))
            {
                Main.NewText("HEY my hands...", Color.Red, false);
            }
        }
    }
    public class ZenFlameHand : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 14;

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
    public class ZenFlameSheath : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 14;

            projectile.friendly = true;
            projectile.hostile = false;
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
