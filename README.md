# Shop Keeper example
This project is a short simple example of test driven example in the context of games development.
It was designed to be used as part of an example for presentations/talks for educational purposes to encourage people to learn the practice.

**This project is coded in C# using the Rider platform and should open in Jet Brains Rider and Visual Studio**
## How to Start
This project is designed as a starting point for people unsure about Test Driven Development. Download this project and try out the practice!
1. Download [GitHub Desktop](https://desktop.github.com/) and Login
2. Clone this Repository
3. Open in [Visual Studio](https://visualstudio.microsoft.com/) or [Jet Brains Rider](https://www.jetbrains.com/rider/)
4. Press play and use some of the commands
5. Look through some of the code, in particular the Game Elements project

**[Never heard of Git, GitHub or GitHub Desktop until now? See this.](https://blog.scottgarryfoster.com/easy-source-control-for-your-game-project-10af6f0331f2)** 

### What is here?
This project is a starting point, therefore there is already some code to make it a little easier to expand.
* Command Manager - This project handles the console level of the project
   * Shop Game Loop - The actual command code runs from here. At the bottom you'll see the Give, SetPrice and Buy commands hooked up to ShopKeeper!
* ShopKeeper - This is the substance, the logic behind a shop keeper.
   * This is incomplete however, buy is not finished. That'll be the task!
* ShopKeeperExample - This is just the very top level application. It kicks off the command manager.

### How did we get here?
As in Test Driven Development, everything is primarily driven by requirements. Below are the requirements and then the design of the application for the existing parts.

**Give**

To get to the point of buy, give is required to setup the Shop Keeper.
These were the requirements and then below the design to meet them.
![Give Design](https://github.com/ScottGarryFoster/EXAMPLE-ShopKeeperTDD/blob/main/ReadmeImages/GiveRequirements.png?raw=true)
The give command is hooked up to the top level as, **Give** *item* *quanity* and may be previewed below.
![](https://github.com/ScottGarryFoster/EXAMPLE-ShopKeeperTDD/blob/main/ReadmeImages/GiveCommandInUse.PNG?raw=true)

**Set Price Get Price**

Price is required before we can buy otherwise we cannot look at the wallet value. This is the design:
![](https://github.com/ScottGarryFoster/EXAMPLE-ShopKeeperTDD/blob/main/ReadmeImages/SetPriceRequirements.png?raw=true)
Set Price and Get Price are both simple methods to store the value ready for buy. Below is it being used.
![](https://github.com/ScottGarryFoster/EXAMPLE-ShopKeeperTDD/blob/main/ReadmeImages/PriceCommand.PNG?raw=true)
### What is the Task?
Buy is the last piece of the puzzle. This is a method within Shop Keeper which is entirely hooked up to the commands but with no implementation. Below are the requirements and existing code:
![](https://github.com/ScottGarryFoster/EXAMPLE-ShopKeeperTDD/blob/main/ReadmeImages/BuyRequirements.png?raw=true)

Within ShopKeeper.cs
```c#
/// <summary>  
/// Buy an item from the shop.  
/// </summary>  
/// <param name="item">The item to buy. </param>  
/// <param name="quantity">How many to buy. </param>  
/// <param name="wallet">Your wallet value. </param>  
/// <returns>Information about the trade. </returns>  
public InventoryAnswer Buy(Item item, int quantity, int wallet)  
{  
    throw new NotImplementedException();  
}
```
This is hooked up already in the command manager:

HandleBuyCommand within ShopGameLoop:
```c#
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
```
![](https://github.com/ScottGarryFoster/EXAMPLE-ShopKeeperTDD/blob/main/ReadmeImages/BuyCommand.PNG?raw=true)
## Your task is to Implement Buy using Test Driven Development!

# Key Items to Remember
Test Driven Development key aspects to remember:
1. Red, Green, Clean!
2. Arrange, Act, Assert
3. Test Public Behaviour

![](https://github.com/ScottGarryFoster/EXAMPLE-ShopKeeperTDD/blob/main/ReadmeImages/TDDFlow.drawio.png?raw=true)

Red Green Clean is the loop for TDD. Write a failing Test, make it pass then ensure your code is clean using your tests to verify everything still works.

![](https://github.com/ScottGarryFoster/EXAMPLE-ShopKeeperTDD/blob/main/ReadmeImages/ArrangeActAssert.drawio.png?raw=true)

The triple A structure might help you write the tests. Arrange the state you would like to test under, perform the test and then check the results. You also may end up writing these in a different order but in the method naturally these will occur in this order.

# Good Luck and Links below
**Links**
* [Introduction to Test Driven Development using examples by Scott Foster](https://blog.scottgarryfoster.com/introduction-to-test-driven-development-using-examples-372f3ea0b571)
* [Test code once by arranging behaviours — using mocks](https://blog.scottgarryfoster.com/test-code-once-by-arranging-behaviours-using-mocks-7416d647b992)
* [Stop pressing play- Unity Test Driven Development](https://blog.scottgarryfoster.com/stop-pressing-play-unity-test-driven-development-1e6ee4cde656)
* [Ian Cooper (2017). TDD, Where Did It All Go Wrong.](https://www.youtube.com/watch?v=EZ05e7EMOLM)
* [David Whitney (2022). Test Driven Development in JavaScript](https://www.youtube.com/watch?v=D7LKslgwxmQ)
* [Unreal 4 Unit Testing](https://benui.ca/unreal/unreal-testing-introduction)
* [From 0-1000: A Test Driven Approach to Tools Development by David Paris](https://www.gdcvault.com/play/1026631/Tools-Summit-From-0-1000)
* [How Rare Tests Sea of Thieves to Stop Bugs Reaching Players](https://www.youtube.com/watch?v=Bu4YV4be6IE)
* [Test Driven Game Development Experiences](https://www.gamedeveloper.com/programming/test-driven-game-development-experiences)
* [GDC: Backwards is Forward: Making Better Games with Test-Driven Development - High Moon Studio developers](https://www.gamedeveloper.com/programming/gdc-backwards-is-forward-making-better-games-with-test-driven-development)

**Books**
* Working effectively with legacy code by Michael Feathers, M.C. (2013)
* Test-driven development : by example by Kent Beck (2003)
* Learning Test-Driven Development: A Polyglot Guide to Writing Uncluttered Code by Saleem Siddiqui (2021)
* Art of Clean Code, The: Best Practices to Eliminate Complexity and Simplify Your Life by Christian Mayer (2022)
* 97 Things Every Programmer Should Know: Collective Wisdom from the Experts by Kevlin  Henney (2010)