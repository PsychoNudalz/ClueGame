using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumToString
{
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
           
            case Room.Ballroom:
                return "Ballroom";
            case Room.BilliardRoom:
                return "Billiard Room";
            case Room.Centre:
                return "Centre Room";
            case Room.Conservatory:
                return "Conservatory";
            case Room.DiningRoom:
                return "Dining Room";
            case Room.Hall:
                return "Hall";
            case Room.Kitchen:
                return "Kitchen";
            case Room.Library:
                return "Library";
            case Room.Lounge:
                return "Lounge";
            case Room.Study:
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
