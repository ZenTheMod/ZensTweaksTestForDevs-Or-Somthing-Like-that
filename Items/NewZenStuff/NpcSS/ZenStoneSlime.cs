using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.Dusts;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.NpcSS
{
    public class ZenStoneSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen-Stone Slime");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.LavaSlime];
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 24;
            npc.aiStyle = 1;
            npc.damage = 25;
            npc.defense = 1;
            npc.lifeMax = 175;
            npc.knockBackResist = 0.8f;
            npc.value = 125f;

            npc.buffImmune[BuffID.Burning] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.lavaImmune = true;

            aiType = NPCID.LavaSlime;
            animationType = NPCID.LavaSlime;

            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            banner = npc.type;
            bannerItem = ModContent.ItemType<ZenStoneSlimeBanner_I>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.GetModPlayer<Charred_Life>().ZenZone)
            {
                return 1f;
            }
            else
            {
                return SpawnCondition.Underworld.Chance * 3f;
            }
        }

        public override void NPCLoot()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);

            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(5, 15));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenStone_I"), 15);

            if (Main.rand.Next(0, 100) >= 35)
            {
                if (NPC.downedPlantBoss)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenitrinOre_I"), 10);
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
    }
}