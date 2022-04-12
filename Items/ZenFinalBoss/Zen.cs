using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.ZenFinalBoss
{
    [AutoloadBossHead]
    public class Zen : ModNPC
    {
        public override void SetStaticDefaults()
        {
            //Name
            DisplayName.SetDefault("Zen");
            //Frames
            Main.npcFrameCount[npc.type] = 4;
            //IoC Trail
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 48;

            npc.boss = true;
            npc.aiStyle = -1;
            npc.npcSlots = 5f;

            npc.lifeMax = 180000;
            npc.damage = 200;
            npc.defense = 16;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(gold: 100);

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

            //bossBag = ModContent.ItemType<SG_Bag>();

            npc.HitSound = SoundID.NPCHit49;
            npc.DeathSound = SoundID.NPCDeath59;//e

            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShowDown");
        }
        private double ERICHUS = 0.00;
        private double ERICHUS1 = 2.06;
        private double ERICHUS2 = 4.13;
        private double ERICHUS3 = 0.00;
        private float rotationD = 0f;
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
            /*
            ERICHUS += 0.01;
            if (ERICHUS >= 6.2)
            {
                ERICHUS = 0.0;
            }

            ERICHUS1 += 0.01;
            if (ERICHUS1 >= 6.2)
            {
                ERICHUS1 = 0.0;
            }

            ERICHUS2 += 0.01;
            if (ERICHUS2 >= 6.2)
            {
                ERICHUS2 = 0.0;
            }
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.UIScaleMatrix);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingBloom"), position + new Vector2(-7, -8), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), position + Vector2.One.RotatedBy(ERICHUS) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), position + Vector2.One.RotatedBy(ERICHUS1) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mod.GetTexture("Items/NewNonZen/Erichus/ThingGlow"), position + Vector2.One.RotatedBy(ERICHUS2) * 5, null, Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            */
            ERICHUS += 0.01;
            if (ERICHUS >= 6.2)
            {
                ERICHUS = 0.0;
            }

            ERICHUS1 += 0.01;
            if (ERICHUS1 >= 6.2)
            {
                ERICHUS1 = 0.0;
            }

            ERICHUS2 += 0.01;
            if (ERICHUS2 >= 6.2)
            {
                ERICHUS2 = 0.0;
            }

            ERICHUS3 += 0.1;
            if (ERICHUS3 >= 6.2)
            {
                ERICHUS3 = 0.0;
            }

            rotationD += 0.01f;

            var TextureGlowZ = mod.GetTexture("Items/ZenFinalBoss/ZenGlow");

            Vector2 position = npc.Center - Main.screenPosition;
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(TextureGlowZ, position + Vector2.One.RotatedBy(ERICHUS) * 5, null, Color.White, 0f, new Vector2(16, 24), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(TextureGlowZ, position + Vector2.One.RotatedBy(ERICHUS1) * 5, null, Color.White, 0f, new Vector2(16, 24), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(TextureGlowZ, position + Vector2.One.RotatedBy(ERICHUS2) * 5, null, Color.White, 0f, new Vector2(16, 24), 1f, SpriteEffects.None, 0f);
            if (Song >= 2550 && Song <= 2670)
            {
                spriteBatch.Draw(TextureGlowZ, position + Vector2.One.RotatedBy(ERICHUS3) * 17f, null, Color.White, 0f, new Vector2(16, 24), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(TextureGlowZ, position + Vector2.One.RotatedBy(ERICHUS3- 1.6) * 17f, null, Color.White, 0f, new Vector2(16, 24), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(TextureGlowZ, position + Vector2.One.RotatedBy(ERICHUS3 - 0.8) * 17f, null, Color.White, 0f, new Vector2(16, 24), 1f, SpriteEffects.None, 0f);

                spriteBatch.Draw(TextureGlowZ, position + Vector2.One.RotatedBy(-ERICHUS3) * 37f, null, Color.White, 0f, new Vector2(16, 24), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(TextureGlowZ, position + Vector2.One.RotatedBy(-ERICHUS3 - 1.6) * 37f, null, Color.White, 0f, new Vector2(16, 24), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(TextureGlowZ, position + Vector2.One.RotatedBy(-ERICHUS3 - 0.8) * 37f, null, Color.White, 0f, new Vector2(16, 24), 1f, SpriteEffects.None, 0f);

                spriteBatch.Draw(mod.GetTexture("Items/ZenFinalBoss/SafeZone"), position, null, Color.White, rotationD, new Vector2(125, 125), 1f, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);

            return true;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            //Reset the counter every 45 ticks
            if (npc.frameCounter >= 45)
                npc.frameCounter = 0;

            //Swap the frame used every 15 ticks
            if (npc.frameCounter < 15)
                npc.frame.Y = 0 * frameHeight;
            else if (npc.frameCounter < 30)
                npc.frame.Y = 1 * frameHeight;
            else if (npc.frameCounter < 45)
                npc.frame.Y = 2 * frameHeight;
        }
        private int ai;
        private int Song = 0;
        private int CapInt = 3;
        private float CapFloat = 3f;
        private bool Immune = false;
        private bool SongDraw = false;
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (Immune)
            {
                return false;
            }
            else
            {
                return base.CanBeHitByProjectile(projectile);
            }
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            if (Immune)
            {
                return false;
            }
            else
            {
                return base.CanBeHitByItem(player, item);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Song >= 2550 && Song <= 2790)
            {
                return false;
            }
            else
            {
                return base.CanHitNPC(target);
            }
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;
            npc.TargetClosest();  //Get a target
            Vector2 direction = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
            npc.velocity += direction * CapFloat / 60f;//SPEEEED
            if (npc.velocity.LengthSquared() > CapInt * CapInt)
                npc.velocity = Vector2.Normalize(npc.velocity) * CapInt;
            if (npc.life < npc.lifeMax * 0.85f)
            {
                if (!Immune)
                {
                    Main.NewText("Did you realy think its this easy???", Color.Red, false);
                }
                Immune = true;
            }
            if (SongDraw)
            {
                float HoverSin = (float)Math.Sin(Main.GameUpdateCount / 5f);
                if (npc.velocity != new Vector2(0, 0))
                {
                    int maxSeparation = 2 * 16;
                    Vector2 offset = Vector2.Normalize(npc.velocity.RotatedBy(MathHelper.PiOver2)) * HoverSin * maxSeparation;
                    Vector2 dustVelocity = offset / 16;
                    Dust dusty = Dust.NewDustPerfect(npc.Center + offset, DustID.PinkFairy, -dustVelocity);
                    Dust dustx = Dust.NewDustPerfect(npc.Center - offset, DustID.PinkFairy, dustVelocity);
                }
            }
            ai++;
            Song++;
            if (Song >= 2250)
            {
                CapInt = 10;
                CapFloat = 10f;
                SongDraw = true;
            }
            else
            {
                CapInt = 3;
                CapFloat = 3f;
            }
            if (Song == 0)
            {
                Main.NewText("Someone has came to free me?", Color.Red, false);
            }
            if (Song == 17)
            {
                Main.NewText("Try your best! you cant win.", Color.Red, false);
            }
            if (Song == 30)
            {
                Main.NewText("I HAVE LOST IT", Color.Black, false);
            }
            if (!(npc.life < npc.lifeMax * 0.85f))
            {
                if (Song >= 2550 && Song <= 2600)
                {
                    npc.velocity = Vector2.Zero;
                    OuterAttack(true, ModContent.ProjectileType<RingFlame>(), 12f, npc.damage);//bool Side_TrueIsRight, int Type, float Speed, int Damage
                    OuterAttack(false, ModContent.ProjectileType<RingFlame>(), 12f, npc.damage);//bool Side_TrueIsRight, int Type, float Speed, int Damage
                }
                if (Song >= 2550 && Song <= 2780)
                {
                    npc.velocity = Vector2.Zero;
                }
                if (Song == 2550)
                {
                    for (int D = 0; D < 80; ++D)
                    {
                        Dust.NewDust(npc.Center, 70, 70, DustID.PinkFairy);
                    }
                }
                if (Song == 2250)
                {
                    Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/RingSpawn"));
                }
                npc.rotation = 0.0f;
                npc.ai[0] = (float)ai * 1f;
                if ((double)npc.ai[0] >= 850.0)
                {
                    ai = 0;
                    npc.alpha = 0;
                    npc.ai[2] = 0;
                }
                if ((double)npc.ai[0] == 200.0)
                {
                    //later
                }
                if ((double)npc.ai[0] == 100.0 || (double)npc.ai[0] == 120.0 || (double)npc.ai[0] == 140.0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/StarFall"));
                    /*int numberofproj = 10;
                    for (int i = 0; i < numberofproj; i++)
                    {
                        Vector2 spawnPos = npc.Center;
                        Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));


                        VelPos.Normalize();
                        Projectile.NewProjectile(spawnPos, VelPos * 8f, ModContent.ProjectileType<StarFall>(), npc.damage, 5f, Main.myPlayer);
                    }*/
                    CircleSpawn(10, ModContent.ProjectileType<StarFall>(), npc.Center, npc.damage, 5f, 8f);
                }
            }
        }
        void CircleSpawn(int numberofproj, int Type, Vector2 spawnPos, int damage, float KB, float Speed)
        {
            for (int i = 0; i < numberofproj; i++)
            {
                Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));


                VelPos.Normalize();
                Projectile.NewProjectile(spawnPos, VelPos * Speed, Type, damage, KB, Main.myPlayer);
            }
        }
        void OuterAttack(bool Side_TrueIsRight, int Type, float Speed, int Damage)
        {
            if (Side_TrueIsRight)
            {
                Projectile.NewProjectile(npc.Center + new Vector2(150 + Main.rand.Next(1, 300), -900), new Vector2(0f, Speed), Type, Damage, 10f, Main.myPlayer);
            }
            if (!Side_TrueIsRight)
            {
                Projectile.NewProjectile(npc.Center + new Vector2(-150 + Main.rand.Next(-300, -1), - 900), new Vector2(0f, Speed), Type, Damage, 10f, Main.myPlayer);
            }
        }
    }
    public class StarFall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;        //The recording mode
        }
        public override void SetDefaults()
        {
            projectile.timeLeft = 300;
            projectile.width = 50;
            projectile.height = 50;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.aiStyle = 0;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 55; i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 212, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0, Main.DiscoColor);
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation += 0.03f;
            Lighting.AddLight(projectile.position, Color.White.ToVector3() * 2f);
        }
    }
    public class RingFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen Flame");
            Main.projFrames[projectile.type] = 4;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.alpha = 255;
            projectile.aiStyle = -1;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, DustID.Clentaminator_Red, new Vector2(0, 0));
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
}
