using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Character", fileName = "New Character")]
public class Character : ScriptableObject
{
    public Texture CharacterDeadSprite;
    public Texture[] CharacterSprites;
}
