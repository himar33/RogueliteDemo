using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet", order = 1)]
public class Bullet : ScriptableObject
{
    public Sprite b_sprite;
    public int b_velocity;
}
