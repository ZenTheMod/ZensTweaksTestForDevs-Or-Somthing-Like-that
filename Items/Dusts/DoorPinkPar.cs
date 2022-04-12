using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.Dusts
{
    public class DoorPinkPar : ModDust
    {

		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 1.5f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.99f;
			float strength = 1.2f;
			Lighting.AddLight(dust.position, Color.Red.ToVector3() * strength);
			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}
