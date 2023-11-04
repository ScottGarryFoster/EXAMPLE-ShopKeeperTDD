namespace GameElements
{
    /// <summary>
    /// When making a trade this give context to the response.
    /// </summary>
    public struct InventoryAnswer
    {
        /// <summary>
        /// Provides context when making a trade with a shop keeper.
        /// </summary>
        public ShopInventoryAnswer Answer;
        
        /// <summary>
        /// The new wallet amount the trade believes you have.
        /// Imagine the wallet is given during the trade, gold removed, this is the amount returned, minus the trade.
        /// </summary>
        public int NewWalletValue;
    }
}