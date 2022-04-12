using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using ZensTweakstest.Items;

namespace ZensTweakstest.Backgrounds
{
    public class ZenSGBG : ModSurfaceBgStyle
    {
		public override bool ChooseBgStyle()
		{
			return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<Charred_Life>().ZenZone;
		}

		// Use this to keep far Backgrounds like the mountains.
		public override void ModifyFarFades(float[] fades, float transitionSpeed)
		{
			for (int i = 0; i < fades.Length; i++)
			{
				if (i == Slot)
				{
					fades[i] += transitionSpeed;
					if (fades[i] > 1f)
					{
						fades[i] = 1f;
					}
				}
				else
				{
					fades[i] -= transitionSpeed;
					if (fades[i] < 0f)
					{
						fades[i] = 0f;
					}
				}
			}
		}
		
		public override int ChooseFarTexture()
		{
			return mod.GetBackgroundSlot("Backgrounds/FarZenSurface");
		}
		public override int ChooseMiddleTexture()
		{
			return mod.GetBackgroundSlot("Backgrounds/MiddleZenSurface");
		}

		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
		{
			return mod.GetBackgroundSlot("Backgrounds/FrontZenSurface");
		}
	}
	public class ZenUGBG : ModUgBgStyle
    {
		public override bool ChooseBgStyle()
		{
			return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<Charred_Life>().ZenZone;
		}
		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/ZenUG0");
			textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/ZenUG1");
			textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/ZenUG2");
			textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/ZenUG3");
			textureSlots[4] = mod.GetBackgroundSlot("Backgrounds/ZenHellMerge");
			//textureSlots[5] = mod.GetBackgroundSlot("Backgrounds/ZenUG4");
		}
	}
}
