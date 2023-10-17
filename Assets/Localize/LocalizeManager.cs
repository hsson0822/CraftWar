using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeManager : MonoBehaviour {
	public string textDataPath = "LanguageData";
	public SystemLanguage baseLanguage;
	public SystemLanguage[] useLanguages;
	private SystemLanguage systemLanguage;

	public Dictionary<SystemLanguage, List<string>> textDic = new Dictionary<SystemLanguage, List<string>> ();

	public static LocalizeManager Instance {
		get {
			return instance;
		}
	}
	private static LocalizeManager instance;

    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        SystemLanguage deviceSystemLanguage = Application.systemLanguage;
        switch (deviceSystemLanguage)
        {

            case SystemLanguage.Korean:
                systemLanguage = deviceSystemLanguage;
                break;
            default:
                systemLanguage = baseLanguage;
                break;
        }

        foreach (SystemLanguage tLanguage in useLanguages)
        {
            textDic.Add(tLanguage, new List<string>());
        }

        TextAsset ta = Resources.Load<TextAsset>(textDataPath);
        string[] lines = ta.text.Split('\n');
        foreach (string line in lines)
        {
            string[] words = line.Split('\t');
            for (int i = 0; i < useLanguages.Length; i++)
            {
                textDic[useLanguages[i]].Add(words[i]);

            }
        }
    }
	public static string GetText(int index) {
		if (Instance == null) {
			Debug.LogError ("Instance Null");
			return string.Empty;
		}
		if (index < 0 || index >= Instance.textDic [Instance.systemLanguage].Count) {
			Debug.LogError ("Wrong Index : " + index);
			return string.Empty;
		}
		return Instance.textDic [Instance.systemLanguage] [index];
	}
}
