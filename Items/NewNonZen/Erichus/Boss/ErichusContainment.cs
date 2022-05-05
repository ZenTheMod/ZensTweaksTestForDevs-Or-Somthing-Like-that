using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.NewNonZen.Erichus.Boss
{
    [AutoloadBossHead]
    public class ErichusContainment : ModNPC//Eric
    {
        private float SlowRotation = 0f;
        private int ai;
        private bool fastSpeed = false;
        public override void SetStaticDefaults()
        {
            //Frames
            Main.npcFrameCount[npc.type] = 12;
            //IoC Trail
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.width = 118;
            npc.height = 142;

            npc.boss = true;
            npc.aiStyle = -1;
            npc.npcSlots = 5f;

            npc.lifeMax = 30000;
            npc.damage = 100;
            npc.defense = 13;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(gold: 5);

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

            bossBag = ModContent.ItemType<ErichusBag>();

            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath10;//pee

            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/OddBlob");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            DateTime dateTime = DateTime.Now;
            if (Main.halloween || dateTime.Month == 10)
            {
                spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/SpooookyPump"), npc.Center - Main.screenPosition + new Vector2(0, -60), null, Color.White, 0, new Vector2(18, 18), 2f, SpriteEffects.None, 0f);
            }
            if (dateTime.Month == 12)
            {
                spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/SantaHata"), npc.Center - Main.screenPosition + new Vector2(0, -50), null, Color.White, 0, new Vector2(18, 18), 2f, SpriteEffects.None, 0f);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SlowRotation += 0.07f;
            spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/Box"), npc.Center - Main.screenPosition, null, new Color(15, 168, 18), SlowRotation, new Vector2(100, 100) / 2f, 1.4f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/Box"), npc.Center - Main.screenPosition, null, new Color(15, 168, 18), -SlowRotation, new Vector2(100, 100) / 2f, 1.0f, SpriteEffects.None, 0f);
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int num93 = 1; num93 < npc.oldPos.Length; num93++)
            {
                Vector2 drawPos = npc.oldPos[num93] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = Color.White * ((float)(NPCID.Sets.TrailCacheLength[npc.type] - num93) / (float)NPCID.Sets.TrailCacheLength[npc.type]);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/Boss/ErichusContainmentVFX"), drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;//ZensTweakstest/Items/NewNonZen/Erichus/Boss/ErichusContainmentVFX
        }
        public override void AI()
        {
            npc.netUpdate = true;
            Lighting.AddLight(npc.Center, Color.Green.ToVector3() * 0.75f * Main.essScale);
            #region Dust
            Vector2 vector = new Vector2(Main.rand.Next(-28, 28) * (0.003f * 40 - 10), Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
            Dust chargeDust = Main.dust[Dust.NewDust(npc.Center + vector, 1, 1, 74, 0, 0, 255, new Color(0.8f, 0.4f, 1f), 0.8f)];//ModContent.DustType<EDust>()
            chargeDust.velocity = -vector / 12;
            chargeDust.velocity -= npc.velocity / 8;
            chargeDust.noLight = true;
            chargeDust.noGravity = true;
            #endregion
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;
            
            ai++;
            npc.rotation = 0.0f;
            npc.ai[0] = (float)ai * 1f;
            if ((double)npc.ai[0] >= 450.0)
            {
                npc.damage = 75;
                npc.defense = 15;
                if (!fastSpeed)
                {
                    fastSpeed = true;// TALKING LINE: 
                }
                else
                {
                    if ((double)npc.ai[0] % 50 == 0)
                    {//DASHING
                        Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/EricDash"));
                        npc.TargetClosest();  //Get a target
                        Vector2 directionD = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
                        npc.velocity = directionD * 26f;//SPEEED
                        //Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/EricDash").WithVolume(.7f).WithPitchVariance(.01f), npc.position);
                        int numberofproj = 4;
                        for (int i = 0; i < numberofproj; i++)
                        {
                            Vector2 spawnPos = new Vector2(npc.position.X + 58, npc.position.Y + 76);
                            Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

                            VelPos.Normalize();
                            Main.PlaySound(SoundID.Item99, npc.position);//
                            Projectile.NewProjectile(spawnPos, VelPos * 18f, ModContent.ProjectileType<spenGwerp>(), 45, 9f, Main.myPlayer, 0, npc.whoAmI);
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<EDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                        }
                    }
                }
                npc.netUpdate = true;
            }
            #region Charge
            if ((double)npc.ai[0] >= 300 && (double)npc.ai[0] < 450.0)
            {//SLOW
                Vector2 vector3 = new Vector2(Main.rand.Next(-28, 28) * (0.003f * 40 - 10), Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
                Dust chargeDust3 = Main.dust[Dust.NewDust(npc.Center + vector3, 1, 1, DustID.PinkFairy, 0, 0, 255, new Color(0.8f, 0.4f, 1f), 0.8f)];//ModContent.DustType<EDust>()
                chargeDust3.velocity = -vector / 12;
                chargeDust3.velocity -= npc.velocity / 8;
                chargeDust3.noLight = true;
                chargeDust3.noGravity = true;
                if (Main.rand.Next(0, 150) == 5)
                {
                    int numberofproj = 15;
                    for (int i = 0; i < numberofproj; i++)
                    {
                        Vector2 spawnPos = new Vector2(npc.position.X + 58, npc.position.Y + 76);
                        Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

                        VelPos.Normalize();
                        Main.PlaySound(SoundID.NPCDeath13, npc.position);//
                        Projectile.NewProjectile(spawnPos, VelPos * 14f, ModContent.ProjectileType<GwerpShot>(), 55, 9f);
                        Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<EDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    }
                }
                npc.TargetClosest();  //Get a target
                Vector2 direction = npc.DirectionTo(player.Center + new Vector2(0, -100));  //Get a direction to the player from the NPC
                npc.velocity += direction * 3f / 60f;//SPEEEED
                if (npc.velocity.LengthSquared() > 3 * 3)
                    npc.velocity = Vector2.Normalize(npc.velocity) * 3;
            }
            #endregion
            #region despawn
            if (Main.dayTime)
            {
                npc.damage = 100000;
            }
            if (npc.target < 0 || npc.target == 255 || player.dead || Main.dayTime)
            {
                npc.TargetClosest(false);
                npc.direction = 1;

                npc.velocity.Y = npc.velocity.Y - 0.1f;
                if (npc.timeLeft > 20)
                {
                    npc.timeLeft = 20;
                    return;
                }
            }
            #endregion
            #region Move
            if ((double)npc.ai[0] < 300)
            {//basic move
                npc.TargetClosest();  //Get a target
                Vector2 direction = npc.DirectionTo(player.Center + new Vector2(0,-210));  //Get a direction to the player from the NPC
                npc.velocity += direction * 10f / 60f;//SPEEEED
                if (npc.velocity.LengthSquared() > 10 * 10)
                    npc.velocity = Vector2.Normalize(npc.velocity) * 10;
            }
            #endregion
            #region TestProjSpread
            if (Main.rand.Next(0, 200) == 50)
            {
                for (int id = 0; id < 15; id++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(npc.position, ModContent.DustType<EDust>(), speed * 5, Scale: 1.5f);
                    d.noGravity = true;
                }
                int numberofproj = 16;
                for (int i = 0; i < numberofproj; i++)
                {
                    Vector2 spawnPos = new Vector2(npc.position.X + 58, npc.position.Y + 76);
                    Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

                    VelPos.Normalize();
                    Main.PlaySound(SoundID.Item99, npc.position);//
                    Projectile.NewProjectile(spawnPos, VelPos * 12f, ModContent.ProjectileType<TestTubeProj>(), 40, 9f);
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<EDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                }
            }
            #endregion
            #region BoneSpread
            if (Main.rand.Next(0, 230) == 49)
            {
                for (int id = 0; id < 25; id++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(npc.position, ModContent.DustType<EDust>(), speed * 5, Scale: 1.5f);
                    d.noGravity = true;
                }
                int numberofproj = 8;
                for (int i = 0; i < numberofproj; i++)
                {
                    Vector2 spawnPos = new Vector2(npc.position.X + 58, npc.position.Y + 76);
                    Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

                    VelPos.Normalize();
                    Main.PlaySound(SoundID.Item99, npc.position);//
                    Projectile.NewProjectile(spawnPos, VelPos * 18f, ModContent.ProjectileType<ErichusBonhus>(), 55, 9f);
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<EDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                }
            }
            #endregion
            #region GwerpShotSmall
            if (Main.rand.Next(0, 280) == 49)
            {
                for (int id = 0; id < 25; id++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust d = Dust.NewDustPerfect(npc.position, ModContent.DustType<EDust>(), speed * 5, Scale: 1.5f);
                    d.noGravity = true;
                }
                int numberofproj = 8;
                for (int i = 0; i < numberofproj; i++)
                {
                    Vector2 spawnPos = new Vector2(npc.position.X + 58, npc.position.Y + 76);
                    Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

                    VelPos.Normalize();
                    Main.PlaySound(SoundID.NPCDeath13, npc.position);//
                    Projectile.NewProjectile(spawnPos, VelPos * 4f, ModContent.ProjectileType<GwerpShot>(), 55, 9f);
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<EDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                }
            }
            #endregion
            if ((double)npc.ai[0] >= 650.0)
            {
                ai = 0;
                npc.alpha = 0;
                npc.ai[2] = 0;
                fastSpeed = false;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ErichusGoreMain"), npc.scale);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            //Reset the counter every 130 ticks
            if (npc.frameCounter >= 130)
                npc.frameCounter = 0;

            //Swap the frame used every 10 ticks
            if (npc.frameCounter < 10)
                npc.frame.Y = 0 * frameHeight;//1

            else if (npc.frameCounter < 20)
                npc.frame.Y = 1 * frameHeight;//2

            else if (npc.frameCounter < 30)
                npc.frame.Y = 2 * frameHeight;//3

            else if (npc.frameCounter < 40)
                npc.frame.Y = 3 * frameHeight;//4

            else if (npc.frameCounter < 50)
                npc.frame.Y = 4 * frameHeight;//5

            else if (npc.frameCounter < 60)
                npc.frame.Y = 5 * frameHeight;//6

            else if (npc.frameCounter < 70)
                npc.frame.Y = 6 * frameHeight;//7

            else if (npc.frameCounter < 80)
                npc.frame.Y = 7 * frameHeight;//8

            else if (npc.frameCounter < 90)
                npc.frame.Y = 8 * frameHeight;//9

            else if (npc.frameCounter < 100)
                npc.frame.Y = 9 * frameHeight;//10

            else if (npc.frameCounter < 110)
                npc.frame.Y = 10 * frameHeight;//11

            else if (npc.frameCounter < 120)
                npc.frame.Y = 11 * frameHeight;//12
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Erichus>());
            ZenWorld.DownedEric = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Loot.ToxicGrenade>());
                        break;
                    case 1:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Loot.ToxicBarrel>());
                        break;
                    case 2:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Loot.ToxicRevolverator>());
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Loot.ToxicRocket>(), Main.rand.Next(50, 100));
                        break;
                    default:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Loot.Butcherer>());
                        break;
                }
            }
            if (Main.rand.Next(0, 10) == 1)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TrophyEric>());
            }
            if (Main.rand.Next(0, 25) == 14)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ErichusContainmentMask>());
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EricBox>());
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
    }
    public class ErichusBonhus : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.thrown = true;

            projectile.width = 8;
            projectile.height = 8;
            projectile.scale = 1.3f;

            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = false;

            projectile.aiStyle = 2;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(1, 10))
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy, projectile.velocity.X / 10, projectile.velocity.Y / 10);
            }
            Lighting.AddLight(projectile.position, 0, 1, 0.3f);
        }

        int bounces = 1;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.DD2_SkeletonHurt, projectile.position);

            if (bounces <= 0)
            {
                projectile.Kill();
            }
            else
            {
                Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Item10, projectile.position);
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                bounces -= 1;
            }

            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(projectile.position, DustID.GreenFairy, speed * 5, Scale: 1.5f);
                d.noGravity = true;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Loot.Toxiblast>(), projectile.damage, 2, projectile.owner);
            Main.PlaySound(SoundID.Item14, (int)projectile.position.X, (int)projectile.position.Y);

            const int NUM_DUSTS = 10;

            for (int i = 0; i < NUM_DUSTS; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(10f, 10f);
                Dust.NewDustPerfect(projectile.Center, DustID.GreenFairy, speed);
            }

            for (int i = 0; i < NUM_DUSTS; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(5f, 5f);
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.GreenFairy, speed * 5, Scale: 1.5f);
                dust.noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/Loot/ToxiboneAI"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
    public class GwerpShot : ModProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 38;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.timeLeft = 400;
            projectile.penetrate = 2;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<EDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                projectile.velocity *= 0.75f;
                Main.PlaySound(SoundID.Item10, projectile.position);
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy);
            }
        }
    }
    public class TestTubeProj : ModProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.timeLeft = 400;
            projectile.penetrate = 1;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, (300));
        }
        public override void AI()
        {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<EDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.GreenFairy);
            }
        }
    }
    public class EDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.1f;
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale *= 1.5f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale *= 0.99f;

            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }
            return false;
        }
    }
    public class spenGwerp : ModProjectile
    {
        private int TimeRBoom = 0;
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 24;
            projectile.scale = 1f;

            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = true;

            projectile.aiStyle = -1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = Color.White * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/Loot/ToxicGrenadeAI"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale - k / (float)projectile.oldPos.Length, SpriteEffects.None, 0f);//projectile.scale - k / (float)projectile.oldPos.Length
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Loot.Toxiblast>(), projectile.damage, 2, projectile.owner);
            Main.PlaySound(SoundID.Item14, (int)projectile.position.X, (int)projectile.position.Y);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AI()
        {
            if (Main.npc[(int)projectile.ai[1]].active && Main.npc[(int)projectile.ai[1]].type == ModContent.NPCType<ErichusContainment>())
            {
                projectile.rotation += 0.1f;
                Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.GreenFairy, new Vector2(0, 0));
                Vector2 direction = projectile.DirectionTo(Main.npc[(int)projectile.ai[1]].Center);  //Get a direction to the player from the NPC
                projectile.velocity += direction * 10f / 60f;//SPEEEED
                if (projectile.velocity.LengthSquared() > 10 * 10)
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * 10;

                TimeRBoom++;
                if (TimeRBoom >= 70)
                {
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        if (Main.projectile[i].active && Main.projectile[i].type == projectile.type && projectile.Hitbox.Intersects(Main.projectile[i].Hitbox) && i != projectile.whoAmI)
                        {
                            Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Loot.Toxiblast>(), projectile.damage, 2, projectile.owner);
                            Main.PlaySound(SoundID.Item14, (int)projectile.position.X, (int)projectile.position.Y);
                            projectile.active = false;
                        }
                    }
                }
            }
            else if (!Main.npc[(int)projectile.ai[1]].active && Main.npc[(int)projectile.ai[1]].type == ModContent.NPCType<ErichusContainment>())
            {
                projectile.active = false;
            }
            else
            {
                projectile.active = false;
            }
        }
    }
}//627 LINES!!!!!!!!!
//line 628: are you sure about that
//you guys are getting regognized