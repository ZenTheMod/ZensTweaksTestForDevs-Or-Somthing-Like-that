using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.Utilities;
using ZensTweakstest.Helper;

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
        public override bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
        {
			if (item.prefix == ModContent.PrefixType<SwordPrefix>())
			{
				if (line.mod == "Terraria" && line.Name == "ItemName")
				{
					Vector2 messageSize = Helplul.MeasureString(line.text);
					Rectangle rec = new Rectangle(line.X - 40, line.Y - 2, (int)messageSize.X + 88, (int)messageSize.Y);
					// we end and begin a Immediate spriteBatch for shader
					Main.spriteBatch.BeginImmediate(true, true);
					GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
					Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, Color.Black);
					Main.spriteBatch.BeginImmediate(true, true, true);
					GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
					Color color = Helplul.CycleColor(new Color(244, 224, 58), new Color(50, 42, 88));
					Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, color);

					Main.spriteBatch.BeginImmediate(true, true);
					Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1);
					return false;
				}
			}
			return true;
		}
    }
}
