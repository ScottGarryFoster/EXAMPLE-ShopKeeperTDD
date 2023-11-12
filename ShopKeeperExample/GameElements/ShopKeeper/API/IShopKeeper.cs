namespace GameElements
{
    /// <summary>
    /// A shop keeper or trader in the game.
    /// </summary>
    public interface IShopKeeper
    {
        /// <summary>
        /// Gives / spawns an item out of no where and 
        /// </summary>
        /// <param name="item">Item to give. </param>
        /// <param name="quantity">Quantity to give. </param>
        /// <returns>
        /// Returns the number of items in their inventory or <c>-1</c> when invalid input.
        /// </returns>
        int Give(Item item, int quantity);

        /// <summary>
        /// Sets the price of the given item.
        /// </summary>
        /// <param name="item">Item to set the price for. </param>
        /// <param name="price">Price to update to. </param>
        void SetPrice(Item item, int price);

        /// <summary>
        /// Gets the price for the given Item.
        /// </summary>
        /// <param name="item">Item to check the price for. </param>
        /// <returns>Price for the item. Returns 0 by default. </returns>
        int GetPrice(Item item);

        // We will make this soon
        //InventoryAnswer Buy(Item item, int quantity, int wallet);
    }
}