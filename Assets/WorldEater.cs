using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(World))]
public class WorldEater : MonoBehaviour
{
    World world;

    void Start()
    {
        world = GetComponent<World>();    
    }

    void FixedUpdate()
    {
        int x = Random.Range(0, world.WorldWidth);
        int y = world.GetHighestY(x);
        world.BreakBlock(x, y);
    }
}
