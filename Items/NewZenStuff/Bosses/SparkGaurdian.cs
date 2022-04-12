using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Bags;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot.SummonStaff;
using ZensTweakstest.Items.NewZenStuff.Tiles;
using ZensTweakstest.Items.Dusts;
using ZensTweakstest.Items.NewZenStuff.NpcSS;
using ZensTweakstest.Items.NewZenStuff.Bosses.Pacifist;
using ZensTweakstest.Items.JupiterStuff;
using ZensTweakstest.Items.JupiterStuff.Pet;
using Terraria.DataStructures;
using ZensTweakstest.Items;
using ZensTweakstest.Items.NewZenStuff.Projectilles;

namespace ZensTweakstest.Items.NewZenStuff.Bosses
{

    [AutoloadBossHead]
    public class SparkGaurdian : ModNPC
    {
        private float SlowRotation = 0f;
        private bool FiftyPerCesAttac = false;

        private bool GlareYnESS = false;
        private int GlareTime = 0;

        private bool glareenes = false;
        private int glareetim = 0;

        private float ScaleE = 1.8f;
        private bool AntiE = true;
        private float ScaleE2 = 1.3f;
        private float PacifistScaleE = 0f;

        private float ScaleE2E = 1.3f;
        private float ScaleEE = 1.8f;

        private int ai;
        private int Pacifist = 0;
        private int PacifistE = 0;
        private bool PacifistBol = false;
        private int attackTimer = 0;
        private bool fastSpeed = false;

        private bool stunned;
        //Junk
        private int stunTimer;

        private int frame = 0;
        private double counting;

        public override void SetStaticDefaults()
        {
            //Name
            DisplayName.SetDefault("The Spark Guardian");
            //Frames
            Main.npcFrameCount[npc.type] = 4;
            //IoC Trail
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.width = 116;
            npc.height = 152;

            npc.boss = true;
            npc.aiStyle = -1;
            npc.npcSlots = 5f;

            npc.lifeMax = 80000;
            npc.damage = 200;
            npc.defense = 16;
            npc.knockBackResist = 0f;
            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);

            npc.value = Item.buyPrice(gold: 1);

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

            bossBag = ModContent.ItemType<SG_Bag>();

