using UnityEngine;

public interface IPlayerController
{
    public void Move(Vector2 moveVector);
    public void OnConfirm();
    public void OnCancel();
}

public class StockMarketPlayerController : MonoBehaviour, IPlayerController
{
    public StockMarketManager stockMarketManager;

    public int playerID;

    private void OnEnable()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Move(Vector2 moveVector)
    {
        // Do nothing
    }

    public void OnCancel()
    {
        /*
        if (playerID == 1)
        {
            stockMarketManager.P1Sell();
        }
        else
        {
            stockMarketManager.P2Sell();
        }*/
    }

    public void OnConfirm()
    {
        if (playerID == 1)
        {
            bool sellWorked = stockMarketManager.P1Sell(this.transform);
            this.GetComponent<SpriteRenderer>().enabled = sellWorked;
        }
        else
        {
            bool sellWorked = stockMarketManager.P2Sell(this.transform);
            this.GetComponent<SpriteRenderer>().enabled = sellWorked;
        }
    }
}
