using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Gyro : MonoBehaviour
{
    /// <summary>
    /// ジャイロという名前の重力変化用スクリプト
    /// PCでも操作可能（Aキーで左に90℃変化・Dキーで右に90℃変化する。）
    /// </summary>
    public float nowGyro;//現在の重力（他スクリプトから参照する）
    public bool isControl = true;//操作不可能状態を指す。もうちょっとマシな名前にできなかったものかとちょっと後悔
    public float waitTime = 1.5f;//重力変化時の硬直時間。インスペクタより変更可能。
    //public int oldGyro;//古い重力を保存しておき、重力に変化があったかを確かめる。
    public float gravity = 30f;
    Vector3 gyroV;
    float[] setgyro = new float[2];
    void Start()
    {
        Physics.gravity = new Vector3(0, -30f, 0);
    }
    void Update()
    {
#if UNITY_EDITOR
        //エディター上でのみ行われる処理
        //重力操作を可能にする
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (gyroV.z >= 360)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                gyroV = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
                gyroV = new Vector3(0, 0, gyroV.z + 90);
            }

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (gyroV.z <= 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
                gyroV = new Vector3(0, 0, 270);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 90);
                gyroV = new Vector3(0, 0, gyroV.z - 90);
            }
        }
#elif UNITY_ANDROID
        //逆にアンドロイドでのみ行われる処理。VisualStudioだとここ白くなってすごく見辛い。
        Input.gyro.enabled = true;
		if (Input.gyro.enabled && isControl)
        {
            Quaternion gyro = Input.gyro.attitude;//スマホの向きを取り
            gyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));//縦持ち用に角度を調整して
            gyroV = gyro.eulerAngles;//オイラー角に変換する。
        }
#elif UNITY_IOS
        Input.gyro.enabled = true;
		if (Input.gyro.enabled && isControl)
        {
            Quaternion gyro = Input.gyro.attitude;//スマホの向きを取り
            gyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));//縦持ち用に角度を調整して
            gyroV = gyro.eulerAngles;//オイラー角に変換する。
        }
#endif
        //Debug.Log(gyroV.z);
        //Debug.Log(setgyro[0]);
        //Debug.Log(setgyro[1]);
        if (gyroV.z <= 90 || gyroV.z >= 270)
        {
            //下に重力y-90
            if (gyroV.z >= 270) setgyro[0] = 270 - gyroV.z;
            else setgyro[0] = gyroV.z - 90;
        }
        else
        {
            //上に重力y+90
            if (gyroV.z <= 180) setgyro[0] = gyroV.z - 90;
            else setgyro[0] = gyroV.z - 180;
        }
        if (gyroV.z <= 180)
        {
            //左に重力x-90
            if (gyroV.z <= 90) setgyro[1] = gyroV.z * -1;
            else setgyro[1] = gyroV.z - 180;
        }
        else
        {
            //右に重力x+90
            if (gyroV.z <= 270) setgyro[1] = gyroV.z - 180;
            else setgyro[1] = 360 - gyroV.z;
        }
        for (int i = 0; i < 2; i++)
        {
            setgyro[i] /= 90;
        }
        nowGyro = gyroV.z;

        Physics.gravity = new Vector3(setgyro[1] * gravity, setgyro[0] * gravity, 0);
    }
}
