
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

public class RemoveAdsIAP : MonoBehaviour
{
    private const string ProductId = "remove_ads";

    private StoreController storeController;
    private bool storeReady;
    private bool ownsRemoveAds;

    private async void Start()
    {
        storeController = UnityIAPServices.StoreController();

        storeController.OnProductsFetched += OnProductsFetched;
        storeController.OnPurchasesFetched += OnPurchasesFetched;
        storeController.OnPurchasePending += OnPurchasePending;
        storeController.OnPurchaseConfirmed += OnPurchaseConfirmed;

        try
        {
            await storeController.Connect();

            storeController.FetchProducts(
                new List<ProductDefinition>
                {
                    new ProductDefinition(
                        ProductId,
                        ProductType.NonConsumable)
                });
        }
        catch (System.Exception e)
        {
            Debug.LogError("IAP connection error: " + e.Message);
        }
    }

    private void OnProductsFetched(List<Product> products)
    {
        storeReady = products.Any(
            p => p.definition.id == ProductId);

        if (storeReady)
        {
            storeController.FetchPurchases();
        }
    }

    private void OnPurchasesFetched(Orders orders)
    {
        foreach (var order in orders.ConfirmedOrders)
        {
            if (ContainsRemoveAds(order))
            {
                UnlockRemoveAds();
            }
        }
    }

    private void OnPurchasePending(PendingOrder order)
    {
        if (!ContainsRemoveAds(order))
            return;

        // W produkcji dodamy walidacje paragonu.
        UnlockRemoveAds();
        storeController.ConfirmPurchase(order);
    }

    private void OnPurchaseConfirmed(Order order)
    {
        if (order is ConfirmedOrder &&
            ContainsRemoveAds(order))
        {
            UnlockRemoveAds();
            Debug.Log("REMOVE ADS purchase confirmed!");
        }
        else if (order is FailedOrder failedOrder)
        {
            Debug.LogError(
                "Purchase confirmation failed: " +
                failedOrder.FailureReason);
        }
    }

    private bool ContainsRemoveAds(Order order)
    {
        return order.CartOrdered.Items().Any(
            item => item.Product.definition.id == ProductId);
    }

    private void UnlockRemoveAds()
    {
        if (ownsRemoveAds)
            return;

        ownsRemoveAds = true;

        PlayerPrefs.SetInt("AdsRemoved", 1);
        PlayerPrefs.Save();

        Debug.Log("REMOVE ADS unlocked!");
    }

    public void BuyRemoveAds()
    {
        if (ownsRemoveAds)
        {
            Debug.Log("REMOVE ADS already owned.");
            return;
        }

        if (!storeReady)
        {
            Debug.LogWarning(
                "REMOVE ADS not available in Google Play yet.");
            return;
        }

        var product = storeController.GetProducts()
            .FirstOrDefault(
                p => p.definition.id == ProductId);

        if (product != null)
        {
            storeController.PurchaseProduct(product);
        }
    }

    private void OnDestroy()
    {
        if (storeController == null)
            return;

        storeController.OnProductsFetched -= OnProductsFetched;
        storeController.OnPurchasesFetched -= OnPurchasesFetched;
        storeController.OnPurchasePending -= OnPurchasePending;
        storeController.OnPurchaseConfirmed -= OnPurchaseConfirmed;
    }
}
