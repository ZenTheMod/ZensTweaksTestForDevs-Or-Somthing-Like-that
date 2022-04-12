using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.Dusts;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using System;
using MonoMod.Cil;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.NpcSS
{
    public class ZenStoneSpirit : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen-Stone Spirit");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.DemonEye];
        }

        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 22;
            npc.aiStyle = 2;
            npc.damage = 25;
            npc.defense = 5;
            npc.lifeMax = 150;
            npc.knockBackResist = 0.8f;
            npc.value = 100f;

            npc.buffImmune[BuffID.Burning] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.lavaImmune = true;

            aiType = -1;
            animationType = NPCID.DemonEye;

            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath51;

            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);

            banner = npc.type;
            bannerItem = ModContent.ItemType<ZenStoneSpiritBanner_I>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.GetModPlayer<Charred_Life>().ZenZone)
            {
                return 1f;
            }
            else
            {
                return SpawnCondition.Underworld.Chance * 5f;
            }
        }
        public override void AI()
        {
            if (!npc.noTileCollide)
            {
                if (npc.collideX)
                {
                    npc.velocity.X = npc.oldVelocity.X * -0.5f;
                    if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                    {
                        npc.velocity.X = 2f;
                    }
                    if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                    {
                        npc.velocity.X = -2f;
                    }
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                    if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                    {
                        npc.velocity.Y = 1f;
                    }
                    if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                    {
                        npc.velocity.Y = -1f;
                    }
                }
            }
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
            if (NPC.downedPlantBoss)
            {
                if (Main.rand.Next(0, 75) == 5)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenStonePlating"), 1);
                    for (int i = 0; i < 55; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                        Dust d = Dust.NewDustPerfect(npc.position, DustID.SomethingRed, speed * 5, Scale: 1.5f);
                        d.noGravity = true;
                    }
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                if (Main.rand.Next(1, 9) == 4)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZenStoneSpirit_Remains"), npc.scale);
                }
            }
        }
    }
}
