namespace Utils
{
    [System.Serializable]
    public class PsuedoRandArray<T>
    {
        public PsuedoRandArray(params T[] _items)
        {
            if (_items == null)
            {
                _items = new T[] { };
            }
            else
            {
                items = _items;
            }

        }
        public T[] items;
        private int currentIndex = 0;


        public T PickNext()
        {
            T selectedItem = items[currentIndex];

            currentIndex += 1;
            if (currentIndex >= items.Length)
            {
                UtilityHelper.ShuffleArray(items);
                currentIndex = 0;
            }


            return selectedItem;
        }

        public void ShuffleArray()
        {
            UtilityHelper.ShuffleArray(items);
        }
    }
}