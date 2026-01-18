using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;

    // SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(rb.velocity.y);
            rb.AddForce(transform.up * jumpForce);
        }

        // 좌우 이동 (flipX 사용)
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) {
            key = 1;
            // sr.flipX = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            key = -1;
            // sr.flipX = true;
        }

        // 플레이어 속도
        float speedx = Mathf.Abs(this.rb.velocity.x);

        // 좌우 이동
        if (speedx < this.maxWalkSpeed)
        {
            this.rb.AddForce(transform.right * key * this.walkForce);
        }

        // 좌우 방향 전환 (LocalScale 사용)
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 애니메이션 속도 조절
        animator.speed = speedx / 2.0f;

        // PLAYER 가 카메라 밖으로 나갔을 때 GameScene 다시 시작. (좌우 포함)
        if (transform.position.x < -3.5f || transform.position.x > 3.5f || transform.position.y < -10f)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D: " + collision.gameObject.name);
        SceneManager.LoadScene("ClearScene");
    }
}

// OnCollistionEnter2D / OnTriggerEnter2D
// 충돌한 순간
// OnCollisionStay2D / OnTriggerStay2D
// 총돌 중
// OnCollisionExit2D / OnTriggerExit2D
// 충돌 끝
