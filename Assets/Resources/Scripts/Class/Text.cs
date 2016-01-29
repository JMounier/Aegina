﻿using UnityEngine;
using System.Collections;

public class Text
{

    private string french;
    private string english;
    private static SystemLanguage language = SystemLanguage.English;

    // Constructors
    public Text()
    {
        this.french = "";
        this.english = "";
    }

    public Text(string english, string french)
    {
        this.french = french;
        this.english = english;
    }

    // Getter/Setter
    public string GetText
    {
        get
        {
            if (language == SystemLanguage.English)
                return this.english;
            return this.french;
        }
    }

    public static SystemLanguage GetLanguage()
    {
        return language;
    }
    public static void SetLanguage(SystemLanguage wantlanguage)
    {
        language = wantlanguage;
    }

}
