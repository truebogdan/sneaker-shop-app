namespace DBRepo.Iter
{
    internal interface AbstractCollection
    {
        Iterator CreateIterator(IEnumerable<CartProductModel> cartProducts);
    }
}
