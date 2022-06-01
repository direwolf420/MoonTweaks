using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace MoonTweaks
{
	public class Config : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		public static Config Instance => ModContent.GetInstance<Config>();

		public const string ModeNone = "None";
		public const string ModeNightly = "Nightly"; //Default
		public const string ModeNightlyExtra = "Nightly (Extra)";
		public const string ModeCustom = "Custom";

		[Header("=== Mode Explanations ===" +
		"\n*" + ModeNone + "*: No changes to the moon style" +
		"\n*" + ModeNightly + "*: Moon changes appearance every night. Disclaimer: Does not work if time is directly set (via Journey Mode, server cmd, etc.)" +
		"\n*" + ModeNightlyExtra + "*: Same as Nightly, but includes special moons ("+ Frost + ", " + Pumpkin + ", " + Smiley + ")" +
		"\n*" + ModeCustom + "*: Use the below config setting to specify the moon style manually")]

		[BackgroundColor(200, 50, 50)]
		[Label("Moon Style Mode")]
		[Tooltip("Specify the way the moon style should change")]
		[DrawTicks]
		[OptionStrings(new string[] { ModeNone, ModeNightly, ModeNightlyExtra, ModeCustom })]
		[DefaultValue(ModeNightly)]
		public string MoonStyleMode;

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

		[BackgroundColor(225, 75, 75)]
		[Label("Custom Moon Style")]
		[Tooltip("Directly sets the moon style. Only takes effect if Moon Style Mode is set to 'Custom'")]
		[DrawTicks]
		[OptionStrings(new string[] { Unchanged, Normal, Yellow, Ringed, Mythril, BrightBlue, Green, Pink, Orange, Purple, Frost, Pumpkin, Smiley })]
		[DefaultValue(Unchanged)]
		public string MoonStyle;

		public const string UnchangedPhase = "Unchanged";
		public const string FullMoon = "Full Moon"; //0
		public const string WaningGibbous = "Waning Gibbous";
		public const string ThirdQuarter = "Third Quarter";
		public const string WaningCrescent = "Waning Crescent";
		public const string NewMoon = "New Moon";
		public const string WaxingCrescent = "Waxing Crescent";
		public const string FirstQuarter = "First Quarter";
		public const string WaxingGibbous = "Waxing Gibbous"; //7

		[Header("Other")]
		[BackgroundColor(78, 78, 78)]
		[Label("Custom Moon Phase")]
		[Tooltip("Directly sets the moon phase")]
		[DrawTicks]
		[OptionStrings(new string[] { UnchangedPhase, FullMoon, WaningGibbous, ThirdQuarter, WaningCrescent, NewMoon, WaxingCrescent, FirstQuarter, WaxingGibbous })]
		[DefaultValue(UnchangedPhase)]
		public string MoonPhase;

		public override void OnChanged()
		{
			if (MoonStyleMode == ModeCustom)
			{
				MoonTweaks.displayMoonType = GetMoonStyleIndexFromStyle(MoonStyle, out int index) ? index : -1;
			}

			if (MoonStyleMode == ModeNone)
			{
				MoonTweaks.displayMoonType = -1;
			}
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			//Correct invalid names
			if (Array.IndexOf(new string[] { ModeNone, ModeNightly, ModeNightlyExtra, ModeCustom }, MoonStyleMode) < 0)
			{
				MoonStyleMode = ModeNightly;
			}

			if (!GetMoonStyleIndexFromStyle(MoonStyle, out _))
			{
				MoonStyle = Unchanged;
			}

			if (!GetMoonPhaseIndexFromPhase(MoonPhase, out _))
			{
				MoonPhase = UnchangedPhase;
			}
		}

		/// <summary>
		/// Assigns the index for Main.moonType (special moons are "appended" to existing styles!), if the given style matches the valid inputs. False if unchanged or invalid input.
		/// </summary>
		/// <param name="style"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		internal static bool GetMoonStyleIndexFromStyle(string style, out int index)
		{
			//No Unchanged
			index = Array.IndexOf(new string[] { Normal, Yellow, Ringed, Mythril, BrightBlue, Green, Pink, Orange, Purple, Frost, Pumpkin, Smiley }, style);
			return index > -1;
		}

		/// <summary>
		/// Assigns the index for Main.moonPhase, if the given type matches the valid inputs. False if unchanged or invalid input.
		/// </summary>
		/// <param name="phase"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		internal static bool GetMoonPhaseIndexFromPhase(string phase, out int index)
		{
			//No UnchangedPhase
			index = Array.IndexOf(new string[] { FullMoon, WaningGibbous, ThirdQuarter, WaningCrescent, NewMoon, WaxingCrescent, FirstQuarter, WaxingGibbous }, phase);
			return index > -1;
		}
	}
}
