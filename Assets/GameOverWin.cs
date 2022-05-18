using UnityEngine;
using UnityEngine.UI;


public class GameOverWin : MonoBehaviour
{
    [SerializeField] private Text _winnerT;
    [SerializeField] private Text _timmerT;

    private void Start()
    {
        //Centred and Hide Window (Easy for setup window)
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void ShowInfo(int winnerID, float gameTime)
    {
        gameObject.SetActive(true);

        _winnerT.text = "Winner Team: " + winnerID.ToString();
        System.TimeSpan t = System.TimeSpan.FromSeconds(gameTime);

        _timmerT.text = "Time: " + string.Format("{0:D2}m:{1:D2}s:{2:D3}ms",
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
