using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.NewNonZen.Erichus
{
    public class ToxicRuble : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(@"Increases speed and all damage.
" + "\"DNA Quote\"");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.09f; // The acceleration multiplier of the player's movement speed
            player.allDamage += 0.09f;
            player.GetModPlayer<Charred_Life>().RubleTrail = true;
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 42;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 7);
            item.rare = ItemRarityID.Pink;
            item.expert = true;
        }
    }
}
