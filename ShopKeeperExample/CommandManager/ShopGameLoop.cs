using System;
using GameElements;

namespace CommandManager
{
    /// <summary>
    /// Logic for the Shop Keeping game
    /// </summary>
    public class ShopGameLoop : IGameLoop
    {
        /// <summary>
        /// Takes input from the User or Manages such from the User.
        /// </summary>
        private readonly IUserInfoExchange userExchange;

        /// <summary>
        /// Takes commands and finds logic to match.
        /// </summary>
        private readonly ICommandResolver resolver;

        /// <summary>
        /// Logic for the shop game.
        /// </summary>
        private readonly IShopKeeper shopKeeper;

        /// <summary>
        /// Helps when interpreting commands.
        /// </summary>
        private readonly ICommandInterpreter commandInterpreter;
        
        public ShopGameLoop(IUserInfoExchange exchange)
        {
            this.userExchange = exchange ??
                                throw new ArgumentNullException(
                                    $"{typeof(ShopGameLoop)}: " +
                                    $"{nameof(exchange)} must not be null.");
            
            this.resolver = new CommandResolver();
            this.shopKeeper = new ShopKeeper();
            this.commandInterpreter = new BasicCommandInterpreter();

            SetupCommands();
        }
        
        /// <summary>
        /// Runs the game.
        /// </summary>
        public void RunGame()
        {
            this.userExchange.WriteText("Loading");
            while (true)
            {
                this.userExchange.WriteText("What is your command");
                string command = this.userExchange.ReadText();
                if (!this.resolver.ResolveCommand(command))
                {
                    this.userExchange.WriteText("Commands:");
                    this.userExchange.WriteText("give [item (sword/shield/armour)] [quantity]");
                    this.userExchange.WriteText("getstock [item (sword/shield/armour)]");
                    this.userExchange.WriteText("setprice [item (sword/shield/armour)] [price]");
                    this.userExchange.WriteText("getprice [item (sword/shield/armour)]");
                    this.userExchange.WriteText("buy [item (sword/shield/armour)] [quantity] [walletvalue]");
                }
            }
        }
        
        /// <summary>
        /// Sets up the commands for the console.
        /// </summary>
        private void SetupCommands()
        {
            SetupGiveCommand();
            SetupGetStockCommand();
            SetupSetPriceCommand();
            SetupGetPriceCommand();
            SetupBuyCommand();
        }

        /// <summary>
        /// Sets up Give Command.
        /// </summary>
        private void SetupGiveCommand()
        {
            IRunableCommand giveCommandItem = new RunableCommand()
            {
                IsValueStage = true,
                Runable = HandleGiveCommand,
            };
            
            IRunableCommand giveCommandQty = new RunableCommand()
            {
                IsValueStage = true,
                NextCommand = giveCommandItem,
            };
            
            IRunableCommand giveCommand = new RunableCommand()
            {
                Entry = new SimpleTextResolver("give"),
                NextCommand = giveCommandQty
            };

            this.resolver.GiveCommand(giveCommand);
        }

        /// <summary>
        /// Handles the give command call back.
        /// </summary>
        private EventHandler HandleGiveCommand(object sender, CommandArgs args)
        {
            bool interpreted = true;
            
            Item item = Item.Armour;
            if(this.commandInterpreter.InterpretItem((string)args.CommandValues[0], out Item itemValue))
            {
                item = itemValue;
            }
            else
            {
                interpreted = false;
            }
            
            int quantity = 0;
            if(this.commandInterpreter.InterpretWholeNumber((string)args.CommandValues[1], out int value))
            {
                quantity = value;
            }
            else
            {
                interpreted = false;
            }

            if (interpreted)
            {
                int answer = this.shopKeeper.Give(item, quantity);
                if (answer >= 0)
                {
                    this.userExchange.WriteText($"Gave: {quantity} {item}, Keeper now has {answer}.");
                }
                else
                {
                    this.userExchange.WriteText($"Took: {quantity * -1} {item}, Keeper now has {answer}.");
                }
            }
            else
            {
                this.userExchange.WriteText("HELP: give [item (sword/shield/armour)] [quantity]");
            }

            return null;
        }
        
        /// <summary>
        /// Sets up Get Stock Command.
        /// </summary>
        private void SetupGetStockCommand()
        {
            IRunableCommand getStockCommandItem = new RunableCommand()
            {
                IsValueStage = true,
                Runable = HandleGetStockCommand,
            };
            
            IRunableCommand getStockCommand = new RunableCommand()
            {
                Entry = new SimpleTextResolver("getstock"),
                NextCommand = getStockCommandItem
            };

            this.resolver.GiveCommand(getStockCommand);
        }

        /// <summary>
        /// Handles the get stock command call back.
        /// </summary>
        private EventHandler HandleGetStockCommand(object sender, CommandArgs args)
        {
            bool interpreted = true;
            
            Item item = Item.Armour;
            if(this.commandInterpreter.InterpretItem((string)args.CommandValues[0], out Item itemValue))
            {
                item = itemValue;
            }
            else
            {
                interpreted = false;
            }

            if (interpreted)
            {
                int answer = this.shopKeeper.Give(item, 0);
                this.userExchange.WriteText($"Get Stock: There are {answer} of {item}.");
            }
            else
            {
                this.userExchange.WriteText("HELP: getstock [item (sword/shield/armour)]");
            }

            return null;
        }
        
