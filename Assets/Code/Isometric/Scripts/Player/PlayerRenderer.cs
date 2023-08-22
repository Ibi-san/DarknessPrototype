using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRenderer : MonoBehaviour
{

    public static readonly string[] StaticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static readonly string[] RunDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    private Animator _animator;
    private int _lastDirection;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void SetDirection(Vector2 direction)
    {

        string[] directionArray = null;


        if (direction.magnitude < .01f)
        {
            directionArray = StaticDirections;
        }
        else
        {
            directionArray = RunDirections;
            _lastDirection = DirectionToIndex(direction, 8);
        }
        _animator.Play(directionArray[_lastDirection]);
    }

    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {

        Vector2 normDir = dir.normalized;
        float step = 360f / sliceCount;
        float halfstep = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfstep;

        if (angle < 0)
            angle += 360;

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }


    public static int[] AnimatorStringArrayToHashArray(string[] animationArray)
    {
        int[] hashArray = new int[animationArray.Length];
        for (int i = 0; i < animationArray.Length; i++)
        {
            hashArray[i] = Animator.StringToHash(animationArray[i]);
        }
        return hashArray;
    }

}
