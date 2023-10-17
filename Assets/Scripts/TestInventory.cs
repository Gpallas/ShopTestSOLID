using System.Collections;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    Item testToAddExisting;

    public Item testToAddNew;

    void Start()
    {
        StartCoroutine(BeginTests());
    }

    IEnumerator BeginTests()
    {
        yield return new WaitForSeconds(0.5f);

        TryGetComponent(out IInventoryAccess invAccess);
        testToAddExisting = invAccess.GetItemAtIndex(0);
        yield return new WaitForSeconds(0.5f);

        invAccess.AddItem(testToAddExisting);
        yield return new WaitForSeconds(0.5f);

        invAccess.AddItem(testToAddNew);
        yield return new WaitForSeconds(0.5f);

        testToAddExisting.amount = testToAddExisting.data.stackLimit;
        invAccess.AddItem(testToAddExisting);
        yield return new WaitForSeconds(0.5f);

        testToAddExisting.amount = invAccess.GetItem(testToAddExisting).amount / 2;
        invAccess.RemoveItem(testToAddExisting);
        yield return new WaitForSeconds(0.5f);

        testToAddExisting.amount = testToAddExisting.data.stackLimit;
        invAccess.RemoveItem(testToAddExisting);
        yield return new WaitForSeconds(0.5f);

        invAccess.SwapItems(0, 1);
        yield return new WaitForSeconds(0.5f);

        testToAddNew.amount = testToAddNew.data.stackLimit;
        invAccess.AddItemToIndex(testToAddNew, 0);
        yield return new WaitForSeconds(0.5f);

        invAccess.AddItemToIndex(testToAddNew, 3);
    }
}
