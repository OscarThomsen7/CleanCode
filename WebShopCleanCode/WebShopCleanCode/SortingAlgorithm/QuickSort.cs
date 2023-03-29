namespace WebShopCleanCode.SortingAlgorithm;

public class QuickSort
{
    private void Swap(List<Product> list, int indexFrom, int indexTo)
    {
        (list[indexTo], list[indexFrom]) = (list[indexFrom], list[indexTo]);
    }

    public void SortPrice(List<Product> list, int start, int end, bool ascending)
    {
        if (start < end)
        {
            int pivotIndex = PartitionPrice(list, start, end, ascending);
            SortPrice(list, start, pivotIndex - 1, ascending);
            SortPrice(list, pivotIndex + 1, end, ascending);
        }
    }
    public void SortName(List<Product> list, int start, int end, bool ascending)
    {
        if (start < end)
        {
            int pivotIndex = PartitionName(list, start, end, ascending);
            SortName(list, start, pivotIndex - 1, ascending);
            SortName(list, pivotIndex + 1, end, ascending);
        }
    }

    private int PartitionPrice(List<Product> list, int start, int end, bool ascending)
    {
        var pivot = list[end].Price;
        int numberOfItemsSmallerThanOurPivot = 0;
        if (ascending)
        {
            for(int i = start; i < end; i++)
            {
                if (list[i].Price < pivot)
                {
                    Swap(list, i, start + numberOfItemsSmallerThanOurPivot);
                    numberOfItemsSmallerThanOurPivot++;
                }
            }
        }
        else
        {
            for(int i = start; i < end; i++)
            {
                if (list[i].Price > pivot)
                {
                    Swap(list, i, start + numberOfItemsSmallerThanOurPivot);
                    numberOfItemsSmallerThanOurPivot++;
                }
            }
        }
        
        Swap(list, start + numberOfItemsSmallerThanOurPivot, end);

        return start + numberOfItemsSmallerThanOurPivot;
    }
    
    private int PartitionName(List<Product> list, int start, int end, bool ascending)
    {
        var pivot = list[end].Name;
        int numberOfItemsSmallerThanOurPivot = 0;
        if (ascending)
        {
            for(int i = start; i < end; i++)
            {
                if (String.Compare(list[i].Name, pivot, StringComparison.Ordinal) > 0)
                {
                    Swap(list, i, start + numberOfItemsSmallerThanOurPivot);
                    numberOfItemsSmallerThanOurPivot++;
                }
            }   
        }
        else
        {
            for(int i = start; i < end; i++)
            {
                if (String.Compare(list[i].Name, pivot, StringComparison.Ordinal) < 0)
                {
                    Swap(list, i, start + numberOfItemsSmallerThanOurPivot);
                    numberOfItemsSmallerThanOurPivot++;
                }
            }
        }
        Swap(list, start + numberOfItemsSmallerThanOurPivot, end);

        return start + numberOfItemsSmallerThanOurPivot;
    }
}