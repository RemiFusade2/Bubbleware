using UnityEngine;

public interface IPlayerController
{
    public void Move(Vector2 moveVector);
    public void OnConfirm();
    public void OnCancel();
}

public class StockMarketPlayerController : MonoBehaviour, IPlayerController
{
    public void Move(Vector2 moveVector)
    {
        // Do nothing
    }

    public void OnCancel()
    {
        Debug.Log($"{this.name} SELL");
    }

    public void OnConfirm()
    {
        Debug.Log($"{this.name} BUY");
    }
}
