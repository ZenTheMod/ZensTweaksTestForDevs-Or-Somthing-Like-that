using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.JupiterStuff
{
    [AutoloadEquip(EquipType.Legs)]
    public class JupiLegs : ModItem
    {
		private float Interval = 0f;
		private Color Test = new Color(255, 145, 206);
		private Color Test2 = new Color(135, 66, 255);
		public override bool CloneNewInstances => true;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jupiter's Pants");
			Tooltip.SetDefault("Thanks for your work!");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.vanity = true;
			item.rare = 8;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine tooltipLine in tooltips)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = Color.Lerp(Test, Test2, Interval); //change the color accordingly to above
				}
			}
		}
		public override void UpdateInventory(Player player)
		{
			Interval += 0.01f;
			if (Interval >= 1f)
			{
				Interval = 0f;
			}
		}
        public override void UpdateVanity(Player player, EquipType type)
        {
			Interval += 0.01f;
			if (Interval >= 1f)
			{
				Interval = 0f;
			}
		}
    }
}
