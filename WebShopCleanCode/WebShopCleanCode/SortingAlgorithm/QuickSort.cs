namespace WebShopCleanCode.SortingAlgorithm;

public class QuickSort
{
    
    //Sorts a list of products in different ways  
    
    
    //Swaps two indexes with each other
    private void Swap(List<Product> list, int indexFrom, int indexTo)
    {
        (list[indexTo], list[indexFrom]) = (list[indexFrom], list[indexTo]);
    }

    
    //Sorts items by price
    public void SortByPrice(List<Product> list, int start, int end, bool ascending)
    {
        if (start < end)
        {
            int pivotIndex = PartitionPrice(list, start, end, ascending);
            SortByPrice(list, start, pivotIndex - 1, ascending);
            SortByPrice(list, pivotIndex + 1, end, ascending);
        }
    }
    
    
    //Sorts items by name
    public void SortByName(List<Product> list, int start, int end, bool ascending)
    {
        if (start < end)
        {
            int pivotIndex = PartitionName(list, start, end, ascending);
            SortByName(list, start, pivotIndex - 1, ascending);
            SortByName(list, pivotIndex + 1, end, ascending);
        }
    }
    
    //Checks what price property at current index and the next index is smaller or larger depending on if ascending is true or not, swaps them accordingly
    //Returns sorted list
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
    
    
    //Checks what alphabetic order the name property at current index and the next index is smaller or larger depending on if ascending is true or not, swaps them accordingly
    //Returns sorted list

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