using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.Dusts;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MonoMod.Cil;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.Bosses
{
    public class ZenSwordNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen-Stone Sword");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.EnchantedSword];
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.EnchantedSword);
            npc.damage = 120;
            npc.width = 60;
            npc.height = 60;
            npc.lifeRegen = 5;
            npc.lifeMax = 600;
            npc.defense = 5;
            npc.knockBackResist = 0.8f;
            animationType = NPCID.EnchantedSword;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (ZenWorld.DownedZenGaurd)
            {
                return SpawnCondition.Underworld.Chance / 3;
            }
            else
            {
                return 0f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int num93 = 1; num93 < npc.oldPos.Length; num93++)
            {
                Vector2 drawPos = npc.oldPos[num93] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(drawColor) * ((float)(NPCID.Sets.TrailCacheLength[npc.type] - num93) / (float)NPCID.Sets.TrailCacheLength[npc.type]);
                spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewZenStuff/Bosses/SwordZenTrail"), drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void NPCLoot()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);

            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenStone_I"), 30);

            if (Main.rand.Next(0, 100) >= 35)
            {
                if (NPC.downedPlantBoss)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenitrinOre_I"), 5);
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenStone_I"), 5);
            }
        }
    }
}
