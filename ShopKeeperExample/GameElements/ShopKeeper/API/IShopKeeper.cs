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
        
        // We will make this soon
        //InventoryAnswer Buy(Item item, int quantity, int wallet);
    }
}