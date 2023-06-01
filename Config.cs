using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace MoonTweaks
{
	public class Config : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		public static Config Instance => ModContent.GetInstance<Config>();

		//Old data and names for reference
		[JsonExtensionData]
		private IDictionary<string, JToken> _additionalData = new Dictionary<string, JToken>();

		public const string ModeNone = "None";
		public const string ModeNightly = "Nightly";
		public const string ModeNightlyExtra = "Nightly (Extra)";
		public const string ModeCustom = "Custom";

		public enum MoonStyleModeType : byte
		{
			None = 0,
			Nightly = 1,
			NightlyExtra = 2,
			Custom = 3,
		}

		[Header("ModeExplanations")]

		[BackgroundColor(200, 50, 50)]
		[DrawTicks]
		[DefaultValue(MoonStyleModeType.Nightly)]
		public MoonStyleModeType MoonStyleModeNew;

		public const string Unchanged = "Unchanged";
		public const string Normal = "Normal";
		public const string Yellow = "Yellow";
		public const string Ringed = "Ringed";
		public const string Mythril = "Mythril";
		public const string BrightBlue = "Bright Blue";
		public const string Green = "Green";
		public const string Pink = "Pink";
		public const string Orange = "Orange";
		public const string Purple = "Purple";
		public const int SpecialStyleCount = 3;
		public const string Frost = "Frost";
		public const string Pumpkin = "Pumpkin";
		public const string Smiley = "Smiley";

		public enum MoonStyleType : byte
		{
			//Values matter as they match vanilla magic numbers
			Unchanged = byte.MaxValue,
			Normal = 0,
			Yellow = 1,
			Ringed = 2,
			Mythril = 3,
			BrightBlue = 4,
			Green = 5,
			Pink = 6,
			Orange = 7,
			Purple = 8,
			Frost = 9,
			Pumpkin = 10,
			Smiley = 11
		}

		[BackgroundColor(225, 75, 75)]
		[DrawTicks]
		[DefaultValue(MoonStyleType.Unchanged)]
		public MoonStyleType MoonStyleNew;

		public const string UnchangedPhase = "Unchanged";
		public const string FullMoon = "Full Moon"; //0
		public const string WaningGibbous = "Waning Gibbous";
		public const string ThirdQuarter = "Third Quarter";
		public const string WaningCrescent = "Waning Crescent";
		public const string NewMoon = "New Moon";
		public const string WaxingCrescent = "Waxing Crescent";
		public const string FirstQuarter = "First Quarter";
		public const string WaxingGibbous = "Waxing Gibbous"; //7

		public enum MoonPhaseType : byte
		{
			//Values matter as they match vanilla magic numbers
			Unchanged = byte.MaxValue,
			FullMoon = 0,
			WaningGibbous = 1,
			ThirdQuarter = 2,
			WaningCrescent = 3,
			NewMoon = 4,
			WaxingCrescent = 5,
			FirstQuarter = 6,
			WaxingGibbous = 7
		}

		[Header("Other")]

		[BackgroundColor(78, 78, 78)]
		[DrawTicks]
		[DefaultValue(MoonPhaseType.Unchanged)]
		public MoonPhaseType MoonPhaseNew;
		
		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			//port "MoonStyleMode": "Custom"
			//"MoonStyle" : "Pumpkin"
			//"MoonPhase" : "Waxing Crescent"
			//from string to enum, which requires (!) a member rename aswell
			JToken token;
			//TODO for no reason I can think of, this doesn't work
			if (_additionalData.TryGetValue("MoonStyleMode", out token))
			{
				var moonStyleMode = token.ToObject<string>();
				if (moonStyleMode == ModeCustom)
				{
					MoonStyleModeNew = MoonStyleModeType.Custom;
				}
				else if(moonStyleMode == ModeNightlyExtra)
				{
					MoonStyleModeNew = MoonStyleModeType.NightlyExtra;
				}
				else if (moonStyleMode == ModeNone)
				{
					MoonStyleModeNew = MoonStyleModeType.None;
				}
				else
				{
					MoonStyleModeNew = MoonStyleModeType.Nightly;
				}
			}
			if (_additionalData.TryGetValue("MoonStyle", out token))
			{
				var moonStyle = token.ToObject<string>();
				if (moonStyle == Normal)
				{
					MoonStyleNew = MoonStyleType.Normal;
				}
				else if (moonStyle == Yellow)
				{
					MoonStyleNew = MoonStyleType.Yellow;
				}
				else if (moonStyle == Ringed)
				{
					MoonStyleNew = MoonStyleType.Ringed;
				}
				else if (moonStyle == Mythril)
				{
					MoonStyleNew = MoonStyleType.Mythril;
				}
				else if (moonStyle == BrightBlue)
				{
					MoonStyleNew = MoonStyleType.BrightBlue;
				}
				else if (moonStyle == Green)
				{
					MoonStyleNew = MoonStyleType.Green;
				}
				else if (moonStyle == Pink)
				{
					MoonStyleNew = MoonStyleType.Pink;
				}
				else if (moonStyle == Orange)
				{
					MoonStyleNew = MoonStyleType.Orange;
				}
				else if (moonStyle == Purple)
				{
					MoonStyleNew = MoonStyleType.Purple;
				}
				else if (moonStyle == Frost)
				{
					MoonStyleNew = MoonStyleType.Frost;
				}
				else if (moonStyle == Pumpkin)
				{
					MoonStyleNew = MoonStyleType.Pumpkin;
				}
				else if (moonStyle == Smiley)
				{
					MoonStyleNew = MoonStyleType.Smiley;
				}
				else
				{
					MoonStyleNew = MoonStyleType.Unchanged;
				}
			}
			if (_additionalData.TryGetValue("MoonPhase", out token))
			{
				var moonPhase = token.ToObject<string>();
				if (moonPhase == FullMoon)
				{
					MoonPhaseNew = MoonPhaseType.FullMoon;
				}
				else if (moonPhase == WaningGibbous)
				{
					MoonPhaseNew = MoonPhaseType.WaningGibbous;
				}
				else if (moonPhase == ThirdQuarter)
				{
					MoonPhaseNew = MoonPhaseType.ThirdQuarter;
				}
				else if (moonPhase == WaningCrescent)
				{
					MoonPhaseNew = MoonPhaseType.WaningCrescent;
				}
				else if (moonPhase == NewMoon)
				{
					MoonPhaseNew = MoonPhaseType.NewMoon;
				}
				else if (moonPhase == WaxingCrescent)
				{
					MoonPhaseNew = MoonPhaseType.WaxingCrescent;
				}
				else if (moonPhase == FirstQuarter)
				{
					MoonPhaseNew = MoonPhaseType.FirstQuarter;
				}
				else if (moonPhase == WaxingGibbous)
				{
					MoonPhaseNew = MoonPhaseType.WaxingGibbous;
				}
				else
				{
					MoonPhaseNew = MoonPhaseType.Unchanged;
				}
			}

			_additionalData.Clear(); //Clear this or it'll crash.

			//Correct invalid values to default fallback
			EnumFallback(ref MoonStyleModeNew, MoonStyleModeType.Nightly);
			EnumFallback(ref MoonStyleNew, MoonStyleType.Unchanged);
			EnumFallback(ref MoonPhaseNew, MoonPhaseType.Unchanged);
		}

		private static void EnumFallback<T>(ref T value, T defaultValue) where T : Enum
		{
			if (!Enum.IsDefined(typeof(T), value))
			{
				value = defaultValue;
			}
		}

		public override void OnChanged()
		{
			if (MoonStyleModeNew == MoonStyleModeType.Custom)
			{
				MoonTweaks.displayMoonType = GetMoonStyleIndexFromStyle(MoonStyleNew, out int index) ? index : -1;
			}

			if (MoonStyleModeNew == MoonStyleModeType.None)
			{
				MoonTweaks.displayMoonType = -1;
			}
		}

		/// <summary>
		/// Assigns the index for Main.moonType (special moons are "appended" to existing styles!), if the given style matches the valid inputs. False if unchanged or invalid input.
		/// </summary>
		/// <param name="style"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		internal static bool GetMoonStyleIndexFromStyle(MoonStyleType style, out int index)
		{
			index = -1;
			if (style != MoonStyleType.Unchanged)
			{
				index = (byte)style;
			}
			return index > -1;
		}

		/// <summary>
		/// Assigns the index for Main.moonPhase, if the given type matches the valid inputs. False if unchanged or invalid input.
		/// </summary>
		/// <param name="phase"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		internal static bool GetMoonPhaseIndexFromPhase(MoonPhaseType phase, out int index)
		{
			index = -1;
			if (phase != MoonPhaseType.Unchanged)
			{
				index = (byte)phase;
			}
			return index > -1;
		}
	}
}
