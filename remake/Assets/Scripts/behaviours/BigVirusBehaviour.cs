using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigVirusBehaviour : MonoBehaviour
{
    private Animator _animator;
    public float rotateSpeed = 1f;
    public float radius = 2f;
    public float positionModifier;
    public Vector2 centre;
    private float _angle;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_animator.GetBool("UserLost"))
        {
            if (_angle == 0)
            {
                _angle = positionModifier;
            }
            else
            {
                _angle += rotateSpeed * (Time.deltaTime);

            }

            Vector2 offset = new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle)) * radius;
            transform.position = centre + offset;
        }
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(positionModifier);
    }

    private IEnumerator WaitVirusDownTime()
    {
        yield return new WaitForSeconds(Constants.VirusWaitToUp);
        _animator.SetBool("isDown", false);
    }

    private IEnumerator WaitVirusDeadTime()
    {
        yield return new WaitForSeconds(Constants.VirusWaitToUp);
        Destroy(transform.gameObject);
    }

    public void SetVirusDown()
    {
        _animator.SetBool("isDown", true);
        StartCoroutine(WaitVirusDownTime());
    }

    public void SetVirusDead()
    {
        _animator.SetBool("isDead", true);
    }

    public void Destroy()
    {
        SetVirusDead();
    }



}