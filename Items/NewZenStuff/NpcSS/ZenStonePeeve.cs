using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.Dusts;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.NewZenStuff.Bosses;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.NpcSS
{
    public class ZenStonePeeve : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen-Stone Peeve");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 34;
            npc.aiStyle = -1;
            npc.damage = 25;
            npc.defense = 1;
            npc.lifeMax = 250;
            npc.knockBackResist = 0.9f;
            npc.value = 125f;

            npc.buffImmune[BuffID.Burning] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;

            animationType = NPCID.DungeonSpirit;

            npc.HitSound = SoundID.NPCHit35;
            npc.DeathSound = SoundID.NPCDeath39;

            banner = npc.type;
            bannerItem = ModContent.ItemType<PeeveBanner_I>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.GetModPlayer<Charred_Life>().ZenZone && ZenWorld.DownedZenGaurd)
            {
                return 0.3f;
            }
            if (ZenWorld.DownedZenGaurd)
            {
                return SpawnCondition.Underworld.Chance * 1.5f;
            }
            else
            {
                return 0f;
            }
        }
        public override void NPCLoot()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);

            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenStone_I"), 10);

            if (Main.rand.Next(0, 100) >= 75)
            {
                if (NPC.downedPlantBoss)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenitrinOre_I"), 15);
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZenStone_I"), 5);
            }
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Zen_Peeve_Essence"), 5);
            if (NPC.downedPlantBoss)
            {
                if (Main.rand.Next(0, 75) <= 5)
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
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if ((npc.life) >= 125)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                npc.rotation = (npc.Center - player.Center).ToRotation();
                npc.TargetClosest();  //Get a target
                Vector2 direction = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
                npc.velocity = direction * 6.5f;
            }
            else
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<ZenStoneFlameDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                npc.rotation = (npc.Center - player.Center).ToRotation();
                npc.TargetClosest();  //Get a target
                Vector2 direction = npc.DirectionTo(player.Center);  //Get a direction to the player from the NPC
                npc.velocity = direction * 10f;
            }
        }
    }
}
