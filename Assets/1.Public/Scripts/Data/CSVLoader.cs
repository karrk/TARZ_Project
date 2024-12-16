using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Zenject;
using UniRx;

public class CSVLoader : IInitializable
{
    [Inject] private CoroutineHelper helper;
    [Inject] private DataParser dataPaser;
    [Inject] private PoolManager pool;

    private Dictionary<E_CSVTableType, (string, string)> mainTableUrls = new Dictionary<E_CSVTableType, (string, string)>();
    
    private string address =
        "https://docs.google.com/spreadsheets/d/137s1RWBL9EhBj7b39oCQNWcrG5j6bLx3q43rzQbTQaA";

    private string range = "A2:A2";
    private string sheetID = "0";

    UnityWebRequest request;
    private string cacheStr;
    private string[] cacheArr;

    /// <summary>
    /// CSV 데이터 테이블을 불러옵니다.
    /// </summary>
    public void Initialize()
    {
        helper.StartCoroutine(ProcedureLoadData());
    }

    private string ConvertCSVAddress(string address, string sheetID, string range = "A2:A2")
    {
        return $"{address}/export?format=csv&range={range}&gid={sheetID}";
    }

    private string[] SplitCSV(string data)
    {
        return data.Split(',');
    }

    private IEnumerator ProcedureLoadData()
    {
        // 메인 데이터 테이블, 상단에 등록된 주소와 범위, SheetID
        yield return MainTableDataLoad();
        yield return TableDataLoad(E_CSVTableType.Monster);

        pool.RegistPools();
    }

    private IEnumerator MainTableDataLoad()
    {
        request = UnityWebRequest.Get(ConvertCSVAddress(address, sheetID, range));
        yield return request.SendWebRequest();
        // GetMainTable Range

        range = request.downloadHandler.text;

        request = UnityWebRequest.Get(ConvertCSVAddress(address, sheetID, range));
        yield return request.SendWebRequest();
        // GetMainTable URL,ID

        cacheArr = SplitCSV(request.downloadHandler.text);
        int typeIdx = 0;

        for (int i = 0; i < cacheArr.Length; i += 2)
        {
            mainTableUrls.Add((E_CSVTableType)typeIdx, (cacheArr[0], cacheArr[1]));
        }
    }

    private IEnumerator TableDataLoad(E_CSVTableType tableType)
    {
        (string, string) container = mainTableUrls[tableType];
        request = UnityWebRequest.Get(ConvertCSVAddress(container.Item1, container.Item2));
        yield return request.SendWebRequest();
        cacheStr = request.downloadHandler.text;
        // GetTypeTableRange

        request = UnityWebRequest.Get(ConvertCSVAddress(container.Item1, container.Item2, cacheStr));
        yield return request.SendWebRequest();
        cacheArr = SplitCSV(request.downloadHandler.text);
        // GetTypeTableForeachSheetID

        string[] sheetDatas = new string[cacheArr.Length];

        for (int i = 0; i < cacheArr.Length; i++)
        {
            request = UnityWebRequest.Get(ConvertCSVAddress(container.Item1, cacheArr[i]));
            yield return request.SendWebRequest();
            cacheStr = request.downloadHandler.text;
            // GetTypeTableInnerRange

            request = UnityWebRequest.Get(ConvertCSVAddress(container.Item1, cacheArr[i], cacheStr));
            yield return request.SendWebRequest();

            sheetDatas[i] = request.downloadHandler.text;
        }

        dataPaser.ParseData(tableType, sheetDatas);
    }
}
