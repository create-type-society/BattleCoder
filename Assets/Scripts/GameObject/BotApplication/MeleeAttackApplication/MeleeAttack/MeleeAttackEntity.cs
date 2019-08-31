using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

 public class MeleeAttackEntity : MonoBehaviour
 {
    [SerializeField] private Animator anime;

    private float ConvertAngle(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return 0f;
            case Direction.Down:
                return 180f;
            case Direction.Left:
                return 270f;
            case Direction.Right:
                return 90f;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void MeleeAttack(Vector3 position, Direction direction)
    {
        Debug.Log(direction);
        gameObject.transform.position = position;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, ConvertAngle(direction));
        gameObject.SetActive(true);
        Debug.Log("近接攻撃しました");
        anime.Play("AttackEffect");
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsTag("end"))
        {
            gameObject.SetActive(false);
        }
    }
}
