using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Items2Because1IsTooFull;
using ZensTweakstest.Items.JupiterStuff;
using ZensTweakstest.Items.NewNonZen.Erichus.Loot;
using ZensTweakstest.Items.NewNonZen.Erichus;
using ZensTweakstest.Items.NewNonZen.Erichus.Boss;
using ZensTweakstest.Items.NewNonZen;
using ZensTweakstest.Items.EventItems;

namespace ZensTweakstest.Items.NewZenStuff.NpcSS
{
    public class GlobalNpc : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type == NPCID.Steampunker)
            {
                if (Main.rand.Next(0,11) == 4 && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    chat = "Rumors of a 'Green Meatball' have been going around." + "\nMaybe [c/63ff47:Sam] has something to say.";
                }
                if (Main.rand.Next(0, 9) == 4 && ZenWorld.DownedEric)
                {
                    chat = "So you did it. [c/63ff47:Erichus] what is it? a mech?";
                }
            }
        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Steampunker)
            {
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {
                    if (Main.netMode != 1)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<ShrineEricI>());
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 20);
                        ++nextSlot;
                    }
                    else
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<OminousFlesh>());
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 20);
                        ++nextSlot;
                    }
                }
                if (ZenWorld.DownedEric)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Butcherer>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 12);
                    ++nextSlot;

                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ToxicRevolverator>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 12);
                    ++nextSlot;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ToxicRocket>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 5);
                    ++nextSlot;

                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ToxicBarrel>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 12);
                    ++nextSlot;

                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<NuclearRotation>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 12);
                    ++nextSlot;

                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ToxicGrenade>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 12);
                    ++nextSlot;
                }
            }
            Mod alchLite =
                ModLoader.GetMod("AlchemistNPCLite");
            if (alchLite != null)
            {
                if (type == alchLite.NPCType("Musician"))
                {
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<Bosses.Loot.BagLoot.SG_Box>());
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        ++nextSlot;
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZenNightMusicBoxI>());
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        ++nextSlot;
                    }
                }
            }
            Mod alchFull =
                ModLoader.GetMod("AlchemistNPC");
            if (alchFull != null)
            {
                if (type == alchFull.NPCType("Musician"))
                {
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<Bosses.Loot.BagLoot.SG_Box>());
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        ++nextSlot;
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZenNightMusicBoxI>());
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        ++nextSlot;
                    }
                }
            }
        }
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.SkeletronHead)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CursedBone>(), Main.rand.Next(5, 16));
            }
        }
    }
    public class GlobalIamTem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag" && arg == ItemID.GolemBossBag)
            {
                if (Main.rand.Next(1, 9) == 5)
                player.QuickSpawnItem(ModContent.ItemType<slimy>(), 1);
            }
            if (context == "bossBag" && arg == ItemID.KingSlimeBossBag)
            {
                if (Main.rand.Next(1, 13) == 5)
                    player.QuickSpawnItem(ModContent.ItemType<DonutShoes>(), 1);
            }
        }
    }
}