            npc.HitSound = SoundID.NPCHit49;
            npc.DeathSound = SoundID.NPCDeath59;//e

            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TheBattleOfShine");
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SlowRotation += 0.07f;
            spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/Box"), npc.Center - Main.screenPosition, null, new Color(254, 84, 92), SlowRotation, new Vector2(100, 100) / 2f, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/Box"), npc.Center - Main.screenPosition, null, new Color(254, 84, 92), -SlowRotation, new Vector2(100, 100) / 2f, 0.7f, SpriteEffects.None, 0f);
            DateTime dateTime = DateTime.Now;
            if (Main.halloween || dateTime.Month == 10)
            {
                spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/SpooookyPump"), npc.Center - Main.screenPosition + new Vector2(0,-50), null, Color.White, 0, new Vector2(18, 18), 2f, SpriteEffects.None, 0f);
            }//SantaHata
            if (dateTime.Month == 12)
            {
                spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/SantaHata"), npc.Center - Main.screenPosition + new Vector2(0, -50), null, Color.White, 0, new Vector2(18, 18), 2f, SpriteEffects.None, 0f);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int num93 = 1; num93 < npc.oldPos.Length; num93++)
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                float num210 = Main.NPCAddHeight(npc.height);
                Vector2 vector2 = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
                _ = ref npc.oldPos[num93];
                Microsoft.Xna.Framework.Color color19 = drawColor;
                color19.R = (byte)(0.5 * (double)(int)color19.R * (double)(10 - num93) / 20.0);
                color19.G = (byte)(0.5 * (double)(int)color19.G * (double)(10 - num93) / 20.0);
                color19.B = (byte)(0.5 * (double)(int)color19.B * (double)(10 - num93) / 20.0);
                color19.A = (byte)(0.5 * (double)(int)color19.A * (double)(10 - num93) / 20.0);
                Main.spriteBatch.Draw(Main.npcTexture[npc.type], new Vector2(npc.oldPos[num93].X - Main.screenPosition.X + (float)(npc.width / 2) - (float)Main.npcTexture[npc.type].Width * npc.scale / 2f + vector2.X * npc.scale, npc.oldPos[num93].Y - Main.screenPosition.Y + (float)npc.height - (float)Main.npcTexture[npc.type].Height * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + vector2.Y * npc.scale + num210), npc.frame, color19, npc.rotation, vector2, npc.scale, spriteEffects, 0f);
            }
            if (Pacifist >= 10000)
            {
                float rotation = PacifistE + 1f;
                spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/HolySymbles"), npc.Center - Main.screenPosition, null, new Color(252, 182, 3), rotation, new Vector2(128, 128) / 2f, PacifistScaleE, SpriteEffects.None, 0f);
                if (PacifistScaleE <= 1f)
                {
                    PacifistScaleE += 0.01f;
                }
                else
                {
                    PacifistScaleE = 1f;
                }
            }
            if (GlareYnESS == true)
            {
                if (GlareTime >= 120)
                {
                    for (int i = 0; i < (Main.expertMode ? 5 : 5); i++)
                    {
                        Dust.NewDust(npc.position + npc.velocity, npc.width + 25, npc.height + 25, ModContent.DustType<ZenStoneFlameDustOpacity>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    }
                }
                else
                {
                    for (int i = 0; i < (Main.expertMode ? 5 : 5); i++)
                    {
                        Dust.NewDust(npc.position + npc.velocity, npc.width + 75, npc.height + 75, ModContent.DustType<ZenStoneFlameDustOpacity>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    }
                }
                Vector2 og = new Vector2(152, 152) / 2f;
                GlareTime++;
                float rotation = PacifistE + 1f;
                float AntiRotation = -rotation;
                spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/SpiceSphere"), npc.Center - Main.screenPosition, null, new Color(254, 84, 92), rotation, og, ScaleE, SpriteEffects.None, 0f);
                if (AntiE == true)
                    {
                    spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/SpiceSphere"), npc.Center - Main.screenPosition, null, new Color(254, 84, 92), AntiRotation, og, ScaleE2, SpriteEffects.None, 0f);
                    }
                ScaleE -= 0.01f;
                ScaleE2 -= 0.01f;
                if (GlareTime >= 160)
                {
                    if (ScaleE <= 0f)
                    {
                        for (int i = 0; i < (Main.expertMode ? 95 : 90); i++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width + 75, npc.height + 75, ModContent.DustType<ZenStoneFlameDustOpacity>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                        }
                        GlareYnESS = false;
                        ScaleE = 2f;
                        AntiE = true;
                        GlareTime = 0;
                    }
                }
                if (GlareTime >= 120)
                {
                    if (ScaleE2 >= 0f)
                    {
                        ScaleE2 = 1.5f;
                        AntiE = false;
                    }
                }
            }

            #region Glaree
            if (glareenes == true)
            {
                if (glareetim >= 120)
                {
                    for (int i = 0; i < (Main.expertMode ? 5 : 5); i++)
                    {
                        Dust.NewDust(npc.position + npc.velocity, npc.width + 25, npc.height + 25, ModContent.DustType<HolyDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    }
                }
                else
                {
                    for (int i = 0; i < (Main.expertMode ? 5 : 5); i++)
                    {
                        Dust.NewDust(npc.position + npc.velocity, npc.width + 75, npc.height + 75, ModContent.DustType<HolyDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    }
                }
                Vector2 og = new Vector2(152, 152) / 2f;
                glareetim++;
                float rotation = PacifistE + 1f;
                float AntiRotation = -rotation;
                spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/SpiceSphere"), npc.Center - Main.screenPosition, null, new Color(252, 182, 3), rotation, og, ScaleEE, SpriteEffects.None, 0f);
                if (AntiE == true)
                {
                    spriteBatch.Draw(mod.GetTexture("Items/NewZenStuff/Bosses/SpiceSphere"), npc.Center - Main.screenPosition, null, new Color(252, 182, 3), AntiRotation, og, ScaleE2E, SpriteEffects.None, 0f);
                }
                ScaleEE -= 0.01f;
                ScaleE2E -= 0.01f;
                if (glareetim >= 160)
                {
                    if (ScaleEE <= 0f)
                    {
                        for (int i = 0; i < (Main.expertMode ? 95 : 90); i++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width + 75, npc.height + 75, ModContent.DustType<HolyDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                        }
                        glareenes = false;
                        ScaleEE = 2f;
                        AntiE = true;
                        glareetim = 0;
                    }
                }
                if (glareetim >= 120)
                {
                    if (ScaleE2E >= 0f)
                    {
                        ScaleE2E = 1.5f;
                        AntiE = false;
                    }
                }
            }
            #endregion
            return true;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            //Reset the counter every 60 ticks
            if (npc.frameCounter >= 60)
                npc.frameCounter = 0;

            //Swap the frame used every 30 ticks
            if (npc.frameCounter < 15)
                npc.frame.Y = 0 * frameHeight;
            else if (npc.frameCounter < 30)
                npc.frame.Y = 1 * frameHeight;
            else if (npc.frameCounter < 45)
                npc.frame.Y = 2 * frameHeight;
            else if (npc.frameCounter < 60)
                npc.frame.Y = 3 * frameHeight;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
            npc.damage = (int)(npc.damage * 1.3f);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SparkHeadDead"), npc.scale);
                for (int i = 0; i < 2; i++) 
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ClothSparkDedlololol"), npc.scale);
                }
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SparkHandDand"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZANHAN"), npc.scale);
            }
        }
        public override void AI()
        {
            PacifistE++;
            Pacifist++;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;
            npc.rotation = 0.0f;
            npc.TargetClosest(true);
            if (Pacifist == 10000)
            {
                Main.PlaySound(SoundID.DD2_BetsyFlameBreath, npc.position);
            }
            if (npc.target < 0 || npc.target == 255 || player.dead)
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
            ai++;

            npc.ai[0] = (float)ai * 1f;
            int distance = (int)Vector2.Distance(target, npc.Center);
            if((double)npc.ai[0] < 300)
            {//basic move
                npc.TargetClosest();  //Get a target
                Vector2 direction = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
                npc.velocity += direction * 7f / 60f;//SPEEEED
                if (npc.velocity.LengthSquared() > 7 * 7)
                    npc.velocity = Vector2.Normalize(npc.velocity) * 10;
            } else if((double)npc.ai[0] >= 300 && (double)npc.ai[0] < 450.0)
            {
                stunned = true;
                npc.defense = 20;
                npc.damage = 50;

                npc.TargetClosest();  //Get a target
                Vector2 direction = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
                npc.velocity = direction * 3f;
            } else if((double)npc.ai[0] >= 450.0)
            {
                stunned = false;
                npc.damage = 75;
                npc.defense = 15;
                if (!fastSpeed)
                {
                    NPC.NewNPC((int)npc.position.X + Main.rand.Next(0, npc.width), (int)npc.position.Y + Main.rand.Next(0, npc.height), ModContent.NPCType<EnslavedPeeve>());
                    Main.PlaySound(SoundID.Camera, npc.position, 0);
                    fastSpeed = true;
                }
                else
                {
                    if ((double)npc.ai[0] % 50 == 0)
                    {//DASHING
                        if (Main.rand.Next(0, 9) == 1)
                        {
                            NPC.NewNPC((int)npc.position.X + Main.rand.Next(0, npc.width), (int)npc.position.Y + Main.rand.Next(0, npc.height), ModContent.NPCType<ZenSwordNPC>());
                        }
                        for (int i = 0; i < (Main.expertMode ? 55 : 50); i++)
                        {
                            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                        }
                        Main.PlaySound(SoundID.DD2_SonicBoomBladeSlash, npc.position);
                        npc.TargetClosest();  //Get a target
                        Vector2 direction = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
                        npc.velocity = direction * 12f;//SPEEED
                        if (FiftyPerCesAttac)
                        {
                            Vector2 shootPos = npc.Center;
                            float accuracy = 5f * (npc.life / npc.lifeMax);
                            Vector2 shootVelO = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
                            shootVelO = (Vector2.UnitX * 7.5f).RotatedByRandom(MathHelper.Pi);
                            Projectile.NewProjectile(shootPos.X + (float)(-1 * npc.direction) + (float)Main.rand.Next(-40, 41), shootPos.Y - (float)Main.rand.Next(-50, 40), shootVelO.X, shootVelO.Y, mod.ProjectileType("ZenStoneFlameProj"), npc.damage - 50, 5f);
                        }
                    }
                }
                npc.netUpdate = true;
            }
            #region ATTACK
            //Attack Code
            if ((double)npc.ai[0] % (Main.expertMode ? 100 : 150) == 0 && !stunned && !fastSpeed)
            {
                attackTimer++; //Increment the timer

                if (attackTimer <= 2)// If big
                {
                    npc.velocity.X = 0f;//Slow boss down
                    npc.velocity.Y = 0f;//Slow boss down
                    Main.PlaySound(SoundID.DD2_WyvernDiveDown, npc.position);//sound
                    Vector2 shootPos = npc.Center;
                    float accuracy = 5f * (npc.life / npc.lifeMax);

                    GlareYnESS = true;//effect
                    for (int i = 0; i < (Main.expertMode ? 95 : 90); i++)
                    {
                        Dust.NewDust(npc.position + npc.velocity, npc.width + 125, npc.height + 125, ModContent.DustType<ZenStoneFlameDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    }
                    int numberofproj = 25;
                    for (int i = 0; i < numberofproj; i++)
                    {
                        Vector2 spawnPos = new Vector2(npc.position.X + 58, npc.position.Y + 76);
                        Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));


                        VelPos.Normalize();
                        Projectile.NewProjectile(spawnPos, VelPos * 8f, ModContent.ProjectileType<ZenSwordBeamEvil>(), 95, 5f);
                        Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    }
                    /*for (int i = 0; i < (15); i++)
                    {
                        Vector2 shootVel = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
                        Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                        shootVel = (Vector2.UnitX * 7.5f).RotatedByRandom(MathHelper.Pi);//okish ring
                        Projectile.NewProjectile(shootPos.X + (float)(-1 * npc.direction) + (float)Main.rand.Next(-40, 41), shootPos.Y - (float)Main.rand.Next(-50, 40), shootVel.X, shootVel.Y, mod.ProjectileType("ZenSwordBeamEvil"), npc.damage / 3, 5f);
                    }//past this point is the orb*/
                    Vector2 shootVelO = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
                    shootVelO = (Vector2.UnitX * 7.5f).RotatedByRandom(MathHelper.Pi);
                    Projectile.NewProjectile(shootPos.X + (float)(-1 * npc.direction) + (float)Main.rand.Next(-40, 41), shootPos.Y - (float)Main.rand.Next(-50, 40), shootVelO.X, shootVelO.Y, mod.ProjectileType("ZenStoneFlameProj"), 75, 5f);
                }
                else
                {
                    attackTimer = 0;//reset the attack
                }
            }
            #endregion
            #region PacifistBreak
            if (npc.life <= npc.lifeMax * 0.5f)
            {
                if (Pacifist >= 10000)
                {
                    PacifistBol = true;
                }
                Pacifist = 0;
            }
            if (npc.life <= npc.lifeMax * 0.5f)
            {
                /*npc.width = 116;
                npc.height = 152;*/
                if (Main.rand.Next(0, 200) == 8)
                {
                    int numberofproj = 8;
                    for (int i = 0; i < numberofproj; i++)
                    {
                        Vector2 spawnPos = new Vector2(npc.position.X + 58, npc.position.Y + 76);
                        Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));


                        VelPos.Normalize();
                        Projectile.NewProjectile(spawnPos, VelPos * 10f, ModContent.ProjectileType<ZenStoneFlameProj>(), 65, 5f);
                        Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneFlameDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    }
                }
            }
            if (npc.life <= npc.lifeMax * 0.5f && !FiftyPerCesAttac)
            {
                npc.velocity.X = 0f;//Slow boss down
                npc.velocity.Y = 0f;//Slow boss down
                Main.PlaySound(SoundID.DD2_WyvernDiveDown, npc.position);//sound
                Vector2 shootPos = npc.Center;
                float accuracy = 5f * (npc.life / npc.lifeMax);

                glareenes = true;//effect
                for (int i = 0; i < (Main.expertMode ? 45 : 40); i++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width + 125, npc.height + 125, ModContent.DustType<ZenStoneFlameDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                }
                int numberofproj = 15;
                for (int i = 0; i < numberofproj; i++)
                {
                    Vector2 spawnPos = new Vector2(npc.position.X + 58, npc.position.Y + 76);
                    Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));


                    VelPos.Normalize();
                    Projectile.NewProjectile(spawnPos, VelPos * 12f, ModContent.ProjectileType<ZenFire>(), 99, 9f);
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                }
                /*for (int i = 0; i < (10); i++)
                {
                    Vector2 shootVel = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                    shootVel = (Vector2.UnitX * 15.5f).RotatedByRandom(MathHelper.Pi);//okish ring
                    Projectile.NewProjectile(shootPos.X + (float)(-1 * npc.direction) + (float)Main.rand.Next(-40, 41), shootPos.Y - (float)Main.rand.Next(-50, 40) + 5, shootVel.X, shootVel.Y, ModContent.ProjectileType<ZenFire>(), npc.damage, 5f);
                }*/
                FiftyPerCesAttac = true;
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
        public override void NPCLoot()
        {
            if (PacifistBol)
            {
                for (int i = 0; i < (Main.expertMode ? 105 : 100); i++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width + 125, npc.height + 125, ModContent.DustType<HolyDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EndurencePlantDeco>());
            }
            #region WorldGen
            #region Structure
            //StructureHelper.StructureHelper.GenerateStructure("WorldGen/ZenHouse", new Point16(30, 30), ModContent.GetInstance<ZensTweakstest>()); // | Bugged because "Index was outside the bounds of the array" no fix as of now. || When Code Not Bugged Remove // || Code Index: 1 || Remove Whole Line |
            if (ZenWorld.DownedZenGaurd == false)
            {

                // If theres an "Index was outside the bounds of the array" error nothing else in this hook runs past the problem.
                //StructureHelper.StructureHelper.GenerateStructure("WorldGen/ZenHouse", new Point16(700, 766), ModContent.GetInstance<ZensTweakstest>()); // | Replace With Code Index: 1 || Replace || Remove // |
                #endregion
                //int RadiusBiome = 325;
                //int biomeX = WorldGen.dungeonX;
                //int biomeY = Main.maxTilesY - 250;
                //for (int x = 0; x < biomeX + RadiusBiome; x++)
                //{
                    //for (int y = 0; y < biomeY + RadiusBiome; y++) 
                    //{
                        //if (Vector2.Distance(new Vector2(biomeX, biomeY), new Vector2(x, y)) < RadiusBiome)
                        //{
                            //if (Framing.GetTileSafely(x, y).active() && Framing.GetTileSafely(x, y).type == TileID.Stone)
                            //{
                                //WorldGen.TileRunner(x, y, 1, 3, ModContent.TileType<ZenitrinOre>());
                            //}
                        //}
                    //}
                //}

                #region Ores
                    // Ores are quite simple, we simply use a for loop and the WorldGen.TileRunner to place splotches of the specified Tile in the world.
                    for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 0.0001); k++)
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY); // WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.
                    if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == TileID.Stone || Framing.GetTileSafely(x, y).type == TileID.Dirt))
                    {
                        // The inside of this for loop corresponds to one single splotch of our Ore.
                        // First, we randomly choose any coordinate in the world by choosing a random x and y value.
                        // Then, we call WorldGen.TileRunner with random "strength" and random "steps", as well as the Tile we wish to place. Feel free to experiment with strength and step to see the shape they generate.
                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(1, 10), ModContent.TileType<ZenitrinOre>());
                    }
                }

                for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00018); k++)
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)Main.maxTilesY - 200, Main.maxTilesY); // WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.
                    if (Framing.GetTileSafely(x, y).active() && Framing.GetTileSafely(x, y).type == TileID.Ash)
                    {
                        // The inside of this for loop corresponds to one single splotch of our Ore.
                        // First, we randomly choose any coordinate in the world by choosing a random x and y value.
                        // Then, we call WorldGen.TileRunner with random "strength" and random "steps", as well as the Tile we wish to place. Feel free to experiment with strength and step to see the shape they generate.
                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), ModContent.TileType<ZenitrinOre>());
                    }
                }
                #endregion
                Main.NewText("The Caverns Sparkle With Red Light", 127, 36, 64, false);
            }
            #endregion
            for (int i = 0; i < (Main.expertMode ? 55 : 50); i++)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
            }
            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<BossSparkle>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
            for (int i = 0; i < (Main.expertMode ? 10 : 5); i++)
            {
                NPC.NewNPC((int)npc.position.X + Main.rand.Next(0, npc.width), (int)npc.position.Y + Main.rand.Next(0, npc.height), ModContent.NPCType<EnslavedPeeve>());
            }
            ZenWorld.DownedZenGaurd = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(0, 15) == 4)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<JupiMask>());
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<JupiBody>());
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<JupiLegs>());
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TheAbyss>());
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BunnyPetItem>());
                }
                if (Main.rand.Next(0, 100) >= 90)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Cursed_Zen_Peeve_Essence>());
                }
                if (Main.rand.Next(0, 100) >= 63)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<GlobeStaff>());
                }
                if (Main.rand.Next(0, 100) >= 75)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Zen_Stone_Trident"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Zen_Peeve_Essence"), 30);
                if (Main.rand.Next(0, 5) == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenSwordBook"));
                }
                if (Main.rand.Next(0, 7) == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SparkBow"));
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SparkMask"));
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SG_LORE"));
            if (Main.rand.Next(0, 10) == 1)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SG_Trophy_I"));
            }
            if (Main.rand.Next(0, 25) == 14)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SparkMask"));
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
    }
}