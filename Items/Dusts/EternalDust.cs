using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.Dusts
{
    public class EternalDust : ModDust
    {
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 4.5f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= .99f;
			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			return false;
		}
		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return Color.White;
		}
	}
}
