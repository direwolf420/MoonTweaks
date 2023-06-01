using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace MoonTweaks
{
	public class MoonTweaks : Mod
	{
		/// <summary>
		/// The override for the drawn moon style. Does NOT affect or change Main.moonType.
		/// </summary>
		public static int displayMoonType = -1;

		public override void Load()
		{
			On_Main.DrawSunAndMoon += Main_DrawSunAndMoon;

			On_Main.UpdateTime_StartNight += Main_UpdateTime_StartNight;
		}

		private void Main_UpdateTime_StartNight(On_Main.orig_UpdateTime_StartNight orig, ref bool stopEvents)
		{
			//Handle nightly change if enabled

			orig(ref stopEvents);

			Config config = Config.Instance;
			bool extra = config.MoonStyleModeNew == Config.MoonStyleModeType.NightlyExtra;
			if (config.MoonStyleModeNew == Config.MoonStyleModeType.Nightly || extra)
			{
				//Main.NewText("current displayed moon: " + displayMoonType);

				int oldType = displayMoonType == -1 ? Main.moonType : displayMoonType;
				do
				{
					int randLen = TextureAssets.Moon.Length;
					if (extra)
					{
						randLen += Config.SpecialStyleCount;
					}
					displayMoonType = Main.rand.Next(randLen);
				}
				while (oldType == displayMoonType);

				//Main.NewText("changed moon: " + displayMoonType);
			}
		}

		private void Main_DrawSunAndMoon(On_Main.orig_DrawSunAndMoon orig, Main self, Main.SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
		{
			//Handle all replacements
			bool origSnowMoon = Main.snowMoon;
			bool origPumpkinMoon = Main.pumpkinMoon;
			bool origDrunkWorldGen = WorldGen.drunkWorldGen;
			int origMoonType = Main.moonType;
			int origMoonPhase = Main.moonPhase;
			try
			{
				Config config = Config.Instance;
				int defLen = TextureAssets.Moon.Length;
				int len = defLen + Config.SpecialStyleCount;

				if (displayMoonType > -1 && displayMoonType < len)
				{
					int specialIndex = displayMoonType - defLen;
					if (specialIndex >= 0)
					{
						if (specialIndex == 0)
						{
							Main.snowMoon = true;
						}
						else if (specialIndex == 1)
						{
							Main.pumpkinMoon = true;
						}
						else if (specialIndex == 2)
						{
							WorldGen.drunkWorldGen = true;
						}
						/*
						else if (WorldGen.drunkWorldGen)
							spriteBatch.Draw(TextureAssets.SmileyMoon.Value, position2, new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.SmileyMoon.Width(), TextureAssets.SmileyMoon.Height()), moonColor, num9 / 2f + (float)Math.PI, new Vector2(TextureAssets.SmileyMoon.Width() / 2, TextureAssets.SmileyMoon.Width() / 2), num8, SpriteEffects.None, 0f);
						else if (pumpkinMoon)
							spriteBatch.Draw(TextureAssets.PumpkinMoon.Value, position2, new Microsoft.Xna.Framework.Rectangle(0, TextureAssets.PumpkinMoon.Width() * moonPhase, TextureAssets.PumpkinMoon.Width(), TextureAssets.PumpkinMoon.Width()), moonColor, num9, new Vector2(TextureAssets.PumpkinMoon.Width() / 2, TextureAssets.PumpkinMoon.Width() / 2), num8, SpriteEffects.None, 0f);
						else if (snowMoon)
							spriteBatch.Draw(TextureAssets.SnowMoon.Value, position2, new Microsoft.Xna.Framework.Rectangle(0, TextureAssets.SnowMoon.Width() * moonPhase, TextureAssets.SnowMoon.Width(), TextureAssets.SnowMoon.Width()), moonColor, num9, new Vector2(TextureAssets.SnowMoon.Width() / 2, TextureAssets.SnowMoon.Width() / 2), num8, SpriteEffects.None, 0f);
						*/
					}
					else
					{
						Main.moonType = displayMoonType;
					}
				}

				Config.MoonPhaseType moonPhase = config.MoonPhaseNew;
				if (moonPhase != Config.MoonPhaseType.Unchanged && Config.GetMoonPhaseIndexFromPhase(moonPhase, out int index))
				{
					Main.moonPhase = index;
				}

				orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
			}
			finally
			{
				Main.snowMoon = origSnowMoon;
				Main.pumpkinMoon = origPumpkinMoon;
				WorldGen.drunkWorldGen = origDrunkWorldGen;
				Main.moonType = origMoonType;
				Main.moonPhase = origMoonPhase;
			}
		}
	}
}
