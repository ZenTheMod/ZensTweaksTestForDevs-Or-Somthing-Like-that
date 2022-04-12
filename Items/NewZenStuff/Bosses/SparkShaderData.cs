using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ZensTweakstest.Items.NewZenStuff.Bosses
{
    public class SparkShaderData : ScreenShaderData
    {
		private int SparkIndex;

		public SparkShaderData(string passName)
			: base(passName)
		{
		}

		private void UpdatePuritySpiritIndex()
		{
			int SparkType = ModLoader.GetMod("ZensTweakstest").NPCType("SparkGaurdian");
			if (SparkIndex >= 0 && Main.npc[SparkIndex].active && Main.npc[SparkIndex].type == SparkType)
			{
				return;
			}
			SparkIndex = -1;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == SparkType)
				{
					SparkIndex = i;
					break;
				}
			}
		}

		public override void Apply()
		{
			UpdatePuritySpiritIndex();
			if (SparkIndex != -1)
			{
				UseTargetPosition(Main.npc[SparkIndex].Center);
			}
			base.Apply();
		}
	}
}
