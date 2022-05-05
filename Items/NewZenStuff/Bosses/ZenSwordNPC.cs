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
using ZensTweakstest.Items.TheBanners;

namespace ZensTweakstest.Items.NewZenStuff.Bosses
{
    public class ZenSwordNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen Stone Sword");
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
            banner = npc.type;
            bannerItem = ModContent.ItemType<ZenStoneSwordBanner>();
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
