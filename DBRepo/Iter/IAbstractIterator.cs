namespace DBRepo.Iter
{
    internal interface IAbstractIterator
    {
        CartProductModel First();
        CartProductModel Next();
        bool IsCompleted { get; }
    }
}
