

public class BabyCatMovement : PlayerMoveBase
{
    protected override float RayUpCastingDistance => 1f;
    protected override float RayUpDistance => 1.1f;
    protected override float RayDownCastingDistance => 0.5f;


    /* 위치 보정, 게임오버 판정 코루틴 (폐기)
    IEnumerator DetectFallAndOver()
    {
        isCoroutineRunning = true;

        yield return new WaitForSeconds(4f);

        Stage1Manager.Instance.StopGame();
        isCoroutineRunning = false;
    }

    IEnumerator JumpRight()
    {
        yield return _waitForSecJump;

        transform.eulerAngles = new Vector3(0.0f, 115f, 0.0f);
        _myRigid.velocity = new Vector3(_xValue, _yValue, 0.0f) * _jumpForce;
    }

    IEnumerator JumpLeft()
    {
        yield return _waitForSecJump;

        transform.eulerAngles = new Vector3(0.0f, 240f, 0.0f);
        _myRigid.velocity = new Vector3(-_xValue, _yValue, 0.0f) * _jumpForce;
    }

    IEnumerator MoveCorrect(RaycastHit hit)
    {
        yield return _waitForSec;

        Vector3 correctPos = new Vector3(hit.collider.bounds.center.x, hit.collider.bounds.center.y + 0.1f, transform.position.z);
        transform.position = correctPos;
    }*/

}


