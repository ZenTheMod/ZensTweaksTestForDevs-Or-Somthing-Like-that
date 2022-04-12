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
        public override void AI()
        {
            npc.ResizeScaleOfJellyNPC();
            if (Main.rand.Next(2,100) == 5)
            {
                SpawnProj(ModContent.ProjectileType<AquaSoulProj>());
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

        void SpawnProj(int Type)
        {
            Projectile.NewProjectile(npc.position + new Vector2(0, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(50, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(-50, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(100, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(-100, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(150, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(-150, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(200, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(-200, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(250, 1800), Vector2.Zero, Type, 35, 9f);
            Projectile.NewProjectile(npc.position + new Vector2(-250, 1800), Vector2.Zero, Type, 35, 9f);
        }
    }
}
