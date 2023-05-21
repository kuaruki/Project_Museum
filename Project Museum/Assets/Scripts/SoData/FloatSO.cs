using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
    [SerializeField]
    private float _value_x;
    [SerializeField]
    private float _value_y;
    [SerializeField]
    private float _value_z;


    public float Value_X {
        get {
            return _value_x;
        }
        set { _value_x = value; }
    }
    public float Value_Y {
        get {
            return _value_y;
        }
        set { _value_y = value; }
    }
    public float Value_Z {
        get {
            return _value_z;
        }
        set { _value_z = value; }
    }

}
