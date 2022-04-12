using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using ZensTweakstest.Items.NewZenStuff.Lore;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Bags;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Tropys;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Summon;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot.Solutions;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot.SummonStaff;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot.Prisim;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;
using ZensTweakstest.Items.NewZenStuff.Tiles;
using ZensTweakstest.Items.NewZenStuff.Tiles.ZSF_I_T;
using ZensTweakstest.Items.NewZenStuff.Bosses.Zen.Loot.Pacebles;
using ZensTweakstest.Items.NewZenStuff.Bosses.Zen.Loot;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZensTweakstest.Items.HMmechZenItems;

namespace ZensTweakstest.Items.Npcs
{
    [AutoloadHead]
    public class ZenMane : ModNPC
    {
        private static bool shop1;

        private static bool shop2;

        private float Rotation = 0f;
        public override string Texture {
            get { return "ZensTweakstest/Items/Npcs/ZenMane"; }
        }

        public override bool Autoload(ref string name)
        {
            name = "Ok-ish Builder";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.DangerDetectRange[npc.type] = 50;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackFrameCount[npc.type] = 5;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 12;
            npc.defense = 30;
            npc.lifeMax = 5000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Rotation += 0.07f;
            float NegiRotation = -Rotation;
            spriteBatch.Draw(mod.GetTexture("Items/Npcs/ZenEffect"), npc.Center - Main.screenPosition, null, new Color(254, 84, 92), Rotation, mod.GetTexture("Items/Npcs/ZenEffect").Size() / 2, 1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(mod.GetTexture("Items/Npcs/ZenEffect2O"), npc.Center - Main.screenPosition, null, new Color(254, 84, 92), NegiRotation, mod.GetTexture("Items/Npcs/ZenEffect2O").Size() / 2, 1f, SpriteEffects.None, 0f);
            return true;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
           for(int k = 0; k < 255; k++)
           {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }

                foreach(Item item in player.inventory)
                {
                    if(item.type == mod.ItemType("electrons"))
                    {
                        return true;
                    }
                }
           }
            return false;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(4))
            {
                case 0:
                    return "Toxini";
                case 1:
                    return "Dread";
                case 2:
                    return "Endra";
                default:
                    return "Zeny";
            }
        }

        public override string GetChat()
        {
            int otherNPC = NPC.FindFirstNPC(NPCID.Angler);
            int otherNPC2 = NPC.FindFirstNPC(NPCID.Cyborg);
            if (otherNPC >= 0 && Main.rand.NextBool(8))
            {
                return "I FRACKING HATE " + Main.npc[otherNPC].GivenName + " the Angler!!! or was his name Timothy?";
            }
            if (otherNPC2 >= 0 && Main.rand.NextBool(7))
            {
                return "I hope " + Main.npc[otherNPC2].GivenName + " the Cyborg knows C#";
            }
            switch (Main.rand.Next(7))
            {
                case 0:
                    return "I haven't had sleep in 7 years";
                case 1:
                    return "Why am I a guide rip-off. Or is the guide a rip-off of me?";
                case 2:
                    return "I can promise you im not a furry.";
                case 3:
                    return $"[c/7f2440:Items with this color of rarity are uncraftable.]" +
                        "\n[c/7f2440:Purchase them from ME!]";
                case 4:
                    return "I attack stuff with doors because I ate a Doorknob";
                case 5:
                    return "Combat text is epic.";
                default:
                    return "... I dont have anything to say.";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Shop");
            button2 = "Zen Stuff";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
                {
                    shop1 = true;
                    shop2 = false;
                }
            }

            else if (!firstButton)
            {
                shop = true;
                {
                    shop2 = true;
                    shop1 = false;
                }
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (shop1)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("electrons"));
                nextSlot++;

                if (Main.hardMode)
                {
                    if (NPC.downedMoonlord)
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("ZensSanityBreaker"));
                        nextSlot++;
                    }

                    shop.item[nextSlot].SetDefaults(mod.ItemType("Moldy_Ichor"));
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(mod.ItemType("szsb"));
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZSBWI>());
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZSPWI>());
                    nextSlot++;
                }

                if (Main.expertMode)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("Zen_s_Visulized_Power"));
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(mod.ItemType("ots"));
                    nextSlot++;
                }

                shop.item[nextSlot].SetDefaults(mod.ItemType("steal"));
                nextSlot++;

                shop.item[nextSlot].SetDefaults(mod.ItemType("nbw_Item"));
                nextSlot++;

                shop.item[nextSlot].SetDefaults(mod.ItemType("navybrickwb_i"));
                nextSlot++;

                shop.item[nextSlot].SetDefaults(mod.ItemType("lbed"));
                nextSlot++;

                shop.item[nextSlot].SetDefaults(mod.ItemType("lchr"));
                nextSlot++;

                shop.item[nextSlot].SetDefaults(mod.ItemType("llubric"));
                nextSlot++;
            }

            else if (shop2)
            {
                if (ZenWorld.DownedMechZen)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<BeaconOfWar>());
                    nextSlot++;
                }
                if (ZenWorld.DownedZenGaurd)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<Zen_Peeve_Essence>());
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZenStonePlating>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5, 0, 0);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ZensCraftingChutes>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10, 99, 99);
                    nextSlot++;
                }

                if (NPC.downedGolemBoss)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ShatteredPrisim>());
                    nextSlot++;
                }
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 25;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 2;
            randExtraCooldown = 5;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<DoorZen>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}