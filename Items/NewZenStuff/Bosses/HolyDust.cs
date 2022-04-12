using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.NewZenStuff.Bosses
{
    public class HolyDust : ModDust
    {
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 1.25f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.99f;
			float light = 0.35f * dust.scale;
			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			else
			{
				float strength = dust.scale / 2f;
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), dust.color.R / 227f * 1.3f * strength, dust.color.G / 68f * 1.3f * strength, dust.color.B / 68f * 1.3f * strength);
			}
			return false;
		}
		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return Color.White;
		}
	}
}
