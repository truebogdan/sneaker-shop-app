namespace DBRepo.Builder
{
    internal class OrderProductBuilder : IOrderProduct, IOrderProductSetImgUrl, IOrderProductSetDescription, IOrderProductSetBrand, IOrderProductSetPrice, IOrderProductSetOrder
    {
        private string? ImgUrl = "";
        private string? Description = "";
        private string? Brand = "";
        private string? Price = "";
        private Order? Order = null;

        private OrderProductBuilder() { }
        public static IOrderProductSetImgUrl CreateNew()
        {
            return new OrderProductBuilder();
        }


        public IOrderProductSetBrand SetDescription(string description)
        {
            this.Description = description;
            return this;
        }

        public IOrderProductSetDescription SetImgUrl(string imgUrl)
        {
            this.ImgUrl = imgUrl;
            return this;
        }

        public IOrderProduct SetOrder(Order order)
        {
            this.Order = order;
            return this;
        }

        public IOrderProductSetOrder SetPrice(string price)
        {
            this.Price = price;
            return this;
        }

        OrderProduct IOrderProduct.Build()
        {
            return new OrderProduct(ImgUrl, Description, Brand, Price, Order);
        }

        IOrderProductSetPrice IOrderProductSetBrand.SetBrand(string brand)
        {
            this.Brand = brand;
            return this;
        }
    }


}
