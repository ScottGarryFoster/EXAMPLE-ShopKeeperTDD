using System;
using System.Collections.Generic;

namespace GameElements
{
    /// <summary>
    /// A shop keeper or trader in the game.
    /// </summary>
    public class ShopKeeper : IShopKeeper
    {
        /// <summary>
        /// Stock for the Shop.
        /// </summary>
        private Dictionary<Item, StockItem> stock;

        public ShopKeeper()
        {
            this.stock = new Dictionary<Item, StockItem>();
            foreach (Item i in Enum.GetValues(typeof(Item)))
            {
                this.stock.Add(i, new StockItem());
            }
        }
        
        /// <summary>
        /// Gives / spawns an item out of no where and 
        /// </summary>
        /// <param name="item">Item to give. </param>
        /// <param name="quantity">Quantity to give. </param>
        /// <returns>
        /// Returns the number of items in their inventory or <c>-1</c> when invalid input.
        /// </returns>
        public int Give(Item item, int quantity)
        {
            int newStock = this.stock[item].Quantity;
            newStock += quantity;
            if (newStock <= 0)
            {
                newStock = 0;
            }
            
            this.stock[item].Quantity = newStock;
            return this.stock[item].Quantity;
        }

        /// <summary>
        /// Structure to make Stock easier to read.
        /// </summary>
        private class StockItem
        {
            public int Quantity = 0;
        }
    }
}