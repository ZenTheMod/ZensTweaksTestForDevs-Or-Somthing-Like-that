using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ZensTweakstest.Config;
using ZensTweakstest.Helper;
using QwertysRandomContent;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Items.NewZenStuff.Tree;

namespace ZensTweakstest.Items.HMmechZen
{
    [AutoloadBossHead]
    public class MechZen : ModNPC
    {
        public override void SetStaticDefaults()
        {
            //Frames
            Main.npcFrameCount[npc.type] = 4;
            //IoC Trail
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.width = 58;
            npc.height = 104;

            npc.boss = true;
            npc.aiStyle = -1;
            npc.npcSlots = 10f;

            npc.lifeMax = 30000;
            npc.damage = 40;
            npc.defense = 15;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(gold: 5);

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath43;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/MechanicalDOOM");
        }//GlitchGlitchy
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            //Reset the counter every 130 ticks
            if (npc.frameCounter >= 20)
                npc.frameCounter = 0;

            //Swap the frame used every 5 ticks
            if (npc.frameCounter < 5)
                npc.frame.Y = 0 * frameHeight;//1

            else if (npc.frameCounter < 10)
                npc.frame.Y = 1 * frameHeight;//2

            else if (npc.frameCounter < 15)
                npc.frame.Y = 2 * frameHeight;//3

