using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnerComponent : MonoBehaviour {

    public enum Owner { NotSet, Player, AI }
    public Owner owner;
}
