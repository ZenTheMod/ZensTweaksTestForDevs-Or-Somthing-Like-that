using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.HMmechZenItems.BossCosmetics
{
    [AutoloadEquip(EquipType.Head)]
    public class MechZenMask : ModItem
    {
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = 4;
			item.vanity = true;
		}
		public override bool DrawHead()
		{
			return false;
		}
	}
}
