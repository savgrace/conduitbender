using UnityEngine;
using System.Collections;


public static class BendMessages
{
    public const string k_BendsTooClose = "Bends are too close.";
    public const string k_SegmentedRadiusTooSmall = "Segmented Radius is too small.";
    public const string k_AtLeast3Bends = "Accurate Method requires at least 3 Bends.";
    public const string k_StubLengthTooSmall = "Stub Length is too small.";
}

[System.Serializable]
public class BendParameter
{
    public enum Name
    {
        AngleDegrees,
        AngleFirstDegrees,
        AngleLastDegrees,
        CenterAngleDegrees,
        DevelopedLength,
        DistanceFromEnd,
        DistanceTo1st,
        DistanceTo2nd,
        DistanceTo3rd,
        DistanceToLast,
        Distance2ndTo3rd,
        DistanceBetween,
        KickFirstMark,
        KickOffset,
        KickSpread,
        KickTravel,
        LengthOfBend,
        LengthOfCenterBend,
        LengthOfHalfBend,
        OffsetHeight,
        Rise,
        Roll,
        RollAngleDegrees,
        SaddleHeight,
        SaddleLength,
        SegmentedAngle,
        SegmentedCount,
        SegmentedMethod,
        SegmentedRadius,
        Shift,
        ShrinkToCenter,     // 3/4 point saddle
        Spacing,
        StubLength,
        StubMethod,
        StubTakeUp,
        TotalShrink
    }
    public enum Type { FloatAngle, Float, Integer, StringEnum }

    public static readonly string[] NameStrings = new string[36] {
        "Bend Angle (Degrees)",
        "1st Bend Angle (Degrees)",
        "Last Bend Angle (Degrees)",
        "Center Angle (Degrees)",
        "Developed Length",
        "Mark Distance from End",
        "Distance to 1st Mark",
        "Distance to 2nd Mark",
        "Distance to 3rd Mark",
        "Distance to Last Mark",
        "Distance 2nd to 3rd Mark",
        "Distance Between Bends",
        "1st Kick Mark Distance",
        "Kick Offset",
        "Kick Spread",
        "Kick Travel",
        "Length of Bend",
        "Length of Center Bend",
        "Length of Half Bend",
        "Offset Height",
        "Rise",
        "Roll",
        "Roll Angle (Degrees)",
        "Saddle Height",
        "Saddle Length",
        "Segmented Angle (Degrees)",
        "Number of Bends",
        "Segmented Method",
        "Segmented Radius",
        "Shift",
        "Shrink To Saddle Center",
        "Spacing",
        "Stub Length",
        "Stub Calculation Method",
        "Stub Take-Up",
        "Total Shrink"
    };
    private static readonly object[][] NameRangesMetric = new object[][] {
        new object[] {0.5f, 90f },  // Bend Angle
        new object[] {0f, 0f },     // 1st Bend Angle
        new object[] {0f, 0f },     // Last Bend Angle
        new object[] {0.5f, 90f },  // Center Angle
        new object[] {0f, 0f },   // Developed Length
        new object[] {0f, 0f },   // Distance from End
        new object[] {0f, 0f },   // Distance to 1st Mark
        new object[] {0f, 0f },   // Distance to 2nd Mark
        new object[] {0f, 0f },   // Distance to 3rd Mark
        new object[] {0f, 0f },   // Distance to Last Mark
        new object[] {0f, 0f },   // Distance 2nd to 3rd
        new object[] {0f, 0f },   // Distance Between Bends
        new object[] {0f, 0f },   // Kick First Mark
        new object[] {0f, 30f },  // Kick Offset
        new object[] {0f, 0f },   // Kick Spread
        new object[] {0f, 0f },   // Kick Travel
        new object[] {0f, 0f },   // Length of Bend
        new object[] {0f, 0f },   // Length of Center Bend
        new object[] {0f, 0f },   // Length of Half Bend
        new object[] {0f, 3f },   // Offset Height
        new object[] {0f, 3f },   // Rise
        new object[] {0f, 3f },   // Roll
        new object[] {0f, 0f },   // Roll Angle
        new object[] {0f, 3f },   // Saddle Height
        new object[] {0f, 3f },   // Saddle Length
        new object[] {0.5f, 90f },  // Segmented Angle
        new object[] {2, 90},     // Number of Bends
        new object[] { GlobalEnum.SegmentedBendMethod.First(), GlobalEnum.SegmentedBendMethod.Last() },
        new object[] {0f, 30f }, // Segmented Radius
        new object[] {0f, 0f },  // Shift
        new object[] {0f, 0f },   // Shrink To Center
        new object[] {0f, 30f },   // Spacing
        new object[] {0f, 3f },    // Stub Length
        new object[] { GlobalEnum.StubUpMethod.First(), GlobalEnum.StubUpMethod.Last() },
        new object[] {0f, 0f },  // Stub Take-Up
        new object[] {0f, 0f }   // Total Shrink
    };
    private static readonly object[][] NameRangesStandard = new object[][] {
        new object[] {0.5f, 90f },  // Bend Angle
        new object[] {0f, 0f },     // 1st Bend Angle
        new object[] {0f, 0f },     // Last Bend Angle
        new object[] {0.5f, 90f },  // Center Angle
        new object[] {0f, 0f },   // Developed Length
        new object[] {0f, 0f },   // Distance from End
        new object[] {0f, 0f },   // Distance to 1st Mark
        new object[] {0f, 0f },   // Distance to 2nd Mark
        new object[] {0f, 0f },   // Distance to 3rd Mark
        new object[] {0f, 0f },   // Distance to Last Mark
        new object[] {0f, 0f },   // Distance 2nd to 3rd
        new object[] {0f, 0f },   // Distance Between Bends
        new object[] {0f, 0f },   // Kick First Mark
        new object[] {0f, 100f },  // Kick Offset
        new object[] {0f, 0f },   // Kick Spread
        new object[] {0f, 0f },   // Kick Travel
        new object[] {0f, 0f },   // Length of Bend
        new object[] {0f, 0f },   // Length of Center Bend
        new object[] {0f, 0f },   // Length of Half Bend
        new object[] {0f, 8f },   // Offset Height
        new object[] {0f, 8f },   // Rise
        new object[] {0f, 8f },   // Roll
        new object[] {0f, 0f },   // Roll Angle
        new object[] {0f, 8f },   // Saddle Height
        new object[] {0f, 8f },   // Saddle Length
        new object[] {0.5f, 90f },  // Segmented Angle
        new object[] {2, 90},     // Number of Bends
        new object[] { GlobalEnum.SegmentedBendMethod.First(), GlobalEnum.SegmentedBendMethod.Last() },
        new object[] {0f, 100f }, // Segmented Radius
        new object[] {0f, 0f },  // Shift
        new object[] {0f, 0f },   // Shrink To Center
        new object[] {0f, 100f },   // Spacing
        new object[] {0f, 8f },     // Stub Length
        new object[] { GlobalEnum.StubUpMethod.First(), GlobalEnum.StubUpMethod.Last() },
        new object[] {0f, 0f },  // Stub Take-Up
        new object[] {0f, 0f }      // Total Shrink
    };