            else if (npc.frameCounter < 20)
                npc.frame.Y = 3 * frameHeight;//4
        }
        private double dialouge;
        private bool FightMode = false;
        private int fightLoop;
        private double Dialouge2;
        private bool Deafend;
        private bool FinalPhase;
        private int increase;
        private int totalIncrease;
        private Vector2 playerPos = new Vector2(-29, -210);
        public override void AI()
        {
            totalIncrease++;
            Dust.NewDustPerfect(npc.Center + new Vector2(0, -10), DustID.RedTorch, new Vector2(7f).RotatedBy(MathHelper.ToRadians(totalIncrease)));
            increase++;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;
            if (player.dead)
            {
                Main.NewText("NOOB!!! imagine dying lol xd so bad...", Color.Red, false);
                npc.active = false;
            }
            dialouge +=0.5;
            #region Dialouge
            if (dialouge == 1)
            {
                Main.NewText("*cough* *cough*", Color.Red, false);
            }
            if (dialouge == 60)
            {
                Main.NewText("Who put all this smoke here!!", Color.Red, false);
            }
            if (dialouge == 130)
            {
                Main.NewText("Ignore... that...", Color.Red, false);
            }
            if (dialouge == 170)
            {
                Main.NewText("OOOh its You...", Color.Red, false);
            }
            if (dialouge == 230)
            {
                Main.NewText("I've been following you...", Color.Red, false);
            }
            if (dialouge == 290)
            {
                Main.NewText("And I've mechanicalized myself Too...", Color.Red, false);
            }
            if (dialouge == 320)
            {
                Main.NewText("I've seen what you have done.", Color.Red, false);
            }
            if (dialouge == 380)
            {
                Main.PlaySound(SoundID.Item14, npc.position);//15
                Main.NewText("Well I guess it has to be this way........", Color.Red, false);
            }
            #endregion
            #region Fight
            if (dialouge == 401)
            {
                NPC.NewNPC((int)npc.Center.X + 20, (int)npc.Center.Y - 5, ModContent.NPCType<ZenHandRight>());
                NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y - 5, ModContent.NPCType<ZenHandLeft>());
                for (int f = 0; f < 5; f++)
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
            if (dialouge == 421)
            {
                FightMode = true;
            }
            if (FightMode == true)
            {
                fightLoop++;
                npc.position = Vector2.Lerp(npc.position, player.position + playerPos, 0.1f);
                //npc.position = player.position + playerPos;
                if (fightLoop >= 1)
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<ZenHandLeft>()) && !NPC.AnyNPCs(ModContent.NPCType<ZenHandRight>()))
                    {
                        Dialouge2+=0.5;
                        if (Dialouge2 == 1)
                        {
                            npc.life = npc.lifeMax / (int)2.5f;
                            for (int f = 0; f < 5; f++)
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
                        if (Dialouge2 == 30)
                        {
                            Main.NewText("Well.", Color.Red, false);
                        }
                        if (Dialouge2 == 60)
                        {
                            Main.NewText("You have deafeted my hands", Color.Red, false);
                        }
                        if (Dialouge2 == 90)
                        {
                            Main.NewText("I wish you good luck...", Color.Red, false);
                        }
                        if (Dialouge2 == 130)
                        {
                            Main.NewText("...", Color.Red, false);
                            FinalPhase = true;
                        }
                        if (Dialouge2 == 850)
                        {
                            Main.NewText("This is taking a long time.", Color.Red, false);
                        }
                        if (Dialouge2 == 900)
                        {
                            Main.NewText("My cat needs feeding, my pizza is cold.", Color.Red, false);
                        }
                        if (Dialouge2 == 1300)
                        {
                            Main.NewText("Still fighting heh I cant feel a thing!", Color.Red, false);
                        }
                        if (Dialouge2 == 1900)
                        {
                            Main.NewText("You can still leave, take a break, go watch TV", Color.Red, false);
                        }
                        if (Dialouge2 == 2100)
                        {
                            Main.NewText("...See what happens to the goblin tinkerer when he scams you.", Color.Red, false);
                        }
                        if (Dialouge2 == 2340)
                        {
                            Main.NewText("Is my monolounging to mono-long?", Color.Red, false);
                        }
                    }
                }
            }
            #endregion
            if (npc.life < npc.lifeMax * 0.5f && !FinalPhase)
            {
                Deafend = true;
            }
            else
            {
                Deafend = false;
            }

            if (FinalPhase)
            {
                playerPos = new Vector2(-29, -280);
                if (increase > 65)
                    increase = 0;
                if (increase == 44)
                    CircleRing(4, ModContent.ProjectileType<ZenFlameHand>());
                if (increase == 34)
                    CircleRing(3, ModContent.ProjectileType<GlitchSword>());
            }
            if (!FightMode || Deafend)
            {
                npc.immortal = true;
            }
            else
            {
                npc.immortal = false;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void NPCLoot()
        {
            Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<ZenProjFake>(),0,0);
            ZenWorld.DownedMechZen = true;
            Main.NewText("DAM YOU ILL BE BACK I SWEAR!!!!!", Color.Red, false);
            //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Weapons.Afterburn>());
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<IgnisWood>(), Main.rand.Next(20,50));
            if (Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.ZenBag>());
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Clinger_Fruit>(), 10);
                switch (Main.rand.Next(6))
                {
                    case 0:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ChaosVigil>());
                        break;
                    case 1:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Weapons.Afterburn>());
                        break;
                    case 2:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.ZenicFlamethrower>());
                        break;
                    case 3:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.StellarUmbrella>());
                        break;
                    case 4:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.VoidSlash>());
                        break;
                    default:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.ThePoker>());
                        break;
                }
                if (Main.rand.Next(0, 100) < 5)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.AfflictionDagger>());
                }
                if (Main.rand.Next(0, 100) < 10)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.BossCosmetics.MechZenMask>());
                }
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.BossCosmetics.MechZenL>());
            if (Main.rand.Next(0, 100) < 10)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.BossCosmetics.ZenTrophy>());
            }
            if (Main.rand.Next(0, 100) < 7)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Weapons.PrismaticStroke>());
            }
            if (Main.rand.Next(0, 100) < 5)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TheLordeSoul>());
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.BossCosmetics.MechZenMusicBox>());
        }
        void CircleRing(int Number, int Type)
        {
            int numberofproj = Number;
            for (int i = 0; i < numberofproj; i++)
            {
                Vector2 spawnPos = new Vector2(npc.Center.X, npc.Center.Y);
                Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

                VelPos.Normalize();
                Main.PlaySound(SoundID.NPCDeath43, npc.position);//
                Projectile.NewProjectile(spawnPos, VelPos * 11f, Type, 35, 9f);
            }
        }
    }
    public class ZenProjFake : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 58;
            projectile.height = 104;
            projectile.timeLeft = 300;
            projectile.aiStyle = -1;
        }
        public override void AI()
        {
            projectile.velocity.Y = projectile.velocity.Y - 0.1f;
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
        }
    }
    public class GlitchSword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            projectile.thrown = true;

            projectile.width = 48;
            projectile.height = 58;
            projectile.scale = 1.0f;

            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.ignoreWater = false;

            projectile.aiStyle = 2;
        }

        public override void AI() 
        {
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 8)
                {
                    projectile.frame = 0;
                }
            }
            Lighting.AddLight(projectile.position, 1f, 1f, 1f);
        }

        int bounces = 1;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);

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
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.BlueTorch);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(projectile.position, DustID.BlueTorch, speed * 5, Scale: 1.5f);
                d.noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture(Texture);
            int frameHeight = texture.Height / Main.projFrames[projectile.type];
            int startY = frameHeight * projectile.frame;
            Rectangle rec = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/HMmechZen/GlitchSword"), drawPos, rec, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
