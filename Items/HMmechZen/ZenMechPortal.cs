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
    public class ZenMechPortal : ModNPC
    {
        private int ai;
        private int doubleBeam;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("◭Portal◮");//HEY THATS NOT WHAT THE SOMMONING ITEM WAS SUPPOSED TO TOOO!!!
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.width = 256;
            npc.height = 256;

            npc.boss = true;
            npc.aiStyle = -1;
            npc.npcSlots = 5f;

            npc.lifeMax = 5000;
            npc.damage = 30;
            npc.defense = 15;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(gold: 5);

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit34;
            npc.DeathSound = SoundID.NPCDeath43;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/GlitchGlitchy");
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            var Portal = mod.GetTexture("Items/HMmechZen/ZenMechPortal");
            Vector2 position = npc.Center - Main.screenPosition;
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int num93 = 1; num93 < npc.oldPos.Length; num93++)
            {
                Vector2 drawPos = npc.oldPos[num93] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = Color.White * ((float)(NPCID.Sets.TrailCacheLength[npc.type] - num93) / (float)NPCID.Sets.TrailCacheLength[npc.type]);
                spriteBatch.Draw(Portal, drawPos, null, color, npc.rotation, drawOrigin, npc.scale - num93 / (float)npc.oldPos.Length, SpriteEffects.None, 0f);//npc.scale - num93 / (float)npc.oldPos.Length
            }
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            npc.TargetClosest();  //Get a target
            Vector2 direction = npc.DirectionTo(player.Center + new Vector2(0, -210));  //Get a direction to the player from the NPC
            npc.velocity += direction * 10f / 60f;//SPEEEED
            if (npc.velocity.LengthSquared() > 10 * 10)
                npc.velocity = Vector2.Normalize(npc.velocity) * 10;
            npc.rotation-=0.1f;

            ai++;
            npc.ai[0] = (float)ai * 1f;

            doubleBeam++;
            if (doubleBeam == 250 || doubleBeam == 340)
            {
                Main.PlaySound(SoundID.NPCDeath43, npc.position);
                CircleRing(5, ModContent.ProjectileType<PortalProj>());
            }
            if (doubleBeam > 430)
            {
                doubleBeam = 0;
            }

            if ((double)npc.ai[0] >= 90.0)
            {
                CircleRing(2, ModContent.ProjectileType<PortalProj>());
                ai = 0;
                npc.ai[2] = 0;
            }
        }
        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HMmechZenItems.BossCosmetics.PortalMusicBox>());
            Main.NewText("Zen Has Awoken", Color.Red, false);
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<MechZen>());
            Player player = Main.player[Main.myPlayer];
            player.CameraShake(23, 50);
            for (int i = 0; i < 75; i++)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 212, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 0, Main.DiscoColor, 2f);
            }
            Main.PlaySound(SoundID.NPCKilled, npc.position, 10);
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
}
