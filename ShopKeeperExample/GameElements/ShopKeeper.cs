namespace GameElements
{
    /// <summary>
    /// A shop keeper or trader in the game.
    /// </summary>
    public class ShopKeeper : IShopKeeper
    {
        private int stock;
        
        public int Give(Item item, int quantity, int price)
        {
            this.stock += quantity;
            return stock;
        }
    }
}