using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //ゲームのステータスを定義
    enum State
    {
        Ready,
        Play,
        GameOver
    }

    State state; //自作した型を扱う変数

    public AzarashiController azarashi; //Azarashiのスクリプト
    public GameObject blocks; //Blocksオブジェクト

    int score; //得点用

    // Start is called before the first frame update
    void Start()
    {
        //開始と同時にReadyステータスにする
        Ready();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //常にゲームのステータスをチェック
        //状況に応じてやることが変わる
        switch (state)
        {
            //もしReady状態だったら
            case State.Ready:
                //ボタンが押され次第、GameStart処理
                if (Input.GetButtonDown("Fire1")) GameStart();
                break;

            //もしPlay状態だったら
            case State.Play:
                //Azarashiを操作して何かに当たったらGameOver処理
                if (azarashi.IsDead()) GameOver();
                break;

            //もしGameOver状態だったら
            case State.GameOver:
                //ボタンが押され次第、Reload処理
                if (Input.GetButtonDown("Fire1")) Reload();
                break;
        }
    }

    void Ready()
    {
        //ステータスをReadyにする
        state = State.Ready;

        //AzarashiのActiveでない状態にする
        //※isKinematicがtrueになる
        azarashi.SetSteerActive(false);

        //Block達は存在しない
        blocks.SetActive(false);
    }

    void GameStart()
    {
        //ステータスをPlayにする
        state = State.Play;

        //AzarashiのActiveな状態にする（動く）
        //※isKinematicがfalseになる
        azarashi.SetSteerActive(true);

        //Block達を存在させる
        blocks.SetActive(true);

        //Playになった時、まずは一回跳ねる
        azarashi.Flap();
    }

    void GameOver()
    {
        //ステータスをPlayにする
        state = State.GameOver;

        //Find系メソッドで全ての「ScrollObject」コンポーネントの情報を取得（配列名 scrollObjectsへ）
        ScrollObject[] scrollObjects = FindObjectsOfType<ScrollObject>();

        //配列に特化した繰り返し構文
        //foreachを使って、配列scrollObjectsの中身
        //全てに順番に処理
        foreach (ScrollObject so in scrollObjects) so.enabled = false; //各々のScrollObjectスクリプトの存在をなしにする
    }

    void Reload()
    {
        //SceneManagerクラスのGetActiveSceneメソッドで「今のScene情報」を取得できる。さらにその名前を取得して変数currentSceneNameへ。
        string currentSceneName = SceneManager.GetActiveScene().name;

        //Sceneの切り替え
        SceneManager.LoadScene(currentSceneName);
    }

    //他のスクリプトから呼び出される
    public void IncreaseScore()
    {
        //スコアが1増える
        score++;
    }
}
