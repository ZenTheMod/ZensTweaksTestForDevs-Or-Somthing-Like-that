using Terraria.ModLoader;
using Terraria.ID;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
    [AutoloadEquip(EquipType.Head)]
    public class SparkMask : ModItem
    {
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = ItemRarityID.Yellow;
			item.vanity = true;
		}
		public override bool DrawHead()
		{
			return false;
		}
	}
}
