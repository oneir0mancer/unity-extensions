namespace Oneiromancer.Utils
{
    public class Counted<T>
    {
        public T Item;
        public int Count;
        
        public Counted() {}

        public Counted(T item, int count)
        {
            if (item == null) throw new System.ArgumentNullException(nameof(item), "Item can't be null");
            if (count <= 0) throw new System.ArgumentOutOfRangeException(nameof(count), "Count can't be <= 0");
            
            Item = item;
            Count = count;
        }
        
        public static implicit operator T(Counted<T> x) => x.Item;
    }
}