using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ZensTweakstest.Prefixes
{
    public class SwordPrefix : ModPrefix
    {
		private readonly byte _power;

		// see documentation for vanilla weights and more information
		// note: a weight of 0f can still be rolled. see CanRoll to exclude prefixes.
		// note: if you use PrefixCategory.Custom, actually use ChoosePrefix instead, see ExampleInstancedGlobalItem
		public override float RollChance(Item item)
			=> 2f;

		// determines if it can roll at all.
		// use this to control if a prefixes can be rolled or not
		public override bool CanRoll(Item item)
			=> true;

		// change your category this way, defaults to Custom
		public override PrefixCategory Category
			=> PrefixCategory.Melee;

		public SwordPrefix()
		{
		}

		public SwordPrefix(byte power)
		{
			_power = power;
		}

		// Allow multiple prefix autoloading this way (permutations of the same prefix)
		public override bool Autoload(ref string name)
		{
			if (!base.Autoload(ref name))
			{
				return false;
			}

			mod.AddPrefix("Radiant", new SwordPrefix(1));
			return false;
		}
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
			damageMult = 1.2f;
			useTimeMult = 0.9f;
        }
        public override void Apply(Item item)
			=> item.GetGlobalItem<InstancedGlobalItem>().awesome = _power;

		public override void ModifyValue(ref float valueMult)
		{
			float multiplier = 1f + 0.05f * _power;
			valueMult *= multiplier;
		}
	}
	public class InstancedGlobalItem : GlobalItem
    {
		public byte awesome;
		public InstancedGlobalItem()
		{
			awesome = 0;
		}

		public override bool InstancePerEntity => true;

		public override GlobalItem Clone(Item item, Item itemClone)
		{
			InstancedGlobalItem myClone = (InstancedGlobalItem)base.Clone(item, itemClone);
			myClone.awesome = awesome;
			return myClone;
		}
		public override int ChoosePrefix(Item item, UnifiedRandom rand)
		{
			if ((item.accessory || item.damage > 0) && item.maxStack == 1 && rand.NextBool(30))
			{
				return mod.PrefixType("Awesome");
			}
			return -1;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			//if (!item.social && item.prefix > 0)
			//{
				//int awesomeBonus = awesome - Main.cpItem.GetGlobalItem<InstancedGlobalItem>().awesome;
				//if (awesomeBonus > 0)
				//{
					//TooltipLine line = new TooltipLine(mod, "PrefixAwesome", "+" + awesomeBonus + " awesomeness")
					//{
						//isModifier = true
					//};
					//tooltips.Add(line);
				//}
			//}
		}
	}
}
