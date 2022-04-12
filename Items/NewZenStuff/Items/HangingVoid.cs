using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using ZensTweakstest.Items.buffs;
using ZensTweakstest.Items.NewZenStuff.Bosses.SparkArmor;

namespace ZensTweakstest.Items.NewZenStuff.Items
{
    public class HangingVoid : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The Eternal Void Drips From This Gateway.");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 42;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Yellow;
            item.expert = true;
            item.buffType = ModContent.BuffType<EternalVoid>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(item.buffType, 2, true);
            player.statDefense += 15;
            player.statLifeMax2 += 5;
            player.allDamage += 0.09f;
        }
        public override void UpdateInventory(Player player)
        {
            if (item.favorited)
            {
                player.GetModPlayer<Charred_Life>().EternalVoidTrail = true;
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawOutlines = true;
            }
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            // When the item is given a prefix, only roll the best modifiers for accessories
            return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding });
        }
    }
}
