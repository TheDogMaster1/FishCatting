using System;

[Serializable]
public class CustomSkins
{
    public enum Skins
    {
        morgana,
        librarian,
        cute,
        cottage
    };

    public Skins customskinName;

    public bool isUnlocked;

}
