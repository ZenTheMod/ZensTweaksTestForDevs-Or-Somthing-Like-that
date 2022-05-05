using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;//helo neko or pyx wellcome to bad code
using QwertysRandomContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Helper;
using ZensTweakstest.Items;

namespace ZensTweakstest.Items.CryoDepths.Enfyshing
{
    public class Enfyshing : ModNPC
    {
        public override void SetDefaults()
        {
            npc.width = 64;
            npc.height = 52;

            npc.boss = true;
            npc.aiStyle = -1;
            npc.npcSlots = 5f;

            npc.lifeMax = 5000;
            npc.damage = 30;
            npc.defense = 13;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(gold: 11);

            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.color = Color.White;
            //Amongus , SUS
            //LOL this is cool
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;//pee
        }//ima create some predraw shit. NAME: EnfyshingTentiticle ill add that
        int ai = 0;
        Vector2 Pos;
        int PosOnOrOff = 0;
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Lighting.AddLight(npc.Center, 0, 0, 1);
            if (PosOnOrOff == 0)
            {
                npc.ResizeScaleOfJellyNPC(0f);
                Pos = npc.position;
                PosOnOrOff = 1;
                npc.position += new Vector2(0, 1500);
            }
            else if (PosOnOrOff == 1)
            {
                npc.ResizeScaleOfJellyNPC(0.1f);
                bool isAtleast50cmclosetothegivenpoint = Vector2.Distance(npc.position, Pos) < 10f;
                npc.position = Vector2.Lerp(npc.position, Pos, 0.03f);
                Dust dust = Dust.NewDustPerfect(npc.Center, DustID.BlueTorch, new Vector2(0, 0),0,default,1.3f);
                Dust dust2 = Dust.NewDustPerfect(npc.Center + new Vector2(30,0), DustID.BlueTorch, new Vector2(0, 0), 0, default, 1.3f);
                Dust dust3 = Dust.NewDustPerfect(npc.Center + new Vector2(-30, 0), DustID.BlueTorch, new Vector2(0, 0), 0, default, 1.3f);
                if (isAtleast50cmclosetothegivenpoint)
                {
                    npc.ResizeScaleOfJellyNPC(0.15f);
                    PosOnOrOff = 2;
                    Main.PlaySound(SoundID.Roar, npc.position, 0);
                    Main.LocalPlayer.CameraShake(12, 60);
                    int numberofproj = 16;
                    for (int i = 0; i < numberofproj; i++)
                    {
                        Vector2 spawnPos = new Vector2(npc.Center.X, npc.Center.Y + 76);
                        Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));

                        VelPos.Normalize();
                        Projectile.NewProjectile(spawnPos, VelPos * 2f, ModContent.ProjectileType<AquaSoulProj>(), 45, 9f, Main.myPlayer, 0, npc.whoAmI);
                    }
                }
            }
            else
            {
                npc.ResizeScaleOfJellyNPC(0f);
                ai++;
                if (ai == 150)
                {
                    SpawnProj(ModContent.ProjectileType<AquaSoulProj>(), 0);
                }
                if (ai == 220)
                {
                    SpawnProj(ModContent.ProjectileType<AquaSoulProj>(), 50);
                }
                if (ai > 700)
                {
                    ai = 0;
                } 
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {//LOL wtf. how did he played it /// IKR LIKE SO GOD LIKE
            //JELLY MAMA jelly gelly mama mah mah
            //minor spelling mistake (insert death emoji)//anyways how do i add it to the current code i has
            //you just had to replace the npc.rotation to tentacleRotation in the spriteBatch. Draw ,ok
            //float tentacleRotation = (pos - npc.Center).ToRotation();

            float SinWave = (float)Math.Sin(Main.GameUpdateCount / 15f);
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.17f);//select a code line
            Vector2 drawPos = npc.Center - Main.screenPosition + drawOrigin;//here is bad predraw vvvvvv
            spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/CryoDepths/Enfyshing/EnfyshingTentiticle"), drawPos + new Vector2(-npc.width * 0.5f, 20) + new Vector2(0, SinWave), null, Color.White, npc.rotation/*no remove/replace*/, new Vector2(7, 0), npc.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/CryoDepths/Enfyshing/EnfyshingTentiticle"), drawPos + new Vector2(-npc.width * 0.5f, 17) + new Vector2(-19, SinWave), null, Color.White, npc.rotation - (SinWave / 3f), new Vector2(7, 0), npc.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/CryoDepths/Enfyshing/EnfyshingTentiticle"), drawPos + new Vector2(-npc.width * 0.5f, 17) + new Vector2(19, SinWave), null, Color.White, npc.rotation + (SinWave / 3f), new Vector2(7, 0), npc.scale, SpriteEffects.None, 0f);
            return false;//yeah sure. but i dont exactly know wat is dis. is it like suppose to be a boss ? yeshy
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 pos = npc.Center - Main.screenPosition;
            spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/CryoDepths/Enfyshing/Enfyshing"), pos + new Vector2(0,8), null, Color.White, npc.rotation, ModContent.GetTexture("ZensTweakstest/Items/CryoDepths/Enfyshing/Enfyshing").Size() * 0.5f, npc.scale, SpriteEffects.None, 0f);
        }
        public override Color? GetAlpha(Color drawColor) => Color.White;

        void SpawnProj(int Type, int Offset)
        {
            Vector2 Position = npc.position + new Vector2(Offset, 0);
            int numberofproj = 10;
            for (int i = 0; i < numberofproj; i++)
            {
                int X = i * 100;
                Projectile.NewProjectile(Position + new Vector2(X, 1600), Vector2.Zero, Type, 25, 9f);
                Projectile.NewProjectile(Position + new Vector2(-X, 1600), Vector2.Zero, Type, 25, 9f);
            }
        }
    }
}