    public string colorHexString
    {
        get { return m_ColorHexString; }
    }
    /// <summary>
    /// A color to associate with the parameter
    /// </summary>
    [SerializeField]
    public Color        color;
    [SerializeField]
    public Name         name;
    [SerializeField]
    public Type         type;
    [SerializeField]
    public object       valueObject;
    /// <summary>
    /// Value should always be in Metric, if it is a measurement of Length.
    /// </summary>
    [SerializeField]
    public object       value;
    [SerializeField]
    public bool         enabled = true;

    private string      m_ColorHexString;

    public BendParameter( Name name, Type type, Color color, object value, object valueObject = null, bool enabled = true)
    {
        this.name = name;
        this.type = type;
        this.color = color;
        this.value = value;
        this.valueObject = valueObject;

        // Make Color String
        m_ColorHexString = ((int)(color.r * 255)).ToString( "X2" ) 
            + ((int)(color.g * 255)).ToString( "X2" )
            + ((int)(color.b * 255)).ToString( "X2" )
            + ((int)(color.a * 255)).ToString( "X2" );
    }

    public static object[] GetRange( Name name )
    {
        switch (Engine.unitType) {
            case Units.Type.Metric:
                return NameRangesMetric[ (int)name ];

            case Units.Type.Standard:
                return NameRangesStandard[ (int)name ];

        }
        return null;
    }
    /// <summary>
    /// Returns value of Parameter formatted as a string, returned in current display unit type (Feet or Meters)
    /// </summary>
    /// <param name="bendParam"></param>
    /// <returns></returns>
    public static string GetFormattedValue( BendParameter bendParam )
    {
        switch (bendParam.type) {
            case BendParameter.Type.FloatAngle:
                return bendParam.value.ToString();
            case BendParameter.Type.Float:
                return Units.Format( Engine.unitType, Engine.outputRulerUnit, GetExternalValue( (float) bendParam.value ) );
            case BendParameter.Type.Integer:
                return bendParam.value.ToString();
            case BendParameter.Type.StringEnum:
                StringEnum se = (StringEnum) bendParam.valueObject;
                return se.ToStringValue( (int)bendParam.value );
        }
        return "";
    }
    public static string GetStringValue( Name name )
    {
        return NameStrings[ (int)name ];
    }
    /// <summary>
    /// Converts 'value' from internal units (which are Metric) to Standard (Feet) if Engine unitMode is set 
    /// to Standard. If 'value' is not a numeric type, an exception may be thrown.
    /// Does Not modify 'value'.
    /// </summary>
    public static float GetExternalValue( BendParameter param )
    {
        if (Engine.unitType == Units.Type.Standard) {
            return (float)param.value * Units.k_MToFt;
        }
        return (float)param.value;
    }
    /// <summary>
    /// Converts 'value' from internal units (which are Metric) to Standard (Feet) if Engine unitMode is set 
    /// to Standard. If 'value' is not a numeric type, an exception may be thrown.
    /// Does Not modify 'value'.
    /// </summary>
    public static float GetExternalValue( float value )
    {
        if (Engine.unitType == Units.Type.Standard) {
            return value * Units.k_MToFt;
        }
        return value;
    }

}