        /// <summary>
        /// Sets up Set Price Command.
        /// </summary>
        private void SetupSetPriceCommand()
        {
            IRunableCommand setPriceCommandItem = new RunableCommand()
            {
                IsValueStage = true,
                Runable = HandleSetPriceCommand,
            };
            
            IRunableCommand setPriceCommandQty = new RunableCommand()
            {
                IsValueStage = true,
                NextCommand = setPriceCommandItem,
            };
            
            IRunableCommand setPriceCommand = new RunableCommand()
            {
                Entry = new SimpleTextResolver("setprice"),
                NextCommand = setPriceCommandQty
            };

            this.resolver.GiveCommand(setPriceCommand);
        }

        /// <summary>
        /// Handler for Set Price.
        /// </summary>
        private EventHandler HandleSetPriceCommand(object sender, CommandArgs args)
        {
            bool interpreted = true;
            
            Item item = Item.Armour;
            if(this.commandInterpreter.InterpretItem((string)args.CommandValues[0], out Item itemValue))
            {
                item = itemValue;
            }
            else
            {
                interpreted = false;
            }
            
            int price = 0;
            if(this.commandInterpreter.InterpretWholeNumber((string)args.CommandValues[1], out int value))
            {
                price = value;
            }
            else
            {
                interpreted = false;
            }

            if (interpreted)
            {
                this.shopKeeper.SetPrice(item, price);
                this.userExchange.WriteText($"Set Price: {item} to {price}");
            }
            else
            {
                this.userExchange.WriteText("HELP: setprice [item (sword/shield/armour)] [price]");
            }

            return null;
        }
        
        /// <summary>
        /// Sets up Get price Command.
        /// </summary>
        private void SetupGetPriceCommand()
        {
            IRunableCommand getPriceCommandItem = new RunableCommand()
            {
                IsValueStage = true,
                Runable = HandleGetPriceCommand,
            };
            
            IRunableCommand getPriceCommand = new RunableCommand()
            {
                Entry = new SimpleTextResolver("getprice"),
                NextCommand = getPriceCommandItem
            };

            this.resolver.GiveCommand(getPriceCommand);
        }

        /// <summary>
        /// Handles the get price command call back.
        /// </summary>
        private EventHandler HandleGetPriceCommand(object sender, CommandArgs args)
        {
            bool interpreted = true;
            
            Item item = Item.Armour;
            if(this.commandInterpreter.InterpretItem((string)args.CommandValues[0], out Item itemValue))
            {
                item = itemValue;
            }
            else
            {
                interpreted = false;
            }

            if (interpreted)
            {
                int answer = this.shopKeeper.GetPrice(item);
                this.userExchange.WriteText($"Get Price: {item} is {answer} price.");
            }
            else
            {
                this.userExchange.WriteText("HELP: getprice [item (sword/shield/armour)]");
            }

            return null;
        }
        
        /// <summary>
        /// Sets up Buy Command.
        /// </summary>
        private void SetupBuyCommand()
        {
            IRunableCommand buyCommandWallet = new RunableCommand()
            {
                IsValueStage = true,
                Runable = HandleBuyCommand,
            };
            
            IRunableCommand buyCommandQty = new RunableCommand()
            {
                IsValueStage = true,
                NextCommand = buyCommandWallet,
            };
            
            IRunableCommand buyCommandItem = new RunableCommand()
            {
                IsValueStage = true,
                NextCommand = buyCommandQty,
            };
            
            IRunableCommand buyCommand = new RunableCommand()
            {
                Entry = new SimpleTextResolver("buy"),
                NextCommand = buyCommandItem
            };

            this.resolver.GiveCommand(buyCommand);
        }

        /// <summary>
        /// Handles the give command call back.
        /// </summary>
        private EventHandler HandleBuyCommand(object sender, CommandArgs args)
        {
            bool interpreted = true;
            
            Item item = Item.Armour;
            if(this.commandInterpreter.InterpretItem((string)args.CommandValues[0], out Item itemValue))
            {
                item = itemValue;
            }
            else
            {
                interpreted = false;
            }
            
            int quantity = 0;
            if(this.commandInterpreter.InterpretWholeNumber((string)args.CommandValues[1], out int value))
            {
                quantity = value;
            }
            else
            {
                interpreted = false;
            }
            
            int wallet = 0;
            if(this.commandInterpreter.InterpretWholeNumber((string)args.CommandValues[2], out int walletValue))
            {
                wallet = walletValue;
            }
            else
            {
                interpreted = false;
            }

            if (interpreted)
            {
                int price = this.shopKeeper.GetPrice(item);
                InventoryAnswer answer = this.shopKeeper.Buy(item, quantity, wallet);
                switch (answer.Answer)
                {
                    case ShopInventoryAnswer.OutOfStock:
                        this.userExchange.WriteText(
                            $"Buy: You did not trade because they are out of stock of {item}");
                        break;
                    case ShopInventoryAnswer.NotEnoughFunds:
                        this.userExchange.WriteText(
                            $"Buy: You did not trade because you do not have enough funds. " +
                            $"Your wallet: {answer.NewWalletValue}, price of {item} {price}.");
                        break;
                    case ShopInventoryAnswer.SuccessfulTrade:
                        this.userExchange.WriteText(
                            $"Buy: You traded successfully. Your new wallet value is {answer.NewWalletValue}.");
                        break;
                }
            }
            else
            {
                this.userExchange.WriteText("HELP: buy [item (sword/shield/armour)] [quantity] [walletvalue]");
            }

            return null;
        }
    }
}