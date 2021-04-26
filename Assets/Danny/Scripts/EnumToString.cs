using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to get a string value from an enum
/// </summary>
public static class EnumToString
{
    /// <summary>
    /// Get a string value from a valid enum
    /// </summary>
    /// <param name="enumInput">Enum to get string value from</param>
    /// <returns>String value of Enum, throws exception if not valid enum</returns>
    public static string GetStringFromEnum(System.Enum enumInput)
    {
        switch (enumInput)
        {
            case CharacterEnum.ColMustard:
                return "Colonel Mustard";
            case CharacterEnum.MissScarlett:
                return "Miss Scarlett";
            case CharacterEnum.MrsPeacock:
                return "Mrs Peacock";
            case CharacterEnum.MrsWhite:
                return "Mrs White";
            case CharacterEnum.ProfPlum:
                return "Professor Plum";
            case CharacterEnum.RevGreen:
                return "Reverend Green";
           
            case RoomEnum.Ballroom:
                return "Ballroom";
            case RoomEnum.BilliardRoom:
                return "Billiard Room";
            case RoomEnum.Centre:
                return "Centre Room";
            case RoomEnum.Conservatory:
                return "Conservatory";
            case RoomEnum.DiningRoom:
                return "Dining Room";
            case RoomEnum.Hall:
                return "Hall";
            case RoomEnum.Kitchen:
                return "Kitchen";
            case RoomEnum.Library:
                return "Library";
            case RoomEnum.Lounge:
                return "Lounge";
            case RoomEnum.Study:
                return "Study";
            
            case WeaponEnum.CandleStick:
                return "Candlestick";
            case WeaponEnum.Dagger:
                return "Dagger";
            case WeaponEnum.LeadPipe:
                return "Lead Pipe";
            case WeaponEnum.Revolver:
                return "Revolver";
            case WeaponEnum.Rope:
                return "Rope";
            case WeaponEnum.Spanner:
                return "Spanner";
            default:
                throw new System.Exception("Name not found");
        }
    }
}
