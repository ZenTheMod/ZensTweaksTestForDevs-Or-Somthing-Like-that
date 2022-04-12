using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest;

namespace ZensTweakstest.Items.HMmechZenItems
{
    public class VoidSheath : ModItem
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var keys = ZensTweakstest.SheathHotkey.GetAssignedKeys();
            if (keys != null) 
            {
                if (keys.Count > 0)
                {
                    //It's assigned to something
                    string key = keys[0];
                    var line = new TooltipLine(mod, "AKeyToolTip", "Summon a ring of zenic flames with " +
                    key +
                    "\n1.5 Second Cooldown.");
                    tooltips.Add(line);
                }
            }
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 40;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 10);
            item.rare = 4;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<Charred_Life>().VoidSheathEQ = true;
        }
    }
}
