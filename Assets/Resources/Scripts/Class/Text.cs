using UnityEngine;
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

    public Text(string french, string english)
    {
        this.french = french;
        this.english = english;
    }

    // Getter/Setter
    public string GetText()
    {
        if (language == SystemLanguage.French)
            return this.french;
        return this.english;
    }

    public string GetText(SystemLanguage language)
    {
        if (language == SystemLanguage.French)
            return this.french;
        return this.english;
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
