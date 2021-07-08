using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public Animator anim;
  public Rigidbody2D rig;
  public SpriteRenderer spriteRenderer;

  public float health;
  public float speed;
  public float forceJump;

  bool isJumping;
  public bool vulnerable;

  private GameController gc;

  float direction;

  // Start is called before the first frame update
  void Start()
  {
    gc = FindObjectOfType<GameController>();
  }

  // Update is called once per frame
  void Update()
  {
    direction = Input.GetAxis("Horizontal");

    if (direction > 0)
    {
      transform.eulerAngles = new Vector2(0, 0);
    }
    if (direction < 0)
    {
      transform.eulerAngles = new Vector2(0, 180);
    }
    if (direction != 0 && !isJumping)
    {
      anim.SetInteger("transition", 1);
    }
    if (direction == 0 && !isJumping)
    {
      anim.SetInteger("transition", 0);
    }

    Jump();
  }

  private void FixedUpdate()
  {
    rig.velocity = new Vector2(direction * speed, rig.velocity.y);
  }

  void Jump()
  {
    if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
    {
      rig.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
      anim.SetInteger("transition", 2);
      isJumping = true;
    }
  }

  public void generateDamage()
  {
    if (!vulnerable)
    {
      gc.loserLife(health);
      health--;
      vulnerable = true;
      StartCoroutine(Respawn());
    }
  }

  IEnumerator Respawn()
  {
    for (int i = 0; i < 6; i++)
    {
      spriteRenderer.enabled = false;
      yield return new WaitForSeconds(0.2f);
      spriteRenderer.enabled = true;
      yield return new WaitForSeconds(0.2f);
    }
    vulnerable = false;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.layer == 3)
    {
      isJumping = false;
    }
  }
}