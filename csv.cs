using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class csv : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        /*
        // ファイル書き出し
        // 現在のフォルダにsaveData.csvを出力する(決まった場所に出力したい場合は絶対パスを指定してください)
        // 引数説明：第1引数→ファイル出力先, 第2引数→ファイルに追記(true)or上書き(false), 第3引数→エンコード
        StreamWriter sw = new StreamWriter(@"saveData.csv", false, Encoding.GetEncoding("Shift_JIS"));
        // ヘッダー出力
        string[] s1 = { "time", "x","y","z" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);

        sw.Close();
        

        // ファイル読み込み
        // 引数説明：第1引数→ファイル読込先, 第2引数→エンコード
        StreamReader sr = new StreamReader(@"saveData.csv", Encoding.GetEncoding("Shift_JIS"));
        string line;
        // 行がnullじゃない間(つまり次の行がある場合は)、処理をする
        while ((line = sr.ReadLine()) != null)
        {
            // コンソールに出力
            Debug.Log(line);
        }
        // StreamReaderを閉じる
        sr.Close();
        */
        var rot = playercamera.transform.rotation;
        var pos = playercamera.transform.rotation;
        time1 = 0;
        //GameObject.Find("Stop").GetComponent<Button>().interactable = false;
    }

    public GameObject playercamera;
    public GameObject input_;
    public float count_trigger = 0;
    public bool saves;
    private float time1;
    private double savetime;
    // Update is called once per frame
    void Update()
    {

        //Debug.Log(GameObject.Find("InputField").GetComponent<InputField>().text);

        if (saves)
        {
            
            if (savetime > 0.1)
            {
                var rot = playercamera.transform.rotation;
                var pos = playercamera.transform.position;
                string  VAS_v = GetComponent<Serial>().VAS.ToString("F0");  
                //StreamWriter sw = new StreamWriter(DataName, true, Encoding.GetEncoding("Shift_JIS"));
                StreamWriter sw = new StreamWriter(DataName, true);
                string[] onepos = { time1.ToString(), pos.x.ToString(), pos.y.ToString(), pos.z.ToString(), rot.x.ToString(), rot.y.ToString(), rot.z.ToString(), rot.w.ToString(), rot.eulerAngles.x.ToString(), rot.eulerAngles.y.ToString(), rot.eulerAngles.z.ToString(), VAS_v, count_trigger.ToString() };
                string poslog = string.Join(",", onepos);
                sw.WriteLine(poslog);
                sw.Flush();
                sw.Close();
                //onetimepos = GameObject.Find("Cube").transform.position;
                //poslog ={ onetimepos.x.ToString() , onetimepos.y.ToString() , onetimepos.z.ToString() };
                //s2 = string.Join(",", poslog);
                savetime = 0;
            }
            time1 += Time.deltaTime;
            savetime += Time.deltaTime;
        }
       
    }
    private string DataName;
    byte filenum_1 = 0;
    byte filenum_10 = 0;
    byte filenum_100 = 0;

    public void RecStart()
    {
        if (!saves)
        {
            saves = true;

            DataName = input_.GetComponent<InputField>().text + "000.csv";
            while (true)
            {
                if (File.Exists(DataName))
                {
                    filenum_1++;
                    if (filenum_1 > 9)
                    {
                        filenum_10++;
                        filenum_1 = 0;
                        if (filenum_10 > 9)
                        {
                            filenum_100++;
                            filenum_10 = 0;
                        }
                    }
                    DataName = input_.GetComponent<InputField>().text;
                    DataName += filenum_100;
                    DataName += filenum_10;
                    DataName += filenum_1;
                    DataName += ".csv";
                }
                else
                {
                    break;
                }
            }
            StreamWriter sw = new StreamWriter(DataName, false);
            // ヘッダー出力
            string[] s1 = { "time", "x", "y", "z", "Rx", "Ry", "Rz", "Rw", "Eulerx", "Eulery", "Eulerz", "VAS", "area"};
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            sw.Close();
        }
        else
        {
            saves = false;
        }   
    }
}